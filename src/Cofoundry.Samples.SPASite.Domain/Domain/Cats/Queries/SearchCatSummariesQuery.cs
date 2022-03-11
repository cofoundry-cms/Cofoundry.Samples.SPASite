using Cofoundry.Domain;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class SearchCatSummariesQuery
        : SimplePageableQuery
        , IQuery<PagedQueryResult<CatSummary>>
    {
    }
}
