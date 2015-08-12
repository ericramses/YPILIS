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

namespace YellowstonePathology.UI.Login.ReceiveSpecimen
{
    /// <summary>
    /// Interaction logic for YpiMolecularOrderPage.xaml
    /// </summary>
    public partial class YpiMolecularOrderPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;        

        public YpiMolecularOrderPage()
        {         
            InitializeComponent();
            this.DataContext = this;
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void TileEGFR_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 60);
            this.Return(this, args);
        }

        private void TileTrichomonas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 61);
            this.Return(this, args);
        }

        private void TileJAKII_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 1);
            this.Return(this, args);
        }

        private void TileProthrombin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 33);
            this.Return(this, args);      
        }

        private void TileFactorIV_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 32);
            this.Return(this, args);
        }

		private void TileCysticFibrosis_MouseUp(object sender, MouseButtonEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 2);
			this.Return(this, args);
		}

		private void TileKrasSta_MouseUp(object sender, MouseButtonEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 27);
			this.Return(this, args);
		}

		private void TileKrasBrafReflex_MouseUp(object sender, MouseButtonEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 30);
			this.Return(this, args);
		}

		private void TileBraf_MouseUp(object sender, MouseButtonEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 18);
			this.Return(this, args);
		}

		private void TileHPV16_MouseUp(object sender, MouseButtonEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 25);
			this.Return(this, args);
		}

		private void TileHer2ByISH_MouseUp(object sender, MouseButtonEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 46);
			this.Return(this, args);
		}
    }
}
