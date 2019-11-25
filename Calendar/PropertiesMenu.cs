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

        public bool isChange = false;

        public MyCommand SaveSettings 
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    isChange = false;
                    Properties.Settings.Default.WorkDay = WorkDay;
                    Properties.Settings.Default.Save();
                    CalculationData.get_array_days(DateTime.Now.Year, DateTime.Now.Month);
                    CalculationData.nowDayObject.NewDayRegistration();
                },
                (obj) =>
                {
                    return isChange; 
                });
            }
        }
        public MyCommand ChangeUpTimeToArrivalHH 
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToArrivalHH = (Convert.ToInt32(TimeToArrivalHH)+1).ToString();
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToArrivalHH) < 23; 
                });
            }
        }
        public MyCommand ChangeDownTimeToArrivalHH
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToArrivalHH = (Convert.ToInt32(TimeToArrivalHH)-1).ToString();
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToArrivalHH) > 0; 
                });
            }
        }
        public MyCommand ChangeUpTimeToArrivalMM 
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToArrivalMM = (Convert.ToInt32(TimeToArrivalMM)+1).ToString();
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToArrivalMM) < 59; 
                });
            }
        }
        public MyCommand ChangeDownTimeToArrivalMM
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToArrivalMM = (Convert.ToInt32(TimeToArrivalMM)-1).ToString();
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToArrivalMM) > 0; 
                });
            }
        }


        public String _RandomInterval = Properties.Settings.Default.MaxTimeSpread.Minutes.ToString();
        public String RandomInterval
        {
            get 
            { 
                return _RandomInterval; 
            }
            set 
            {
                _RandomInterval = value;
                OnPropertyChanged("RandomInterval");
                isChange = true;
            }
        }
        public DateTime _WorkDay = Properties.Settings.Default.WorkDay;
        public DateTime WorkDay
        {
            get 
            { 
                return _WorkDay; 
            }
            set 
            {
                _WorkDay = value;
                OnPropertyChanged("WorkDay");
                isChange = true;
            }
        }
      
        public String _TimeToArrivalHH = Properties.Settings.Default.TimeToArrival.ToString("hh");
        public String TimeToArrivalHH
        {
            get
            {
                return _TimeToArrivalHH;
            }
            set
            {
                int result;
                //Если передано число
                if (int.TryParse(value, out result) || value == "")
                {
                    if (value != "")
                    {
                        if (result >= 0)
                        {
                            //Проверяем, меньше ли оно 23
                            if (Convert.ToInt32(value) <= 23)
                            {
                                _TimeToArrivalHH = value;
                                OnPropertyChanged("TimeToArrivalHH");
                                isChange = true;
                            }
                        }
                    }
                    else 
                    {
                        _TimeToArrivalHH = value;
                        OnPropertyChanged("TimeToArrivalHH");
                        isChange = true;
                    }

                }
            }
        }
        public String _TimeToArrivalMM = Properties.Settings.Default.TimeToArrival.ToString("mm");
        public String TimeToArrivalMM
        {
            get
            {
                return _TimeToArrivalMM;
            }
            set
            {
                int result;
                //Если передано число
                if (int.TryParse(value, out result) || value == "")
                {
                    if (value != "")
                    {
                        if (result >= 0)
                        {
                            //Проверяем, меньше ли оно 23
                            if (Convert.ToInt32(value) <= 59)
                            {
                                _TimeToArrivalMM = value;
                                OnPropertyChanged("TimeToArrivalMM");
                                isChange = true;
                            }
                        }
                    }
                    else
                    {
                        _TimeToArrivalMM = value;
                        OnPropertyChanged("TimeToArrivalMM");
                        isChange = true;
                    }

                }
            }
        }
        public String _TimeToLiavingHH = Properties.Settings.Default.LiavingTime.ToString("hh");
        public String TimeToLiavingHH
        {
            get
            {
                return _TimeToLiavingHH;
            }
            set
            {
                int result;
                //Если передано число
                if (int.TryParse(value, out result) || value == "")
                {
                    if (value != "")
                    {
                        if (result >= 0)
                        {
                            //Проверяем, меньше ли оно 23
                            if (Convert.ToInt32(value) <= 23)
                            {
                                _TimeToLiavingHH = value;
                                OnPropertyChanged("TimeToLiavingHH");
                                isChange = true;
                            }
                        }
                    }
                    else 
                    {
                        _TimeToLiavingHH = value;
                        OnPropertyChanged("TimeToLiavingHH");
                        isChange = true;
                    }

                }
            }
        }
        public String _TimeToLiavingMM = Properties.Settings.Default.LiavingTime.ToString("mm");
        public String TimeToLiavingMM
        {
            get
            {
                return _TimeToLiavingMM;
            }
            set
            {
                int result;
                //Если передано число
                if (int.TryParse(value, out result) || value == "")
                {
                    if (value != "")
                    {
                        if (result >= 0)
                        {
                            //Проверяем, меньше ли оно 23
                            if (Convert.ToInt32(value) <= 59)
                            {
                                _TimeToLiavingMM = value;
                                OnPropertyChanged("TimeToLiavingMM");
                                isChange = true;
                            }
                        }
                    }
                    else
                    {
                        _TimeToLiavingMM = value;
                        OnPropertyChanged("TimeToLiavingMM");
                        isChange = true;
                    }

                }
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
