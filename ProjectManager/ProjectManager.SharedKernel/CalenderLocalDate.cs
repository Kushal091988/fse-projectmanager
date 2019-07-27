using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.SharedKernel
{
    /// <summary>
    /// Convert UTC Date to Local Date and removed its time elements
    /// </summary>
    public struct CalendarLocalDate
    {
        public DateTime Value { get; }

        public CalendarLocalDate(DateTime inputDate, bool endDayTime = false)
        {
            var convertDate = inputDate;
            switch (inputDate.Kind)
            {
                case DateTimeKind.Utc:
                    convertDate = inputDate.ToLocalTime();
                    break;
            }

            if (!endDayTime)
            {
                Value = new DateTime(convertDate.Year, convertDate.Month, convertDate.Day, 0, 0, 0);
            }
            else
            {
                Value = new DateTime(convertDate.Year, convertDate.Month, convertDate.Day, 23, 59, 59);
            }
        }

    }
}
