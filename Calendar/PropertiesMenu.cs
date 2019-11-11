using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class PropertiesMenu: INotifyPropertyChanged
    {
        public String _WorkDay = Properties.Settings.Default.WorkDay.ToString("dd'.'MM'.'yyyy");
        public String WorkDay
        {
            get 
            { 
                return _WorkDay; 
            }
            set 
            {
                _WorkDay = value;
                OnPropertyChanged("WorkDay");
            }
        }
        public PropertiesMenu() 
        { 
        
        }






        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
