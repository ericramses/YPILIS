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
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for ClientSupplyOrderDialog.xaml
	/// </summary>
	public partial class ClientSupplyOrderDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Client.Model.ClientSupplyCollection m_ClientSupplyCollection;
		private YellowstonePathology.Business.Client.Model.ClientSupplyOrder m_ClientSupplyOrder;

		public ClientSupplyOrderDialog(YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder)
		{
			this.m_ClientSupplyOrder = clientSupplyOrder;

			InitializeComponent();

			this.DataContext = this;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Client.Model.ClientSupplyOrder ClientSupplyOrder
		{
			get { return this.m_ClientSupplyOrder; }
		}

		public YellowstonePathology.Business.Client.Model.ClientSupplyCollection ClientSupplyCollection
		{
			get { return this.m_ClientSupplyCollection; }
		}

		private void FillClientSupplyCollection(string clientSupplyCategory)
		{
			this.m_ClientSupplyCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyCollection(clientSupplyCategory);
			this.NotifyPropertyChanged("ClientSupplyCollection");
		}

		private void ButtonCytologySupplies_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Cytology);
		}

		private void ButtonBiopsySupplies_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Histology);
		}

		private void ButtonTransportSupplies_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Transport);
		}

		private void ButtonForms_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Forms);
		}

		private void ButtonRemoveItem_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewOrderDetails.SelectedItem != null)
			{
				this.m_ClientSupplyOrder.ClientSupplyOrderDetailCollection.Remove((YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail)this.ListViewOrderDetails.SelectedItem);
			}
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ListViewSupplies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (string.IsNullOrEmpty(this.TextBoxQuantity.Text) == false)
			{
				string quantityOrdered = this.TextBoxQuantity.Text;
				int quantity = 0;
				bool hasQuantity = Int32.TryParse(quantityOrdered, out quantity);
				if (hasQuantity == true)
				{
					if (this.ListViewSupplies.SelectedItem != null)
					{
						YellowstonePathology.Business.Client.Model.ClientSupply clientSupply = (YellowstonePathology.Business.Client.Model.ClientSupply)this.ListViewSupplies.SelectedItem;
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail = new Business.Client.Model.ClientSupplyOrderDetail(objectId,
							this.m_ClientSupplyOrder.ClientSupplyOrderId, clientSupply.clientsupplyid, clientSupply.supplyname, clientSupply.description, quantityOrdered);
						this.m_ClientSupplyOrder.ClientSupplyOrderDetailCollection.Add(clientSupplyOrderDetail);

					}
					else
					{
						MessageBox.Show("Select a supply item to add");
					}
				}
				else
				{
					MessageBox.Show("Enter a valid number");
				}
			}
			else
			{
				MessageBox.Show("Enter a quantity");
			}
		}
	}
}
