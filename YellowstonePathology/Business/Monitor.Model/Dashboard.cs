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

        private Microsoft.Office.Interop.Outlook.Application m_OutlookApp;
        private Microsoft.Office.Interop.Outlook._NameSpace m_OutlookNameSpace;
        private Microsoft.Office.Interop.Outlook.MAPIFolder m_MAPIFolder;
        private Microsoft.Office.Interop.Outlook._Explorer m_Explorer;

        private string m_YPIBlockCount;
        private int m_YPIBlocks;
        private string m_BozemanBlockCount;
        private int m_BozemanBlocks;
        private MonitorStateEnum m_State;

        public Dashboard()
        {
            this.m_YPIBlockCount = "0";
            this.m_YPIBlocks = 0;
            this.m_BozemanBlockCount = "Unknown";
            this.m_BozemanBlocks = 0;
            this.m_State = MonitorStateEnum.Normal;
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
            set { this.m_YPIBlocks = value; }
        }

        public int TotalBlockCount
        {
            get { return this.m_YPIBlocks + this.m_BozemanBlocks; }
        }

        public MonitorStateEnum State
        {
            get { return this.m_State; }
            set
            {
                if (this.m_State != value)
                {
                    this.m_State = value;
                    this.NotifyPropertyChanged("State");
                }
            }
        }

        public void SetState()
        {
            /*TimeSpan diff = DateTime.Now - this.m_OrderTime;
            if (diff.TotalHours > 72)
            {
                this.m_State = MonitorStateEnum.Critical;
            }
            else if (diff.TotalHours > 24)
            {
                this.m_State = MonitorStateEnum.Warning;
            }
            else
            {
                this.m_State = MonitorStateEnum.Warning;
            }*/
        }

        public void SetBozemanBlockCount()
        {
            if (this.m_BozemanBlocks == 0)
            {
                this.m_OutlookApp = new Microsoft.Office.Interop.Outlook.Application();
                this.m_OutlookNameSpace = (Microsoft.Office.Interop.Outlook._NameSpace)this.m_OutlookApp.GetNamespace("MAPI");
                this.m_MAPIFolder = this.m_OutlookNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
                this.m_Explorer = this.m_MAPIFolder.GetExplorer(false);
                this.m_OutlookNameSpace.Logon(System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, true);

                Microsoft.Office.Interop.Outlook.Items items = this.m_MAPIFolder.Items;
                foreach (object item in items)
                {
                    if (item is Microsoft.Office.Interop.Outlook.MailItem)
                    {
                        Microsoft.Office.Interop.Outlook.MailItem mailItem = (Microsoft.Office.Interop.Outlook.MailItem)item;
                        if (mailItem.UnRead)
                        {
                            if (mailItem.SentOn.ToShortDateString() == DateTime.Today.ToShortDateString() && mailItem.Subject.Contains("Block Count"))
                            {
                                string subject = mailItem.Subject;
                                int idx = subject.IndexOf("Block Count") + 12;
                                string countString = subject.Substring(idx);
                                string count = string.Empty;
                                for (int x = 0; x < countString.Length; x++)
                                {
                                    if (char.IsDigit(countString[x]))
                                    {
                                        count = count + countString[x];
                                    }
                                }
                                if (string.IsNullOrEmpty(count) == false)
                                {
                                    this.BozemanBlockCount = count;
                                }
                            }
                        }
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(mailItem);
                    }
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(item);
                }
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(items);
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
