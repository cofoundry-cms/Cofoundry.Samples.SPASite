using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain.CQS;
using Cofoundry.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Cofoundry.Domain;
using Cofoundry.Samples.SPASite.Domain;

namespace Cofoundry.Samples.SPASite
{
    [Route("api/auth")]
    [AutoValidateAntiforgeryToken]
    public class AuthApiController : Controller
    {
        private readonly IApiResponseHelper _apiResponseHelper;
        private readonly IAntiforgery _antiforgery;
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        private readonly IUserSessionService _userSessionService;
        private readonly IUserContextService _userContextService;

        public AuthApiController(
            IApiResponseHelper apiResponseHelper,
            IAntiforgery antiforgery,
            ICommandExecutor commandExecutor,
            IQueryExecutor queryExecutor,
            IUserSessionService userSessionService,
            IUserContextService userContextService
            )
        {
            _apiResponseHelper = apiResponseHelper;
            _antiforgery = antiforgery;
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _userSessionService = userSessionService;
            _userContextService = userContextService;
        }

        #region queries

        /// <summary>
        /// Once we have logged in we need to re-fetch the csrf token because
        /// the user identity will have changed and the old token will be invalid
        /// </summary>
        [HttpGet("session")]
        public async Task<IActionResult> GetAuthSession()
        {
            var sessionInfo = await GetSessionInfoAsync();

            return _apiResponseHelper.SimpleQueryResponse(this, sessionInfo);
        }

        //[HttpGet("testLogin")]
        //public async Task<IActionResult> testLogin()
        //{
        //    var command = new LogMemberInCommand()
        //    {
        //        Email = "joseph.michaels@uberdigital.com",
        //        Password = "M00vingH0me"
        //    };

        //    await _commandExecutor.ExecuteAsync(command);
        //    var userId = _userSessionService.GetCurrentUserId();
        //    var userContext = await _userContextService.GetCurrentContextAsync();

        //    var userId2 = await _userSessionService.GetUserIdByUserAreaCodeAsync(MemberUserArea.MemberUserAreaCode);
        //    var userContext2 = await _userContextService.GetCurrentContextByUserAreaAsync(MemberUserArea.MemberUserAreaCode);

        //    var info = await GetSessionInfoAsync();
        //    return _apiResponseHelper.SimpleCommandResponse(this, null, info);
        //}
        //[HttpGet("testLogOut")]
        //public async Task<IActionResult> testLogOut()
        //{
        //    await _commandExecutor.ExecuteAsync(new LogMemberOutCommand());
        //    var sessionInfo = await GetSessionInfoAsync();

        //    return _apiResponseHelper.SimpleQueryResponse(this, sessionInfo);
        //}

        #endregion

        #region commands

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RegisterMemberAndLogInCommand command)
        {
            return _apiResponseHelper.RunWithResultAsync(this, () => ExecuteCommandAndReturnMemberAsync(command));
        }

        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] LogMemberInCommand command)
        {
            return _apiResponseHelper.RunWithResultAsync(this, () => ExecuteCommandAndReturnMemberAsync(command));
        }

        [HttpPost("sign-out")]
        public Task<IActionResult> SignOut()
        {
            var command = new LogMemberOutCommand();
            return _apiResponseHelper.RunWithResultAsync(this, () => ExecuteCommandAndReturnMemberAsync(command));
        }

        private async Task<object> ExecuteCommandAndReturnMemberAsync(ICommand command)
        {
            await _commandExecutor.ExecuteAsync(command);
            return await GetSessionInfoAsync();
        }

        #endregion

        /// <summary>
        /// This model is returned in a few places where the member session status
        /// has changed.
        /// </summary>
        private async Task<object> GetSessionInfoAsync()
        {
            var member = await _queryExecutor.ExecuteAsync(new GetCurrentMemberSummaryQuery());
            var token = _antiforgery.GetAndStoreTokens(HttpContext);

            return new
            {
                Member = member,
                AntiForgeryToken = token.RequestToken
            };
        }
    }
}