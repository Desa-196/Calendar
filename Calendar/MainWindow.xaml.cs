using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Calendar
{
   
   

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int month = DateTime.Now.Month;
        public int year = DateTime.Now.Year;
        private DateTime yesturday = DateTime.Today;
        public DispatcherTimer timer;
        public int seconds_to_leaving_time;
        public bool type_timestamp;

        public string[] array_string_month = new string[12] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        public string   text_block_1;
        public string   text_block_2;
        public int      value_progress_bar;
        public Random random = new Random();

        public TimeSpan amount_time;

        public DoubleAnimation SittingsAnimations = new DoubleAnimation();
        public bool SittingShow = true;

        public NowDay NowDay1 = CalculationData.nowDayObject;



        public MainWindow()
        {
            InitializeComponent();

            //Запускаем таймер
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Start();

            ListT.ItemsSource = CalculationData.get_array_days(year, month);
            month_year.Text = array_string_month[month - 1] + " " + year.ToString();

            if (CalculationData.is_work(DateTime.Now.Date) && SQLConnector.GetTypeDay(DateTime.Now.Date) == 0)
            {
                CalculationData.nowDayObject.start_auto_update();
            }


        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

            if (yesturday != DateTime.Today)
            {
                CalculationData.get_array_days(year, month);
                month_year.Text = array_string_month[month - 1] + " " + year.ToString();
                if (CalculationData.is_work(DateTime.Now.Date))
                {
                    CalculationData.nowDayObject.start_auto_update();
                }
               
                yesturday = DateTime.Today;

            }
  
        }

        private void Button_Click_next(object sender, RoutedEventArgs e)
        {
            month++;
            if (month > 12)
            {
                month = 1;
                year++;

            }
            CalculationData.get_array_days(year, month);
            month_year.Text = array_string_month[month - 1] + " " + year.ToString();


        }
        private void Button_Click_old(object sender, RoutedEventArgs e)
        {
            month--;
            if (month <= 0)
            {
                month = 12;
                year--;

            }
            CalculationData.get_array_days(year, month);
            month_year.Text = array_string_month[month - 1] + " " + year.ToString();
            

        }
        private void Button_Click_now_month(object sender, RoutedEventArgs e)
        {

            month   = DateTime.Now.Month;
            year    = DateTime.Now.Year;

            CalculationData.get_array_days(year, month);
            month_year.Text = array_string_month[month - 1] + " " + year.ToString();
            

        }

        private void MenuItem_Click_Otpusk(object sender, RoutedEventArgs e)
        {

            foreach (Day day in CalculationData.array_objects_days)
            {
                if (day.IsSelected) 
                {
                    SQLConnector.SetTypeDay(day.Data_index, 1);
                    day.Type_day = 1;
                }
               
            
            }

        }

        private void MenuItem_Click_Work_Day(object sender, RoutedEventArgs e)
        {
            foreach (Day day in CalculationData.array_objects_days)
            {
                if (day.IsSelected)
                {
                    SQLConnector.SetTypeDay(day.Data_index, 2);
                    day.Type_day = 2;
                }


            }
        }
        private void MenuItem_Click_Cancel(object sender, RoutedEventArgs e)
        {
            foreach (Day day in CalculationData.array_objects_days)
            {
                if (day.IsSelected)
                {
                    SQLConnector.SetTypeDay(day.Data_index, 0);
                    day.Type_day = 0;
                }


            }
        }
        private void Button_Click_Sittings(object sender, RoutedEventArgs e)
        {

            SittingsAnimations.From = SittingGrid.Width;
            SittingsAnimations.Duration = TimeSpan.FromMilliseconds(150);


            SittingsAnimations.To = SittingShow ? 400 : 20;

            SittingShow = !SittingShow;




            SittingGrid.BeginAnimation(ContentControl.WidthProperty, SittingsAnimations);
        }

    }
}
