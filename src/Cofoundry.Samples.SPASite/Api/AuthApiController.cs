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

namespace Cofoundry.Samples.SPASite
{
    [RoutePrefix("api/auth")]
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

        #region commands

        [HttpPost]
        [Route("register")]
        public Task<IHttpActionResult> Register(RegisterMemberAndLogInCommand command)
        {
            return _apiResponseHelper.RunWithResultAsync(this, () => RunCommandAndReturnLoginData(command));
        }

        [HttpPost]
        [Route("login")]
        public Task<IHttpActionResult> Login(LogMemberInCommand command)
        {
            return _apiResponseHelper.RunWithResultAsync(this, () => RunCommandAndReturnLoginData(command));
        }

        private async Task<object> RunCommandAndReturnLoginData<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandExecutor.ExecuteAsync(command);

            // In order to process further requests we need to pass back the new anto csrf token.
            var data = new
            {
                AntiForgeryToken = _antiCSRFService.GetToken()
            };

            return data;
        }

        #endregion
    }
}