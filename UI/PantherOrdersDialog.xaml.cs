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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PantherOrdersDialog.xaml
    /// </summary>
    public partial class PantherOrdersDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherHPVOrderList;
        private YellowstonePathology.Business.Test.PantherAliquotList m_PantherAliquotList;
        private YellowstonePathology.UI.Login.LoginPageWindow m_LoginPageWindow;

        public PantherOrdersDialog()
        {
            this.m_PantherAliquotList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
            this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV();
            InitializeComponent();
            this.DataContext = this;            
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherHPVOrderList
        {
            get { return this.m_PantherHPVOrderList; }
        }

        public YellowstonePathology.Business.Test.PantherAliquotList PantherAliquotList
        {
            get { return this.m_PantherAliquotList; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            PantherAliquotReport pantherAliquotReport = new PantherAliquotReport(this.m_PantherAliquotList);
            pantherAliquotReport.Print();
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewPantherHPVOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = (YellowstonePathology.Business.Test.PantherOrderListItem)this.ListViewPantherHPVOrders.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(pantherOrderListItem.ReportNo);
                YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(accessionOrder);

                YellowstonePathology.Business.User.SystemIdentity systemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
                this.m_LoginPageWindow = new Login.LoginPageWindow(systemIdentity);
                this.m_LoginPageWindow.Show();

                YellowstonePathology.UI.Test.HPVTWIResultPath hpvResultPath = new Test.HPVTWIResultPath(pantherOrderListItem.ReportNo, accessionOrder, objectTracker, this.m_LoginPageWindow.PageNavigator, systemIdentity);
                hpvResultPath.Finish += HpvResultPath_Finish;
                hpvResultPath.Start();
            }
        }

        private void HpvResultPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ComboBoxListType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded == true)
            {
                switch (this.ComboBoxListType.SelectedIndex)
                {
                    case 0:
                        this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV();
                        break;
                    case 1:
                        this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalHPV();
                        break;
                    case 2:
                        this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalHPV();
                        break;
                }
                this.NotifyPropertyChanged("PantherHPVOrderList");
            }
        }        

        private void ButtonResendPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherHPVOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = (YellowstonePathology.Business.Test.PantherOrderListItem)this.ListViewPantherHPVOrders.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(pantherOrderListItem.ReportNo);

                if (accessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
                {
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetThinPrep();
                    if (specimenOrder.AliquotOrderCollection.HasPantherAliquot() == true)
                    {
                        YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetPantherAliquot();
                        YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pantherOrderListItem.ReportNo);
                        YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = null;

                        switch (panelSetOrder.PanelSetId)
                        {
                            case 14:
                                pantherAssay = new Business.HL7View.Panther.PantherAssayHPV();
                                break;
                            case 3:
                                pantherAssay = new Business.HL7View.Panther.PantherAssayNGCT();
                                break;
                            default:
                                throw new Exception(panelSetOrder.PanelSetName + " is mot implemented yet.");
                        }

                        YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, aliquotOrder, accessionOrder, panelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                        pantherOrder.Send();
                    }
                    else
                    {
                        MessageBox.Show("No Panther aliquot found.");
                    }
                }
                else
                {
                    MessageBox.Show("No Thin Prep Fluid Specimen Found.");
                }
            }
        }
    }
}
