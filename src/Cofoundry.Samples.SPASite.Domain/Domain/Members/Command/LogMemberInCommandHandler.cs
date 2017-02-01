using Cofoundry.Core.Validation;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class LogMemberInCommandHandler
        : IAsyncCommandHandler<LogMemberInCommand>
        , IIgnorePermissionCheckHandler
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ILoginService _loginService;
        
        public LogMemberInCommandHandler(
            ICommandExecutor commandExecutor,
            IQueryExecutor queryExecutor,
            ILoginService loginService
            )
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _loginService = loginService;
        }

        public async Task ExecuteAsync(LogMemberInCommand command, IExecutionContext executionContext)
        {
            // If user logged into another area, log them out first
            var currentContext = executionContext.UserContext;
            var isLoggedIntoDifferentUserArea = currentContext.UserArea?.UserAreaCode != MemberUserArea.AreaCode;

            if (currentContext.UserId.HasValue && !isLoggedIntoDifferentUserArea) return;

            await CheckLoginMaxLoginAttemptsNotExceeded(command.Email);

            var user = await Authenticate(command.Email, command.Password);

            if (user == null)
            {
                await _loginService.LogFailedLoginAttemptAsync(MemberUserArea.AreaCode, command.Email);
                
                throw new PropertyValidationException("The give username/password combination was invalid", nameof(command.Password));
            }

            ValidateLoginArea(user.UserAreaCode);
            await _loginService.LogAuthenticatedUserInAsync(user.UserId, true);
        }

        private async Task CheckLoginMaxLoginAttemptsNotExceeded(string username)
        {
            var query = new HasExceededMaxLoginAttemptsQuery()
            {
                UserAreaCode = MemberUserArea.AreaCode,
                Username = username
            };

            if (await _queryExecutor.ExecuteAsync(query))
            {
                throw new ValidationException("Too many failed login attempts have been detected, please try again later.");
            }
        }

        private void ValidateLoginArea(string userArea)
        {
            if (MemberUserArea.AreaCode != userArea)
            {
                throw new ValidationException("This user account is not permitted to log in via this route.");
            }
        }

        private async Task<UserLoginInfo> Authenticate(string email, string password)
        {
            var authCommand = new GetUserLoginInfoIfAuthenticatedQuery()
            {
                Username = email,
                Password = password
            };

            var user = await _queryExecutor.ExecuteAsync(authCommand);

            return user;
        }
    }
}
