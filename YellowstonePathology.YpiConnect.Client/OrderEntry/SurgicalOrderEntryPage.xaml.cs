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
	public partial class SurgicalOrderEntryPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        ClientOrderDetailViewCollection m_ClientOrderDetailViewCollection;		
        private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder m_ClientOrder;		
		bool m_DialogResult;                      

        private bool m_AreDemographicsEnabled;
        private bool m_AreSpecimenEnabled;
        private bool m_AreButtonsEnabled;

        private Rules.HandleClientDataEntryPropertyAccess m_HandleClientDateentryPropertyAccess;
        private YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new Business.Rules.ExecutionStatus();
        
        private bool m_ShowInactiveSpecimen;		
        private bool m_IsLoadingSpecimen;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public SurgicalOrderEntryPage(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder clientOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            this.m_ShowInactiveSpecimen = false;
            this.m_IsLoadingSpecimen = false;		
			this.m_ClientOrder = clientOrder;
			this.m_ObjectTracker = objectTracker;

            this.m_ClientOrderDetailViewCollection = new ClientOrderDetailViewCollection(this.m_ClientOrder.ClientOrderDetailCollection, this.m_ShowInactiveSpecimen);
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
			this.m_HandleClientDateentryPropertyAccess = new Rules.HandleClientDataEntryPropertyAccess(executionStatus);
			this.m_HandleClientDateentryPropertyAccess.Execute(this.m_ClientOrder, methodResult);
			this.AreDemographicsEnabled = methodResult.Success;
			this.AreSpecimenEnabled = methodResult.Success;
			this.AreButtonsEnabled = methodResult.Success;

            InitializeComponent();
            
            this.DataContext = this;            
            this.Loaded += new RoutedEventHandler(OrderEntryPage_Loaded);            
        }        

        public void SaveChangesCommandHandler(object target, ExecutedRoutedEventArgs args)
        {
            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(this.m_ClientOrder, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);
		}

        public ClientOrderDetailViewCollection ClientOrderDetailViewCollection
        {
            get { return this.m_ClientOrderDetailViewCollection; }            
        }

        public bool AreDemographicsEnabled
        {
            get { return this.m_AreDemographicsEnabled; }
            set
            {
                if (this.m_AreDemographicsEnabled != value)
                {
                    this.m_AreDemographicsEnabled = value;
                    this.NotifyPropertyChanged("AreDemographicsEnabled");
                }
            }
        }

        public bool AreSpecimenEnabled
        {
            get { return this.m_AreSpecimenEnabled; }
            set
            {
                if (this.m_AreSpecimenEnabled != value)
                {
                    this.m_AreSpecimenEnabled = value;
                    this.NotifyPropertyChanged("AreSpecimenEnabled");
                }
            }
        }

        public bool AreButtonsEnabled
        {
            get { return this.m_AreButtonsEnabled; }
            set
            {
                if (this.m_AreButtonsEnabled != value)
                {
                    this.m_AreButtonsEnabled = value;
                    this.NotifyPropertyChanged("AreButtonsEnabled");
                }
            }
        }

        public bool DialogResult
        {
            get { return this.m_DialogResult; }
            set { this.m_DialogResult = value; }
        }

        private void OrderEntryPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);			
			this.m_ClientOrderDetailViewCollection.Reload(this.m_ShowInactiveSpecimen);
		}        

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }                                     

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
        {            
            this.m_ClientOrder.NotifyPropertyChanged("");
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

        private bool CompareSavedData()
        {
            bool result = false;
            return result;
        }        

		private void HyperlinkDeleteSpecimen_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this specimen?", "Delete specimen", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK)
			{
				Hyperlink hyperlink = (Hyperlink)sender;
				ClientOrderDetailView clientOrderDetailView = (ClientOrderDetailView)hyperlink.Tag;                        
				YellowstonePathology.Business.Rules.ExecutionMessage executionMessage = new Business.Rules.ExecutionMessage();
				this.m_ClientOrder.ClientOrderDetailCollection.ClientRequestDeleteSpecimen(clientOrderDetailView.ClientOrderDetail, this.m_ClientOrder, executionMessage);
				this.Save();
				
				this.m_ClientOrderDetailViewCollection.Reload(this.m_ShowInactiveSpecimen);
				if (string.IsNullOrEmpty(executionMessage.Message) == false)
				{
					MessageBox.Show(executionMessage.Message);
				}
			}
		}

        private void HyperlinkSubmit_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new Business.Rules.ExecutionStatus();
            Rules.ClientOrderValidation clientOrderValidation = new Rules.ClientOrderValidation(executionStatus);
            clientOrderValidation.Execute(this.m_ClientOrder);

            if (executionStatus.Halted == false)
            {
                this.m_ClientOrder.Submitted = true;
                this.m_ClientOrder.ClientOrderDetailCollection.MarkAsSubmitted();                
				Save();

				YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
				Return(this, args);
            }
            else
            {
                YellowstonePathology.Business.Rules.ExecutionStatusDialog executionStatusDialog = new Business.Rules.ExecutionStatusDialog(executionStatus);
                executionStatusDialog.ShowDialog();
            }
        }

        private void HyperlinkAddSpecimen_Click(object sender, RoutedEventArgs e)
        {			             
            if (this.m_ClientOrder.ClientLocation.AllowMultipleOrderDetailTypes == true)
            {
				this.ShowOrdrDetailTypePage();
            }
            else
            {
                string orderedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;  
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrder.ClientOrderDetailCollection.GetNextItem(string.Empty, this.m_ClientOrder.ClientOrderId, 
                    this.m_ClientOrder.ClientLocation.DefaultOrderDetailTypeCode, "YPICONNECT", null, null, this.m_ClientOrder.ClientLocation.SpecimenTrackingInitiated, orderedBy, this.m_ClientOrder.CollectionDate);
                this.StartSpecimenOrderEntryPath(clientOrderDetail, ClientOrderDetailWizardModeEnum.AddNew);			
            }		    
        }

		private void ShowOrdrDetailTypePage()
		{
			OrderDetailTypePage orderDetailTypePage = new OrderDetailTypePage(this.m_ClientOrder, this.m_ObjectTracker);
			orderDetailTypePage.Return += new OrderDetailTypePage.ReturnEventHandler(OrderDetailTypePage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderDetailTypePage);
		}

		private void OrderDetailTypePage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this);
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:

					string orderedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
					YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailType clientOrderDetailType = (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailType)e.Data;
					YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrder.ClientOrderDetailCollection.GetNextItem(string.Empty, this.m_ClientOrder.ClientOrderId,
						clientOrderDetailType.Code, "YPICONNECT", null, null, this.m_ClientOrder.ClientLocation.SpecimenTrackingInitiated, orderedBy, this.m_ClientOrder.CollectionDate);
					this.StartSpecimenOrderEntryPath(clientOrderDetail, ClientOrderDetailWizardModeEnum.AddNew);			
					break;
			}
		}

		private void StartSpecimenOrderEntryPath(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail, ClientOrderDetailWizardModeEnum clientOrderDetailWizardMode)
		{
			SpecimenOrderEntryPath specimenOrderEntryPath = new SpecimenOrderEntryPath(this.m_ClientOrder, clientOrderDetail, clientOrderDetailWizardMode, this.m_ObjectTracker);
			specimenOrderEntryPath.Return += new SpecimenOrderEntryPath.ReturnEventHandler(Path_Return);
			specimenOrderEntryPath.Start();            
		}

		private void Path_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					this.Save();
					ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this);
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					this.Save();
					ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this);
					break;
			}
		}        

        protected void SelectCurrentListBoxItem(object sender, KeyboardFocusChangedEventArgs e) 
        { 
            ListBoxItem item = (ListBoxItem)sender; 
            item.IsSelected = true; 
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperlinkOwnership_Click(object sender, RoutedEventArgs e)
        {
			OwnershipPage ownershipPage = new OwnershipPage(this.m_ClientOrder, this.m_ObjectTracker);
			ownershipPage.Return += new OwnershipPage.ReturnEventHandler(OwnershipPage_Return);
			ApplicationNavigator.ApplicationContentFrame.Navigate(ownershipPage);
        }

		private void OwnershipPage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(this);
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new Business.Rules.ExecutionStatus();
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
			this.m_HandleClientDateentryPropertyAccess = new Rules.HandleClientDataEntryPropertyAccess(executionStatus);
			this.m_HandleClientDateentryPropertyAccess.Execute(this.m_ClientOrder, methodResult);
			this.AreDemographicsEnabled = methodResult.Success;
			this.AreSpecimenEnabled = methodResult.Success;
			this.AreButtonsEnabled = methodResult.Success;
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
            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(this.m_ClientOrder, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);
		}             

        private void TextBoxSpecimenDescription_Loaded(object sender, RoutedEventArgs e)
        {            
            if (this.m_IsLoadingSpecimen == true)
            {
                var textbox = sender as TextBox;
                if (textbox == null) return;
                textbox.Focus();
            }
        }

		private void ListViewSpecimen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewSpecimen.SelectedItem != null)
			{
				ClientOrderDetailView clientOrderDetailView = (ClientOrderDetailView)this.ListViewSpecimen.SelectedItem;
				YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = clientOrderDetailView.ClientOrderDetail;
                this.StartSpecimenOrderEntryPath(clientOrderDetail, ClientOrderDetailWizardModeEnum.EditExisting);
			}
		}

		public void UpdateBindingSources()
		{

		}
	}
}
