using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Samples.SPASite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class GetCatDetailsByIdQueryHandler
        : IAsyncQueryHandler<GetCatDetailsByIdQuery, CatDetails>
        , IIgnorePermissionCheckHandler
    {
        private readonly ICustomEntityRepository _customEntityRepository;
        private readonly IImageAssetRepository _imageAssetRepository;
        private readonly IQueryExecutor _queryExecutor;
        private readonly SPASiteDbContext _dbContext;

        public GetCatDetailsByIdQueryHandler(
            ICustomEntityRepository customEntityRepository,
            IImageAssetRepository imageAssetRepository,
            IQueryExecutor queryExecutor,
            SPASiteDbContext dbContext
            )
        {
            _customEntityRepository = customEntityRepository;
            _imageAssetRepository = imageAssetRepository;
            _queryExecutor = queryExecutor;
            _dbContext = dbContext;
        }

        public async Task<CatDetails> ExecuteAsync(GetCatDetailsByIdQuery query, IExecutionContext executionContext)
        {
            var customEntityQuery = new GetCustomEntityRenderSummaryByIdQuery() { CustomEntityId = query.CatId };
            var customEntity = await _customEntityRepository.GetCustomEntityRenderSummaryByIdAsync(customEntityQuery); ;
            if (customEntity == null) return null;

            return await MapCat(customEntity);
        }

        private async Task<CatDetails> MapCat(CustomEntityRenderSummary customEntity)
        {
            var model = customEntity.Model as CatDataModel;
            var cat = new CatDetails();

            cat.CatId = customEntity.CustomEntityId;
            cat.Name = customEntity.Title;
            cat.Description = model.Description;
            cat.Breed = await GetBreedAsync(model.BreedId);
            cat.Features = await GetFeaturesAsync(model.FeatureIds);
            cat.Images = await GetImagesAsync(model.ImageAssetIds);
            cat.TotalLikes = await GetLikeCount(customEntity.CustomEntityId);

            return cat;
        }

        private Task<int> GetLikeCount(int catId)
        {
            return _dbContext
                .CatLikeCounts
                .AsNoTracking()
                .Where(c => c.CatCustomEntityId == catId)
                .Select(c => c.TotalLikes)
                .FirstOrDefaultAsync();
        }

        private async Task<Breed> GetBreedAsync(int? breedId)
        {
            if (!breedId.HasValue) return null;
            var query = new GetBreedByIdQuery(breedId.Value);

            return await _queryExecutor.ExecuteAsync(query);
        }

        private async Task<IEnumerable<Feature>> GetFeaturesAsync(int[] featureIds)
        {
            if (EnumerableHelper.IsNullOrEmpty(featureIds)) return Enumerable.Empty<Feature>();
            var query = new GetFeaturesByIdRangeQuery(featureIds);

            var features = await _queryExecutor.ExecuteAsync(query);

            return features
                .Select(f => f.Value)
                .OrderBy(f => f.Title)
                .ToList();
        }

        private async Task<IEnumerable<ImageAssetRenderDetails>> GetImagesAsync(int[] imageAssetIds)
        {
            if (EnumerableHelper.IsNullOrEmpty(imageAssetIds)) return Enumerable.Empty<ImageAssetRenderDetails>();

            var images = await _imageAssetRepository.GetImageAssetRenderDetailsByIdRangeAsync(imageAssetIds);

            return images.ToFilteredAndOrderedCollection(imageAssetIds);
        }
    }
}
