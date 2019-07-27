using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.SharedKernel
{
    public static class Extensions
    {
        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static int ToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;

            int.TryParse(value, out int result);

            return result;
        }

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
