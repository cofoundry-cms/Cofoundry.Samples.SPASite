using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class CatDetails
    {
        public int CatId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Breed Breed { get; set; }

        public IEnumerable<Feature> Features { get; set; }

        public IEnumerable<ImageAssetRenderDetails> Images { get; set; }
    }
}
