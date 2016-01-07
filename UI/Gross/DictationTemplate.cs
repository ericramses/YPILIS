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

        public virtual string BuildResultText(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            string result = this.ReplaceIdentifier(this.m_Text, specimenOrder, accessionOrder);
            result = this.AppendInitials(result, specimenOrder, accessionOrder, systemIdentity);
            return result;            
        }

        protected string ReplaceIdentifier(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string identifier = "Specimen " + specimenOrder.SpecimenNumber + " ";
            if (specimenOrder.ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                identifier += "is received in a formalin filled container labeled \"" + accessionOrder.PatientDisplayName + " - [description]\"";
            }
            else if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                identifier += " is received fresh in a container labeled \"" + accessionOrder.PatientDisplayName + " - [description]\"";
            }
            return text.Replace("[identifier]", identifier);
        }

        protected string ReplaceCassetteLabel(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[cassettelabel]", "\"" + specimenOrder.SpecimenNumber.ToString() + "A\"");
        }

        protected string ReplaceCassetteSummary(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[cassettesummary]", "\"" + specimenOrder.SpecimenNumber.ToString() + "A\"");
        }

        protected string ReplaceRepresentativeSectionsAgeRestricted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string result = null;
            if (accessionOrder.PBirthdate.HasValue == true && YellowstonePathology.Business.Helper.PatientHelper.GetAge(accessionOrder.PBirthdate.Value) >= 13)
            {
                result = specimenOrder.GetRepresentativeSectionsSubmittedIn();
            }
            else
            {
                result = "No sections are submitted for microscopic examination";
            }
            result = text.Replace("[representativesectionsagerestricted]", result);
            return result;
        }

        protected string ReplaceRepresentativeSections(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[representativesections]", "\"" + specimenOrder.GetRepresentativeSectionsSubmittedIn());
        }

        protected string ReplaceSubmitted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string submittedStatement = "[procedure] and " + specimenOrder.GetGrossSubmittedInString();
            return text.Replace("[submitted]", submittedStatement);
        }

        protected string ReplaceTipsSubmitted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string statement = null;
            if (specimenOrder.AliquotOrderCollection.Count == 1)
            {
                statement = "Entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection[0].Label + "\"";
            }
            else
            {
                statement = "Tips submitted in cassette \"" + specimenOrder.SpecimenNumber + "A\" and remainder " + specimenOrder.GetGrossRemainderSubmittedInString();
            }
            return text.Replace("[tipssubmitted]", statement);

        }

        protected string ReplaceSummarySubmission(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[summarysubmission]", specimenOrder.GetSummarySubmissionString());
        }

        protected string AppendInitials(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = text;
            string initials = null;

            
            if (specimenOrder.AliquotOrderCollection.Count != 0)
            {
                if (accessionOrder.SpecimenOrderCollection.IsLastSpecimen(specimenOrder.SpecimenOrderId) == true)
                {
                    int grossVerifiedById = specimenOrder.AliquotOrderCollection[0].GrossVerifiedById;
                    string grossedByInitials = "[??]";

                    if (grossVerifiedById != 0)
                    {
                        YellowstonePathology.Business.User.SystemUser grossedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(grossVerifiedById);
                        grossedByInitials = grossedBy.Initials.ToUpper();
                    }

                    string supervisedByInitials = "[??]";
                    if (YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.GPathologistId.HasValue == true)
                    {
                        YellowstonePathology.Business.User.SystemUser supervisedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.GPathologistId.Value);
                        supervisedByInitials = supervisedBy.Initials.ToUpper();
                    }

                    string typedByInitials = systemIdentity.User.Initials.ToLower();

                    initials = grossedByInitials + "/" + supervisedByInitials + "/" + typedByInitials;
                    result = result + initials;
                }
            }

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
