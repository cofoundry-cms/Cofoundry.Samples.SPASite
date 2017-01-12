using Cofoundry.Domain.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Data
{
    public class CatLikeMap : EntityTypeConfiguration<CatLike>
    {
        public CatLikeMap()
        {
            HasKey(e => new { e.CatCustomEntityId, e.UserId });

            // Relationships
            HasRequired(s => s.CatCustomEntity)
                .WithMany()
                .HasForeignKey(d => d.CatCustomEntityId);

            HasRequired(s => s.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);
        }
    }
}
