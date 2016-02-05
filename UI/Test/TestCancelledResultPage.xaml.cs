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
	public partial class TestCancelledResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder m_ReportOrderTestCancelled;

        public TestCancelledResultPage(YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

			this.m_ReportOrderTestCancelled = testCancelledTestOrder;            
            this.m_PageHeaderText = "Test Cancelled Results For: " + this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();

			DataContext = this;

            Loaded += TestCancelledResultPage_Loaded;
            Unloaded += TestCancelledResultPage_Unloaded;				
		}

        private void TestCancelledResultPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void TestCancelledResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder ReportOrder
        {
            get { return this.m_ReportOrderTestCancelled; }
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

		public void Save(bool releaseLock)
		{
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(this.m_AccessionOrder, false);
        }

        public void UpdateBindingSources()
		{

		}                

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_ReportOrderTestCancelled.Final == false)
			{
				this.m_ReportOrderTestCancelled.Finalize(this.m_SystemIdentity.User);
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}            
        }

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
			if (this.Next != null) this.Next(this, new EventArgs());            
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            this.Save(false);
			YellowstonePathology.Business.Test.TestCancelled.TestCancelledWordDocument report = new Business.Test.TestCancelled.TestCancelledWordDocument();
            report.Render(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportOrderTestCancelled.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ReportOrderTestCancelled.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ReportOrderTestCancelled.Accepted == false)
            {
                this.m_ReportOrderTestCancelled.Accept(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show("This case cannot be accepted because it is already accepted.");
            }    
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_ReportOrderTestCancelled.Final == true)
			{
				this.m_ReportOrderTestCancelled.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ReportOrderTestCancelled.Final == false)
            {
                if (this.m_ReportOrderTestCancelled.Accepted == true)
                {
                    this.m_ReportOrderTestCancelled.Unaccept();
                }
                else
                {
                    MessageBox.Show("This case cannot be unaccepted because it is not accepted.");
                }
            }
            else
            {
                MessageBox.Show("This case cannot be unaccepted because it is final.");
            }
        }        
	}
}
