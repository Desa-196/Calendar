using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Calendar
{
    public class ViewModelCalendar : INotifyPropertyChanged
    {
        public DispatcherTimer dispatcherTimer;

        private string _ButtonNowTime;
        public string ButtonNowTime
        {
            get { return _ButtonNowTime; }
            set
            {
                _ButtonNowTime = value;
                OnPropertyChanged("ButtonNowTime");
            }
        }

        private string _ButtonNowDate;
        public string ButtonNowDate
        {
            get { return _ButtonNowDate; }
            set
            {
                _ButtonNowDate = value;
                OnPropertyChanged("ButtonNowDate");
            }
        }

        public ViewModelCalendar()
        {
            ButtonNowDate = DateTime.Now.ToString("dd'.'MM'.'yyyy");

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(delegate (object sender, EventArgs e)
            {
                ButtonNowTime = DateTime.Now.ToString("HH':'mm");
                ButtonNowDate = DateTime.Now.ToString("dd'.'MM'.'yyyy");
            });

            dispatcherTimer.Start();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
