using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories.Intefaces;
using Moq;
using NUnit.Framework;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WebApp.Api;

namespace Web.Api.Tests
{
    [TestFixture]
    public class TestParentTaskController
    { 
        [Test]
        public void GetTasks_ShouldReturnAllParenTasks()
        {
            //arrange
            var testTasks = GetTestTasks();
            var mockTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);
            
            var taskFacade = new ParentTaskFacade(mockTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.GetTasks() as OkNegotiatedContentResult<List<ParentTaskDto>>;

            //assert
            Assert.AreEqual(testTasks.Count(), result.Content.Count);
        }

        [Test]
        public void  GetUser_ShouldReturnCorrectUser()
        {
            //arrange
            var taskIdToBeQueried = 1;
            var testTasks = GetTestTasks();

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(taskIdToBeQueried)).Returns(testTasks.First(u=>u.Id == taskIdToBeQueried));

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);
            var expectetUser = testTasks.First(u => u.Id == taskIdToBeQueried);

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(expectetUser.Name, result.Content.Name);
        }

        [Test]
        public void Update_ShouldAddNewUser()
        {
            //arrange
            var testTasks = GetTestTasks();
            var newTaskDto = new ParentTaskDto() {
                Name = "Name_Mocked",
            };
            var newUser = Mapper.Map<ParentTask>(newTaskDto);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Add(newUser)).Returns(newUser);

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.Update(newTaskDto) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(newTaskDto.Name, result.Content.Name); 
        }

        [Test]
        public void Update_ShouldUpdateCorrectUser()
        {
            //arrange
            var testTasks = GetTestTasks();
            var userDtoToBeUpdated = new ParentTaskDto()
            {
                Id = 2,
                Name = "Name_updated"
            };

            var oldTask = testTasks.First(u => u.Id == userDtoToBeUpdated.Id);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(userDtoToBeUpdated.Id)).Returns(oldTask);

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.Update(userDtoToBeUpdated) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(userDtoToBeUpdated.Name, result.Content.Name); 
        }


        private IQueryable<ParentTask> GetTestTasks()
        {
            var parentTask = new List<BusinessTier.Models.ParentTask>
            {
            new BusinessTier.Models.ParentTask { Id =1, Name = "ParentTask_1"},
            new BusinessTier.Models.ParentTask { Id =2, Name = "parentTask_2" },
            };

            return parentTask.AsQueryable();
        }
    }
}