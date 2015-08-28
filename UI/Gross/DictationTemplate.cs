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

        private const string SpecimenNumberPlaceHolder = "*SPECIMENNUMBER*";
        private const string PatientNamePlaceHolder = "*PATIENTNAME*";

        protected string m_TemplateName;        
        protected string m_Text;
        protected string m_TranscribedText;
        protected string m_TranscribedWords;
        protected string m_Preamble;

        protected List<TemplateWord> m_WordList;
        protected YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;

        public DictationTemplate()
        {
            this.m_WordList = new List<TemplateWord>();
            this.m_SpecimenCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenCollection();

            this.m_Preamble = "This is *TECH* doing gross dictation, case number \"*CASENUMBER*\", received is/are \"*#*\" containers." + Environment.NewLine +
                              "Specimen number \"*#*\" is labeled \"*PATIENTNAME*\" \"*SPECIMENDESCRIPTION*\", and consists of:";
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
        

        public void SetInitialTranscribedText(int specimenNumber, string patientName)
        {
            this.m_TranscribedText = this.m_Text;
            this.m_TranscribedText = this.m_TranscribedText.Replace(SpecimenNumberPlaceHolder, specimenNumber.ToString());
            this.m_TranscribedText = this.m_TranscribedText.Replace(PatientNamePlaceHolder, patientName);
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
