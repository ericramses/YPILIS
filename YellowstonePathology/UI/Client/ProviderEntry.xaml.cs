﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace YellowstonePathology.UI.Client
{
    public partial class ProviderEntry : Window, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private YellowstonePathology.Business.Domain.Physician m_Physician;
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HpvStandingOrders;
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPV1618StandingOrderCollection;
		private YellowstonePathology.Business.View.PhysicianClientView m_PhysicianClientView;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;
		private List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> m_PhysicianClientDistributionViewList;
        private YellowstonePathology.Business.Client.Model.HPVStandingOrderCollection m_HPVStandingOrderCollection;
        private YellowstonePathology.Business.Client.Model.HPVStandingOrder m_HPVStandingOrder;

        private List<string> m_AgeList;
        private List<string> m_PAPResultList;
        private List<string> m_HPVTestingList;
        private List<string> m_EndocervicalList;

        private string m_PhysicianClientId;
        private Window m_ParentWindow;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public ProviderEntry(YellowstonePathology.Business.Domain.Physician physician)
        {
            this.m_Physician = physician;            

            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
			this.m_PhysicianClientView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientView(this.m_Physician.ObjectId);
			this.m_HpvStandingOrders = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
			this.m_HPV1618StandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPV1618StandingOrders();
			this.m_ClientCollection = new YellowstonePathology.Business.Client.Model.ClientCollection();
            this.m_HPVStandingOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetHPVStandingOrderCollectionByPhysicianId(this.m_Physician.PhysicianId);

            this.m_AgeList = new List<string>();
            this.m_AgeList.Add("No Standing Order");
            this.m_AgeList.Add("Not Set");
            this.m_AgeList.Add("Any");
            this.m_AgeList.Add("older than 30");
            this.m_AgeList.Add("25 and older");
            this.m_AgeList.Add("older than 20");
            this.m_AgeList.Add("between 21 and 29 years old");

            this.m_PAPResultList = new List<string>();
            this.m_PAPResultList.Add("No Standing Order");
            this.m_PAPResultList.Add("Not Set");
            this.m_PAPResultList.Add("Any");
            this.m_PAPResultList.Add("ASCUS");
            this.m_PAPResultList.Add("Normal or Reactive");
            this.m_PAPResultList.Add("ASCUS or LSIL");
            this.m_PAPResultList.Add("ASCUS, LSIL or HSIL");
            this.m_PAPResultList.Add("ASCUS or higher");
            this.m_PAPResultList.Add("ASCUS, AGUS, LSIL or HSIL");
            this.m_PAPResultList.Add("Abnormal");
            this.m_PAPResultList.Add("ASCUS or AGUS");

            this.m_HPVTestingList = new List<string>();
            this.m_HPVTestingList.Add("No Standing Order");
            this.m_HPVTestingList.Add("Not Set");
            this.m_HPVTestingList.Add("Any");
            this.m_HPVTestingList.Add("No HPV testing within the past year");

            this.m_EndocervicalList = new List<string>();
            this.m_EndocervicalList.Add("No Standing Order");
            this.m_EndocervicalList.Add("Not Set");
            this.m_EndocervicalList.Add("Any");
            this.m_EndocervicalList.Add("Absent");

            InitializeComponent();

            this.m_ParentWindow = Window.GetWindow(this);
            this.DataContext = this;
            Loaded += ProviderEntry_Loaded;
            Closing += ProviderEntry_Closing;
        }

        private void ProviderEntry_Loaded(object sender, RoutedEventArgs e)
        {
           if(this.ProviderClients.Count > 0)
            {
                this.ListBoxClientMembership.SelectedIndex = 0;
            }
        }

        private void ProviderEntry_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public YellowstonePathology.Business.Domain.Physician Physician
        {
            get { return this.m_Physician; }            
        }

		public YellowstonePathology.Business.Client.Model.StandingOrderCollection HpvStandingOrders
		{
			get { return this.m_HpvStandingOrders; }
		}

		public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPV1618StandingOrderCollection
		{
			get { return this.m_HPV1618StandingOrderCollection; }
		}


		public ObservableCollection<YellowstonePathology.Business.Client.Model.Client> ProviderClients
		{
			get { return this.m_PhysicianClientView.Clients; }
		}

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
		{
			get { return this.m_ClientCollection; }
		}

		public List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> PhysicianClientDistributionViewList
		{
			get { return this.m_PhysicianClientDistributionViewList; }
		}

        public YellowstonePathology.Business.Client.Model.HPVStandingOrderCollection HPVStandingOrderCollection
        {
            get { return this.m_HPVStandingOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.HPVStandingOrder HPVStandingOrder
        {
            get { return this.m_HPVStandingOrder; }
        }

        public List<string> AgeList
        {
            get { return this.m_AgeList; }
        }

        public List<string> PAPResultList
        {
            get { return this.m_PAPResultList; }
        }

        public List<string> HPVTestingList
        {
            get { return this.m_HPVTestingList; }
        }

        public List<string> EndocervicalList
        {
            get { return this.m_EndocervicalList; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            if (this.CanSave() == true)
            {
                Close();
            }
		}

        private bool CanSave()
        {
            bool result = true;

            Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection { new Business.Audit.Model.ProviderDisplayNameAudit(this.m_Physician.DisplayName),
                new YellowstonePathology.Business.Audit.Model.ProviderNpiAudit(this.m_Physician),
                new Business.Audit.Model.ProviderHomeBaseAudit(this.m_Physician),
                new Business.Audit.Model.ProviderClientsHaveDistributionSetAudit(this.m_Physician.ObjectId, this.m_PhysicianClientView) };

            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = auditCollection.Run2();
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditResult.Message + "  Do you want to continue?", "Missing Information", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }
            return result;
        }

		private void ButtonAddToClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxClientSelection.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClientSelection.SelectedItem;
				if (this.m_PhysicianClientView.ClientExists(client.ClientId) == false)
				{					
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, this.m_Physician.PhysicianId, this.m_Physician.ObjectId, client.ClientId);
					YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClient, this.m_ParentWindow);					
					this.m_PhysicianClientView.Clients.Add(client);
				}
			}
		}

		private void ButtonRemoveFromClient_Click(object sender, RoutedEventArgs e)
		{
			if(this.ListBoxClients.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_PhysicianClientView.ObjectId, client.ClientId);
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanRemoveMember(physicianClient);
                if (methodResult.Success == true)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(physicianClient, this.m_ParentWindow);                    
                    this.m_PhysicianClientView.Clients.Remove(client);
                }
                else
                {
                    MessageBox.Show(methodResult.Message, "Unable to remove membership.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
			}
		}

        private YellowstonePathology.Business.Rules.MethodResult CanRemoveMember(YellowstonePathology.Business.Domain.PhysicianClient physicianClient)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            List<Business.Client.Model.PhysicianClientDistributionView> physicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(physicianClient.PhysicianClientId);
            if(physicianClientDistributionViewList.Count > 0)
            {
                result.Success = false;
                result.Message = "This provider has distributions for this client.  These distributions must be removed before the provider can be removed from the client membership.";
            }
            return result;
        }

        private void TextBoxClientName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.TextBoxClientName.Text.Length > 0)
			{
				this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(this.TextBoxClientName.Text);
				NotifyPropertyChanged("ClientCollection");
			}
		}

		private void ButtonAddToDistribution_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxNewDistributionSelection.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxNewDistributionSelection.SelectedItem;
				if (this.ListBoxClientMembership.SelectedItems.Count != 0)
				{
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, client.ClientId);
					string physicianClientId = physicianClient.PhysicianClientId;
					foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
					{
						if (physicianClientDistributionView.PhysicianClientDistribution.DistributionID == physicianClientId)
						{
							MessageBox.Show("The item has already been added.");
							return;
						}
					}

					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(objectId, this.m_PhysicianClientId, physicianClientId);
					YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClientDistribution, this);					

					this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
					this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
				}
			}
		}

		private void ButtonRemoveFromDistribution_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxDistributionSelection.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Remove the selected item?", "Remove", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView)this.ListBoxDistributionSelection.SelectedItem;
					YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(physicianClientDistributionView.PhysicianClientDistribution, this.m_ParentWindow);					

					this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
					this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
				}
			}
		}

		private void ListBoxClientMembership_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(this.ListBoxClientMembership.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClientMembership.SelectedItem;
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, client.ClientId);
				this.m_PhysicianClientId = physicianClient.PhysicianClientId;
				this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
				this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
			}
		}

        private void TextBoxNames_KeyUp(object sender, KeyEventArgs e)
        {
            this.CreateDisplayName();
        }

        private void CreateDisplayName()
        {
            string firstName = this.TextBoxFirstName.Text;
            string middleInitial = this.TextBoxMiddleInitial.Text;
            string lastName = this.TextBoxLastName.Text;
            string credentials = this.TextBoxCredentials.Text;

            StringBuilder displayName = new StringBuilder();
            if (string.IsNullOrEmpty(firstName) == false)
            {
                displayName.Append(firstName);
            }

            if (string.IsNullOrEmpty(middleInitial) == false)
            {
                if(displayName.Length > 0)
                {
                    displayName.Append(" ");
                }
                displayName.Append(middleInitial);
                displayName.Append(".");
            }

            if (string.IsNullOrEmpty(lastName) == false)
            {
                if (displayName.Length > 0)
                {
                    displayName.Append(" ");
                }
                displayName.Append(lastName);
            }

            if(string.IsNullOrEmpty(credentials) == false)
            {
                if (displayName.Length > 0)
                {
                    displayName.Append(", ");
                }
                displayName.Append(credentials);
            }
            this.m_Physician.DisplayName = displayName.ToString();
        }

        private void ComboBoxHomeBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ComboBoxHomeBase.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.Client selectedHomeBaseClient = (YellowstonePathology.Business.Client.Model.Client)this.ComboBoxHomeBase.SelectedItem;
                this.m_Physician.HomeBaseClientId = selectedHomeBaseClient.ClientId;
            }
        }

        private void ListViewStandingOrders_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(this.ListViewStandingOrders.SelectedItem != null)
            {
                this.m_HPVStandingOrder = (YellowstonePathology.Business.Client.Model.HPVStandingOrder)this.ListViewStandingOrders.SelectedItem;
                this.NotifyPropertyChanged("HPVStandingOrder");
            }
        }

        private void ButtonAddHPVStandingOrder_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
