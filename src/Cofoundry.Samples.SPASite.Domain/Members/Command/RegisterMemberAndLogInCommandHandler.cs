using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Cofoundry.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class RegisterMemberAndLogInCommandHandler
        : IAsyncCommandHandler<RegisterMemberAndLogInCommand>
        , IIgnorePermissionCheckHandler
    {
        private readonly CofoundryDbContext _dbContext;
        private readonly ICommandExecutor _commandExecutor;
        private readonly IUserContextService _userContextService;
        private readonly ILoginService _loginService;
        private readonly IExecutionContextFactory _executionContextFactory;
        
        public RegisterMemberAndLogInCommandHandler(
            CofoundryDbContext dbContext,
            ICommandExecutor commandExecutor,
            IUserContextService userContextService,
            ILoginService loginService,
            IExecutionContextFactory executionContextFactory
            )
        {
            _dbContext = dbContext;
            _commandExecutor = commandExecutor;
            _userContextService = userContextService;
            _loginService = loginService;
            _executionContextFactory = executionContextFactory;
        }

        public async Task ExecuteAsync(RegisterMemberAndLogInCommand command, IExecutionContext executionContext)
        {
            // if user does not exist

            // TODO: Implement a better way to define, auto-populate and get ahold of roles programatically
            var roleId = await _dbContext
                .Roles
                .AsNoTracking()
                .Where(r => r.SpecialistRoleTypeCode == "USR" && r.UserAreaCode == MemberUserArea.AreaCode)
                .Select(r => r.RoleId)
                .SingleOrDefaultAsync();

            var addUserCommand = new AddUserCommand();
            addUserCommand.Email = command.Email;
            addUserCommand.FirstName = command.FirstName;
            addUserCommand.LastName = command.LastName;
            addUserCommand.Password = command.Password;
            addUserCommand.RoleId = roleId;
            addUserCommand.UserAreaCode = MemberUserArea.AreaCode;

            // Because we're not logged in, we'll need to elevate permissions to allow
            // the anonymous user to add a new user account
            var systemExecutionContext = await _executionContextFactory.CreateSystemUserExecutionContextAsync(executionContext);
            ((ExecutionContext)systemExecutionContext).ExecutionDate = DateTime.UtcNow;
            await _commandExecutor.ExecuteAsync(addUserCommand, systemExecutionContext);

            // TODO: Send thank you email
            //await _mailService.SendAsync(member.EmailAddress, new NewUserWelcomeMailTemplate());

            // Log the user in
            await _loginService.LogAuthenticatedUserInAsync(addUserCommand.OutputUserId, true);
        }
    }
}
