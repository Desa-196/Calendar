﻿using System;
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
                    Properties.Settings.Default.WorkDay = new DateTime(Convert.ToInt32(WorkDayYYYY), Convert.ToInt32(WorkDayMM), Convert.ToInt32(WorkDayDD));
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
        public String _WorkDayDD = Properties.Settings.Default.WorkDay.ToString("dd");
        public String WorkDayDD
        {
            get 
            { 
                return _WorkDayDD; 
            }
            set 
            {
                _WorkDayDD = value;
                OnPropertyChanged("WorkDayDD");
                isChange = true;
            }
        }
        public String _WorkDayMM = Properties.Settings.Default.WorkDay.ToString("MM");
        public String WorkDayMM
        {
            get 
            { 
                return _WorkDayMM; 
            }
            set 
            {
                _WorkDayMM = value;
                OnPropertyChanged("WorkDayMM");
                isChange = true;
            }
        }
        public String _WorkDayYYYY = Properties.Settings.Default.WorkDay.ToString("yyyy");
        public String WorkDayYYYY
        {
            get 
            { 
                return _WorkDayYYYY; 
            }
            set 
            {
                _WorkDayYYYY = value;
                OnPropertyChanged("WorkDayYYYY");
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
