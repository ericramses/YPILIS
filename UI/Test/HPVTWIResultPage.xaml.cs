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
	public partial class HPVTWIResultPage : UserControl, INotifyPropertyChanged, Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private Microsoft.Office.Interop.Excel.Application m_ExcelApplication;
        private Microsoft.Office.Interop.Excel.Workbook m_WorkBook;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private string m_PageHeaderText;
        private YellowstonePathology.UI.Test.TecanSample m_TecanSample;

		private YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI m_PanelSetOrderHPVTWI;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

		public HPVTWIResultPage(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrderHPVTWI,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            Microsoft.Office.Interop.Excel.Application excelApplication,
            Microsoft.Office.Interop.Excel.Workbook workBook,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_PanelSetOrderHPVTWI = panelSetOrderHPVTWI;
			int idx = this.m_PanelSetOrderHPVTWI.PanelOrderCollection.Count - 1;			
			this.m_AccessionOrder = accessionOrder;
            this.m_ExcelApplication = excelApplication;
            this.m_WorkBook = workBook;
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;
			this.m_PageNavigator = pageNavigator;

			this.m_PageHeaderText = "HPV TWI Results For: " + this.m_AccessionOrder.PatientDisplayName;

            this.SetTcanWell();
			InitializeComponent();

			DataContext = this;                      
		}

        public void SetTcanWell()
        {
            if (this.m_ExcelApplication != null)
            {
                Microsoft.Office.Interop.Excel.Worksheet workSheet = this.m_WorkBook.Sheets["Summary"];
                workSheet.Select(Type.Missing);                
                
                YellowstonePathology.UI.Test.TecanSamplePlacementQueue tecanSamplePlacementQueue = new TecanSamplePlacementQueue();
                foreach (YellowstonePathology.UI.Test.TecanSample tecanSample in tecanSamplePlacementQueue.Queue)
                {
                    Microsoft.Office.Interop.Excel.Range range = workSheet.Cells[tecanSample.ResultIdCell.RowIndex, tecanSample.ResultIdCell.ColumnIndex];
                    string cellValue = Convert.ToString(range.Value);                    
                    string reportNo = YellowstonePathology.UI.Test.TecanSample.GetReportNo(cellValue);

                    if (reportNo == this.m_PanelSetOrderHPVTWI.ReportNo)
                    {
                        this.m_TecanSample = tecanSample;
                        break;
                    }                    
                }
            }
        }

        private void HyperLinkImportResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ExcelApplication != null)
            {                
                YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
                if (methodResult.Success == true)
                {
                    Microsoft.Office.Interop.Excel.Worksheet workSheet = this.m_WorkBook.Sheets["Summary"];
                    workSheet.Select(Type.Missing);
                    Microsoft.Office.Interop.Excel.Range range = workSheet.Cells[this.m_TecanSample.ResultIdCell.RowIndex, 2];
                    string sampleResult = (string)range.Value;
                    YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult hpvTWIResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection.ParseFromTcanResult(sampleResult);

                    if (hpvTWIResult != null)
                    {
                        YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
						hpvTWIResult.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
                    }
                    else
                    {
                        MessageBox.Show("The system was not able to translate the result from the spreadsheet.");
                    }
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }   
            }            
        }

		public YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI PanelSetOrder
		{
			get { return this.m_PanelSetOrderHPVTWI; }
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
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
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToFinal(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetLastAcceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection resultCollection = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection.GetAllResults();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult hpvTWIResult = resultCollection.GetResultByPreliminaryResultCode(panelOrder.ResultCode);
                hpvTWIResult.FinalizeResults(this.m_PanelSetOrderHPVTWI, this.m_SystemIdentity);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }            
		}        

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToUnFinalize(this.m_PanelSetOrderHPVTWI);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetLastAcceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection resultCollection = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection.GetAllResults();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult hpvTWIResult = resultCollection.GetResultByPreliminaryResultCode(panelOrder.ResultCode);
                hpvTWIResult.UnFinalizeResults(this.m_PanelSetOrderHPVTWI);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToUnAccept(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetLastAcceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection resultCollection = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection.GetAllResults();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult hpvTWIResult = resultCollection.GetResultByPreliminaryResultCode(panelOrder.ResultCode);
                hpvTWIResult.UnacceptResults(this.m_PanelSetOrderHPVTWI);
            }
            else
            {            
                MessageBox.Show(methodResult.Message);
            }
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToAccept(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection resultCollection = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResultCollection.GetAllResults();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult hpvTWIResult = resultCollection.GetResultByPreliminaryResultCode(panelOrder.ResultCode);                
                hpvTWIResult.AcceptResults(this.m_PanelSetOrderHPVTWI, this.m_SystemIdentity);
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

        private void HyperLinkA56Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA56PositiveResult result = new Business.Test.HPVTWI.HPVTWIA56PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkNegativeResult_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWINegativeResult result = new Business.Test.HPVTWI.HPVTWINegativeResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkA7Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA7PositiveResult result = new Business.Test.HPVTWI.HPVTWIA7PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkA9Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {                
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA9PositiveResult result = new Business.Test.HPVTWI.HPVTWIA9PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);                   
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkA567Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA567PositiveResult result = new Business.Test.HPVTWI.HPVTWIA567PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkA569Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA569PositiveResult result = new Business.Test.HPVTWI.HPVTWIA569PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkA5679Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA5679PositiveResult result = new Business.Test.HPVTWI.HPVTWIA5679PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkLowFamFOZ_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWILowFamFOZResult result = new Business.Test.HPVTWI.HPVTWILowFamFOZResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkLowgDNA_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWILowgDNAResult result = new Business.Test.HPVTWI.HPVTWILowgDNAResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkLowgDNARepeat_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWILowgDNARepeatResult result = new Business.Test.HPVTWI.HPVTWILowgDNARepeatResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }	

        private void HyperLinkHighCV_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIHighCVResult result = new Business.Test.HPVTWI.HPVTWIHighCVResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkQNS_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIQNSResult result = new Business.Test.HPVTWI.HPVTWIQNSResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkA79Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.IsOkToSetResult(this.m_PanelSetOrderHPVTWI.PanelOrderCollection);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrderHPVTWI.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.HPVTWI.HPVTWIA79PositiveResult result = new Business.Test.HPVTWI.HPVTWIA79PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPVTWI, panelOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkProvider_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_PanelSetOrderHPVTWI.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            providerDistributionPath.Back += new Login.FinalizeAccession.ProviderDistributionPath.BackEventHandler(ProviderDistributionPath_Back);
            providerDistributionPath.Next += new Login.FinalizeAccession.ProviderDistributionPath.NextEventHandler(ProviderDistributionPath_Next);
            providerDistributionPath.Start();
        }

        private void ProviderDistributionPath_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void ProviderDistributionPath_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }        	
	}
}
