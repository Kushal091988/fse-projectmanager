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

namespace WebApp.Api
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        public TasksController()
        {

        }
        private PMDbContext db = new PMDbContext();

        [Route("getTasks")]
        [HttpGet]
        // GET: api/Users
        public IHttpActionResult GetTasks()
        {
            return Json(db.Tasks);
        }

        // GET: api/tasks/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            var task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
    }
}