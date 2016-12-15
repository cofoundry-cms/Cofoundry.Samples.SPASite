using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class SearchCatSummariesQueryHandler
        : IAsyncQueryHandler<SearchCatSummariesQuery, PagedQueryResult<CatSummary>>
        , IIgnorePermissionCheckHandler
    {
        private readonly ICustomEntityRepository _customEntityRepository;
        private readonly IImageAssetRepository _imageAssetRepository;

        public SearchCatSummariesQueryHandler(
            ICustomEntityRepository customEntityRepository,
            IImageAssetRepository imageAssetRepository
            )
        {
            _customEntityRepository = customEntityRepository;
            _imageAssetRepository = imageAssetRepository;
        }

        public async Task<PagedQueryResult<CatSummary>> ExecuteAsync(SearchCatSummariesQuery query, IExecutionContext executionContext)
        {
            var customEntityQuery = new SearchCustomEntityRenderSummariesQuery();
            customEntityQuery.CustomEntityDefinitionCode = CatCustomEntityDefinition.DefinitionCode;
            customEntityQuery.PageSize = query.PageSize = query.PageSize;
            customEntityQuery.PageNumber = query.PageNumber;

            var catCustomEntities = await _customEntityRepository.SearchCustomEntityRenderSummariesAsync(customEntityQuery);
            var allMainImages = await GetMainImages(catCustomEntities);

            return MapCats(catCustomEntities, allMainImages);
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

        private PagedQueryResult<CatSummary> MapCats(
            PagedQueryResult<CustomEntityRenderSummary> customEntityResult,
            IDictionary<int, ImageAssetRenderDetails> images
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
                cat.MainImage = images.GetOrDefault(model.ImageAssetIds.FirstOrDefault());

                cats.Add(cat);
            }

            return customEntityResult.ChangeType(cats);
        }
    }
}
