using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories.Intefaces;
using Moq;
using NBench;
using NUnit.Framework;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WebApp;
using WebApp.Api;

namespace Web.Api.Tests
{
    [TestFixture]
    public class TestTaskController
    {
        [SetUp]
        [PerfSetup]
        public void InitializeOneTimeData()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Initialize();
        }

        [Test]
        public void GetTasks_ShouldReturnAllTasks()
        {
            //arrange
            var testTasks = GetTestTasks();
            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);
            
            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
            var result = TaskController.GetTasks() as OkNegotiatedContentResult<List<TaskDto>>;

            //assert
            Assert.AreEqual(testTasks.Count(), result.Content.Count);
        }

        [Test]
        public void  GetTask_ShouldReturnCorrectTask()
        {
            //arrange
            var TaskIdToBeQueried = 1;
            var testTasks = GetTestTasks();

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBeQueried)).Returns(testTasks.First(u=>u.Id == TaskIdToBeQueried));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);
            var expectetTask = testTasks.First(u => u.Id == TaskIdToBeQueried);

            //act
            var result = TaskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(expectetTask.Name, result.Content.Name);
            Assert.AreEqual(expectetTask.Priority, result.Content.Priority);
        }

        [Test]
        public void Update_ShouldAddNewTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var newTaskDto = new TaskDto() {
                Id = 5,
                Name = "Task_5",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };
            var newTask = Mapper.Map<Task>(newTaskDto);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Add(newTask)).Returns(newTask);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
            var result = TaskController.Update(newTaskDto) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(newTaskDto.Name, result.Content.Name);
            Assert.AreEqual(newTaskDto.Priority, result.Content.Priority);
        }

        [Test]
        public void Update_ShouldUpdateCorrectTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var TaskDtoToBeUpdated = new TaskDto()
            {
                Id = 4,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };

            var oldTask = testTasks.First(u => u.Id == TaskDtoToBeUpdated.Id);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.Id)).Returns(oldTask);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
            var result = TaskController.Update(TaskDtoToBeUpdated) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(TaskDtoToBeUpdated.Name, result.Content.Name);
            Assert.AreEqual(TaskDtoToBeUpdated.Priority, result.Content.Priority);
        }

        [Test]
        public void Delete_ShouldDeleteCorrectTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var TaskIdToBDeleted = 4;

            var Task = testTasks.First(u => u.Id == TaskIdToBDeleted);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBDeleted)).Returns(Task);
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Remove(Task));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
            var result = TaskController.Delete(TaskIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content); 
        }

        private IQueryable<Task> GetTestTasks()
        {
            var testTasks = new List<BusinessTier.Models.Task>
            {
             new BusinessTier.Models.Task { Id = 1, Name = "Task_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , OwnerId = 2, ProjectId = 1, ParentTaskId = 1},
            new BusinessTier.Models.Task { Id = 2, Name = "Task_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2 , OwnerId = 2, ProjectId = 2, ParentTaskId = 1},
            new BusinessTier.Models.Task { Id = 2, Name = "Task_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 3 , OwnerId = 3, ProjectId = 3, ParentTaskId = 1 },
            new BusinessTier.Models.Task { Id = 4, Name = "Task_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 4 , OwnerId = 4, ProjectId = 4, ParentTaskId = 1 },
            };

            return testTasks.AsQueryable();
        }
    }
}