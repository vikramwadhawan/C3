using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper.Helper
{
    public static class DateHelper
    {

        public static DateTime ConvertToCurrentTimeZone(DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
        }

        public static DateTime ConvertInTimeZone(this DateTime dt, string timeZone)
        {
            DateTime convertedDt = new DateTime();
            TimeZoneInfo dstnTz = null;
            try
            {
                dstnTz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            }
            catch { }

            TimeZoneInfo srcTz = TimeZoneInfo.Local;
            if (dstnTz != null)
                convertedDt = TimeZoneInfo.ConvertTime(dt, dstnTz);
            else
                convertedDt = dt;
            return convertedDt;
        }
    }
}
