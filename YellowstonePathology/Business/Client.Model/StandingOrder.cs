using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrder
    {
        protected string m_StandingOrderCode;
        protected string m_Description;
        protected string m_PanelSetName;
        protected ReflexOrder m_ReflexOrder;
        protected bool m_IsCompoundRule;

        public StandingOrder()
        {
            this.m_IsCompoundRule = false;
        }        

        public virtual bool IsRequired(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return this.m_ReflexOrder.IsRequired(accessionOrder);
        }


        public string StandingOrderCode
        {
            get { return this.m_StandingOrderCode; }
            set { this.m_StandingOrderCode = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public bool IsCompoundRule
        {
            get { return this.m_IsCompoundRule; }
        }

        public string PanelSetName
        {
            get { return this.m_ReflexOrder.PanelSet.PanelSetName; }
        }

        public override string ToString()
        {
            return this.m_ReflexOrder.RuleNumber.ToString() + ".) " +  this.m_Description;
        }

        public virtual string PatientAge
        {
            get { return this.m_ReflexOrder.PatientAge; }
        }

        public virtual string PatientAgeCompound
        {
            get { return this.m_ReflexOrder.PatientAgeCompound; }
        }

        public virtual string PAPResult
        {
            get { return this.m_ReflexOrder.PAPResult; }
        }

        public virtual string PAPResultCompound
        {
            get { return this.m_ReflexOrder.PAPResultCompound; }
        }

        public virtual string HPVResult
        {
            get { return this.m_ReflexOrder.HPVResult; }
        }

        public virtual string HPVResultCompound
        {
            get { return this.m_ReflexOrder.HPVResultCompound; }
        }

        public virtual string HPVTesting
        {
            get { return this.m_ReflexOrder.HPVTesting; }
        }

        public virtual string HPVTestingCompound
        {
            get { return this.m_ReflexOrder.HPVTestingCompound; }
        }
    }
}
