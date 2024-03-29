﻿using BusinessTier.Models;
using System.Data.Entity;

namespace DataAccess
{
    public partial class PMDbContext : DbContext
    {
        public PMDbContext() :
          base("ConnectionString")
        {
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

            modelBuilder.Entity<BusinessTier.Models.Task>()
                .HasOptional<ParentTask>(s => s.ParentTask)
                .WithMany(p=>p.Tasks)
                .HasForeignKey<int?>(t=>t.ParentTaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
              .HasRequired<User>(p => p.Manager)
              .WithMany(u => u.Projects)
              .HasForeignKey<int>(p => p.ManagerId)
              .WillCascadeOnDelete(false);

            //modelBuilder.Entity<BusinessTier.Models.Task>()
            //    .HasKey<int>(s => s.Id)
            //    .HasRequired<Project>(s => s.Project)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}