using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Calendar
{
   public static class Sittings
    {
        public static DateTime work_day = new DateTime(2019, 10, 4);

        public static TimeSpan time_arrival_start = new TimeSpan(8, 55, 0);
        public static TimeSpan time_arrival_end = new TimeSpan(9, 0, 0);

        public static TimeSpan liaving_time_start = new TimeSpan(10, 25, 0);
        public static TimeSpan liaving_time_end = new TimeSpan(10, 30, 0);

        public static string SQLBaseName = "Calendar.db";


    }
}
