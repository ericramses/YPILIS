using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class ClientOrderDetailFNAPropertyView
	{
		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
		private YellowstonePathology.YpiConnect.Contract.Order.ClientOrderDetailFNAProperty m_ClientOrderDetailFNAProperty;

		public ClientOrderDetailFNAPropertyView(YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetail, YellowstonePathology.YpiConnect.Contract.Order.ClientOrderDetailFNAProperty clientOrderDetailFNAProperty)
		{
			this.m_ClientOrderDetail = clientOrderDetail;
			this.m_ClientOrderDetailFNAProperty = clientOrderDetailFNAProperty;
		}

		public string PassDescription
		{
			get { return this.m_ClientOrderDetail.Description; }
		}
	}
}
