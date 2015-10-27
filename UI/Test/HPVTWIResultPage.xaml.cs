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
	/// <summary>
	/// Interaction logic for HPVTWIResultPage.xaml
	/// </summary>
	public partial class HPVTWIResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;                
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private string m_PageHeaderText;
        private YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection m_ResultCollection;
        private YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI m_PanelSetOrderHPVTWI;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private string m_AdditionalTestingComment;

        public HPVTWIResultPage(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrderHPVTWI,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_PanelSetOrderHPVTWI = panelSetOrderHPVTWI;			
			this.m_AccessionOrder = accessionOrder;                        
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;
			this.m_PageNavigator = pageNavigator;

            this.m_ResultCollection = Business.Test.HPVTWI.HPVTWIResultCollection.GetAllResults();
			this.m_PageHeaderText = "HPV TWI Results For: " + this.m_AccessionOrder.PatientDisplayName + "  (" + this.m_PanelSetOrderHPVTWI.ReportNo + ")";

            bool hpv1618HasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(62);            
            if (hpv1618HasBeenOrdered == true)
            {
                this.m_AdditionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPV1618HasBeenOrderedComment;
            }
            else
            {
                this.m_AdditionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            InitializeComponent();

			DataContext = this;
            Loaded += HPVTWIResultPage_Loaded;
            Unloaded += HPVTWIResultPage_Unloaded;
		}

        private void HPVTWIResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged += ComboBoxResult_SelectionChanged;
        }

        private void HPVTWIResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged -= ComboBoxResult_SelectionChanged;
        }

        public string AdditionalTestingComment
        {
            get { return this.m_AdditionalTestingComment; }
        }           

		public YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI PanelSetOrder
		{
			get { return this.m_PanelSetOrderHPVTWI; }
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection ResultCollection
        {
            get { return this.m_ResultCollection; }
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
            this.Next(this, new EventArgs());
        }		

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrderHPVTWI.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_PanelSetOrderHPVTWI.Finalize(this.m_SystemIdentity.User);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }            
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderHPVTWI.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrderHPVTWI.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderHPVTWI.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrderHPVTWI.Unaccept();
            }
            else
            {            
                MessageBox.Show(methodResult.Message);
            }
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderHPVTWI.IsOkToAccept();
            if (methodResult.Success == true)
            {                                
                this.m_PanelSetOrderHPVTWI.Accept(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
			YellowstonePathology.Business.Test.HPVTWI.HPVTWIWordDocument report = new Business.Test.HPVTWI.HPVTWIWordDocument();
			report.Render(this.m_PanelSetOrderHPVTWI.MasterAccessionNo, this.m_PanelSetOrderHPVTWI.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrderHPVTWI.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }        

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxResult.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
                this.m_PanelSetOrderHPVTWI.ResultCode = testResult.ResultCode;
            }
        }
    }
}
