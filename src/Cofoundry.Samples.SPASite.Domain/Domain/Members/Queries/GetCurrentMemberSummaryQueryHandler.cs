using Cofoundry.Domain.CQS;

namespace Cofoundry.Samples.SPASite.Domain;

/// <summary>
/// Query to get some information about the currently logged in user. We can use
/// IIgnorePermissionCheckHandler here because if the user is not logged in then
/// we return null, so there's no need for a permission check.
/// </summary>
public class GetCurrentMemberSummaryQueryHandler
    : IQueryHandler<GetCurrentMemberSummaryQuery, MemberSummary>
    , IIgnorePermissionCheckHandler
{
    private readonly IContentRepository _contentRepository;

    public GetCurrentMemberSummaryQueryHandler(
        IContentRepository contentRepository
        )
    {
        _contentRepository = contentRepository;
    }

    public async Task<MemberSummary> ExecuteAsync(GetCurrentMemberSummaryQuery query, IExecutionContext executionContext)
    {
        if (!IsSignedInMember(executionContext.UserContext)) return null;

        var user = await _contentRepository
            .Users()
            .Current()
            .Get()
            .AsMicroSummary()
            .ExecuteAsync();

        return new MemberSummary()
        {
            UserId = user.UserId,
            DisplayName = user.DisplayName
        };
    }

    private bool IsSignedInMember(IUserContext userContext)
    {
        return userContext.IsSignedIn() && userContext.UserArea.UserAreaCode == MemberUserArea.Code;
    }
}
