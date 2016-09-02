using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskOrderDetailFedexShipment : TaskOrderDetail
    {
        private string m_TrackingNumber;
        private string m_ZPLII;

        public TaskOrderDetailFedexShipment()
        {
            
        }

        [PersistentProperty()]
        public string TrackingNumber
        {
            get { return this.m_TrackingNumber; }
            set
            {
                if (this.m_TrackingNumber != value)
                {
                    this.m_TrackingNumber = value;
                    this.NotifyPropertyChanged("TrackingNumber");
                }
            }
        }

        [PersistentProperty()]
        public string ZPLII
        {
            get { return this.m_ZPLII; }
            set
            {
                if (this.m_ZPLII != value)
                {
                    this.m_ZPLII = value;
                    this.NotifyPropertyChanged("ZPLII");
                }
            }
        }
    }
}
