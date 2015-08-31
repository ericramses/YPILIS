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

        private YellowstonePathology.UI.Gross.DictationTemplateCollection m_DictationTemplateCollection;
        private YellowstonePathology.UI.Gross.DictationTemplate m_DictationTemplate;
        private YellowstonePathology.UI.Gross.GrossDictation m_GrossDictation;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public DictationTemplatePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_DictationTemplateCollection = YellowstonePathology.UI.Gross.DictationTemplateCollection.GetAll();
            this.m_GrossDictation = new Gross.GrossDictation(accessionOrder, systemIdentity.User);

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

        public YellowstonePathology.UI.Gross.GrossDictation GrossDictation
        {
            get { return this.m_GrossDictation; }
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonCreateParagraph_Click(object sender, RoutedEventArgs e)
        {
            //this.m_DictationTemplate.BuildText();            
        }

        private void ListBoxSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListBoxSpecimenOrders.SelectedItem != null)
            {                                
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListBoxSpecimenOrders.SelectedItem;
                this.m_DictationTemplate = this.m_DictationTemplateCollection.GetTemplate(specimenOrder.SpecimenId);
                this.m_GrossDictation.InitializeSpecimen(this.m_DictationTemplate, specimenOrder);                
                this.NotifyPropertyChanged("DictationTemplate");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void AddToGross_Click(object sender, RoutedEventArgs e)
        {
            /*
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            if (surgicalTestOrder.GrossX == "???")
            {
                surgicalTestOrder.GrossX = this.m_DictationTemplate.TranscribedText;
            }
            else
            {
                surgicalTestOrder.GrossX += Environment.NewLine + Environment.NewLine + this.m_DictationTemplate.TranscribedText;
            } 
            */
        }
	}
}
