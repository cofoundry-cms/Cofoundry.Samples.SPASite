using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Samples.SPASite.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class GetCatSummariesByUserLikedQueryHandler
        : IAsyncQueryHandler<GetCatSummariesByUserLikedQuery, IEnumerable<CatSummary>>
        , ILoggedInPermissionCheckHandler
    {
        private readonly SPASiteDbContext _dbContext;
        private readonly ICustomEntityRepository _customEntityRepository;
        private readonly IImageAssetRepository _imageAssetRepository;

        public GetCatSummariesByUserLikedQueryHandler(
            ICustomEntityRepository customEntityRepository,
            IImageAssetRepository imageAssetRepository,
            SPASiteDbContext dbContext
            )
        {
            _customEntityRepository = customEntityRepository;
            _imageAssetRepository = imageAssetRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatSummary>> ExecuteAsync(GetCatSummariesByUserLikedQuery query, IExecutionContext executionContext)
        {
            var userCatIds = await _dbContext
                .CatLikes
                .AsNoTracking()
                .Where(c => c.UserId == query.UserId)
                .OrderByDescending(c => c.CreateDate)
                .Select(c => c.CatCustomEntityId)
                .ToListAsync();

            var customEntityQuery = new GetCustomEntityRenderSummariesByIdRangeQuery(userCatIds);
            var catCustomEntities = await _customEntityRepository.GetCustomEntityRenderSummariesByIdRangeAsync(customEntityQuery);

            // GetByIdRange queries return a dictionary to make lookups easier, so we 
            // have an extra step to do if we want to set the ordering
            var orderedCats = catCustomEntities
                .FilterAndOrderByKeys(userCatIds)
                .ToList();

            var allMainImages = await GetMainImages(orderedCats);
            var allLikeCounts = await GetLikeCounts(orderedCats);

            return MapCats(orderedCats, allMainImages, allLikeCounts);
        }

        private Task<IDictionary<int, ImageAssetRenderDetails>> GetMainImages(IEnumerable<CustomEntityRenderSummary> customEntities)
        {
            var imageAssetIds = customEntities
                .Select(i => (CatDataModel)i.Model)
                .Where(m => !EnumerableHelper.IsNullOrEmpty(m.ImageAssetIds))
                .Select(m => m.ImageAssetIds.First())
                .Distinct();

            return _imageAssetRepository.GetImageAssetRenderDetailsByIdRangeAsync(imageAssetIds);
        }

        private Task<Dictionary<int, int>> GetLikeCounts(IEnumerable<CustomEntityRenderSummary> customEntities)
        {
            var catIds = customEntities
                .Select(i => i.CustomEntityId)
                .Distinct()
                .ToList();

            return _dbContext
                .CatLikeCounts
                .AsNoTracking()
                .Where(c => catIds.Contains(c.CatCustomEntityId))
                .ToDictionaryAsync(c => c.CatCustomEntityId, c => c.TotalLikes);
        }

        private List<CatSummary> MapCats(
            IEnumerable<CustomEntityRenderSummary> customEntities,
            IDictionary<int, ImageAssetRenderDetails> images,
            IDictionary<int, int> allLikeCounts
            )
        {
            var cats = new List<CatSummary>(customEntities.Count());

            foreach (var customEntity in customEntities)
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

            return cats;
        }
    }
}
