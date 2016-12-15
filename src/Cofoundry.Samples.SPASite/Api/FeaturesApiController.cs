using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.WebApi;
using Cofoundry.Samples.SPASite.Domain;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite
{
    [RoutePrefix("api/features")]
    public class FeaturesApiController : ApiController
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly ApiResponseHelper _apiResponseHelper;

        public FeaturesApiController(
            IQueryExecutor queryExecutor,
            ApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        #region queries

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> Get()
        {
            var query = new GetAllFeaturesQuery();
            var results = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }

        #endregion
    }
}