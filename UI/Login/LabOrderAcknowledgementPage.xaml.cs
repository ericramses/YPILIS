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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for LabOrderAcknowledgementPage.xaml
	/// </summary>
	public partial class LabOrderAcknowledgementPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Acknowledge Orders";
		private YellowstonePathology.Business.Domain.XElementFromSql m_OrdersToAcknowledge;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_PanelOrderIds;

		public LabOrderAcknowledgementPage(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_SystemIdentity = systemIdentity;
            this.SetOrdersToAcknowledge();

			InitializeComponent();
			
			DataContext = this;
		}

        public void SetOrdersToAcknowledge()
        {
            this.m_OrdersToAcknowledge = new YellowstonePathology.Business.Domain.XElementFromSql();
            this.m_PanelOrderIds = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPanelOrderIdsToAcknowledge();
            if (string.IsNullOrEmpty(this.m_PanelOrderIds) == false)
            {
                this.m_OrdersToAcknowledge = YellowstonePathology.Business.Gateway.XmlGateway.GetXmlOrdersToAcknowledge(this.m_PanelOrderIds);
            }
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Domain.XElementFromSql OrdersToAcknowledge
		{
            get { return this.m_OrdersToAcknowledge; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public void AcknowledgeOrder()
		{
            DateTime acknowledgementDate = DateTime.Today;
            DateTime acknowledgementTime = DateTime.Now;
            int acknowledgementId = this.m_SystemIdentity.User.UserId;

            if (this.m_PanelOrderIds.Length > 0)
            {
                List<YellowstonePathology.Business.Test.PanelOrder> panelOrders = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPanelOrdersToAcknowledge(this.m_PanelOrderIds);				
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelOrders)
                {                    
                    panelOrder.Acknowledged = true;
                    panelOrder.AcknowledgedById = acknowledgementId;
                    panelOrder.AcknowledgedDate = acknowledgementDate;
                    panelOrder.AcknowledgedTime = acknowledgementTime;
                    //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(panelOrder, true);
                }
                
                YellowstonePathology.Business.Reports.LabOrderSheet labOrderSheet = new YellowstonePathology.Business.Reports.LabOrderSheet();
                labOrderSheet.CreateReport(this.m_PanelOrderIds, acknowledgementDate, acknowledgementTime);
            }

            this.m_OrdersToAcknowledge = new YellowstonePathology.Business.Domain.XElementFromSql();
            this.m_PanelOrderIds = string.Empty;
            NotifyPropertyChanged("AcknowledgeOrders");        

			this.CloseForm();
		}        

		private void CloseForm()
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, null);
			this.Return(this, args);
		}

		private void ButtonAcknowledge_Click(object sender, RoutedEventArgs e)
		{
			this.AcknowledgeOrder();
		}

		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{
			this.CloseForm();
		}		
	}
}
