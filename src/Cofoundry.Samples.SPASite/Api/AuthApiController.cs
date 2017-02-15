using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.WebApi;
using Cofoundry.Samples.SPASite.Domain;
using System.Threading.Tasks;
using Cofoundry.Web;
using Cofoundry.Core;
using Cofoundry.Core.Validation;

namespace Cofoundry.Samples.SPASite
{
    [RoutePrefix("api/auth")]
    [ValidateApiAntiForgeryToken]
    public class AuthApiController : ApiController
    {
        private readonly IApiResponseHelper _apiResponseHelper;
        private readonly IAntiCSRFService _antiCSRFService;
        private readonly ICommandExecutor _commandExecutor;

        public AuthApiController(
            IApiResponseHelper apiResponseHelper,
            IAntiCSRFService antiCSRFService,
            ICommandExecutor commandExecutor
            )
        {
            _apiResponseHelper = apiResponseHelper;
            _antiCSRFService = antiCSRFService;
            _commandExecutor = commandExecutor;
        }

        #region queries

        /// <summary>
        /// Once we have logged in we need to re-fetch the csrf token because
        /// the user identity will have changed and the old token will be invalid
        /// </summary>
        [HttpGet]
        [Route("csrf-token")]
        public IHttpActionResult GetCsrfToken()
        {
            var token = _antiCSRFService.GetToken();
            return _apiResponseHelper.SimpleQueryResponse(this, token);
        }

        #endregion

        #region commands

        [HttpPost]
        [Route("register")]
        public Task<IHttpActionResult> Register(RegisterMemberAndLogInCommand command)
        {
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        [HttpPost]
        [Route("login")]
        public Task<IHttpActionResult> Login(LogMemberInCommand command)
        {
            return _apiResponseHelper.RunCommandAsync(this, command);
        }

        #endregion
    }
}