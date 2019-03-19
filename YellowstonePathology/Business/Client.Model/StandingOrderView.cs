using System;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderView
    {

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

        public bool IsCompoundRule
        {
            get { return this.m_StandingOrder.IsCompoundRule; }
        }

        public string ReflexDescription
        {
            get { return this.m_StandingOrder.ToString(); }
        }

        public string PatientAge
        {
            get { return this.m_StandingOrder.PatientAge; }
        }

        public virtual string PAPResult
        {
            get { return this.m_StandingOrder.PAPResult; }
        }

        public virtual string HPVResult
        {
            get { return this.m_StandingOrder.HPVResult; }
        }

        public string PatientAgeCompound
        {
            get { return this.m_StandingOrder.PatientAgeCompound; }
        }

        public virtual string PAPResultCompound
        {
            get { return this.m_StandingOrder.PAPResultCompound; }
        }

        public virtual string HPVResultCompound
        {
            get { return this.m_StandingOrder.HPVResultCompound; }
        }
    }
}
