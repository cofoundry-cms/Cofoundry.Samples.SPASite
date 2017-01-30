using Cofoundry.Core.EntityFramework;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    /// <summary>
    /// This handler uses ILoggedInPermissionCheckHandler to make sure
    /// a user logged in before allowing them to set the cat as liked.
    /// We could use IPermissionRestrictedCommandHandler to be more 
    /// specific here and create a specific permission for the action,
    /// but that isn't neccessary here because any logged in user
    /// can perform this action.
    /// 
    /// For more on permission see https://github.com/cofoundry-cms/cofoundry/wiki/Permissions
    /// </summary>
    public class SetCatLikedCommandHandler
        : IAsyncCommandHandler<SetCatLikedCommand>
        , ILoggedInPermissionCheckHandler
    {
        private readonly IEntityFrameworkSqlExecutor _entityFrameworkSqlExecutor;

        public SetCatLikedCommandHandler(
            IEntityFrameworkSqlExecutor entityFrameworkSqlExecutor
            )
        {
            _entityFrameworkSqlExecutor = entityFrameworkSqlExecutor;
        }

        public Task ExecuteAsync(SetCatLikedCommand command, IExecutionContext executionContext)
        {
            // We could use the EF DbContext here, but it's faster to make this change using a 
            // stored procedure. We use IEntityFrameworkSqlExecutor here to simplify this.
            // For more info see https://github.com/cofoundry-cms/cofoundry/wiki/Entity-Framework-&-DbContext-Tools#executing-stored-procedures--raw-sql

            return _entityFrameworkSqlExecutor
                .ExecuteCommandAsync("app.CatLike_SetLiked",
                 new SqlParameter("@CatId", command.CatId),
                 new SqlParameter("@UserId", executionContext.UserContext.UserId),
                 new SqlParameter("@IsLiked", command.IsLiked),
                 new SqlParameter("@CreateDate", executionContext.ExecutionDate)
                 );
        }
    }

}
