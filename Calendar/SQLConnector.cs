using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    static class SQLConnector
    {

        public static SQLiteConnection Connect;

        static SQLConnector()
        {
            Connect = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "\\" + Properties.Settings.Default.DBName, true);
            Connect.Open();
        }
        public static short GetTypeDay(DateTime day) 
        {
            int unixTime = (int)(day - new DateTime(1970, 1, 1)).TotalSeconds;
            SQLiteCommand command = new SQLiteCommand(@"select type from Date WHERE date = " + unixTime + ";", Connect);

            SQLiteDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                return (short)Convert.ToInt32(read["type"]);
            }
            else
            {
                return 0;
            }
        }
        public static void SetTypeDay(DateTime day, int type) 
        {
            int unixTime = (int)(day - new DateTime(1970, 1, 1)).TotalSeconds;
            SQLiteCommand command = new SQLiteCommand(@"select type from Date WHERE date = " + unixTime + ";", Connect);

            SQLiteDataReader read = command.ExecuteReader();

            if (read.Read())
            {
                command = new SQLiteCommand(@"UPDATE Date SET type = '"+type+"' WHERE date = "+unixTime+";", Connect);
                command.ExecuteNonQuery();
            }
            else
            {
                command = new SQLiteCommand(@"INSERT INTO Date(date, type) VALUES(" + unixTime + ", 1);", Connect);
                command.ExecuteNonQuery();
            }
        }
        public static string get_log_arrival_time(DateTime day)
        {
            int unixTime = (int)(day - new DateTime(1970, 1, 1)).TotalSeconds;
            SQLiteCommand command = new SQLiteCommand(@"select * from Date date LEFT JOIN Log l ON date.id = l.Date_id WHERE date.date = " + unixTime + " and l.Type = '0';", Connect);

            //System.Windows.MessageBox.Show(day+"\n"+@"select * from Date date LEFT JOIN Log l ON date.id = l.Date_id WHERE date.date = " + unixTime + " and l.Type = '0';");

            SQLiteDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                return TimeSpan.FromSeconds(Convert.ToInt32(read["Time"])).ToString("hh':'mm':'ss");
            }
            else
            {
                return "";
            }

        }
         public static TimeSpan TimeSpanGetArrivalTiem(DateTime day)
        {
            int unixTime = (int)(day - new DateTime(1970, 1, 1)).TotalSeconds;
            SQLiteCommand command = new SQLiteCommand(@"select * from Date date LEFT JOIN Log l ON date.id = l.Date_id WHERE date.date = " + unixTime + " and l.Type = '0';", Connect);

            SQLiteDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                return TimeSpan.FromSeconds(Convert.ToInt32(read["Time"]));
            }
            else
            {
                return TimeSpan.FromSeconds(0);
            }

        }

        public static string get_log_liaving_time(DateTime day)
        {
            int unixTime = (int)(day - new DateTime(1970, 1, 1)).TotalSeconds;
            SQLiteCommand command = new SQLiteCommand(@"select * from Date date LEFT JOIN Log l ON date.id = l.Date_id WHERE date.date = " + unixTime + " and l.Type = '1';", Connect);

            SQLiteDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                return TimeSpan.FromSeconds(Convert.ToInt32(read["Time"])).ToString("hh':'mm':'ss");
            }
            else
            {
                return "";
            }

        }
        public static bool Log_Exist(DateTime day)
        {
            int unixTimeDate = (int)(day - new DateTime(1970, 1, 1)).TotalSeconds;

            SQLiteCommand command = new SQLiteCommand(@"select * from Date date inner JOIN Log l ON date.id = l.Date_id WHERE date.date = '" + unixTimeDate + "' and l.Type is not null;", Connect);

           

           SQLiteDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public static void set_log(DateTime day, int type)
        {

            int unixTimeDate = (int)(day.Date - new DateTime(1970, 1, 1)).TotalSeconds;
            int unixTimeTime = day.Hour * 60 * 60 + day.Minute * 60 + day.Second;

            SQLiteCommand command = new SQLiteCommand(@"select * from Date WHERE date = " + unixTimeDate + ";", Connect);

            SQLiteDataReader read = command.ExecuteReader();
            
            //Если за текущую дату есть запись в таблице Date то проверяем есть ли сам лог в таблице Log
            if (read.Read())
            {
                int DateId = Convert.ToInt32(read["id"]);

                command = new SQLiteCommand(@"select * from Log WHERE Date_id = " + unixTimeDate + " and Type = '" + type + "';", Connect);

                read = command.ExecuteReader();

                if (read.Read())
                {
                    throw new Exception("Попытка записи лога, хотя он уже существует за эту дату.");
                }
                else
                {

                    command = new SQLiteCommand(@"INSERT INTO Log (Date_id, Time, Type) VALUES ('" + DateId + "', '" + unixTimeTime + "', '" + type + "');", Connect);
                    command.ExecuteNonQuery();
                }
            }
            else
            {

                command = new SQLiteCommand(@"INSERT INTO Date (date, type) VALUES ('" + unixTimeDate + "', '0');", Connect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(@"select id from Date WHERE date = " + unixTimeDate + ";", Connect);
                read = command.ExecuteReader();
                int DateId = 0;
                if (read.Read())
                {
                    DateId = Convert.ToInt32(read["id"]);
                    command = new SQLiteCommand(@"INSERT INTO Log (Date_id, Time, Type) VALUES ('" + DateId + "', '" + unixTimeTime + "', '" + type + "');", Connect);
                    command.ExecuteNonQuery();
                }

            }

        }
        public static void set_log_liaving_time(DateTime day)
        {
            set_log(day, 1);
        }
        public static void set_log_arrival_time(DateTime day)
        {
            set_log(day, 0);
        }

    }
       
}
