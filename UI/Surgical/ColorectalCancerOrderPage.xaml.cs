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
    /// <summary>
    /// Interaction logic for ColorectalCancerOrderPage.xaml
    /// </summary>
    public partial class ColorectalCancerOrderPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        public delegate void OrderTestEventHandler(object sender, CustomEventArgs.PanelSetReturnEventArgs e);
        public event OrderTestEventHandler OrderTest;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private List<string> m_Messages;
        private System.Windows.Visibility m_BackButtonVisibility;
        private System.Windows.Visibility m_NextButtonVisibility;

        public ColorectalCancerOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            List<string> messages,
            System.Windows.Visibility backButtonVisibility,
            System.Windows.Visibility nextButtonVisibility)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_Messages = messages;
            this.m_BackButtonVisibility = backButtonVisibility;
            this.m_NextButtonVisibility = nextButtonVisibility;

            InitializeComponent();
            DataContext = this;
        }

        public void Save()
        {
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return false;
        }

        public bool OkToSaveOnClose()
        {
            return false;
        }

        public void UpdateBindingSources()
        {
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public List<string> Messages
        {
            get { return this.m_Messages; }
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public System.Windows.Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public System.Windows.Visibility CloseButtonVisibility
        {
            get
            {
                if (this.m_NextButtonVisibility == System.Windows.Visibility.Hidden)
                {
                    return System.Windows.Visibility.Visible;
                }
                return System.Windows.Visibility.Hidden;
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        private void HyperLinkLynchSyndrome_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(lynchSyndromeEvaluationTest));
            }
        }

        private void HyperLinkComprehensiveColonCancerProfile_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest comprehensiveColonCancerProfileTest = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(comprehensiveColonCancerProfileTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(comprehensiveColonCancerProfileTest));
            }
        }

        private void HyperLinkBRAFV600E_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new Business.Test.BRAFV600EK.BRAFV600EKTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(brafV600EKTest));
            }
        }

        private void HyperLinkHighRiskHPVTWI_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.HPVTWI.HPVTWITest hpvTWITest = new Business.Test.HPVTWI.HPVTWITest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(hpvTWITest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(hpvTWITest));
            }
        }

        private void HyperLinkKRASStandardMutationAnalysis_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest krasStandardTest = new Business.Test.KRASStandard.KRASStandardTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(krasStandardTest));
            }
        }

        private void HyperLinkPNH_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PNH.PNHTest pnhTest = new Business.Test.PNH.PNHTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(pnhTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(pnhTest));
            }
        }
    }
}
