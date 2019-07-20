using ProjectManager.Api.Extension.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Api.Extension.Interfaces
{
    public interface IProjectFacade
    {
        ProjectDto Get(int Id);

        List<ProjectDto> GetAll();

        ProjectDto Update(ProjectDto user);

        ProjectDto Delete(int id);
    }
}
