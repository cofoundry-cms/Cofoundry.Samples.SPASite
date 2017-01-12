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
    public class SetCatLikedCommandHandler
        : IAsyncCommandHandler<SetCatLikedCommand>
        , IIgnorePermissionCheckHandler
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
            return _entityFrameworkSqlExecutor
                .ExecuteCommandAsync("Cofoundry.CustomEntity_AddDraft",
                 new SqlParameter("@CatId", command.CatId),
                 new SqlParameter("@UserId", executionContext.UserContext.UserId),
                 new SqlParameter("@IsLiked", command.IsLiked),
                 new SqlParameter("@CreateDate", executionContext.ExecutionDate)
                 );
        }
    }

}
