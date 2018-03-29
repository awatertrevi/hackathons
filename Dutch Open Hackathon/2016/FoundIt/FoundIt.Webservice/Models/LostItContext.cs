//
// LostItContext.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;
using FoundIt.Models;

namespace FoundIt.Webservice
{
    public class LostItContext : IdentityDbContext<User>
    {
        public DbSet<LostItem> LostItems { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; }

        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public LostItContext() : base("name=LostItContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            base.Database.Log = (l) => System.Diagnostics.Debug.WriteLine(l);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static LostItContext Create()
        {
            return new LostItContext();
        }
    }
}