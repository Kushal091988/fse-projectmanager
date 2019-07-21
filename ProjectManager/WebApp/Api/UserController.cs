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
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserFacade _userFacade;
        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public UserController()
        {
            _userFacade = new UserFacade(new DataAccess.Repositories.UserRepository());
        }

        [Route("getUsers")]
        [ResponseType(typeof(List<UserDto>))]
        [HttpGet]
        // GET: api/users
        public IHttpActionResult GetUsers()
        {
            return Try(() =>
            {
                return Ok(_userFacade.GetAll());
            });
        }

        // GET: api/user/5
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult GetUser(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Get(id));
            });
        }

        // POST: api/user/5
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult Update(UserDto user)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Update(user));
            });
        }


        // DELETE: api/user/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            return Try(() =>
            {
                return Ok(_userFacade.Delete(id));
            });
        }
    }
}