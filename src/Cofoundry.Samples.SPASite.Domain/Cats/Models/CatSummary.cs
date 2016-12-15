using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class CatSummary
    {
        public int CatId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ImageAssetRenderDetails MainImage { get; set; }
    }
}
