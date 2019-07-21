using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessTier.Models;
using DataAccess;
using DataAccess.Repositories;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;

namespace WebApp.Api
{
    [RoutePrefix("api/task")]
    public class TaskController : BaseApiController
    {
        private readonly ITaskFacade _taskFacade;
        public TaskController(ITaskFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }

        public TaskController()
        {
            _taskFacade = new TaskFacade(new TaskRepository());
        }

        [Route("getTasks")]
        [ResponseType(typeof(List<TaskDto>))]
        [HttpGet]
        // GET: api/tasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }

        // GET: api/tasks/5
        [ResponseType(typeof(TaskDto))]
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        // POST: api/tasks/5
        [ResponseType(typeof(TaskDto))]
        public IHttpActionResult Update(TaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }


        // DELETE: api/Users/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Delete(id));
            });
        }
    }
}