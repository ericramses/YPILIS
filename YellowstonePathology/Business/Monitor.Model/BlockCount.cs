using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class BlockCount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_Date;        
        private int m_YPIBlocks;        
        private int m_BozemanBlocks;

        public BlockCount()
        {

        }                  

        public DateTime Date
        {
            get { return this.m_Date; }
            set
            {
                if (this.m_Date != value)
                {
                    this.m_Date = value;
                    this.NotifyPropertyChanged("Date");
                }
            }
        }    
        
        public string DateDisplayString
        {
            get { return Date.DayOfWeek.ToString() + "-" + Date.Month + "/" + Date.Day;  }
        }         

        public int YPIBlocks
        {
            get { return this.m_YPIBlocks; }
            set
            {
                if(this.m_YPIBlocks != value)
                {
                    this.m_YPIBlocks = value;
                    this.NotifyPropertyChanged("YPIBlocks");
                }                
            }
        }

        public int BozemanBlocks
        {
            get { return this.m_BozemanBlocks; }
            set
            {
                if (this.m_BozemanBlocks != value)
                {
                    this.m_BozemanBlocks = value;
                    this.NotifyPropertyChanged("BozemanBlocks");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
