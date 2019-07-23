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
        // GET: api/parentTask/getTasks
        public IHttpActionResult GetTasks()
        {
            return Try(() =>
            {
                return Ok(_taskFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ResponseType(typeof(ParentTaskDto))]
        [HttpGet]
        // GET: api/parentTask/5
        public IHttpActionResult GetTask(int id)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(ParentTaskDto))]
        [HttpPost]
        // POST: api/parentTask/update
        public IHttpActionResult Update(ParentTaskDto task)
        {
            return Try(() =>
            {
                return Ok(_taskFacade.Update(task));
            });
        }
    }
}