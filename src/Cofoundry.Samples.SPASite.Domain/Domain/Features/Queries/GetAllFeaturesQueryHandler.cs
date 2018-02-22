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
    public class GetAllFeaturesQueryHandler
        : IAsyncQueryHandler<GetAllFeaturesQuery, ICollection<Feature>>
        , IIgnorePermissionCheckHandler
    {
        private readonly ICustomEntityRepository _customEntityRepository;

        public GetAllFeaturesQueryHandler(
            ICustomEntityRepository customEntityRepository
            )
        {
            _customEntityRepository = customEntityRepository;
        }

        public async Task<ICollection<Feature>> ExecuteAsync(GetAllFeaturesQuery query, IExecutionContext executionContext)
        {
            var customEntityQuery = new GetCustomEntityRenderSummariesByDefinitionCodeQuery(FeatureCustomEntityDefinition.DefinitionCode);
            var customEntities = await _customEntityRepository.GetCustomEntityRenderSummariesByDefinitionCodeAsync(customEntityQuery); ;

            var features = customEntities
                .Select(MapFeature)
                .ToList();

            return features;
        }

        private Feature MapFeature(CustomEntityRenderSummary customEntity)
        {
            var feature = new Feature();

            feature.FeatureId = customEntity.CustomEntityId;
            feature.Title = customEntity.Title;

            return feature;
        }
    }
}
