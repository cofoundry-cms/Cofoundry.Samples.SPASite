using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain.CQS;
using Cofoundry.Web;
using Cofoundry.Samples.SPASite.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Samples.SPASite
{
    [Route("api/features")]
    public class FeaturesApiController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;

        public FeaturesApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        #region queries

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllFeaturesQuery();
            var results = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }

        #endregion
    }
}