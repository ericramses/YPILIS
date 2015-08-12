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
	public partial class IHCQCResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.IHCQC.IHCQCTestOrder m_IHCQCTestOrder;		

        private string m_PageHeaderText;

        public IHCQCResultPage(YellowstonePathology.Business.Test.IHCQC.IHCQCTestOrder ihcTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{            
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

			this.m_ObjectTracker = objectTracker;

            this.m_IHCQCTestOrder = ihcTestOrder;            
            this.m_PageHeaderText = "IHC QC Results For: " + this.m_AccessionOrder.PatientDisplayName;			

			InitializeComponent();

			DataContext = this;				
		}

        public YellowstonePathology.Business.Test.IHCQC.IHCQCTestOrder IHCQCTestOrder
        {
            get { return this.m_IHCQCTestOrder; }
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

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult methodResult =  this.m_IHCQCTestOrder.IsOkToFinalize();
			if(methodResult.Success == true)
			{
                if (this.m_IHCQCTestOrder.ControlsReactedAppropriately == true)
                {
                    this.m_IHCQCTestOrder.Finalize(this.m_SystemIdentity.User);
                }
                else
                {
                    MessageBox.Show("You must set Controls Reacted Appropriately before you can finalize this case.");
                }
			}
            else
            {
                MessageBox.Show(methodResult.Message);
            }            
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_IHCQCTestOrder.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
				this.m_IHCQCTestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}		

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_IHCQCTestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_IHCQCTestOrder.Accept(this.m_SystemIdentity.User);
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_IHCQCTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_IHCQCTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}		
	}
}
