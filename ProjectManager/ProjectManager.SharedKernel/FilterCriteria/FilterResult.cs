using System.Collections.Generic;

namespace ProjectManager.SharedKernel.FilterCriteria
{
    public class FilterResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int? Total { get; set; }

        public object Auxiliary { get; set; }
    }
}