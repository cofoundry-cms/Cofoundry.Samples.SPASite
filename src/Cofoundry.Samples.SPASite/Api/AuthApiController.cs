using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.WebApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;

namespace Cofoundry.Samples.SPASite
{
    [Route("api/auth")]
    [AutoValidateAntiforgeryToken]
    public class AuthApiController : Controller
    {
        private readonly IApiResponseHelper _apiResponseHelper;
        private readonly IAntiforgery _antiforgery;
        private readonly ICommandExecutor _commandExecutor;

        public AuthApiController(
            IApiResponseHelper apiResponseHelper,
            IAntiforgery antiforgery,
            ICommandExecutor commandExecutor
            )
        {
            _apiResponseHelper = apiResponseHelper;
            _antiforgery = antiforgery;
            _commandExecutor = commandExecutor;
        }

        #region queries

        /// <summary>
        /// Once we have logged in we need to re-fetch the csrf token because
        /// the user identity will have changed and the old token will be invalid
        /// </summary>
        [HttpGet("csrf-token")]
        public IActionResult GetCsrfToken()
        {
            var token = _antiforgery.GetAndStoreTokens(HttpContext);
            
            return _apiResponseHelper.SimpleQueryResponse(this, token.RequestToken);
        }

        #endregion

        #region commands

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RegisterMemberAndLogInCommand command)
        {
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] LogMemberInCommand command)
        {
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        #endregion
    }
}