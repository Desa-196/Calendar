using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

using System.Threading.Tasks;

namespace Calendar
{
    public static class CalculationData
    {

        private static DateTime Date_even_week = new DateTime(2019, 7, 1);
        public static ObservableCollection<Day> array_objects_days = new ObservableCollection<Day>();
        public static  NowDay nowDayObject  = new NowDay(new DateTime());

        private static int index_now_day = 0;
        public static int Index_now_day
        {
            get
            {
                return index_now_day;
            }
        }



        // Возвращает день недели 1-Понедельник, 2-Вторник,.......
        public static int get_day_week(DateTime date)
        {

            if ((int)date.DayOfWeek == 0)
            {
                return 7;
            }

            else
            {
                return (int)date.DayOfWeek;
            }
        }

        // Функция расчитывает в четной или нечетной неделе находится дата
        public static bool is_even_week(DateTime date)
        {

            //Считаем кол-во дней между датой отправленной в качестве параметра и 1.07.2019
            int count_days = (date - Date_even_week).Days;

            //Получаем абсолютное значение кол-ва дней разницы между датами
            count_days = Math.Abs(count_days);

            //сколько целых недель прошло с 1.07.2019
            int countWeek = count_days / 7;

            //если целых недель прошло кратно двум, значит неделя четная
            if (countWeek % 2 == 0)
            {
                return true;
            }
            //Если не кратно двум, значит не четная
            else
            {
                return false;
            }
        }


