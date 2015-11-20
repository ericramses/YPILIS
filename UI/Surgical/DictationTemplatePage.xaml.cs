using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Surgical
{
    public partial class DictationTemplatePage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.UI.Gross.DictationTemplateCollection m_DictationTemplateCollection;
        private YellowstonePathology.UI.Gross.DictationTemplate m_DictationTemplate;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_GrossDescription;

        public DictationTemplatePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_SurgicalTestOrder = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            this.m_DictationTemplateCollection = YellowstonePathology.UI.Gross.DictationTemplateCollection.GetAll();            

			InitializeComponent();

			DataContext = this;
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.UI.Gross.DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }            
        }  
        
        public string GrossDescription
        {
            get { return this.m_GrossDescription; }
            set
            {
                if(this.m_GrossDescription != value)
                {
                    this.m_GrossDescription = value;
                    this.NotifyPropertyChanged("GrossDescription");
                }
            }
        }      	                        

        private void ListBoxSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListBoxSpecimenOrders.SelectedItem != null)
            {                                              
                if(string.IsNullOrEmpty(this.m_GrossDescription) == true)
                {
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListBoxSpecimenOrders.SelectedItem;
                    this.m_DictationTemplate = this.m_DictationTemplateCollection.GetTemplate(specimenOrder.SpecimenId);

                    string grossX = "Specimen " + specimenOrder.SpecimenNumber + " ";
                    if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Formalin)
                    {
                        grossX += "is received in formalin filled container labeled \"" + this.m_AccessionOrder.PatientDisplayName + " - " + specimenOrder.Description + "\"";
                    }
                    else if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
                    {
                        grossX += " is received fresh in a container labeled \"" + this.m_AccessionOrder.PatientDisplayName + " - " + specimenOrder.Description + "\"";
                    }
                    grossX += ". " + Environment.NewLine + Environment.NewLine;
                    grossX += this.m_DictationTemplate.Text;

                    //YellowstonePathology.Business.Common.PrintMateCarousel printMateCarousel = new Business.Common.PrintMateCarousel();
                    //YellowstonePathology.Business.Common.PrintMateColumn printMateColumn = printMateCarousel.GetColumn(this.m_AccessionOrder.PrintMateColumnNumber);
                    //grossX += "It is bisected and " + this.m_SpecimenOrder.GetGrossSubmittedInString(printMateColumn.ColorCode);
                    this.m_GrossDescription = grossX;
                    this.NotifyPropertyChanged(string.Empty);
                    this.TextBoxGrossDescription.Focus();
                    this.SelectNextInput();
                }                
            }
        }   
        
        private void SelectNextInput()
        {
            int startingPosition = 0;
            if(string.IsNullOrEmpty(this.TextBoxGrossDescription.SelectedText) == false)
            {
                startingPosition = this.TextBoxGrossDescription.SelectionStart;
            }

            int positionOfNextLeftBracket = this.TextBoxGrossDescription.Text.IndexOf("[", startingPosition + 1);
            if (positionOfNextLeftBracket != -1)
            {
                int positionOfNextRightBracket = this.TextBoxGrossDescription.Text.IndexOf("]", positionOfNextLeftBracket);
                this.TextBoxGrossDescription.SelectionStart = positionOfNextLeftBracket;
                this.TextBoxGrossDescription.SelectionLength = positionOfNextRightBracket - positionOfNextLeftBracket + 1;
            }            
        }

        private void SelectPreviousInput()
        {
            int startingPosition = 0;
            if (string.IsNullOrEmpty(this.TextBoxGrossDescription.SelectedText) == false)
            {
                startingPosition = this.TextBoxGrossDescription.SelectionStart;
            }

            int positionOfPreviousLeftBracket = this.TextBoxGrossDescription.Text.LastIndexOf("[", startingPosition - 1);
            if (positionOfPreviousLeftBracket != -1)
            {
                int positionOfPreviousRightBracket = this.TextBoxGrossDescription.Text.IndexOf("]", positionOfPreviousLeftBracket);
                this.TextBoxGrossDescription.SelectionStart = positionOfPreviousLeftBracket;
                this.TextBoxGrossDescription.SelectionLength = positionOfPreviousRightBracket - positionOfPreviousLeftBracket + 1;
            }
        }

        private void HyperLinkAddDicationToGross_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_SurgicalTestOrder.GrossX == "???") this.m_SurgicalTestOrder.GrossX = null;
            if(string.IsNullOrEmpty(this.m_SurgicalTestOrder.GrossX) == true)
            {
                this.m_SurgicalTestOrder.GrossX = this.m_GrossDescription;
            }
            else
            {
                this.m_SurgicalTestOrder.GrossX = this.m_SurgicalTestOrder.GrossX + Environment.NewLine + this.m_GrossDescription;
            }            
        }

        private void TextBoxGrossDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F1)
            {
                this.SelectNextInput();
            }
            else if(e.Key == Key.F2)
            {
                this.SelectPreviousInput();
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void Save()
        {
            //this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);	
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void UpdateBindingSources()
        {

        }        
    }
}
