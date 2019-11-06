using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static class Registration
    {
        public static List<TimeStampLog> ListLog = new List<TimeStampLog>();

        public struct TimeStampLog
        {
            //Дата лога
            public DateTime date;
            //Время прихода
            public TimeSpan arrival_timestamp;
            //Время ухода
            public TimeSpan liaving_timestamp;
        }

        static  Registration() {
            ListLog.Add(new TimeStampLog
            {
                date = new DateTime(2019, 10, 21),
                arrival_timestamp = new TimeSpan(8, 56, 23),
                liaving_timestamp = new TimeSpan(20, 58, 35)
            }); 
            ListLog.Add(new TimeStampLog
            {
                date = new DateTime(2019, 10, 28),
                arrival_timestamp = new TimeSpan(8, 56, 23),
            }); 
        }
        public static bool Log_Exist(DateTime date) {
            return ListLog.Exists(x => x.date.Date == date);
        }
        public static string get_time_arrival_from_day(DateTime date) 
        {
            if (ListLog.Find(x => x.date.Date == date).arrival_timestamp.ToString("hh':'mm':'ss") == "00:00:00")
            {
                return "";
            }
            else {
                return ListLog.Find(x => x.date.Date == date).arrival_timestamp.ToString("hh':'mm':'ss");
            }
        }
        public static string get_liaving_time_from_day(DateTime date) 
        {
            if (ListLog.Find(x => x.date.Date == date).liaving_timestamp.ToString("hh':'mm':'ss") == "00:00:00")
            {
                return "";
            }
            else {
                return ListLog.Find(x => x.date.Date == date).liaving_timestamp.ToString("hh':'mm':'ss");
            }
        }
        public static void add_log_arrival_time(DateTime date, TimeSpan time)
        {
            SQLConnector.set_log_arrival_time(date+time);
        }
        
        public static void add_log_liaving_time(DateTime date, TimeSpan time)
        {
            SQLConnector.set_log_liaving_time(date + time);
        }

        public static int start_registration_arrival_time() {
            //Выполняем регистрацию прихода
            //Если всё нормально, возвращаем 1, Если уже отмечен 2, если не удалось подключиться к серверу 3.
            return 1;
        }
        public static int start_registration_liaving_time() {
            //Выполняем регистрацию ухода
            //Если всё нормально, возвращаем 1, Если уже отмечен 2, если не удалось подключиться к серверу 3.
            return 1;
        }
    }
}
