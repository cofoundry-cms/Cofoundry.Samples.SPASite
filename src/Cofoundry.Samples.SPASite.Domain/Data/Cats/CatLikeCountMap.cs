using Cofoundry.Domain.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Data
{
    public class CatLikeCountMap : EntityTypeConfiguration<CatLikeCount>
    {
        public CatLikeCountMap()
        {
            HasKey(e => e.CatCustomEntityId);
        }
    }
}
