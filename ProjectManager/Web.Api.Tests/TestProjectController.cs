using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories.Intefaces;
using Moq;
using NBench;
using NUnit.Framework;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WebApp;
using WebApp.Api;

namespace Web.Api.Tests
{
    [TestFixture]
    public class TestProjectController
    {
        [SetUp]
        [PerfSetup]
        public void InitializeOneTimeData()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Initialize();
        }

        [Test]
        public void GetProjects_ShouldReturnAllProjects()
        {
            //arrange
            var testProjects = GetTestProjects();
            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.GetAll()).Returns(testProjects);
            
            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result = projectController.GetProjects() as OkNegotiatedContentResult<List<ProjectDto>>;

            //assert
            Assert.AreEqual(testProjects.Count(), result.Content.Count);
        }

        [Test]
        public void  GetProject_ShouldReturnCorrectProject()
        {
            //arrange
            var ProjectIdToBeQueried = 1;
            var testProjects = GetTestProjects();

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBeQueried)).Returns(testProjects.First(u=>u.Id == ProjectIdToBeQueried));

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);
            var expectetProject = testProjects.First(u => u.Id == ProjectIdToBeQueried);

            //act
            var result = projectController.GetProject(ProjectIdToBeQueried) as OkNegotiatedContentResult<ProjectDto>;

            //assert
            Assert.AreEqual(expectetProject.Name, result.Content.Name);
            Assert.AreEqual(expectetProject.Priority, result.Content.Priority);
            Assert.AreEqual(expectetProject.ManagerId, result.Content.ManagerId);
        }

        [Test]
        public void Update_ShouldAddNewProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var newProjectDto = new ProjectDto()
            {
                Id = 5,
                Name = "Project_5",
                StartDate = "20190101",
                EndDate = "20200901",
                Priority = 2,
                ManagerId = 2
            };
            var newProject = Mapper.Map<Project>(newProjectDto);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Add(newProject)).Returns(newProject);

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result = projectController.Update(newProjectDto) as OkNegotiatedContentResult<ProjectDto>;

            //assert
            Assert.AreEqual(newProjectDto.Name, result.Content.Name);
            Assert.AreEqual(newProjectDto.Priority, result.Content.Priority);
        }

        [Test]
        public void Update_ShouldUpdateCorrectProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var projectDtoToBeUpdated = new ProjectDto()
            {
                Id = 4,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result = projectController.Update(projectDtoToBeUpdated) as OkNegotiatedContentResult<ProjectDto>;

            //assert
            Assert.AreEqual(projectDtoToBeUpdated.Name, result.Content.Name);
            Assert.AreEqual(projectDtoToBeUpdated.Priority, result.Content.Priority);
        }

        [Test]
        public void Delete_ShouldDeleteCorrectProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var ProjectIdToBDeleted = 4;

            var Project = testProjects.First(u => u.Id == ProjectIdToBDeleted);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBDeleted)).Returns(Project);
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Remove(Project));

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result = projectController.Delete(ProjectIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content); 
        }

        private IQueryable<Project> GetTestProjects()
        {
            var testProjects = new List<Project>
            {
            new Project { Id =1, Name = "Project_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , ManagerId = 1},
            new Project { Id = 2, Name = "Project_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5, ManagerId = 1 },
            new Project { Id = 3, Name = "Project_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 10, ManagerId = 1 },
            new Project { Id =4, Name = "Project_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 15, ManagerId = 1 },
            };

            return testProjects.AsQueryable();
        }
    }
}