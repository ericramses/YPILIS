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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for RetrospectiveReviews.xaml
    /// </summary>
    public partial class RetrospectiveReviews : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private List<string> m_WordList;

        public RetrospectiveReviews()
        {
            this.m_WordList = this.GetWordList();
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(DateTime.Parse("2017-5-18"));
            this.NotifyPropertyChanged("ReportSearchList");

            InitializeComponent();
            this.DataContext = this;
            this.TrimTheList();
        }

        private void TrimTheList()
        {
            string xx = string.Empty;
            foreach(string s in this.m_WordList)
            {
                xx = xx + s + "|";
            }
            Console.WriteLine(xx);
        }

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }

        private bool DoWordsExist(Business.Test.AccessionOrder ao)
        {
            bool result = false;
            foreach (Business.Specimen.Model.SpecimenOrder specimenOrder in ao.SpecimenOrderCollection)
            {
                foreach (string word in this.m_WordList)
                {
                    if (string.IsNullOrEmpty(specimenOrder.Description) == false)
                    {
                        if (specimenOrder.Description.ToLower().Contains(word.ToLower()))
                        {
                            return true;
                        }
                    }
                }
            }
            return result;
        }

        private List<string> GetWordList()
        {
            List<string> result = new List<string>();
            result.Add("esophagus");
            result.Add("ge junction");
            result.Add("gastroesophageal junction");
            result.Add("stomach");
            result.Add("small bowel");
            result.Add("duodenum");
            result.Add("jejunum");
            result.Add("ampulla");
            result.Add("common bile duct");
            result.Add("ileum");
            result.Add("terminal ileum");
            result.Add("ileocecal valve");
            result.Add("cecum");
            result.Add("colon");
            result.Add("rectum");
            result.Add("anus");
            result.Add("cervix");
            result.Add("endocervix");
            result.Add("endometrium");
            result.Add("vagina");
            result.Add("vulva");
            result.Add("perineum");
            result.Add("labia majora");
            result.Add("labia minora");
            result.Add("ovary");
            result.Add("fallopian tube");
            result.Add("skin");
            result.Add("lung");
            result.Add("bronchus");
            result.Add("larynx");
            result.Add("vocal cord");
            result.Add("oral mucosa");
            result.Add("oral cavity");
            result.Add("tongue");
            result.Add("gingiva");
            result.Add("pharynx");
            result.Add("epiglottis");
            result.Add("kidney");
            result.Add("nasopharynx");
            result.Add("oropharynx");
            result.Add("peritoneum");
            result.Add("pleura");
            result.Add("tonsil");
            result.Add("trachea");
            result.Add("ureter");
            result.Add("urethra");
            result.Add("bladder");
            return result;
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
