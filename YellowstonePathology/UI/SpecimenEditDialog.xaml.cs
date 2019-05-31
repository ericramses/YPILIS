using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SpecimenEditDialog.xaml
    /// </summary>
    public partial class SpecimenEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_SpecimenString;
        private string m_HoldToCompareString;
        private YellowstonePathology.Business.Specimen.Model.Specimen m_Specimen;

        public SpecimenEditDialog(YellowstonePathology.Business.Specimen.Model.Specimen specimen)
        {
            this.m_Specimen = specimen;

            this.m_SpecimenString = this.m_Specimen.ToJSON();
            this.m_HoldToCompareString = this.m_Specimen.ToJSON();
            InitializeComponent();

            DataContext = this;
            Loaded += SpecimenEditDialog_Loaded;
        }

        private void SpecimenEditDialog_Loaded(object sender, RoutedEventArgs e)
        {
           if(this.m_Specimen.SpecimenId == null)
            {
                MessageBox.Show("The Null Specimen may not be altered.  This edit window will close.");
                Close();
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string SpecimenId
        {
            get { return this.m_Specimen.SpecimenId; }
        }

        public string SpecimenString
        {
            get { return this.m_SpecimenString; }
            set { this.m_SpecimenString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                if (this.m_HoldToCompareString != this.m_SpecimenString)
                {
                    MessageBoxResult result = MessageBox.Show("Do you  want to save the changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        YellowstonePathology.Business.Specimen.Model.Specimen specimenToSave = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.FromJSON(this.m_SpecimenString);
                        specimenToSave.Save();
                    }
                }

                this.Close();
            }
        }

        private bool CanSave()
        {
            Business.Rules.MethodResult result = YellowstonePathology.Business.Helper.JSONHelper.IsValidJSONString(this.m_SpecimenString);

            if (result.Success == true)
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(this.m_SpecimenString);
                if (jObject["specimenId"] == null)
                {
                    result.Success = false;
                    result.Message = "The Specimen Id must be present.";
                }
                else
                {
                    string specimenIdToCompare = jObject["specimenId"].ToString();
                    if (string.IsNullOrEmpty(specimenIdToCompare) == true)
                    {
                        result.Success = false;
                        result.Message = "The Specimen Id must have a value.";
                    }
                    else if (this.m_Specimen.SpecimenId != specimenIdToCompare)
                    {
                        result.Success = false;
                        result.Message = "The Specimen Ids must be the same.";
                    }
                }
            }

            if (result.Success == false)
            {
                MessageBox.Show(result.Message);
            }

            return result.Success;
        }
    }
}
