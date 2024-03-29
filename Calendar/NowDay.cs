﻿using System;

using System.Windows.Threading;

namespace Calendar
{
    public class NowDay : Day
    {
        public DispatcherTimer timer;
        public TimeSpan time_to_registrations;
        public TimeSpan time_start_timer;
        public double text_block_1_opacity;
        public double text_block_2_opacity;
        public bool _ViewProgressBar = true;
        public bool ViewProgressBar 
        {
            get { return _ViewProgressBar; }
            set 
            {
                _ViewProgressBar = value;
                OnPropertyChanged("ViewProgressBar");
            }
        }

        private Random random = new Random();

        public NowDay(DateTime data_index) : base(data_index)
        {
            Data_index = data_index;
            Text_block_1_opacity = 1;
            Text_block_2_opacity = 1;
        }
        public double Text_block_1_opacity
        {
            get { return text_block_1_opacity; }
            set
            {
                text_block_1_opacity = value;
                OnPropertyChanged("Text_Block_1_opacity");
           }
        }
        public double Text_block_2_opacity
        {
            get { return text_block_2_opacity; }
            set
            {
                text_block_2_opacity = value;
                OnPropertyChanged("Text_Block_2_opacity");
           }
        }



        public void start_auto_update()
        {

            //Если в массиве логов нет лога за текущую дату, значит ещё не отмечали приход
            if (!SQLConnector.Log_Exist(data_index.Date))
            {
                //Если сечас раньше чем минимальное время отметки прихода значит отмечаем в минимальное время отметки прихода плюс случайное число секунд.
                if (DateTime.Now.TimeOfDay <= Properties.Settings.Default.TimeToArrival - Properties.Settings.Default.MaxTimeSpread)
                {

                    CounterTimer timer_arrival = new CounterTimer(this);
                    timer_arrival.TimeToEndOfACounter = Properties.Settings.Default.TimeToArrival - TimeSpan.FromSeconds(random.Next(0, (int) Properties.Settings.Default.MaxTimeSpread.TotalSeconds));
                    timer_arrival.TimeToStartOfACounter = DateTime.Now.TimeOfDay;
                    timer_arrival.endOfACount += OnCounterArrivalTimeEnd;

                    //Передаем ламбда выражения для вывода счетчика в нужное место, в данном случае в 1-й блок
                    timer_arrival.ViewTimerText = text => this.Text_block_1 = text;
                    timer_arrival.ViewTimerTextInfo = text => this.Info_text_block_1 = text;
                    timer_arrival.ViewTimerTextOpacity = text => this.Text_block_1_opacity = text;
                    timer_arrival.StartCounterTimer();

                }
                //Если сечас позже чем минимальное время отметки прихода значит отмечаем немедленно, даём 10 сек на передумать.
                else
                {
                    CounterTimer timer_arrival = new CounterTimer(this);
                    timer_arrival.TimeToEndOfACounter = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(15);
                    timer_arrival.TimeToStartOfACounter = DateTime.Now.TimeOfDay;
                    timer_arrival.endOfACount += OnCounterArrivalTimeEnd;
                    timer_arrival.ViewTimerText = text => this.Text_block_1 = text;
                    timer_arrival.ViewTimerTextInfo = text => this.Info_text_block_1 = text;
                    timer_arrival.ViewTimerTextOpacity = text => this.Text_block_1_opacity = text;
                    timer_arrival.StartCounterTimer();
                }
            }
            //Если логи за текущую дату имеются и нет отметки об уходе
            else if (SQLConnector.get_log_liaving_time(data_index.Date) == "")
            {
                Text_block_1 = Text_block_1 = SQLConnector.get_log_arrival_time(data_index.Date);
                //Если сечас позже чем минимальное время отметки прихода значит отмечаем в минимальное время отметки прихода плюс случайное число секунд.
                if (DateTime.Now.TimeOfDay <= Properties.Settings.Default.LiavingTime)
                {
                    CounterTimer timer_arrival = new CounterTimer(this);
                    timer_arrival.TimeToEndOfACounter = Properties.Settings.Default.LiavingTime + TimeSpan.FromSeconds(random.Next(0, (int)Properties.Settings.Default.MaxTimeSpread.TotalSeconds));
                    timer_arrival.TimeToStartOfACounter = SQLConnector.TimeSpanGetArrivalTiem(DateTime.Now.Date);
                    timer_arrival.endOfACount += OnCounterLiavingTimeEnd;
                    timer_arrival.ViewTimerText = text => this.Text_block_2 = text;
                    timer_arrival.ViewTimerTextInfo = text => this.Info_text_block_2 = text;
                    timer_arrival.ViewTimerTextOpacity = text => this.Text_block_2_opacity = text;
                    timer_arrival.StartCounterTimer();
                }
                //Если сечас позже чем минимальное время отметки прихода значит отмечаем немедленно, даём 10 сек на передумать.
                else
                {
                    CounterTimer timer_arrival = new CounterTimer(this);
                    timer_arrival.TimeToEndOfACounter = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(15);
                    timer_arrival.TimeToStartOfACounter = DateTime.Now.TimeOfDay;
                    timer_arrival.endOfACount += OnCounterLiavingTimeEnd;
                    timer_arrival.ViewTimerText = text => this.Text_block_2 = text;
                    timer_arrival.ViewTimerTextInfo = text => this.Info_text_block_2 = text;
                    timer_arrival.ViewTimerTextOpacity = text => this.Text_block_2_opacity = text;
                    timer_arrival.StartCounterTimer();

                }

            }
            else 
            {
                Text_block_1 = SQLConnector.get_log_arrival_time(Data_index.Date);
                Text_block_2 = SQLConnector.get_log_liaving_time(Data_index.Date);
                ViewProgressBar = false;
            }



        }
        private void OnCounterArrivalTimeEnd() 
        {
                
            Registration.add_log_arrival_time(DateTime.Today, DateTime.Now.TimeOfDay);
            Text_block_2_opacity = 1;
            Text_block_2 = Registration.get_time_arrival_from_day(DateTime.Today);
             
            start_auto_update();

        }
        private void OnCounterLiavingTimeEnd() 
        {
                
            SQLConnector.set_log_liaving_time(DateTime.Now);
            Text_block_2_opacity = 1;
            Text_block_2 = SQLConnector.get_log_liaving_time(DateTime.Today);
            ViewProgressBar = false;

            start_auto_update();

        }

    }

}


