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
        // GET: api/task/getTasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }
        [Route("{id}")]
        [ResponseType(typeof(TaskDto))]
        [HttpGet]
        // GET: api/task/5
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(TaskDto))]
        [HttpPost]
        // POST: api/task/update
        public IHttpActionResult Update(TaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/task/delete/5
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Delete(id));
            });
        }
    }
}