using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProjectManager.Api.Extension.Helper
{
    public static class Extensions
    {
        public static DateTime YYYYMMDDToDate(this string dateStr)
        {
            return DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public static string DateToYYYYMMDD(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }
    }
}