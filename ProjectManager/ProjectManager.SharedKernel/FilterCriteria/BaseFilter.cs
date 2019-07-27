using ProjectManager.SharedKernel.Base;
using System;
using System.Linq;

namespace ProjectManager.SharedKernel.FilterCriteria
{
    public abstract class BaseEntityFilter<T> where T : IBaseEntity
    {
        public virtual void CompositeFilter(CompositeFilterDescriptor root, ref IQueryable<T> query)
        {
            var filters = FilterStateHelper.FlattenCompositeFilterDescriptor(root);

            foreach (var f in filters)
                Filter(f, ref query);
        }

        public virtual void Filter(FilterDescriptor filter, ref IQueryable<T> query)
        {
            if (filter != null && !string.IsNullOrWhiteSpace(filter.Field) && !string.IsNullOrWhiteSpace(filter.Operator))
            {
                switch (filter.Field.Trim().ToLower())
                {
                    case "id":
                        if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Id.ToString().ToLower() == filter.Value.ToString().ToLower());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                }
            }
        }
    }
}