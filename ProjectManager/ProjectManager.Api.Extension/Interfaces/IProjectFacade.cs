using ProjectManager.Api.Extension.DTO;
using ProjectManager.SharedKernel.FilterCriteria;
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

        bool Delete(int id);

        FilterResult<ProjectDto> Query(FilterState filterState);
    }
}
