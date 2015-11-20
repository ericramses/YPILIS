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

namespace YellowstonePathology.UI.Gross
{
    public partial class DictationTemplatePage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        private string m_GrossDescription;
        
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private DictationTemplate m_DictationTemplate;

        public DictationTemplatePage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            DictationTemplateCollection dictationTemplateCollection = DictationTemplateCollection.GetAll();
            this.m_DictationTemplate = dictationTemplateCollection.GetTemplate(m_SpecimenOrder.SpecimenId);
            this.SetGrossDescription();

            InitializeComponent();

            DataContext = this;
        }

        public void SetGrossDescription()
        {
            string grossX = "Specimen " + this.m_SpecimenOrder.SpecimenNumber + " ";
            if(this.m_SpecimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Formalin)
            {
                grossX += "is received in formalin filled container labeled \"" + this.m_AccessionOrder.PatientDisplayName + " - "  + this.m_SpecimenOrder.Description + "\"";
            }
            else if(this.m_SpecimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                grossX += " is received fresh in a container labeled \"" + this.m_AccessionOrder.PatientDisplayName + " - " + this.m_SpecimenOrder.Description + "\"";
            }
            grossX += ". " + Environment.NewLine + Environment.NewLine;
            grossX += this.m_DictationTemplate.Text;

            //YellowstonePathology.Business.Common.PrintMateCarousel printMateCarousel = new Business.Common.PrintMateCarousel();
            //YellowstonePathology.Business.Common.PrintMateColumn printMateColumn = printMateCarousel.GetColumn(this.m_AccessionOrder.PrintMateColumnNumber);
            //grossX += "It is bisected and " + this.m_SpecimenOrder.GetGrossSubmittedInString(printMateColumn.ColorCode);
            this.m_GrossDescription = grossX;
        }

        public string GrossDescription
        {
            get { return this.m_GrossDescription; }
            set { this.m_GrossDescription = value; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }
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
    }
}
