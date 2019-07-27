using System;
using ProjectManager.SharedKernel.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.SharedKernel.FilterCriteria
{
    public abstract class BaseEntitySort<T> where T : IBaseEntity
    {
        public virtual void Sort(SortDescriptor sort, ref IQueryable<T> query)
        {
            if (sort != null && !string.IsNullOrWhiteSpace(sort.Field) && !string.IsNullOrWhiteSpace(sort.Dir))
            {
                switch (sort.Field.Trim().ToLower())
                {
                    case "id":
                        if (sort.Direction == SortDirection.ASC)
                        {
                            query = query.OrderBy(p => p.Id);
                        }
                        else
                        {
                            query = query.OrderByDescending(p => p.Id);
                        }

                        break;
                }
            }
        }
    }
}
