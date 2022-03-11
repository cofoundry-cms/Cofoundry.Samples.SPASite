using Cofoundry.Domain.CQS;
using System.Collections.Generic;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class GetAllFeaturesQuery : IQuery<ICollection<Feature>>
    {
    }
}