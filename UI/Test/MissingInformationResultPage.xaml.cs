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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{
	public partial class MissingInformationResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;        

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;        
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder m_MissingInformtionTestOrder;

        public MissingInformationResultPage(YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder missingInformationTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{            
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;

			this.m_MissingInformtionTestOrder = missingInformationTestOrder;
            this.m_PageHeaderText = "Missing Information For: " + this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();            

			this.DataContext = this;				
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Test.MissingInformation.MissingInformtionTestOrder MissingInformtionTestOrder
        {
            get { return this.m_MissingInformtionTestOrder; }
        }        

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}				        

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{            
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
		}
        
		public void UpdateBindingSources()
		{
            
		}                                
        
		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{		
		    if (this.Next != null) this.Next(this, new EventArgs());		
		}

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_MissingInformtionTestOrder.Final == false)
			{				
				this.m_MissingInformtionTestOrder.Finalize(this.m_SystemIdentity.User);				
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_MissingInformtionTestOrder.Final == true)
			{
				this.m_MissingInformtionTestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}        

        private void HyperLinkFirstCall_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_MissingInformtionTestOrder.FirstCall == false)
            {
                this.m_MissingInformtionTestOrder.FirstCall = true;
                this.m_MissingInformtionTestOrder.FirstCallMadeBy = this.m_SystemIdentity.User.DisplayName;
                this.m_MissingInformtionTestOrder.TimeOfFirstCall = DateTime.Now;
            }
            else
            {
                MessageBox.Show("The first call has already been made.");
            }
        }

        private void HyperLinkSecondCall_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
