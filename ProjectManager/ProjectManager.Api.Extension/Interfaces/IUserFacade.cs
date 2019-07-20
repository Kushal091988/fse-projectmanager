using ProjectManager.Api.Extension.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Api.Extension.Interfaces
{
    public interface IUserFacade
    {
        UserDto Get(int Id);

        List<UserDto> GetAll();

        UserDto Update(UserDto user);

        bool Delete(int id);

    }
}
