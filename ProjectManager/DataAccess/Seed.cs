using BusinessTier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DataAccess
{
    public class ProjectManagerInitializer
    {
        public static void Seed(PMDbContext context)
        {
            var users = new List<User>
            {
            new User{Id = 1, FirstName="FirstName_1",LastName="LastName_1", EmployeeId = "1" },
            new User{Id = 2, FirstName="FirstName_2",LastName="LastName_2", EmployeeId = "2"},
            new User{Id = 3, FirstName="FirstName_3",LastName="LastName_3" ,EmployeeId = "3"},
            new User{Id = 4, FirstName="FirstName_4",LastName="LastName_4" ,EmployeeId = "4"},
            };

            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();

            var projects = new List<Project>
            {
            new Project { Id =1, Name = "Project_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , ManagerId = 1},
            new Project { Id = 2, Name = "Project_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5, ManagerId = 1 },
            new Project { Id = 3, Name = "Project_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2, ManagerId = 1 },
            new Project { Id =4, Name = "Project_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 0, ManagerId = 1 },
            };
            projects.ForEach(p => context.Projects.AddOrUpdate(p));
            context.SaveChanges();

            var parentTask = new List<BusinessTier.Models.ParentTask>
            {
            new BusinessTier.Models.ParentTask { Id =1, Name = "ParentTask_1"},
            new BusinessTier.Models.ParentTask { Id =2, Name = "parentTask_2" },
            };
            parentTask.ForEach(pt => context.ParentTasks.AddOrUpdate(pt));
            context.SaveChanges();

            var tasks = new List<BusinessTier.Models.Task>
            {
            new BusinessTier.Models.Task { Id = 1, Name = "Task_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , OwnerId = 2, ProjectId = 1, ParentTaskId = 1},
            new BusinessTier.Models.Task { Id = 2, Name = "Task_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2 , OwnerId = 2, ProjectId = 2, ParentTaskId = 1},
            new BusinessTier.Models.Task { Id = 2, Name = "Task_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 3 , OwnerId = 3, ProjectId = 3, ParentTaskId = 1 },
            new BusinessTier.Models.Task { Id = 4, Name = "Task_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 4 , OwnerId = 4, ProjectId = 4, ParentTaskId = 1 },
            };
            tasks.ForEach(t => context.Tasks.AddOrUpdate(t));
            context.SaveChanges();
        }
    }
}