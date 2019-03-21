using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ReflexOrder
    {
        protected int m_RuleNumber;
        protected string m_ReflexOrderCode;
        protected string m_Description;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        protected string m_PatientAge;
        protected string m_PAPResult;
        protected string m_HPVResult;
        protected string m_HPVTesting;
        protected string m_PatientAgeCompound;
        protected string m_PAPResultCompound;
        protected string m_HPVResultCompound;
        protected string m_HPVTestingCompound;

        public ReflexOrder()
        {
            this.m_PatientAge = HPVRuleValues.NotSet;
            this.m_PAPResult = HPVRuleValues.NotSet;
            this.m_HPVResult = HPVRuleValues.NotSet;
            this.m_HPVTesting = HPVRuleValues.NotSet;

            this.m_PatientAgeCompound = HPVRuleValues.NotSet;
            this.m_PAPResultCompound = HPVRuleValues.NotSet;
            this.m_HPVResultCompound = HPVRuleValues.NotSet;
            this.m_HPVTestingCompound = HPVRuleValues.NotSet;
        }

        public int RuleNumber
        {
            get { return this.m_RuleNumber; }
            set { this.m_RuleNumber = value; }
        }

        public string ReflexOrderCode
        {
            get { return this.m_ReflexOrderCode; }
            set { this.m_ReflexOrderCode = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
            set { this.m_PanelSet = value; }
        }

        public virtual bool IsRequired(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("Not implemented here.");
        }

        public string PatientAge
        {
            get { return this.m_PatientAge; }
        }

        public string PAPResult
        {
            get { return this.m_PAPResult; }
        }

        public string HPVTesting
        {
            get { return this.m_HPVTesting; }
        }

        public string HPVResult
        {
            get { return this.m_HPVResult; }
        }

        public string PatientAgeCompound
        {
            get { return this.m_PatientAgeCompound; }
        }

        public string PAPResultCompound
        {
            get { return this.m_PAPResultCompound; }
        }

        public string HPVResultCompound
        {
            get { return this.m_HPVResultCompound; }
        }

        public string HPVTestingCompound
        {
            get { return this.m_HPVTestingCompound; }
        }
    }
}
