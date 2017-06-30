using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.WebApi;
using Cofoundry.Samples.SPASite.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cofoundry.Web.Admin;

namespace Cofoundry.Samples.SPASite
{
    [Route("api/cats")]
    [AutoValidateAntiforgeryToken]
    public class CatsApiController : Controller
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

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] SearchCatSummariesQuery query)
        {
            if (query == null) query = new SearchCatSummariesQuery();
            var results = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }

        [HttpGet("{catId:int}")]
        public async Task<IActionResult> Get(int catId)
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
        [AuthorizeUserArea(MemberUserArea.AreaCode)]
        [HttpPost("{catId:int}/likes")]
        public Task<IActionResult> Like(int catId)
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

        [AuthorizeUserArea(MemberUserArea.AreaCode)]
        [HttpDelete("{catId:int}/likes")]
        public Task<IActionResult> UnLike(int catId)
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