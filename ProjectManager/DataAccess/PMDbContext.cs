using BusinessTier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class PMDbContext : DbContext
    {
        public PMDbContext() :
          base("Fse")
        {
            // Database.SetInitializer<PMDbContext>(new ProjectManagerInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PMDbContext, Migrations.Configuration>());
        }

        public static PMDbContext Create()
        {
            return new PMDbContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BusinessTier.Models.Task> Tasks { get; set; }
        public DbSet<ParentTask> ParentTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey<int>(s => s.Id);

            modelBuilder.Entity<ParentTask>().HasKey<int>(s => s.Id);

            //modelBuilder.Entity<BusinessTier.Models.Task>()
            //    .HasRequired<User>(s => s.Owner)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<BusinessTier.Models.Task>()
            //    .HasRequired<ParentTask>(s => s.ParentTask)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
              .HasRequired<User>(p => p.Manager)
              .WithMany(u => u.Projects)
              .HasForeignKey<int>(p => p.ManagerId)
              .WillCascadeOnDelete(false) ;


            //modelBuilder.Entity<BusinessTier.Models.Task>()
            //    .HasKey<int>(s => s.Id)
            //    .HasRequired<Project>(s => s.Project)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}
