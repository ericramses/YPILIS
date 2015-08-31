using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplate : INotifyPropertyChanged
    {




        public event PropertyChangedEventHandler PropertyChanged;

        public const string PreambleTemplate = "This is *SYSTEMUSER* doing gross dictation, case \"*MASTERACCESSIONNO*\", received *ISARE* \"*CONTAINERCOUNT*\" *CONTAINER*.";
                              //"Specimen number \"*SPECIMENNUMBER*\" is labeled \"*PATIENTNAME*\" \"*SPECIMENDESCRIPTION*\", and consists of:";

        protected string m_TemplateName;        
        protected string m_Text;                       
        protected List<TemplateWord> m_WordList;
        protected YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;

        public DictationTemplate()
        {
            this.m_WordList = new List<TemplateWord>();
            this.m_SpecimenCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenCollection();           
        }

        public List<TemplateWord> WordList
        {
            get { return this.m_WordList; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return this.m_SpecimenCollection; }
        }

        public string TemplateName
        {
            get { return this.m_TemplateName; }
            set
            {
                if (this.m_TemplateName != value)
                {
                    this.m_TemplateName = value;
                    this.NotifyPropertyChanged("TemplateName");
                }
            }
        }

        public string Text
        {
            get { return this.m_Text; }
            set 
            {
                if (this.m_Text != value)
                {
                    this.m_Text = value;
                    this.NotifyPropertyChanged("Text");
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
