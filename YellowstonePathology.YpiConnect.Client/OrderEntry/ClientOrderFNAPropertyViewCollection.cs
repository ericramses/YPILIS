using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class ClientOrderDetailFNAPropertyViewCollection : ObservableCollection<ClientOrderDetailFNAPropertyView>
	{
		public ClientOrderDetailFNAPropertyViewCollection()
		{
		}

		public void Refresh(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.YpiConnect.Contract.Order.ClientOrderFNAProperty clientOrderFNAProperty)
		{
			/*this.Clear();
			foreach (YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetailFNAProperty clientOrderDetailFNAProperty in clientOrderFNAProperty.ClientOrderDetailFNAPropertyCollection)
			{
				foreach (YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
				{
					if (clientOrderDetailFNAProperty.ClientOrderDetailId == clientOrderDetail.ClientOrderDetailId)
					{
						ClientOrderDetailFNAPropertyView clientOrderDetailFNAPropertyView = new ClientOrderDetailFNAPropertyView(clientOrderDetail, clientOrderDetailFNAProperty);
						this.Add(clientOrderDetailFNAPropertyView);
						break;
					}
				}
			}*/
		}
	}
}
