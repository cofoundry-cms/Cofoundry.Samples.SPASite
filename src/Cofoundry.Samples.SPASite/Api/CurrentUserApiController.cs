using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.WebApi;
using Cofoundry.Samples.SPASite.Domain;
using System.Threading.Tasks;
using Cofoundry.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cofoundry.Web.Admin;

namespace Cofoundry.Samples.SPASite
{
    [AuthorizeUserArea(MemberUserArea.AreaCode)]
    [Route("api/users/current")]
    public class CurrentUserApiController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;
        private readonly IUserContextService _userContextService;

        public CurrentUserApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper,
            IUserContextService userContextService
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
            _userContextService = userContextService;
        }

        #region queries

        [HttpGet("cats/liked")]
        public async Task<IActionResult> GetLikedCats()
        {
            // Here we get the userId of the currently logged in user. We could have
            // done this in the query handler, but instead we've chosen to keep the query 
            // flexible so it can be re-used in a more generic fashion
            var userContext = await _userContextService.GetCurrentContextAsync();
            var query = new GetCatSummariesByUserLikedQuery(userContext.UserId.Value);
            var results = await _queryExecutor.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }

        #endregion
    }
}