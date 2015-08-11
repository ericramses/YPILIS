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
	public partial class PlacentalPathologyQuestionnairePage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail m_PlacentaClientOrderDetail;

		public PlacentalPathologyQuestionnairePage(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail placentaClientOrderDetail)
        {
            this.m_PlacentaClientOrderDetail = placentaClientOrderDetail;
            InitializeComponent();
			this.DataContext = this.m_PlacentaClientOrderDetail;
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
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
            
		}

		public void UpdateBindingSources()
		{
		}
	}
}
