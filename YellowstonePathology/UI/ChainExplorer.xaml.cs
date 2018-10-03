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
    public partial class ChainExplorer : Window
    {
        Business.GethAPI m_GethAPI;

        public ChainExplorer()
        {
            this.m_GethAPI = new Business.GethAPI();
            InitializeComponent();
        }

        private void Button_Go(object sender, RoutedEventArgs e)
        {
            this.GetAverageBlockTime();
            //Business.GethAPI gethAPI = new Business.GethAPI();
            //Business.EthBlock latestBlock = gethAPI.GetLatestBlockNumber();
            //gethAPI.CallMethod();
            //Business.EthBlock ethBlock = gethAPI.GetBlockByNumber(512384);
            //gethAPI.GetTransactionReceipt("0x5cb44be5bc15fcb45210f4bb5b0e41adb607548c0b755d6f5cb22ebba985c2fc");
        }

        private void GetFirstBlockForDate(DateTime workDate)
        {
            double blockTime = this.GetAverageBlockTime();
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
                        
        }

        private double GetAverageBlockTime()
        {            
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
            List<double> blockTimes = new List<double>();

            Business.EthBlock lastBlock = this.m_GethAPI.GetBlockByNumber(latestBlock.Number - 100);
            for (int i=latestBlock.Number - 99; i<latestBlock.Number; i++)
            {
                Business.EthBlock thisBlock = this.m_GethAPI.GetBlockByNumber(i);
                blockTimes.Add(thisBlock.TimeStamp.Subtract(lastBlock.TimeStamp).TotalSeconds);
                lastBlock = thisBlock;
            }

            return blockTimes.Average();            
        }
    }
}
