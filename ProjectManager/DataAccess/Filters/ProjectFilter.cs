﻿using BusinessTier.Models;
using ProjectManager.SharedKernel;
using ProjectManager.SharedKernel.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Filters
{
    internal class ProjectFilter 
    {
        public void CompositeFilter(CompositeFilterDescriptor root, ref IQueryable<Project> query)
        {
            var filters = FilterStateHelper.FlattenCompositeFilterDescriptor(root);

            foreach (var f in filters)
                Filter(f, ref query);
        }

        public  void Filter(FilterDescriptor filter, ref IQueryable<Project> query)
        {
           // base.Filter(filter, ref query);

            if (filter != null && !string.IsNullOrWhiteSpace(filter.Field) && !string.IsNullOrWhiteSpace(filter.Operator))
            {
                switch (filter.Field.Trim().ToLower())
                {
                    case "name":
                        if (filter.FilterOperator == FilterOperator.Contains)
                        {
                            query = query.Where(q => q.Name.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Name == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "managerdisplayname":
                        if (filter.FilterOperator == FilterOperator.Contains)
                        {
                            query = query.Where(q => q.Manager.FirstName.Contains(filter.Value.ToString()));
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Manager.FirstName == filter.Value.ToString());
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "priority":
                        int.TryParse(filter.Value.ToString(), out int filterValue);
                        if (filter.FilterOperator == FilterOperator.GreaterThanEqual)
                        {
                            query = query.Where(q => q.Priority >= filterValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.LessThanEqual)
                        {
                            query = query.Where(q => q.Priority <= filterValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.EqualTo)
                        {
                            query = query.Where(q => q.Priority == filterValue);
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;

                    case "startdate":
                        if (filter.FilterOperator == FilterOperator.GreaterThanEqual)
                        {
                            var compareValue = new CalendarLocalDate((DateTime)filter.Value).Value;
                            query = query.Where(e => e.StartDate >= compareValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.LessThanEqual)
                        {
                            var compareValue = new CalendarLocalDate((DateTime)filter.Value, true).Value;
                            query = query.Where(e => e.StartDate <= compareValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.Range)
                        {
                            if (filter.Value != null)
                            {
                                var range = filter.Value
                                .ToString()
                                .ToObject<RangeFilter<string>>();

                                query = query.Where(e => e.StartDate.AddMinutes((double)range.TimeZoneOffset).Date >= range.From.YYYYMMDDToDate().Date
                                    && e.StartDate.AddMinutes((double)range.TimeZoneOffset).Date <= range.To.YYYYMMDDToDate().Date);
                            }
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                    case "enddate":
                        if (filter.FilterOperator == FilterOperator.GreaterThanEqual)
                        {
                            var compareValue = new CalendarLocalDate((DateTime)filter.Value).Value;
                            query = query.Where(e => e.EndDate >= compareValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.LessThanEqual)
                        {
                            var compareValue = new CalendarLocalDate((DateTime)filter.Value, true).Value;
                            query = query.Where(e => e.EndDate <= compareValue);
                        }
                        else if (filter.FilterOperator == FilterOperator.Range)
                        {
                            if (filter.Value != null)
                            {
                                var range = filter.Value
                                .ToString()
                                .ToObject<RangeFilter<string>>();

                                query = query.Where(e => e.EndDate.AddMinutes((double)range.TimeZoneOffset).Date >= range.From.YYYYMMDDToDate().Date
                                    && e.EndDate.AddMinutes((double)range.TimeZoneOffset).Date <= range.To.YYYYMMDDToDate().Date);
                            }
                        }
                        else throw new NotImplementedException("Operator not handled");
                        break;
                }
            }
        }
    }
}
