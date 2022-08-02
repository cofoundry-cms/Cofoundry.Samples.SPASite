namespace Cofoundry.Samples.SPASite.Domain;

public class SearchCatSummariesQuery
    : SimplePageableQuery
    , IQuery<PagedQueryResult<CatSummary>>
{
}
