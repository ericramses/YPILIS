using System;
using System.ComponentModel;
using System.Linq;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_AgeDescription;

        public HPVRuleAge()
        { }

        public HPVRuleAge(string ageDescription)
        {
            this.m_AgeDescription = ageDescription;
        }

        public string AgeDescription
        {
            get { return this.m_AgeDescription; }
            set
            {
                if (this.m_AgeDescription != value)
                {
                    this.m_AgeDescription = value;
                    this.NotifyPropertyChanged("AgeDescription");
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

        public virtual bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("Not implemented in the HPVRuleAge base class");
        }
    }
}
