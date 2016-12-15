using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class GetFeaturesByIdRangeQuery : IQuery<Dictionary<int, Feature>>
    {
        public GetFeaturesByIdRangeQuery() { }

        public GetFeaturesByIdRangeQuery(int[] ids)
        {
            FeatureIds = ids;
        }

        public int[] FeatureIds { get; set; }
    }
}
