using System;
using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace Things02.Module.BusinessObjects {

    [NavigationItem("Stuff")]
	public class Thing {

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		[Aggregated]
		public virtual List<SubThing> SubThings { get; set; }

	}

	public class SubThing  
	{

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public int ThingId { get; set; }
        [ForeignKey("ThingId")]
      
		public virtual Thing Thing {get;set; }

        public void Init(object parent)
        {
            Thing = parent as Thing;
        }
    }

    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
	public class Things02ContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			var optionsBuilder = new DbContextOptionsBuilder<Things02EFCoreDbContext>()
                .UseSqlServer(@";");
            return new Things02EFCoreDbContext(optionsBuilder.Options);
		}
	}
	//This factory creates DbContext for design-time services. For example, it is required for database migration.
	public class Things02DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Things02EFCoreDbContext> {
		public Things02EFCoreDbContext CreateDbContext(string[] args) {
			throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
			//var optionsBuilder = new DbContextOptionsBuilder<Things02EFCoreDbContext>();
			//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Things02");
			//return new Things02EFCoreDbContext(optionsBuilder.Options);
		}
	}
	[TypesInfoInitializer(typeof(Things02ContextInitializer))]
	public class Things02EFCoreDbContext : DbContext {
		public Things02EFCoreDbContext(DbContextOptions<Things02EFCoreDbContext> options) : base(options) {
		}

		public DbSet<Thing>  Things { get; set; }
		public DbSet<SubThing> SubThings { get; set; }
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
	    public DbSet<Things02.Module.BusinessObjects.ApplicationUser> Users { get; set; }
        public DbSet<Things02.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Things02.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });

			modelBuilder.Entity<Thing>().HasMany(x => x.SubThings).WithOne(x => x.Thing);
        }
	}
}
