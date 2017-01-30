using Cofoundry.Core;
using Cofoundry.Domain.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class SPASiteDbContext : DbContext
    {
        #region constructor

        static SPASiteDbContext()
        {
            Database.SetInitializer<SPASiteDbContext>(null);
        }

        public SPASiteDbContext()
            : base(DbConstants.ConnectionStringName)
        {
            // This helper just turns off LazyLoading and adds a console debug logger.
            DbContextConfigurationHelper.SetDefaults(this);
        }

        #endregion

        #region properties

        public DbSet<CatLike> CatLikes { get; set; }
        public DbSet<CatLikeCount> CatLikeCounts { get; set; }

        #endregion

        #region mapping

        /// <summary>
        /// We use the Cofoundry suggested config here which removes the PluralizingTableNameConvention
        /// and makes "app" the default schema. We also use the helper to map Cofoundry objects to this 
        /// DbContext so we can use them as relations on our data model.
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder
                .UseDefaultConfig()
                .MapCofoundryContent()
                .Map(new CatLikeMap())
                .Map(new CatLikeCountMap());
        }

        #endregion
    }
}