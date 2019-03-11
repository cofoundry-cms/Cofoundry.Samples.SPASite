using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    /// <summary>
    /// Query to get some information about the currently logged in user. We can use
    /// IIgnorePermissionCheckHandler here because if the user is not logged in then
    /// we return null, so there's no need for a permission check.
    /// </summary>
    public class GetCurrentMemberSummaryQueryHandler
        : IAsyncQueryHandler<GetCurrentMemberSummaryQuery, MemberSummary>
        , IIgnorePermissionCheckHandler
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentMemberSummaryQueryHandler(
            IUserRepository userRepository
            )
        {
            _userRepository = userRepository;
        }

        public async Task<MemberSummary> ExecuteAsync(GetCurrentMemberSummaryQuery query, IExecutionContext executionContext)
        {
            if (!IsLoggedInMember(executionContext.UserContext)) return null;

            var user = await _userRepository.GetCurrentUserMicroSummaryAsync();

            return new MemberSummary()
            {
                UserId = user.UserId,
                Name = user.GetFullName()
            };
        }

        private bool IsLoggedInMember(IUserContext userContext)
        {
            return userContext.UserId.HasValue && userContext.UserArea.UserAreaCode == MemberUserArea.MemberUserAreaCode;

        }
    }
}
