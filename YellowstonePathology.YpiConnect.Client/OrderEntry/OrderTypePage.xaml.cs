using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OrderEntryWindow.xaml
    /// </summary>
	public partial class OrderTypePage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_BaseClientOrder;        
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSetCollection;

		public OrderTypePage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder baseClientOrder)
        {
			this.m_BaseClientOrder = baseClientOrder;
			this.m_ObjectTracker = new Business.Persistence.ObjectTracker();
            this.m_PanelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetYPIOrderTypes();			

			InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(OrderTypePage_Loaded);            
        }

		private void OrderTypePage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.ListBoxOrderType.Focus();
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSetCollection
		{
			get { return this.m_PanelSetCollection; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder BaseClientOrder
		{
			get { return this.m_BaseClientOrder; }
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.Validate(true) == true)
            {                
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ListBoxOrderType.SelectedItem;

                YellowstonePathology.Business.ClientOrder.Model.ClientOrder specificClientOrder = YellowstonePathology.Business.ClientOrder.Model.ClientOrderFactory.GetSpecificClientOrder(panelSet);
                specificClientOrder.Join(this.m_BaseClientOrder);                

                YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy submitterServiceProxy = new Proxy.SubmitterServiceProxy();
                submitterServiceProxy.InsertBaseClassOnly(specificClientOrder);
                this.m_ObjectTracker.RegisterObject(specificClientOrder);
                specificClientOrder.PanelSetId = panelSet.PanelSetId;

				YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
                this.m_ObjectTracker.PrepareRemoteTransferAgent(specificClientOrder, remoteObjectTransferAgent);
                submitterServiceProxy.Submit(remoteObjectTransferAgent);

				YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next, specificClientOrder);
                Return(this, args);
            }
		}        

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			this.Validate(false);
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private bool Validate(bool showMessages)
		{
			bool result = true;
			if (this.ValidateOrderType(showMessages) == false)
			{
                result = false;                
			}			
			return result;
		}

		private bool ValidateOrderType(bool showMessages)
		{
			bool result = true;
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.OrderTypeIsSelected();
			if (methodResult.Success == false)
			{
				result = false;
				if (showMessages == true)
				{
					MessageBox.Show(methodResult.Message);
				}
			}
			return result;
		}

		private YellowstonePathology.Business.Rules.MethodResult OrderTypeIsSelected()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			result.Success = true;
			if (this.ListBoxOrderType.SelectedItem == null)
			{
				result.Success = false;
				result.Message = "Please select an order type.";
			}
			return result;
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{                        
            return false;
		}

		public bool OkToSaveOnClose()
		{        
            return false;
		}

		public void Save(bool releaseLock)
		{
            
		}

		public void UpdateBindingSources()
		{
			this.Validate(false);
		}
	}
}
