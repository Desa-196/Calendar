using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class ViewModelCalendar: INotifyPropertyChanged
    {

        private string _ButtonNowDayMonth;
        public string ButtonNowDayMonth 
        {
            get { return _ButtonNowDayMonth; }
            set 
            {
                _ButtonNowDayMonth = value;
                OnPropertyChanged("ButtonNowDayMonth");
            }
        }
        private int _ButtonNowYear;
        public int ButtonNowYear
        {
            get { return _ButtonNowYear; }
            set 
            {
                _ButtonNowYear = value;
                OnPropertyChanged("ButtonNowYear");
            }
        }

        public ViewModelCalendar() 
        {
            ButtonNowDayMonth = DateTime.Now.ToString("dd'.'MM");
            ButtonNowYear = DateTime.Now.Year;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
