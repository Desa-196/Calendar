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
        private bool IsRunTest = true;


        public MyCommand TestSMS
        {
            get
            {
                return new MyCommand((obj) =>
                {

                    LabelErrorSMS = SenderSMS.CheckLoginPassword(TelephoneNumber, Password);

                    //if (status == 1)
                    //{
                    //    LabelErrorSMS = "Логин/Пароль подтверждены";
                    //}
                    //else if (status == 2)
                    //{
                    //    LabelErrorSMS = "Неверный логин или пароль.\nНастройки SMS рассылки не будут сохранены.";
                    //}
                    //else
                    //{
                    //    LabelErrorSMS = "Ошибка подключения к серверу SMS.RU.\nНастройки SMS рассылки не будут сохранены.";
                    //}
                },
                (obj) =>
                {
                    return IsRunTest;
                });
            }
        }

        public MyCommand SaveSettings 
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    isChange = false;
                    Properties.Settings.Default.WorkDay = WorkDay;
                    Properties.Settings.Default.TimeToArrival = new TimeSpan(Convert.ToInt32(TimeToArrivalHH), Convert.ToInt32(TimeToArrivalMM), 0);
                    Properties.Settings.Default.LiavingTime = new TimeSpan(Convert.ToInt32(TimeToLiavingHH), Convert.ToInt32(TimeToLiavingMM), 0);
                    Properties.Settings.Default.MaxTimeSpread = new TimeSpan(0, Convert.ToInt32(RandomInterval), 0);
                    
                    Properties.Settings.Default.TelephoneNumber = TelephoneNumber;
                    Properties.Settings.Default.Password = PasswordSave;

                    Properties.Settings.Default.EnableSendSMS = EnableSendSMS;
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
                    TimeToArrivalHH = (Convert.ToInt32(TimeToArrivalHH)+1).ToString("00");
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
                    TimeToArrivalHH = (Convert.ToInt32(TimeToArrivalHH)-1).ToString("00");
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
                    TimeToArrivalMM = (Convert.ToInt32(TimeToArrivalMM)+1).ToString("00");
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
                    TimeToArrivalMM = (Convert.ToInt32(TimeToArrivalMM)-1).ToString("00");
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToArrivalMM) > 0; 
                });
            }
        }


        public MyCommand ChangeUpTimeToLiavingHH 
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToLiavingHH = (Convert.ToInt32(TimeToLiavingHH) +1).ToString("00");
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToLiavingHH) < 23; 
                });
            }
        }
        public MyCommand ChangeDownTimeToLiavingHH
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToLiavingHH = (Convert.ToInt32(TimeToLiavingHH) -1).ToString("00");
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToLiavingHH) > 0; 
                });
            }
        }
        public MyCommand ChangeUpTimeToLiavingMM 
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToLiavingMM = (Convert.ToInt32(TimeToLiavingMM) +1).ToString("00");
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToLiavingMM) < 59; 
                });
            }
        }
        public MyCommand ChangeDownTimeToLiavingMM
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    TimeToLiavingMM = (Convert.ToInt32(TimeToLiavingMM) -1).ToString("00");
                },
                (obj) =>
                {
                    return Convert.ToInt32(TimeToLiavingMM) > 0; 
                });
            }
        }


        public MyCommand ChangeUpMaxRandom
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    RandomInterval = (Convert.ToInt32(RandomInterval) +1).ToString();
                },
                (obj) =>
                {
                    return Convert.ToInt32(RandomInterval) < 30; 
                });
            }
        }
        public MyCommand ChangeDownMaxRandom
        {
            get 
            {
                return new MyCommand((obj) =>
                {
                    RandomInterval = (Convert.ToInt32(RandomInterval) -1).ToString();
                },
                (obj) =>
                {
                    return Convert.ToInt32(RandomInterval) > 0; 
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
        public bool _EnableSendSMS = Properties.Settings.Default.EnableSendSMS;
        public bool EnableSendSMS
        {
            get { return _EnableSendSMS; }
            set 
            {
                isChange = true;
                _EnableSendSMS = value;
                OnPropertyChanged("EnableSendSMS");
            }
        }

       
        
        public string _TelephoneNumber = Properties.Settings.Default.TelephoneNumber;
        public string TelephoneNumber
        {
            get { return _TelephoneNumber; }
            set
            {
                isChange = true;
                _TelephoneNumber = value;
                OnPropertyChanged("TelephoneNumber");
            }
        }
        private string PasswordSave;

        public string _Password = Properties.Settings.Default.Password;

        public string Password
        {
            get 
            {
                return _Password; 
            }
            set
            {
                isChange = true;
                _Password = value;
                OnPropertyChanged("Password");
            }
        }
        public int _LabelErrorSMS = 3;
        public int LabelErrorSMS
        {
            get 
            {
                return _LabelErrorSMS; 
            }
            set
            {
                _LabelErrorSMS = value;
                OnPropertyChanged("LabelErrorSMS");
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
