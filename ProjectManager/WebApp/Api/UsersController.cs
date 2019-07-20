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
using AutoMapper;
using BusinessTier.Models;
using DataAccess;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;

namespace WebApp.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserFacade _userFacade;
        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public UsersController()
        {
            _userFacade = new UserFacade(new DataAccess.Repositories.UserRepository());
        }

       [Route("getUsers")]
        [ResponseType(typeof(List<UserDto>))]
        [HttpGet]
        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            //return
            return Ok(_userFacade.GetAll());
        } 

        // GET: api/Users/5
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult GetUser(int id)
        {
            return Ok(_userFacade.Get(id));
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            return Ok(_userFacade.Update(user));
        }

        
        // DELETE: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_userFacade.Delete(id));
        }
    }
}