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
	public partial class ComprehensiveColonCancerProfilePage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;
        private System.Windows.Visibility m_BackButtonVisibility;

		private YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile m_ComprehensiveColonCancerProfile;
		private YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult m_ComprehensiveColonCancerProfileResult;

        private string m_OrderedOnDescription;

		public ComprehensiveColonCancerProfilePage(YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile comprehensiveColonCancerProfile,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            System.Windows.Visibility backButtonVisibility)
		{
			this.m_ComprehensiveColonCancerProfile = comprehensiveColonCancerProfile;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
            this.m_BackButtonVisibility = backButtonVisibility;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(comprehensiveColonCancerProfile.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(comprehensiveColonCancerProfile.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if (aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

			this.m_ComprehensiveColonCancerProfileResult = new Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, this.m_ComprehensiveColonCancerProfile);

            this.m_PageHeaderText = "Comprehensive Colon Cancer Profile: " + this.m_AccessionOrder.PatientDisplayName + " (" + this.m_ComprehensiveColonCancerProfile.ReportNo + ")";           

			InitializeComponent();

			DataContext = this;

            Loaded += ComprehensiveColonCancerProfilePage_Loaded;
            Unloaded += ComprehensiveColonCancerProfilePage_Unloaded;
		}

        private void ComprehensiveColonCancerProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void ComprehensiveColonCancerProfilePage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}

		public YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile PanelSetOrder
		{
            get { return this.m_ComprehensiveColonCancerProfile; }
		}

		public YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult ComprehensiveColonCancerProfileResult
		{
			get { return this.m_ComprehensiveColonCancerProfileResult; }
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
            YellowstonePathology.Business.Persistence.ObjectGateway.Instance.SubmitChanges(this.m_AccessionOrder, false);
        }

        public void UpdateBindingSources()
		{

		}			

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_ComprehensiveColonCancerProfile.Final == false)
			{
                this.m_ComprehensiveColonCancerProfile.Finalize(this.m_SystemIdentity.User);
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save(false);
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWordDocument report = new Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWordDocument();
			report.Render(this.m_AccessionOrder.MasterAccessionNo, this.m_ComprehensiveColonCancerProfile.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ComprehensiveColonCancerProfile.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_ComprehensiveColonCancerProfile.Final == true)
			{
                this.m_ComprehensiveColonCancerProfile.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

        private void CheckBoxIncludeTestsPerformedOnOtherBlocks_Checked(object sender, RoutedEventArgs e)
        {
            this.m_ComprehensiveColonCancerProfileResult = new Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, this.m_ComprehensiveColonCancerProfile);
            this.NotifyPropertyChanged("ComprehensiveColonCancerProfileResult");
        }

        private void CheckBoxIncludeTestsPerformedOnOtherBlocks_UnChecked(object sender, RoutedEventArgs e)
        {
            this.m_ComprehensiveColonCancerProfileResult = new Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, this.m_ComprehensiveColonCancerProfile);
            this.NotifyPropertyChanged("ComprehensiveColonCancerProfileResult");
        }        
	}
}
