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

namespace YellowstonePathology.UI.Surgical
{	
	public partial class DictationTemplatePage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
	{
        private YellowstonePathology.UI.Gross.DictationTemplate m_DictationTemplate;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public DictationTemplatePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_AccessionOrder = accessionOrder;
            //DictationTemplateCollection dictationTemplateCollection = DictationTemplateCollection.GetAll();
            //this.m_DictationTemplate = dictationTemplateCollection.GetTemplate(specimenId);
            
			InitializeComponent();

			DataContext = this;
		}

        public YellowstonePathology.UI.Gross.DictationTemplate DictationTemplate
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
