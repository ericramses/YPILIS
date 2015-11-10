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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for AcidWashResultDialog.xaml
    /// </summary>
    public partial class AcidWashResultDialog : Window
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.ThinPrepPap.PanelOrderAcidWash m_PanelOrderAcidWash;
        private string m_HeaderText;
        private Business.User.SystemIdentity m_SystemIdentity;
        private Business.Persistence.ObjectTracker m_ObjectTracker;

        public AcidWashResultDialog(string reportNo)
        {
            this.m_ObjectTracker = new Business.Persistence.ObjectTracker();
            this.m_AccessionOrder = Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);
            this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
            Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel thinPrepPapAcidWashPanel = new Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel();
            this.m_PanelOrderAcidWash = (Business.Test.ThinPrepPap.PanelOrderAcidWash)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo).PanelOrderCollection.GetPanelByPanelId(thinPrepPapAcidWashPanel.PanelId);

            this.m_HeaderText = "Results for " + this.m_AccessionOrder.PatientDisplayName;

            this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);

            InitializeComponent();

            DataContext = this;
        }

        public string HeaderText
        {
            get { return this.m_HeaderText; }
        }

        public Business.Test.ThinPrepPap.PanelOrderAcidWash PanelOrderAcidWash
        {
            get { return this.m_PanelOrderAcidWash; }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelOrderAcidWash.IsOkToAccept();
            if (result.Success == true)
            {
                this.m_PanelOrderAcidWash.AcceptResults(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelOrderAcidWash.UnacceptResults();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
            this.Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
        }
    }
}
