using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Intefaces;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;
using ProjectManager.SharedKernel.FilterCriteria;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Api.Extension
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserRepository _userRepository;
        public UserFacade(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public FilterResult<UserDto> Query(FilterState filterState)
        {
            var filterResult = _userRepository.Query(filterState);

            if (filterResult != null)
            {
                var users = Mapper.Map<List<UserDto>>(filterResult.Data);
                return new FilterResult<UserDto>
                {
                    Total = filterResult.Total,
                    Data = users
                };
            }

            return null; 
        }

        /// <summary>
        /// get user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user for the given id</returns>
        public UserDto Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new InvalidOperationException("user does not exists");
            }

            var userDto = Mapper.Map<UserDto>(user);
            return userDto;
        }


        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flag to know if deleted</returns>
        public bool Delete(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new InvalidOperationException("user does not exists so could not be deleted");
            }

            if (user.Projects.Count > 0)
            {
                throw new InvalidOperationException("user has associated projects, so could not be deleted");
            }

            if (user.Tasks.Count > 0)
            {
                throw new InvalidOperationException("user has associated tasks, so could not be deleted");
            }

            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            return true;
        }
               

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>users list</returns>
        public List<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            var userDtos = Mapper.Map<List<UserDto>>(users);

            return userDtos;
        }

        /// <summary>
        /// either create or update provided user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public UserDto Update(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.FirstName) && string.IsNullOrWhiteSpace(userDto.LastName))
            {
                throw new InvalidOperationException("Either First Name or Last Name required");
            }
            var user = _userRepository.Get(userDto.Id);
            if (user == null)
            {
                //create user
                user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    EmployeeId = userDto.EmployeeId
                };
                _userRepository.Add(user);
            }
            else
            {
                //update user
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.EmployeeId = userDto.EmployeeId;
            }
            _userRepository.SaveChanges();

            return userDto;
        }
    }
}
