using System;
using System.ComponentModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private StandingOrder m_StandingOrder;
        private string m_StandingOrderName;

        public StandingOrderView(StandingOrder standingOrder)
        {
            this.m_StandingOrder = standingOrder;
            this.m_StandingOrderName = this.m_StandingOrder.GetType().Name;
        }

        public string StandingOrderName
        {
            get { return this.m_StandingOrderName; }
        }

        public string Description
        {
            get { return this.m_StandingOrder.Description; }
        }

        public string ReflexDescription
        {
            get { return this.m_StandingOrder.ToString(); }
        }
    }
}