        //Расчитывает рабочий день или нет
        public static bool is_work(DateTime date)
        {
            //Проверяем нашу дату, находится она в длинной рабочей(возвращаем 1) неделе или короткой(возвращаем 0)
            int date_work_of_the_week = get_day_week(Properties.Settings.Default.WorkDay);
            bool long_week;
            if (date_work_of_the_week == 1 || date_work_of_the_week == 2 || date_work_of_the_week == 5 || date_work_of_the_week == 6 || date_work_of_the_week == 7)
            {
                long_week = true;
            }

            else
            {
                long_week = false;
            }

            //Получаем номер дня недели переданной даты
            int now_day_of_the_week = get_day_week(date);

            //Если сегодняшняя неделя по четности  совпадает  с указанной рабочей датой
            if (is_even_week(date) == is_even_week(Properties.Settings.Default.WorkDay))
            {
                //Если указанный рабочий день был в длинной неделе то и сегодняшняя неделя длинная. Тогда проверяем рабочий ли сегодня день
                if (now_day_of_the_week == 1 || now_day_of_the_week == 2 || now_day_of_the_week == 5 || now_day_of_the_week == 6 || now_day_of_the_week == 7)
                {
                    if (long_week) return true;
                    else return false;
                }

                else
                {
                    if (long_week) return false;
                    else return true;
                }
            }

            else
            {
                //Если указанный рабочий день был в длинной неделе то  сегодняшняя неделя наоборот короткая, тогда проверяем рабочий ли сегодня день
                if (now_day_of_the_week == 3 || now_day_of_the_week == 4)
                {
                    if (long_week) return true;
                    else return false;
                }

                else
                {
                    if (long_week) return false;
                    else return true;
                }
            }
        }
        public static int[] get_old_month(int year, int month)
        {

            int[] old_month = new int[2];

            if (month - 1 == 0)
            {
                old_month[0] = 12;
                old_month[1] = year - 1;
            }
            else
            {
                old_month[0] = month - 1;
                old_month[1] = year;
            }
            return old_month;
        }
        public static int[] get_new_month(int year, int month)
        {

            int[] new_month = new int[2];

            if (month + 1 > 12)
            {
                new_month[0] = 1;
                new_month[1] = year + 1;
            }
            else
            {
                new_month[0] = month + 1;
                new_month[1] = year;
            }
            return new_month;
        }
        //Возвращает массив с данными для ячеек календаря
        public static ObservableCollection<Day> get_array_days(int year, int month)
        {

            array_objects_days.Clear();
            GC.Collect();


            //Определяем кол-во дней в переданном месяце
            int day_in_now_month = DateTime.DaysInMonth(year, month);

            //Определяем с какого дня недели начинается переданный месяц
            int day_of_the_week = get_day_week(new DateTime(year, month, 1));

            //Определям предидущий месяц и в каком он был году
            int old_month = get_old_month(year, month)[0];
            int old_year = get_old_month(year, month)[1];

            //Определям следующий месяц и в каком он был году
            int new_month = get_new_month(year, month)[0];
            int new_year = get_new_month(year, month)[1];

            //Определяем кол-во дней в прошлом месяце
            int day_in_old_month = DateTime.DaysInMonth(old_year, old_month);

            int i = 0;
            int d = 1;

            while (i + 1 < day_of_the_week)
            {

                array_objects_days.Add(new Day( new DateTime(old_year, old_month, i + day_in_old_month - day_of_the_week + 2) ));
                array_objects_days[i].day_is_now_month = false;

                array_objects_days[i].Text_block_1 = Registration.get_time_arrival_from_day ( new DateTime(old_year, old_month, i + day_in_old_month - day_of_the_week + 2) );
                array_objects_days[i].Text_block_2 = Registration.get_liaving_time_from_day( new DateTime(old_year, old_month, i + day_in_old_month - day_of_the_week + 2) );

                if (array_objects_days[i].Data_index.Date > DateTime.Now.Date) 
                {
                    array_objects_days[i].EnableContextMenu = true;
                }

                if (array_objects_days[i].Data_index.Date == DateTime.Now.Date) {
                    array_objects_days[i].Is_now_day = true;
                }   

                if (CalculationData.is_work(new DateTime(old_year, old_month, array_objects_days[i].getDay() )))
                {
                    array_objects_days[i].IsWork = true;
                }
                else
                {
                    array_objects_days[i].IsWork = false;
                }
                i++;

            }

            d = 1;
            while (d <= day_in_now_month)
            {
                if (DateTime.Now.Date == new DateTime(year, month, d) && CalculationData.is_work(new DateTime(year, month, d)) && SQLConnector.GetTypeDay(new DateTime(year, month, d)) == 0)
                {
                    nowDayObject.Data_index = new DateTime( year, month, d );
                    array_objects_days.Add(nowDayObject);
                        
                }
                else 
                {
                    array_objects_days.Add(new Day(new DateTime(year, month, d)));
                    array_objects_days[i].Text_block_1 = SQLConnector.get_log_arrival_time(new DateTime(year, month, d));
                    array_objects_days[i].Text_block_2 = SQLConnector.get_log_liaving_time(new DateTime(year, month, d));
                }

                if (array_objects_days[i].Data_index > DateTime.Now.Date)
                {
                    array_objects_days[i].EnableContextMenu = true;
                }

                array_objects_days[i].day_is_now_month = true;
                if (array_objects_days[i].Data_index.Date == DateTime.Now.Date)
                {
                    array_objects_days[i].Is_now_day = true;
                }

            if (CalculationData.is_work( new DateTime(year, month, array_objects_days[i].getDay() )))
                {
                    array_objects_days[i].IsWork = true;
                }
                else
                {
                    array_objects_days[i].IsWork = false;
                }

                i++;
                d++;
            }

            d = 1;
            while (i < 42)
            {

                array_objects_days.Add(new Day(new DateTime(new_year, new_month, d) ));
                array_objects_days[i].day_is_now_month = false;
                if (array_objects_days[i].Data_index.Date == DateTime.Now.Date)
                {
                    array_objects_days[i].Is_now_day = true;
                }

                if (CalculationData.is_work(new DateTime(new_year, new_month, array_objects_days[i].getDay() )))
                {
                    array_objects_days[i].IsWork = true;
                }
                else
                {
                    array_objects_days[i].IsWork = false;
                }

                if (array_objects_days[i].Data_index > DateTime.Now)
                {
                    array_objects_days[i].EnableContextMenu = true;

                }

                i++;
                d++;
                
            }



            return array_objects_days;
        }      

    }

}
