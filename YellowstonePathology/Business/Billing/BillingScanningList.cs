using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Billing
{
    public class BillingScanningList : List<BillingScanningListItem>
    {
        public BillingScanningList()
        {
            this.Add(new BillingScanningListItem("EOBBCBS"));
            this.Add(new BillingScanningListItem("EOBCOMM"));
            this.Add(new BillingScanningListItem("EOBMCD"));
            this.Add(new BillingScanningListItem("EOBMCR"));
            this.Add(new BillingScanningListItem("EOBRRMCR"));
            this.Add(new BillingScanningListItem("EOBWYMCD"));
            this.Add(new BillingScanningListItem("DailyPostings"));
        }

        /*public void MoveFiles(DateTime scanDate)
        {
            int count = 0;
            System.Random random = new Random();                        
            foreach (BillingScanningListItem item in this)
            {
                if (item.FileCount != 0)
                {
                    string moveFolderPath = @"\\CFileServer\documents\Billing\" + scanDate.Year.ToString() + "\\" + scanDate.ToString("MMMM") + "\\" + item.ScanFolderName + "\\";
                    if (Directory.Exists(moveFolderPath) == false)
                    {
                        Directory.CreateDirectory(moveFolderPath);
                    }                    

                    string[] files = Directory.GetFiles(item.ScanFullPath);
                    string datePart = scanDate.ToString("MM") + scanDate.ToString("dd") + scanDate.ToString("yyyy");
                    int fileCount = GetNextFileNumber(moveFolderPath, datePart);                    

                    foreach (string file in files)
                    {                        
                        double rNumber = random.NextDouble();
                        string sNumber = rNumber.ToString().Substring(2, 6);                                                
                        string destinationFile = moveFolderPath + datePart + "R" + fileCount.ToString().PadLeft(4, '0') + ".tif";                        
                        File.Move(file, destinationFile);
                        fileCount += 1;
                        count += 1;
                    }
                }
            }
            System.Windows.MessageBox.Show(count.ToString() + " files where moved.");
        }*/

        public int GetNextFileNumber(string path, string datePart)
        {            
            int count = 0;
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string[] slashSplit = file.Split('\\');
                string [] rSplit = slashSplit[slashSplit.Length - 1].Split('R');                
                if (rSplit.Length > 1)
                {                    
                    if (rSplit[0] == datePart)
                    {                        
                        int currentCount = Convert.ToInt16(rSplit[1].Substring(0,4));                        
                        if (count < currentCount)
                        {
                            count = currentCount;
                        }
                    }
                }
            }
            if (count == 0)
            {
                return 1;
            }
            else
            {
                return count + 1;
            }
        }
    }    

    public class BillingScanningListItem
    {
        string m_ScanFolderName;
        string m_ScanFolderPath = @"\\CFileServer\documents\Scanning\Billing\";        
        string[] m_Files;

        public BillingScanningListItem(string scanFolderName)
        {
            this.m_ScanFolderName = scanFolderName;
            this.m_Files = Directory.GetFiles(this.ScanFullPath);
        }

        public string ScanFolderName
        {
            get { return this.m_ScanFolderName; }
            set { this.m_ScanFolderName = value; }
        }

        public string ScanFolderPath
        {
            get { return this.m_ScanFolderPath; }
        }

        public string ScanFullPath
        {
            get { return this.m_ScanFolderPath + this.m_ScanFolderName; }
        }

        public int FileCount
        {
            get { return this.m_Files.Length; }            
        }
    }
}
