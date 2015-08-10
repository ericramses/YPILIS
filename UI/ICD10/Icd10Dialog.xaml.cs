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
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace YellowstonePathology.UI.ICD10
{
    /// <summary>
    /// Interaction logic for Icd10Dialog.xaml
    /// </summary>
    public partial class Icd10Dialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string DIndexPath = @"C:\Users\sid.harder\Downloads\ICD10CM_FY2014_Full_XML\ICD10CM_FY2014_Full_XML_DIndex.xml";

        private XDocument m_IndexDocument;                
        private List<string> m_CleanedWords;
        private List<string> m_MainTerms;
        private List<YellowstonePathology.Domain.SpecimenDiagnosis> m_SpecimenDiagnosisList;

        public Icd10Dialog(string masterAccessionNoList)
        {
            this.m_SpecimenDiagnosisList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenDiagnosisList(masterAccessionNoList);

            InitializeComponent();

            this.m_IndexDocument = XDocument.Load(@"C:\Users\sid.harder\Downloads\ICD10CM_FY2014_Full_XML\ICD10CM_FY2014_Full_XML_DIndex.xml");
            this.m_MainTerms = (from x in this.m_IndexDocument.Descendants("mainTerm") select x.Value).ToList<string>();            

            this.DataContext = this;
        }

        public List<YellowstonePathology.Domain.SpecimenDiagnosis> SpecimenDiagnosisList
        {
            get { return this.m_SpecimenDiagnosisList; }
        }

        public List<string> CleanedWords
        {
            get { return this.m_CleanedWords; }
        }

        private void ListViewSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (this.ListViewSpecimen.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Test.SpecimenOrder)this.ListViewSpecimen.SelectedItem;
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                YellowstonePathology.Business.Surgical.SurgicalResultItem surgicalResult = panelSetOrder.PanelSetResultOrderCollection.GetSurgicalResultItem();
                this.m_SurgicalSpecimenResult = surgicalResult.SurgicalSpecimenResultItemCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
                this.NotifyPropertyChanged("SurgicalSpecimenResult");
            }
            */
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            /*
            ICD10.Tokenizer tokenizer = new ICD10.Tokenizer(this.m_SurgicalSpecimenResult.Diagnosis);
            string [] allWords = tokenizer.GetWords();
            ICD10.Cleaner cleaner = new ICD10.Cleaner();
            this.m_CleanedWords = cleaner.Clean(allWords);
            this.NotifyPropertyChanged("CleanedWords");
             */
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