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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Scanning
{
    /// <summary>
    /// Interaction logic for ViewScannedDocumentWindow.xaml
    /// </summary>
    public partial class ViewScannedDocumentWindow : Window
    {
        string m_LocalScanFilePath = @"C:\Program Files\Yellowstone Pathology Institute\Scanning\LocalStorage\";
        ScannedFileCollection m_FileNameTable;
        int m_CurrentRowIndex = 0;
        string m_DefaultCaseType;
        string m_DefaultYear;

        public ViewScannedDocumentWindow(ScannedFileCollection fileNameTable, int startPosition)
        {
            this.m_FileNameTable = fileNameTable;

            this.m_DefaultCaseType = "X";
            this.m_DefaultYear = DateTime.Now.ToString("yy");
            this.m_CurrentRowIndex = startPosition;

            InitializeComponent();

            this.KeyUp += new KeyEventHandler(ViewScannedDocumentWindow_KeyUp);
            this.Closing += new System.ComponentModel.CancelEventHandler(ViewScannedDocumentWindow_Closing);
            this.Loaded += new RoutedEventHandler(ViewScannedDocumentWindow_Loaded);
        }

        private void ViewScannedDocumentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetNextFile();
        }

        private void ViewScannedDocumentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            this.SetReportNo();            
        }               

        private void GetNextFile()
        {
			//WHC ReportNo issue
			string fileName = this.m_FileNameTable[this.m_CurrentRowIndex].Name;
            string reportNo = this.m_FileNameTable[this.m_CurrentRowIndex].ReportNo;

            if (reportNo == "NULL" | reportNo == "")
            {
                this.TextBoxPrefix.Text = this.m_DefaultCaseType + this.m_DefaultYear + "-";
                this.TextBlockReportNo.Text = "";
            }
            else
            {
                string[] dotSplit = reportNo.Split('-');
                if (dotSplit.Length > 1)
                {
                    this.m_DefaultCaseType = dotSplit[0].Substring(0, 1);
                    this.TextBoxPrefix.Text = dotSplit[0] + "-";
                    this.TextBlockReportNo.Text = dotSplit[1];
                }
            }

            Image image = new Image(); 

            BitmapImage src = new BitmapImage(); 
            src.BeginInit(); 
            src.UriSource = new Uri(fileName, UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad; 
            src.EndInit(); 

            image.Source = src; 
            image.Stretch = Stretch.Uniform;            
            
            this.StackPanelImage.Children.Add(image);             
        }

        private void SetAccessionCaseType(string caseType)
        {
            string accessionPrefix = this.TextBoxPrefix.Text;
            string year = accessionPrefix.Substring(accessionPrefix.Length - 3, 3);
            string caseTypeOld = accessionPrefix.Replace(year, "");
            string newPrefix = accessionPrefix.Replace(caseTypeOld, caseType);
            this.TextBoxPrefix.Text = newPrefix;
        }

        private void SetAccessionYear(int increment)
        {            
            string prefix = this.TextBoxPrefix.Text;
            string year = prefix.Substring(prefix.Length - 3, 2);
            int decade = Convert.ToInt32(year.Substring(0, 1));
            if (decade == 0)
            {
                year = "20" + year;
            }
            else
            {
                year = "19" + year;
            }
            int intYear = Convert.ToInt32(year);
            intYear = intYear + increment;
            string newYear = intYear.ToString().Substring(2, 2);
            string newPrefix = prefix.Replace(year.Substring(2, 2), newYear);
            this.TextBoxPrefix.Text = newPrefix;            
        }


        private void ViewScannedDocumentWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.TextBlockReportNo.Text == "NULL")
            {
                this.TextBlockReportNo.Text = "";
            }
            if (e.Key >= Key.NumPad0 & e.Key <= Key.NumPad9)
            {
                this.TextBlockReportNo.Text += e.Key.ToString().Substring(6, 1);
            }
            if (e.Key == Key.PageUp)
            {
                this.SetReportNo();
                if (this.m_CurrentRowIndex != 0)
                {
                    this.m_CurrentRowIndex -= 1;
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
                this.GetNextFile();
            }
            if (e.Key == Key.PageDown)
            {
                this.SetReportNo();
                if (this.m_CurrentRowIndex != this.m_FileNameTable.Count - 1)
                {
                    this.m_CurrentRowIndex += 1;
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
                this.GetNextFile();
            }
            if (e.Key == Key.Subtract)
            {
                this.SetAccessionYear(-1);
            }
            if (e.Key == Key.Add)
            {
                this.SetAccessionYear(1);
            }
            if (e.Key >= Key.D0 & e.Key <= Key.D9)
            {
                this.TextBlockReportNo.Text += e.Key.ToString().Substring(1, 1);
            }
            if (e.Key == Key.X)
            {
                this.TextBlockReportNo.Text += "X";
            }
            if (e.Key == Key.Back)
            {
                string value = this.TextBlockReportNo.Text;
                if (value.Length > 0)
                {
                    value = value.Substring(0, value.Length - 1);
                    this.TextBlockReportNo.Text = value;
                }
            }
            if (e.Key >= Key.A & e.Key <= Key.Z)
            {
                if (e.Key == Key.E)
                {
                    if (this.m_DefaultCaseType == "M")
                    {
                        this.m_DefaultCaseType = "ME";
                    }
                }
                else if (e.Key == Key.M)
                {
                    if (this.m_DefaultCaseType == "F")
                    {
                        this.m_DefaultCaseType = "FM";
                    }
                    else
                    {
                        this.m_DefaultCaseType = e.Key.ToString();
                    }
                }
                else
                {
                    this.m_DefaultCaseType = e.Key.ToString();
                }
                this.SetAccessionCaseType(this.m_DefaultCaseType);
            }
        }       

        private void SetReportNo()
        {
            string reportNo = this.TextBoxPrefix.Text + this.TextBlockReportNo.Text;
            this.m_FileNameTable[this.m_CurrentRowIndex].ReportNo = reportNo;
        }

        private void ViewScannedDocuments_Activated(object sender, EventArgs e)
        {
            //this.axMiDocView.Focus();
        }
                
    }
}
