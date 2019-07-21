using ProjectManager.Api.Extension.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Api.Extension.Interfaces
{
    public interface IParentTaskFacade
    {
        ParentTaskDto Get(int Id);

        List<ParentTaskDto> GetAll();

        ParentTaskDto Update(ParentTaskDto task);

        bool Delete(int id);
    }
}
