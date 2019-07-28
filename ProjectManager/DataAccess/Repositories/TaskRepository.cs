using BusinessTier.Models;
using DataAccess.Filters;
using DataAccess.Repositories.Intefaces;
using DataAccess.Sort;
using ProjectManager.SharedKernel.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TaskRepository : Repository<BusinessTier.Models.Task>, ITaskRepository
    {
        public FilterResult<BusinessTier.Models.Task> Query(FilterState filterState)
        {
            var result = new FilterResult<BusinessTier.Models.Task>();
            IQueryable<BusinessTier.Models.Task> query = Context.Tasks.Include(t=>t.ParentTask);
            if (filterState != null)
            {
                // Filtering
                if (filterState.Filter?.Filters != null)
                {
                    var filter = new TaskFilter();
                    if (filterState.Filter.Logic.ToLower() == "and")
                    {
                        filter.CompositeFilter(filterState.Filter, ref query);
                    }
                    else
                    {
                        throw new NotImplementedException("Logic not handled");
                    }
                }

                // Sorting
                if (filterState.Sort != null)
                {
                    foreach (var sort in filterState.Sort)
                    {
                        var purchaseOrderSort = new TaskSort();
                        purchaseOrderSort.Sort(sort, ref query);
                    }
                }

                if (filterState.Take > 0)
                {
                    // Pagination
                    result.Data =  query
                                 .Skip(filterState.Skip)
                                 .Take(filterState.Take)
                                 .ToList(); 
                }
                else
                {
                    result.Data = query.ToList();
                }
            }
            else
            {
                result.Data = query.ToList();
            }

            // Get total records count
            result.Total = query.Count();

            return result;
        }
    }
}
