using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Calendar
{


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

}


