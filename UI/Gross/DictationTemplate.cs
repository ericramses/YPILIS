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

        protected string m_TemplateName;        
        protected string m_Text;
        protected string m_TranscribedText;
        protected string m_TranscribedWords;
        protected string m_PreambleTemplate;

        protected List<TemplateWord> m_WordList;
        protected YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;

        public DictationTemplate()
        {
            this.m_WordList = new List<TemplateWord>();
            this.m_SpecimenCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenCollection();

            this.m_PreambleTemplate = "This is *SYSTEMUSER* doing gross dictation, case \"*MASTERACCESSIONNO*\", received *ISARE* \"*CONTAINERCOUNT*\" *RECIEVEDIN* *CONTAINER*." + Environment.NewLine +
                              "Specimen number \"*SPECIMENNUMBER*\" is labeled \"*PATIENTNAME*\" \"*SPECIMENDESCRIPTION*\", and consists of:";
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

        public string TranscribedText
        {
            get { return this.m_TranscribedText; }
            set 
            {
                if (this.m_TranscribedText != value)
                {
                    this.m_TranscribedText = value;
                    this.NotifyPropertyChanged("TranscribedText");
                }
            }
        }

        public string TranscribedWords
        {
            get { return this.m_TranscribedWords; }
            set
            {
                if (this.m_TranscribedWords != value)
                {
                    this.m_TranscribedWords = value;
                    this.NotifyPropertyChanged("TranscribedWords");
                }
            }
        }   

        public string Preamble
        {
            get { return this.m_Preamble; }
            set
            {
                if (this.m_Preamble != value)
                {
                    this.m_Preamble = value;
                    this.NotifyPropertyChanged("Preamble");
                }
            }
        }   
        

        public string PreambleTemplate
        {
            get { return this.m_PreambleTemplate; }
            set
            {
                if (this.m_PreambleTemplate != value)
                {
                    this.m_PreambleTemplate = value;
                    this.NotifyPropertyChanged("PreambleTemplate");
                }
            }
        }   

        public void SetInitialTranscribedText(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemUser systemUser)
        {
            this.m_TranscribedText = this.m_PreambleTemplate + Environment.NewLine + this.m_Text;
            this.m_TranscribedText = this.m_TranscribedText.Replace("*SYSTEMUSER*", systemUser.DisplayName);
            this.m_TranscribedText = this.m_TranscribedText.Replace("*MASTERACCESSIONNO*", specimenOrder.MasterAccessionNo);

            if (accessionOrder.SpecimenOrderCollection.Count == 1)
            {
                this.m_TranscribedText = this.m_TranscribedText.Replace("*ISARE*", "is");
                this.m_TranscribedText = this.m_TranscribedText.Replace("*CONTAINER*", "container");
            }
            else
            {
                this.m_TranscribedText = this.m_TranscribedText.Replace("*ISARE*", "are");
                this.m_TranscribedText = this.m_TranscribedText.Replace("*CONTAINER*", "containers");
            }

            if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Formalin)
            {
                this.m_TranscribedText = this.m_TranscribedText.Replace("*RECIEVEDIN*", "formalin filled container");
            }
            else if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                this.m_TranscribedText = this.m_TranscribedText.Replace("*RECIEVEDIN*", "container fresh with formalin added at YPI");
            }

            this.m_TranscribedText = this.m_TranscribedText.Replace("*CONTAINERCOUNT*", accessionOrder.SpecimenOrderCollection.Count.ToString());
            this.m_TranscribedText = this.m_TranscribedText.Replace("*SPECIMENNUMBER*", specimenOrder.SpecimenNumber.ToString());
            this.m_TranscribedText = this.m_TranscribedText.Replace("*PATIENTNAME*", accessionOrder.PatientDisplayName);
            this.m_TranscribedText = this.m_TranscribedText.Replace("*SPECIMENDESCRIPTION*", specimenOrder.Description);
        }                   

        public void BuildText()
        {
            if (string.IsNullOrEmpty(this.m_TranscribedWords) == false)
            {                
                string [] delimeter = { "\r\n" };
                string[] lineSplit = this.m_TranscribedWords.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lineSplit.Length; i++)
                {
                    string text = lineSplit[i];
                    if (i < this.m_WordList.Count)
                    {
                        string placeHolder = this.m_WordList[i].PlaceHolder;
                        this.m_TranscribedText = this.m_TranscribedText.Replace(placeHolder, text);
                    }
                }
                this.NotifyPropertyChanged("TranscribedText");
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
