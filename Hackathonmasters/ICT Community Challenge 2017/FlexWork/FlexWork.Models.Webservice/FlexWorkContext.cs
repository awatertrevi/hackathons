//
// FlexWorkContext.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FlexWork.Models
{
	public class FlexWorkContext : IdentityDbContext<User>
	{
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, please use data migrations.
		// For more information refer to the documentation:
		// http://msdn.microsoft.com/en-us/data/jj591621.aspx

		public FlexWorkContext() : base("name=FlexWorkContext")
		{
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = true;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

		    modelBuilder.Entity<OpeningHours>().HasKey(oh => new {oh.Day, oh.WorkspaceId});
            modelBuilder.Entity<Rating>().HasKey(r => new { r.WorkspaceId, r.UserId });
            modelBuilder.Entity<WorkspaceFacility>().HasKey(r => new { r.FacilityId, r.WorkspaceId });

            base.OnModelCreating(modelBuilder);
		}

		public static FlexWorkContext Create()
		{
			return new FlexWorkContext();
		}

        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<WorkspaceRegistration> WorkspaceRegistrations { get; set; }
        public DbSet<Facility> Facilities { get; set; }
    }
}
