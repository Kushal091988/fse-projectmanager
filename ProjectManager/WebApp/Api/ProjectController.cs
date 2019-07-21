using DataAccess.Repositories;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApp.Api
{
    [RoutePrefix("api/project")]
    public class ProjectController : BaseApiController
    {
        private readonly IProjectFacade _projectFacade;
        public ProjectController(IProjectFacade projectFacade)
        {
            _projectFacade = projectFacade;
        }
        public ProjectController()
        {
            _projectFacade = new ProjectFacade(new ProjectRepository());
        }

        [Route("getProjects")]
        [ResponseType(typeof(List<ProjectDto>))]
        [HttpGet]
        // GET: api/project
        public IHttpActionResult GetProjects()
        {
            return Try(() =>
            {
                return Ok(_projectFacade.GetAll());
            });
        }

        // GET: api/project/5
        [ResponseType(typeof(ProjectDto))]
        public IHttpActionResult GetProject(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Get(id));
            });
        }

        // POST: api/project/
        [ResponseType(typeof(ProjectDto))]
        public IHttpActionResult Update(ProjectDto project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Update(project));
            });
        }


        // DELETE: api/project/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Delete(id));
            });
        }
    }
}
