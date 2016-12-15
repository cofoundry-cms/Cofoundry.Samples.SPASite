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
    public class GetAllBreedsQueryHandler
        : IAsyncQueryHandler<GetAllBreedsQuery, IEnumerable<Breed>>
        , IIgnorePermissionCheckHandler
    {
        private readonly ICustomEntityRepository _customEntityRepository;

        public GetAllBreedsQueryHandler(
            ICustomEntityRepository customEntityRepository
            )
        {
            _customEntityRepository = customEntityRepository;
        }

        public async Task<IEnumerable<Breed>> ExecuteAsync(GetAllBreedsQuery query, IExecutionContext executionContext)
        {
            var customEntityQuery = new GetCustomEntityRenderSummariesByDefinitionCodeQuery(BreedCustomEntityDefinition.DefinitionCode);
            var customEntities = await _customEntityRepository.GetCustomEntityRenderSummariesByDefinitionCodeAsync(customEntityQuery); ;

            var breeds = customEntities.Select(MapBreed);

            return breeds;
        }

        private Breed MapBreed(CustomEntityRenderSummary customEntity)
        {
            var breed = new Breed();

            breed.BreedId = customEntity.CustomEntityId;
            breed.Title = customEntity.Title;

            return breed;
        }
    }
}
