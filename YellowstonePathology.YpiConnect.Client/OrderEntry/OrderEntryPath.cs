using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class OrderEntryPath
	{
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

		public OrderEntryPath(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_ClientOrder = clientOrder;
		}

		public void Start()
		{
			this.ShowOrderTypePage();
		}

		private void ShowOrderTypePage()
		{
			OrderTypePage orderTypePage = new OrderTypePage(this.m_ClientOrder);
			orderTypePage.Return += new OrderTypePage.ReturnEventHandler(OrderTypePage_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderTypePage);
		}

		private void OrderTypePage_Return(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back:
					//Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
					Return(this, e);
					break;
				case YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next:
					//Shared.PageNavigationReturnEventArgs args1 = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
					Return(this, e);
					break;
			}
		}
	}
}
