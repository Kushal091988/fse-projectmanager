using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Intefaces;
using ProjectManager.Api.Extension.DTO;
using ProjectManager.Api.Extension.Interfaces;
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


        public bool Delete(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new InvalidOperationException("user does not exists");
            }

            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            return true;
        }



        public List<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            var userDtos = Mapper.Map<List<UserDto>>(users);

            return userDtos;
        }

        public UserDto Update(UserDto userDto)
        {
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
