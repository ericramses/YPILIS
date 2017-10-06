using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Monitor.Model
{
    [PersistentClass("tblDashboard", "YPIDATA")]
    public class Dashboard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        private DateTime m_DashboardDate;
        private string m_YPIBlockCount;
        private int m_YPIBlocks;
        private string m_BozemanBlockCount;
        private int m_BozemanBlocks;
        private MonitorStateEnum m_State;

        public Dashboard()
        {
            this.m_State = MonitorStateEnum.Normal;
        }

        [PersistentPrimaryKeyProperty(false)]
        public DateTime DashboardDate
        {
            get { return this.m_DashboardDate; }
            set
            {
                if (this.m_DashboardDate != value)
                {
                    this.m_DashboardDate = value;
                    this.NotifyPropertyChanged("DashboardDate");
                }
            }
        }

        [PersistentProperty()]
        public string BozemanBlockCount
        {
            get { return this.m_BozemanBlockCount; }
            set
            {
                if (this.m_BozemanBlockCount != value)
                {
                    if (value == null || value == "Unknown")
                    {
                        this.m_BozemanBlockCount = "Unknown";
                        this.m_BozemanBlocks = 0;
                    }
                    else
                    {
                        this.m_BozemanBlockCount = value;
                        this.m_BozemanBlocks = Convert.ToInt32(value);
                    }
                    this.NotifyPropertyChanged("BozemanBlockCount");
                    this.NotifyPropertyChanged("TotalBlockCount");
                }
            }
        }

        public string YPIBlockCount
        {
            get { return this.m_YPIBlockCount; }
            set
            {
                if (this.m_YPIBlockCount != value)
                {
                    this.m_YPIBlockCount = value;
                    this.NotifyPropertyChanged("YPIBlockCount");
                    this.NotifyPropertyChanged("TotalBlockCount");
                }
            }
        }

        public int YPIBlocks
        {
            get { return this.m_YPIBlocks; }
            set
            {
                this.m_YPIBlocks = value;
                this.YPIBlockCount = this.m_YPIBlocks.ToString();
            }
        }

        public int TotalBlockCount
        {
            get { return this.m_YPIBlocks + this.m_BozemanBlocks; }
        }             

        public void SetBozemanBlockCount()
        {                        
            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook._NameSpace outlookNameSpace = (Microsoft.Office.Interop.Outlook._NameSpace)outlookApp.GetNamespace("MAPI");            

            string recipientName = "blockcount@ypii.com";
            Microsoft.Office.Interop.Outlook.Recipient recipient = outlookNameSpace.CreateRecipient(recipientName);
            
            Microsoft.Office.Interop.Outlook.MAPIFolder mapiFolder = outlookNameSpace.GetSharedDefaultFolder(recipient, Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
            Microsoft.Office.Interop.Outlook._Explorer explorer = mapiFolder.GetExplorer(false);           

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\d{1,3}(?=\\D*$)");

            try
            {
                Microsoft.Office.Interop.Outlook.Items items = mapiFolder.Items;
                foreach (object item in items)
                {
                    if (item is Microsoft.Office.Interop.Outlook.MailItem)
                    {                        
                        Microsoft.Office.Interop.Outlook.MailItem mailItem = (Microsoft.Office.Interop.Outlook.MailItem)item;
                        if(mailItem.UnRead)
                        {
                            if (mailItem.SentOn.ToShortDateString() == DateTime.Today.ToShortDateString() == true)
                            {
                                System.Text.RegularExpressions.Match match = regex.Match(mailItem.Subject);
                                if (match.Captures.Count != 0)
                                {
                                    this.m_BozemanBlockCount = match.Value;
                                    this.m_BozemanBlocks = Convert.ToInt32(match.Value);
                                    this.NotifyPropertyChanged(string.Empty);

                                    mailItem.UnRead = false;
                                    mailItem.Save();                                    
                                }
                            }
                        }                        
                    }
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(item);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }            
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
