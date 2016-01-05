﻿using System;
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
        protected YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;
        protected int m_FontSize;

        public DictationTemplate()
        {            
            this.m_SpecimenCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenCollection();
            this.m_FontSize = 20;
        }        

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return this.m_SpecimenCollection; }
        }

        public int FontSize
        {
            get { return this.m_FontSize; }
            set
            {
                if (this.m_FontSize != value)
                {
                    this.m_FontSize = value;
                    this.NotifyPropertyChanged("FontSize");
                }
            }
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
        
        public virtual string GetDictationText(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimen, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string result = null;
            return result;
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
