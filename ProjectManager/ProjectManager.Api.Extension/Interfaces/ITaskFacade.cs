using ProjectManager.Api.Extension.DTO;
using ProjectManager.SharedKernel.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Api.Extension.Interfaces
{
    public interface ITaskFacade
    {
        TaskDto Get(int Id);

        List<TaskDto> GetAll();

        TaskDto Update(TaskDto task);

        bool Delete(int id);

        FilterResult<TaskDto> Query(FilterState filterState);
    }
}
