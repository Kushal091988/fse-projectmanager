using DataAccess.Repositories;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;
using ProjectManager.SharedKernel.FilterCriteria;
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

        [Route("query")]
        [HttpPost()]
        [ResponseType(typeof(List<UserDto>))]
        public IHttpActionResult Query([FromBody]FilterState filterState)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Query(filterState));
            });
        }

        [Route("getProjects")]
        [ResponseType(typeof(List<ProjectDto>))]
        [HttpGet]
        // GET: api/project/getProjects
        public IHttpActionResult GetProjects()
        {
            return Try(() =>
            {
                return Ok(_projectFacade.GetAll());
            });
        }

        [Route("{id}")]
        [ResponseType(typeof(ProjectDto))]
        [HttpGet]
        // GET: api/project/5
        public IHttpActionResult GetProject(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Get(id));
            });
        }

        [Route("update")]
        [ResponseType(typeof(ProjectDto))]
        [HttpPost]
        // POST: api/project/update
        public IHttpActionResult Update(ProjectDto project)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Update(project));
            });
        }

        [Route("delete/{id}")]
        [ResponseType(typeof(bool))]
        [HttpDelete]
        // DELETE: api/project/5
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_projectFacade.Delete(id));
            });
        }
    }
}
