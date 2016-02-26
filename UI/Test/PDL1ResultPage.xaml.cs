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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for PDL1ResultPage.xaml
    /// </summary>
    public partial class PDL1ResultPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PDL1.PDL1ResultCollection m_ResultCollection;
        private YellowstonePathology.Business.Test.PDL1.PDL1TestOrder m_PanelSetOrder;

        private string m_PageHeaderText;
        private string m_OrderedOnDescription;

        public PDL1ResultPage(YellowstonePathology.Business.Test.PDL1.PDL1TestOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_PanelSetOrder = testOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = this.m_PanelSetOrder.PanelSetName + " Result For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;

            this.m_ResultCollection = new YellowstonePathology.Business.Test.PDL1.PDL1ResultCollection();
            InitializeComponent();

            DataContext = this;
            
            Loaded += PDL1ResultPage_Loaded;
        }

        public void PDL1ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
        	this.ComboBoxResult.SelectionChanged += ComboBoxResult_SelectionChanged;             
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.PDL1.PDL1TestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.PDL1.PDL1ResultCollection ResultCollection
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

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PDL1.PDL1WordDocument report = new YellowstonePathology.Business.Test.PDL1.PDL1WordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();

            YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToFinalize();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Finalize();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnfinalize();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxResult.SelectedItem != null)
            {
                YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
                if (result.Success == true)
                {
                    this.m_PanelSetOrder.Accept();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
            else
            {
                MessageBox.Show("A result must be selected before it can be accepted.");
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Unaccept();
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

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxResult.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
                this.m_PanelSetOrder.ResultCode = testResult.ResultCode;
            }
        }
        
        private void TextBoxStainPercent_LostFocus(object sender, RoutedEventArgs e)
        {
        	if(string.IsNullOrEmpty(this.TextBoxStainPercent.Text) == false)
        	{        		
        		int percent = this.NumberFromText(this.TextBoxStainPercent.Text);
        		if(percent > 0)
        		{
        			if(percent > 50)
        			{
        				this.m_PanelSetOrder.Result = "Positive";
        			}
        			else
        			{
        				this.m_PanelSetOrder.Result = "Negative";
        			}
        		}        		
        	}
        }
        
        private int NumberFromText(string text)
        {
        	int result = 0;
        	string tmp = string.Empty;
        	bool added = false;
        	for(int idx = 0; idx < text.Length; idx++)
        	{
        		char c = text[idx];
        		if(Char.IsDigit(c) == true)
        		{
        			tmp += c;
        			added = true;
        		}
        		else
        		{
        			if(added == true) break;
        		}
        	}
        	
    		Int32.TryParse(tmp, out result);
    		return result;
        }
    }
}
