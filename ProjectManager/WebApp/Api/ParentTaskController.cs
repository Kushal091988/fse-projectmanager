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
    [RoutePrefix("api/parentTask")]
    public class ParentTaskController : BaseApiController
    {
        private readonly IParentTaskFacade _taskFacade;
        public ParentTaskController(IParentTaskFacade taskFacade)
        {
            _taskFacade = taskFacade;
        }
        public ParentTaskController()
        {
            _taskFacade = new ParentTaskFacade(new ParentTaskRepository());
        }


        [Route("getTasks")]
        [ResponseType(typeof(List<ParentTaskDto>))]
        [HttpGet]
        // GET: api/tasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }

        // GET: api/parent-task/5
        [ResponseType(typeof(ParentTaskDto))]
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        // POST: api/parent-task/5
        [ResponseType(typeof(ParentTaskDto))]
        public IHttpActionResult Update(ParentTaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }


        // DELETE: api/parent-task/5
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