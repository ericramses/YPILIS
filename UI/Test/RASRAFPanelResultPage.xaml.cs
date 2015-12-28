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
    /// Interaction logic for RASRAFPanelResultPage.xaml
    /// </summary>
    public partial class RASRAFPanelResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder m_RASRAFPanelTestOrder;
        private string m_OrderedOnDescription;
        
        private List<string> m_ResultList;

        public RASRAFPanelResultPage(YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder rasRAFPanelTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_RASRAFPanelTestOrder = rasRAFPanelTestOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_ObjectTracker = objectTracker;

            this.m_PageHeaderText = "RAS/RAF Panel Result For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_RASRAFPanelTestOrder.OrderedOn, this.m_RASRAFPanelTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            
            this.m_ResultList = new List<string>();
            this.m_ResultList.Add(YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelResult.NotDetected);
            this.m_ResultList.Add(YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelResult.Detected);
			this.m_ResultList.Add("");

            InitializeComponent();

            DataContext = this;
            Loaded += this.RASRAFPanelResultPage_Loaded;
        }
        
		public void RASRAFPanelResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxBRAFResult.SelectionChanged += ComboBoxBRAFResult_SelectionChanged;
            this.ComboBoxKRASResult.SelectionChanged += ComboBoxKRASResult_SelectionChanged;
            this.ComboBoxNRASResult.SelectionChanged += ComboBoxNRASResult_SelectionChanged;
            this.ComboBoxHRASResult.SelectionChanged += ComboBoxHRASResult_SelectionChanged;
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder PanelSetOrder
        {
            get { return this.m_RASRAFPanelTestOrder; }
        }
        
        public List<string> ResultList
        {
        	get { return this.m_ResultList; }
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

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelWordDocument report = new Business.Test.RASRAFPanel.RASRAFPanelWordDocument();
            report.Render(this.m_AccessionOrder.MasterAccessionNo, this.m_RASRAFPanelTestOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

            YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_RASRAFPanelTestOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This case cannot be finalized because IT needs more information to create the correct report." + Environment.NewLine + "Please contact IT.");

            /*if (this.m_RASRAFPanelTestOrder.Final == false)
            {
                this.m_RASRAFPanelTestOrder.Finalize(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show("This case cannot be finalized because it is already final.");
            }*/
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_RASRAFPanelTestOrder.Final == true)
            {
                this.m_RASRAFPanelTestOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show("This case cannot be unfinalized because it is not final.");
            }
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_RASRAFPanelTestOrder.IsOkToSet();
            if (result.Success == true)
            {
            	YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelResult rasrafPanelResult = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelResult();
                rasrafPanelResult.SetResults(this.m_RASRAFPanelTestOrder);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_RASRAFPanelTestOrder.IsOkToAccept();
            if (result.Success == true)
            {
                this.m_RASRAFPanelTestOrder.Accept(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_RASRAFPanelTestOrder.IsOkToUnaccept();
            if (result.Success == true)
            {
                this.m_RASRAFPanelTestOrder.Unaccept();
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
        
        private void ComboBoxBRAFResult_SelectionChanged(object sender, RoutedEventArgs e)
        {
        	if(this.ComboBoxBRAFResult.SelectedItem != null)
        	{
        		string result = this.ComboBoxBRAFResult.SelectedItem.ToString();
        		if(result == "Not Detected")
        		{
        			this.m_RASRAFPanelTestOrder.BRAFAlternateNucleotideMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.BRAFConsequence = "N/A";
        			this.m_RASRAFPanelTestOrder.BRAFMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.BRAFPredictedEffectOnProtein = "N/A";
        		}
        	}
        }
        
        private void ComboBoxKRASResult_SelectionChanged(object sender, RoutedEventArgs e)
        {
        	if(this.ComboBoxKRASResult.SelectedItem != null)
        	{
        		string result = this.ComboBoxKRASResult.SelectedItem.ToString();
        		if(result == "Not Detected")
        		{
        			this.m_RASRAFPanelTestOrder.KRASAlternateNucleotideMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.KRASConsequence = "N/A";
        			this.m_RASRAFPanelTestOrder.KRASMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.KRASPredictedEffectOnProtein = "N/A";
        		}
        	}
        }
        
        private void ComboBoxNRASResult_SelectionChanged(object sender, RoutedEventArgs e)
        {
        	if(this.ComboBoxNRASResult.SelectedItem != null)
        	{
        		string result = this.ComboBoxNRASResult.SelectedItem.ToString();
        		if(result == "Not Detected")
        		{
        			this.m_RASRAFPanelTestOrder.NRASAlternateNucleotideMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.NRASConsequence = "N/A";
        			this.m_RASRAFPanelTestOrder.NRASMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.NRASPredictedEffectOnProtein = "N/A";
        		}
        	}
        }
        
        private void ComboBoxHRASResult_SelectionChanged(object sender, RoutedEventArgs e)
        {
        	if(this.ComboBoxHRASResult.SelectedItem != null)
        	{
        		string result = this.ComboBoxHRASResult.SelectedItem.ToString();
        		if(result == "Not Detected")
        		{
        			this.m_RASRAFPanelTestOrder.HRASAlternateNucleotideMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.HRASConsequence = "N/A";
        			this.m_RASRAFPanelTestOrder.HRASMutationName = "N/A";
        			this.m_RASRAFPanelTestOrder.HRASPredictedEffectOnProtein = "N/A";
        		}
        	}
        }
    }
}
