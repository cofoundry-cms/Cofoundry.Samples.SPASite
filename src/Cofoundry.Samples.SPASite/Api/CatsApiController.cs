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
    [ValidateApiAntiForgeryToken]
    public class CatsApiController : ApiController
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;

        public CatsApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper
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

        /// <summary>
        /// Note that here we use the standard Authorize attribute to restrict
        /// access to this endpoint because you need to be logged in to 'like' a 
        /// cat
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("{catId:int}/likes")]
        public Task<IHttpActionResult> Like(int catId)
        {
            var command = new SetCatLikedCommand()
            {
                CatId = catId,
                IsLiked = true
            };

            // IApiResponseHelper will validate the command and permissions before executing it
            // and return any validation errors in a formatted data object
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        [Authorize]
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