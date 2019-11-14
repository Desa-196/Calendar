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

        private int _ButtonNowDay;
        public int ButtonNowDay 
        {
            get { return _ButtonNowDay; }
            set 
            {
                _ButtonNowDay = value;
                OnPropertyChanged("ButtonNowDay");
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
            ButtonNowDay = DateTime.Now.Day;
            ButtonNowYear = DateTime.Now.Year;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
