using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Windows.Threading;

namespace Calendar
{

    public class CounterTimer
    {
        public DispatcherTimer timer = new DispatcherTimer();

        public delegate void EndOfACount();
        public EndOfACount endOfACount;
        public delegate void ViewDelegateText(string text);
        public delegate void ViewDelegateOpacity(double opacity);
        public ViewDelegateText ViewTimerText;
        public ViewDelegateText ViewTimerTextInfo;
        public ViewDelegateOpacity ViewTimerTextOpacity;

        public NowDay ObjectNowDay;
        public TimeSpan TimeToEndOfACounter = new TimeSpan(0);
        public TimeSpan TimeToStartOfACounter = new TimeSpan(0);
        public TimeSpan IntervalCounter = new TimeSpan(0);

        public CounterTimer(NowDay ObjectNowDay)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += new EventHandler(OnTimedEvent);
            this.ObjectNowDay = ObjectNowDay;
        }
        public void StartCounterTimer() 
        {
            timer.Start();
        }
        private void OnTimedEvent(object sender, EventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            //Если изменилос оставшееся время в секундах, такая сложная конструкция, чтобы окрулить до секунды.
            if ((int)((TimeToEndOfACounter - DateTime.Now.TimeOfDay).TotalSeconds) != IntervalCounter.TotalSeconds)
            {
                IntervalCounter = TimeSpan.FromSeconds((int)((TimeToEndOfACounter - DateTime.Now.TimeOfDay).TotalSeconds));

                ViewTimerTextInfo("Время отметки: " + TimeToEndOfACounter.ToString("hh':'mm':'ss"));
                ViewTimerText(IntervalCounter.ToString("hh':'mm':'ss"));
                ViewTimerTextOpacity(0.6);
                     

                int progress_bar = (int)(100 - Math.Round(((IntervalCounter.TotalSeconds) / (TimeToEndOfACounter.TotalSeconds - TimeToStartOfACounter.TotalSeconds)) * 100.0));
                if (progress_bar > 100) { progress_bar = 100; }
                ObjectNowDay.Progress_bar = progress_bar;


                if (IntervalCounter <= TimeSpan.FromSeconds(0))
                {

                    ViewTimerTextInfo("");
                    ViewTimerText(IntervalCounter.ToString("hh':'mm':'ss"));
                    ViewTimerTextOpacity(1);
                    timer.Stop();
                    endOfACount();
                }

            }
        }
    }


    public class Day : INotifyPropertyChanged
    {
        private bool isWork;
        public bool IsWork
        {
            get { 
                return isWork; }
            set 
            {
                isWork = value;
                OnPropertyChanged("IsWork");
            }
        } 
        
        private bool enableContextMenu;
        public bool EnableContextMenu
        {
            get { 
                return enableContextMenu; }
            set 
            {
                enableContextMenu = value;
                OnPropertyChanged("EnableContextMenu");
            }
        }
        
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set 
            {
                isSelected = value;
                OnPropertyChanged("isSelected");
            }
        }
        protected DateTime data_index;
        private bool is_now_day = false;

        //Тип дня, 0-выходной, 1-рабочий, 2-Отпуск
        public short type_day;
        public short Type_day
        {
            get { return type_day; }
            set
            {
                type_day = value;
                OnPropertyChanged("Type_day");
            }
        }
        //День из текущего отображаемого месяца
        public bool day_is_now_month { get; set; }

        private string text_block_1;
        private string info_text_block_1;


        private string text_block_2;
        private string info_text_block_2;

        private int progress_bar;

       
        public bool Is_now_day
        {
            get { return is_now_day; }
            set
            {
                is_now_day = value;
                OnPropertyChanged("Is_now_day");
            }
        }
        public string Info_text_block_1
        {
            get { return info_text_block_1; }
            set
            {
                info_text_block_1 = value;
                OnPropertyChanged("Info_text_block_1");
            }
        }
        public string Info_text_block_2
        {
            get { return info_text_block_2; }
            set
            {
                info_text_block_2 = value;
                OnPropertyChanged("Info_text_block_2");
            }
        }
        public string Text_block_2
        {
            get { return text_block_2; }
            set
            {
                text_block_2 = value;
                OnPropertyChanged("Text_block_2");
            }
        }
        public string Text_block_1
        {
            get { return text_block_1; }
            set
            {
                text_block_1 = value;
                OnPropertyChanged("Text_block_1");
            }
        }
        public int Progress_bar
        {
            get { return progress_bar; }
            set
            {
                progress_bar = value;
                OnPropertyChanged("Progress_bar");
            }
        }
        public DateTime Data_index
        {
            get { return data_index; }
            set
            {
                data_index = value;
                OnPropertyChanged("Data_index");
            }
        }
        public int  getDay() {
            return data_index.Day;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Day( DateTime data_index)
        {
            Data_index = data_index;
            Type_day =  SQLConnector.GetTypeDay(Data_index);
            IsSelected = false;
            EnableContextMenu = false;
            IsWork = false;
        }
    }

    public class NowDay : Day
    {
        public DispatcherTimer timer;
        public TimeSpan time_to_registrations;
        public TimeSpan time_start_timer;
        public double text_block_1_opacity;
        public double text_block_2_opacity;

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
                if (DateTime.Now.TimeOfDay <= Sittings.time_arrival_start)
                {

                    CounterTimer timer_arrival = new CounterTimer(this);
                    timer_arrival.TimeToEndOfACounter =  Sittings.time_arrival_start + TimeSpan.FromSeconds(random.Next(0, (int)Sittings.time_arrival_end.TotalSeconds - (int)Sittings.time_arrival_start.TotalSeconds));
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
            else if (Registration.get_liaving_time_from_day(data_index.Date) == "") 
            {
                Text_block_1 = Text_block_1 = SQLConnector.get_log_arrival_time(data_index.Date);
                //Если сечас позже чем минимальное время отметки прихода значит отмечаем в минимальное время отметки прихода плюс случайное число секунд.
                if (DateTime.Now.TimeOfDay <= Sittings.liaving_time_start)
                {
                    CounterTimer timer_arrival = new CounterTimer(this);
                    timer_arrival.TimeToEndOfACounter = Sittings.liaving_time_start + TimeSpan.FromSeconds(random.Next(0, (int)Sittings.liaving_time_end.TotalSeconds - (int)Sittings.liaving_time_start.TotalSeconds));
                    timer_arrival.TimeToStartOfACounter = DateTime.Now.TimeOfDay;
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
                
                Registration.add_log_arrival_time(DateTime.Today, DateTime.Now.TimeOfDay);
                Text_block_2_opacity = 1;
                Text_block_2 = Registration.get_time_arrival_from_day(DateTime.Today);

                start_auto_update();

            }

    }

}


