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
	public partial class BoneMarrowInformationPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private BoneMarrowParameters m_BoneMarrowParameters;

		public BoneMarrowInformationPage(BoneMarrowParameters boneMarrowParameters)
        {
			this.m_BoneMarrowParameters = boneMarrowParameters;

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(BoneMarrowInformationPage_Loaded);            
        }

		private void BoneMarrowInformationPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
        }

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public BoneMarrowParameters BoneMarrowParameters
		{
			get { return this.m_BoneMarrowParameters; }
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
        {
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
        }

		private void HyperlinkNext_Click(object sender, RoutedEventArgs e)
        {
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save()
		{
		}

		public void UpdateBindingSources()
		{
		}
	}
}
