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
    public partial class ARUPOrderPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;        

        public ARUPOrderPage()
        {            
            InitializeComponent();
            this.DataContext = this;            
        }
        
        private void TileALK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 68);
            this.Return(this, args);
        }        

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save(bool releaseLock)
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

        private void Hyperlink_GoBack(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
            this.Return(this, args);
        }

        private void TileHPV1618_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 62);
            this.Return(this, args);

        }

        private void TileMusclePathologyAnalysis_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 76);
            this.Return(this, args);
        }        
    }
}
