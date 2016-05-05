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
using NHunspell;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for NHunspell.xaml
    /// </summary>
    public partial class NHunspell : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;
        
        private List<string> m_SuggestedWordList;
        private Hunspell m_Hunspell;

        private int m_CurrentSelectionStart;
        private int m_CurrentSelectionLength;

        
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        List<Binding> m_BindingList;

        public NHunspell(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

            this.m_Hunspell = new Hunspell();
            //this.m_Hunspell.Load(@"C:\Program Files\Yellowstone Pathology Institute\en_US-custom.aff", @"C:\Program Files\Yellowstone Pathology Institute\en_US-custom.dic");
            //this.m_Hunspell.Load(@"C:\Program Files\Yellowstone Pathology Institute\en_med_glut.aff", @"C:\Program Files\Yellowstone Pathology Institute\en_med_glut.dic");
            this.m_Hunspell.Load(@"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.aff", @"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.dic");

            InitializeComponent();
            
            this.DataContext = this;
            this.Loaded += NHunspell_Loaded;
        }

        private void NHunspell_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_BindingList = new List<Binding>();
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();

            Binding grossBinding = new Binding();
            grossBinding.Source = surgicalTestOrder;
            grossBinding.Path = new PropertyPath("GrossX");
            grossBinding.Mode = BindingMode.TwoWay;
            grossBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding clinicalInfoBinding = new Binding();
            clinicalInfoBinding.Source = surgicalTestOrder;
            clinicalInfoBinding.Path = new PropertyPath("ClinicalInfo");
            clinicalInfoBinding.Mode = BindingMode.TwoWay;
            clinicalInfoBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            this.m_BindingList.Add(grossBinding);
            this.m_BindingList.Add(clinicalInfoBinding);

            BindingOperations.SetBinding(this.TextBoxText, TextBox.TextProperty, grossBinding);
        }

        public List<Binding> BindingList
        {
            get { return this.m_BindingList; }
        }

        public List<string> SuggestedWordList
        {
            get { return this.m_SuggestedWordList; }
        }

        private void ButtonSpellCheck_Click(object sender, RoutedEventArgs e)
        {
            this.CheckSpelling();
        }

        private void CheckSpelling()
        {
            List<System.Text.RegularExpressions.Match> matches = this.GetWordList(this.TextBoxText.Text);
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                bool correct = this.m_Hunspell.Spell(match.Value);
                if (correct == false)
                {
                    if(match.Index > this.m_CurrentSelectionStart)
                    {
                        this.m_SuggestedWordList = this.m_Hunspell.Suggest(match.Value);
                        this.NotifyPropertyChanged("SuggestedWordList");

                        this.TextBoxText.Focus();
                        this.TextBoxText.SelectionStart = match.Index;
                        this.TextBoxText.SelectionLength = match.Length;

                        this.m_CurrentSelectionStart = match.Index;
                        this.m_CurrentSelectionLength = match.Length;

                        break;
                    }                    
                }
            }
        }

        private List<System.Text.RegularExpressions.Match> GetWordList(string text)
        {            
            List<System.Text.RegularExpressions.Match> result = new List<System.Text.RegularExpressions.Match>();
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(@"\b\w+\b");
            foreach (System.Text.RegularExpressions.Match match in rx.Matches(text))
            {                
                result.Add(match);
            }
            return result;
        }        

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewSuggestedWordList.SelectedItems.Count != 0)
            {
                string selectedWord = (string)this.ListViewSuggestedWordList.SelectedItem;                
                //this.m_Text = this.m_Text.Remove(this.m_CurrentSelectionStart, this.m_CurrentSelectionLength);
                //this.m_Text = this.m_Text.Insert(this.m_CurrentSelectionStart, selectedWord);
                this.m_SuggestedWordList = new List<string>();

                this.NotifyPropertyChanged("Text");
                this.NotifyPropertyChanged("SuggestedWordList");                

                this.CheckSpelling();                
            }            
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();         
        }

        private void ExportDic()
        {
            using (System.IO.StreamWriter writefile = new System.IO.StreamWriter(@"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.aff"))
            {
                string line;
                System.IO.StreamReader readFile = new System.IO.StreamReader(@"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.dic");
                while ((line = readFile.ReadLine()) != null)
                {
                    if (line.Contains("'") == true)
                    {
                        line = line.Replace("'", "''");
                    }

                    writefile.WriteLine("Insert tblWord (Word) values ('" + line + "')");
                }
            }
        }

        private void ButtonSkip_Click(object sender, RoutedEventArgs e)
        {
            this.CheckSpelling();
            this.m_SuggestedWordList = new List<string>();
            this.NotifyPropertyChanged("SuggestedWordList");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonTesting_Click(object sender, RoutedEventArgs e)
        {
            this.ReadSentanceFile();
        }

        private void ReadSentanceFile()
        {
            List<string> wordList = new List<string>();
            string line;
            System.IO.StreamReader readFile = new System.IO.StreamReader(@"C:\Program Files\Yellowstone Pathology Institute\alldiagnosis.txt");
            while ((line = readFile.ReadLine()) != null)
            {
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(@"\b\w+\b");
                foreach (System.Text.RegularExpressions.Match match in rx.Matches(line))
                {
                    if(wordList.Contains(match.Value) == false)
                    {
                        wordList.Add(match.Value);            
                    }                                              
                }                    
            }

            wordList.Sort();
            using (System.IO.StreamWriter writefile = new System.IO.StreamWriter(@"C:\Program Files\Yellowstone Pathology Institute\alldiagnosis.json"))
            {
                foreach(string word in wordList)
                {
                    writefile.WriteLine(word);
                }
            }
        }
    }
}
