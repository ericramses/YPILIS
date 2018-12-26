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
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace YellowstonePathology.Policy
{
    /// <summary>
    /// Interaction logic for PolicyExplorer.xaml
    /// </summary>
    public partial class PolicyExplorer : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        public PolicyExplorer()
        {            
            InitializeComponent();
            this.DataContext = this;
        }        

        private void ContextMenuAddFolder_Click(object sender, RoutedEventArgs e)
        {
            this.DoIt();            
        }
        
        private async void DoIt()
        {
            //string fileName = @"c:\testing\phoneprefix.txt";
            //JObject result = await IPFS.AddAsync(fileName);
            //Console.WriteLine(result["Hash"]);            

            //JObject result = await IPFS.FilesLs("/policy_chain");
            //await IPFS.FilesMkdir("/policy_chain/boat");   

            Directory directory = await Directory.Build("/policy_chain");
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
