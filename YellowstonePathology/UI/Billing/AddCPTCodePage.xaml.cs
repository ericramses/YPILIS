using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for AddCPTCodePage.xaml
    /// </summary>
    public partial class AddCPTCodeDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        YellowstonePathology.Business.Billing.Model.FISHCPTCodeList m_FISHCPTCodeList;

        public AddCPTCodeDialog(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_FISHCPTCodeList = new Business.Billing.Model.FISHCPTCodeList();

            DataContext = this;

            InitializeComponent();
            this.Title = "CPT Codes For: " + this.m_AccessionOrder.PatientDisplayName + " - " + this.m_AccessionOrder.PBirthdate.Value.ToShortDateString();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Billing.Model.FISHCPTCodeList FISHCPTCodeList
        {
            get { return this.m_FISHCPTCodeList; }
        }
        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            bool ok = Int32.TryParse(this.TextBoxQuantity.Text, out quantity);
            if(ok == true)
            {
                if(this.ListViewCPTCodes.SelectedItem != null)
                {
                    YellowstonePathology.Business.Billing.Model.CptCode code = (YellowstonePathology.Business.Billing.Model.CptCode)this.ListViewCPTCodes.SelectedItem;
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
                    if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(code.Code, specimenOrder.SpecimenOrderId) == false)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        panelSetOrderCPTCode.Quantity = quantity;
                        panelSetOrderCPTCode.CPTCode = code.Code;
                        panelSetOrderCPTCode.Modifier = code.Modifier == null ? null: code.Modifier.Modifier;
                        panelSetOrderCPTCode.CodeType = code.CodeType.ToString();
                        panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.m_PanelSetOrder.PanelSetName;
                        panelSetOrderCPTCode.CodeableType = "Billable Test";
                        panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
                        panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
                        panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                        panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                    }
                    else
                    {
                        MessageBox.Show("Unable to add a CPT Code as the code already exists.");
                    }
                }
                else
                {
                    MessageBox.Show("Unable to add a CPT Code as a code is not selected.");
                }
            }
            else
            {
                MessageBox.Show("Unable to add a CPT Code as the quantity is not entered.");
            }
        }

        private void MenuItemDeletePanelSetOrderCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCode.SelectedItems.Count != 0)
            {
                for (int i = this.ListViewPanelSetOrderCPTCode.SelectedItems.Count - 1; i >= 0; i--)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = (YellowstonePathology.Business.Test.PanelSetOrderCPTCode)this.ListViewPanelSetOrderCPTCode.SelectedItems[i];
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Remove(panelSetOrderCPTCode);
                }
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
