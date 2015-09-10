using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI.Gross
{
    public class GrossDictation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_Preamble;
        private string m_Text;
        private string m_Words;

        public GrossDictation(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.Business.User.SystemUser systemUser)
        {
            this.m_Preamble = DictationTemplate.PreambleTemplate;
            this.m_Preamble = this.m_Preamble.Replace("*SYSTEMUSER*", systemUser.DisplayName);
            this.m_Preamble = this.m_Preamble.Replace("*MASTERACCESSIONNO*", accessionOrder.MasterAccessionNo);            

            if (accessionOrder.SpecimenOrderCollection.Count == 1)
            {
                this.m_Preamble = this.m_Preamble.Replace("*ISARE*", "is");
                this.m_Preamble = this.m_Preamble.Replace("*CONTAINER*", "container");
            }
            else
            {
                this.m_Preamble = this.m_Preamble.Replace("*ISARE*", "are");
                this.m_Preamble = this.m_Preamble.Replace("*CONTAINER*", "containers");
            }

            this.m_Preamble = this.m_Preamble.Replace("*CONTAINERCOUNT*", accessionOrder.SpecimenOrderCollection.Count.ToString());                       
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

        public string Words
        {
            get { return this.m_Words; }
            set
            {
                if (this.m_Words != value)
                {
                    this.m_Words = value;
                    this.NotifyPropertyChanged("Words");
                }
            }
        }        

        public void InitializeSpecimen(DictationTemplate dictationTemplate, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            
        }

        public void BuildWordList(DictationTemplate dictationTemplate)
        {
            if (string.IsNullOrEmpty(this.m_Words) == false)
            {
                string[] delimeter = { "\r\n" };
                string[] lineSplit = this.m_Words.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lineSplit.Length; i++)
                {
                    string text = lineSplit[i];
                    if (i < dictationTemplate.WordList.Count)
                    {
                        string placeHolder = dictationTemplate.WordList[i].PlaceHolder;
                        this.m_Text = this.m_Text.Replace(placeHolder, text);
                    }
                }
                this.NotifyPropertyChanged("Text");
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
