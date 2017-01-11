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
    [RoutePrefix("api/cats")]
    public class CatsApiController : ApiController
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly ApiResponseHelper _apiResponseHelper;

        public CatsApiController(
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
        public async Task<IHttpActionResult> Get([FromUri] SearchCatSummariesQuery query)
        {
            if (query == null) query = new SearchCatSummariesQuery();
            var results = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }

        [HttpGet]
        [Route("{catId:int}")]
        public async Task<IHttpActionResult> Get(int catId)
        {
            var query = new GetCatDetailsByIdQuery(catId);
            var results = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }

        #endregion

        #region commands

        [HttpPost]
        [Route("{catId:int}/likes")]
        public Task<IHttpActionResult> Like(int catId)
        {
            var command = new SetCatLikedCommand()
            {
                CatId = catId,
                IsLiked = true
            };
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        [HttpDelete]
        [Route("{catId:int}/likes")]
        public Task<IHttpActionResult> UnLike(int catId)
        {
            var command = new SetCatLikedCommand()
            {
                CatId = catId
            };
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        #endregion
    }
}