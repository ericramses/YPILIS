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
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
            int startingBlockNumber = this.GetFirstBlockNumberForDate(DateTime.Today.AddDays(-1));
            for(int i=startingBlockNumber; i< latestBlock.Number; i++)
            {                
                int transactionCount = this.m_GethAPI.GetBlockTransactionCountByNumber(i);
                if(transactionCount > 0)
                {
                    Business.EthBlock block = this.m_GethAPI.GetBlockByNumber(i);
                    foreach(string transactionHash in block.TransactionHashes)
                    {
                        string contractAddress = this.m_GethAPI.GetContractAddress(transactionHash);
                        if(string.IsNullOrEmpty(contractAddress) == false)
                        {
                            int containerCount = this.m_GethAPI.GetContainerCount(contractAddress);
                        }                        
                    }
                }
            }

            //Business.GethAPI gethAPI = new Business.GethAPI();
            //Business.EthBlock latestBlock = gethAPI.GetLatestBlockNumber();
            //gethAPI.CallMethod();
            //Business.EthBlock ethBlock = gethAPI.GetBlockByNumber(512384);
            //gethAPI.GetTransactionReceipt("0x5cb44be5bc15fcb45210f4bb5b0e41adb607548c0b755d6f5cb22ebba985c2fc");
        }

        private int GetFirstBlockNumberForDate(DateTime workDate)
        {
            double averageBlockTime = this.GetAverageBlockTime();
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
            double secondsInADay = 86400;
            double numberOfBlocksInADay = secondsInADay / averageBlockTime;
            return Convert.ToInt32(latestBlock.Number - numberOfBlocksInADay);
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
