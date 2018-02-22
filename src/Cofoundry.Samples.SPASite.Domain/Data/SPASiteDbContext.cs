using Cofoundry.Core;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Data
{
    /// <summary>
    /// This is a code-first EF DbContext that uses a handful of Cofoundry helpers
    /// to make setting it up a bit easier, especially when including with Cofoundry 
    /// data. You can of course do data access any way you like.
    /// 
    /// See https://github.com/cofoundry-cms/cofoundry/wiki/Entity-Framework-&-DbContext-Tools
    /// </summary>
    public class SPASiteDbContext : DbContext
    {
        #region constructor

        private readonly DatabaseSettings _databaseSettings;

        public SPASiteDbContext(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        #endregion

        #region config

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseSettings.ConnectionString);
        }

        /// <summary>
        /// We use the Cofoundry suggested config here which removes the PluralizingTableNameConvention
        /// and makes "app" the default schema. We also use the helper to map Cofoundry objects to this 
        /// DbContext so we can use them as relations on our data model.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseDefaultConfig()
                .MapCofoundryContent()
                .ApplyConfiguration(new CatLikeMap())
                .ApplyConfiguration(new CatLikeCountMap());
        }

        #endregion

        #region properties

        public DbSet<CatLike> CatLikes { get; set; }
        public DbSet<CatLikeCount> CatLikeCounts { get; set; }

        #endregion
    }
}