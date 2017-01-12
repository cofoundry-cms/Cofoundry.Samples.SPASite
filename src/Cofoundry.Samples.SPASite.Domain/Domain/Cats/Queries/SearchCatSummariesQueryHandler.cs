using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Samples.SPASite.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class SearchCatSummariesQueryHandler
        : IAsyncQueryHandler<SearchCatSummariesQuery, PagedQueryResult<CatSummary>>
        , IIgnorePermissionCheckHandler
    {
        private readonly SPASiteDbContext _dbContext;
        private readonly ICustomEntityRepository _customEntityRepository;
        private readonly IImageAssetRepository _imageAssetRepository;

        public SearchCatSummariesQueryHandler(
            ICustomEntityRepository customEntityRepository,
            IImageAssetRepository imageAssetRepository,
            SPASiteDbContext dbContext
            )
        {
            _customEntityRepository = customEntityRepository;
            _imageAssetRepository = imageAssetRepository;
            _dbContext = dbContext;
        }

        public async Task<PagedQueryResult<CatSummary>> ExecuteAsync(SearchCatSummariesQuery query, IExecutionContext executionContext)
        {
            var customEntityQuery = new SearchCustomEntityRenderSummariesQuery();
            customEntityQuery.CustomEntityDefinitionCode = CatCustomEntityDefinition.DefinitionCode;
            customEntityQuery.PageSize = query.PageSize = query.PageSize;
            customEntityQuery.PageNumber = query.PageNumber;

            var catCustomEntities = await _customEntityRepository.SearchCustomEntityRenderSummariesAsync(customEntityQuery);
            var allMainImages = await GetMainImages(catCustomEntities);
            var allLikeCounts = await GetLikeCounts(catCustomEntities);

            return MapCats(catCustomEntities, allMainImages, allLikeCounts);
        }

        private Task<IDictionary<int, ImageAssetRenderDetails>> GetMainImages(PagedQueryResult<CustomEntityRenderSummary> customEntityResult)
        {
            var imageAssetIds = customEntityResult
                .Items
                .Select(i => (CatDataModel)i.Model)
                .Where(m => !EnumerableHelper.IsNullOrEmpty(m.ImageAssetIds))
                .Select(m => m.ImageAssetIds.First())
                .Distinct();

            return _imageAssetRepository.GetImageAssetRenderDetailsByIdRangeAsync(imageAssetIds);
        }

        private Task<Dictionary<int, int>> GetLikeCounts(PagedQueryResult<CustomEntityRenderSummary> customEntityResult)
        {
            var catIds = customEntityResult
                .Items
                .Select(i => i.CustomEntityId)
                .Distinct()
                .ToList();

            return _dbContext
                .CatLikeCounts
                .AsNoTracking()
                .Where(c => catIds.Contains(c.CatCustomEntityId))
                .ToDictionaryAsync(c => c.CatCustomEntityId, c => c.TotalLikes);
        }

        private PagedQueryResult<CatSummary> MapCats(
            PagedQueryResult<CustomEntityRenderSummary> customEntityResult,
            IDictionary<int, ImageAssetRenderDetails> images,
            IDictionary<int, int> allLikeCounts
            )
        {
            var cats = new List<CatSummary>(customEntityResult.Items.Count());

            foreach (var customEntity in customEntityResult.Items)
            {
                var model = (CatDataModel)customEntity.Model;

                var cat = new CatSummary();
                cat.CatId = customEntity.CustomEntityId;
                cat.Name = customEntity.Title;
                cat.Description = model.Description;
                cat.TotalLikes = allLikeCounts.GetOrDefault(customEntity.CustomEntityId);

                if (!EnumerableHelper.IsNullOrEmpty(model.ImageAssetIds))
                {
                    cat.MainImage = images.GetOrDefault(model.ImageAssetIds.FirstOrDefault());
                }

                cats.Add(cat);
            }

            return customEntityResult.ChangeType(cats);
        }
    }
}
