using BusinessTier.Models;
using ProjectManager.SharedKernel.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Intefaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        FilterResult<Project> Query(FilterState filterState);
    }
}
