using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ChainBrowser.xaml
    /// </summary>
    public partial class ChainBrowser : Window
    {
        public ChainBrowser()
        {
            InitializeComponent();
        }

        private void Button_Go(object sender, RoutedEventArgs e)
        {
            Business.GethAPI gethAPI = new Business.GethAPI();
            //Business.EthBlock ethBlock = gethAPI.GetBlockByNumber(512384);
            gethAPI.GetTransactionReceipt("0x5cb44be5bc15fcb45210f4bb5b0e41adb607548c0b755d6f5cb22ebba985c2fc");
        }
    }
}
