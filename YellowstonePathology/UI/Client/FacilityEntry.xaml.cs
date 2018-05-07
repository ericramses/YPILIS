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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for FacilityEntry.xaml
    /// </summary>
    public partial class FacilityEntry : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Facility.Model.Facility m_Facility;
        private bool m_NewButtonEnabled;

        public FacilityEntry(YellowstonePathology.Business.Facility.Model.Facility facility, bool isNewFacility)
        {
            if (isNewFacility == true)
            {
                this.m_Facility = new Business.Facility.Model.Facility();
            }
            else
            {
                this.m_Facility = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullFacility(facility.FacilityId, this);
            }
            this.m_NewButtonEnabled = isNewFacility;

            InitializeComponent();
            this.DataContext = this;
            Closing += FacilityEntry_Closing;
        }

        private void FacilityEntry_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            YellowstonePathology.Business.Facility.Model.FacilityCollection.Refresh();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Facility.Model.Facility Facility
        {
            get { return this.m_Facility; }
        }

        public bool NewButtonEnabled
        {
            get { return this.m_NewButtonEnabled; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                Close();
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanAdd() == true)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_Facility, this);
            }
        }
        
        private bool CanAdd()
        {
            bool result = true;
            if (string.IsNullOrEmpty(m_Facility.FacilityName) == false)
            {
                this.m_Facility.FacilityId = YellowstonePathology.Business.Helper.IdHelper.CapitalConsonantsInString(m_Facility.FacilityName);
                if (string.IsNullOrEmpty(this.m_Facility.FacilityId) == true)
                {
                    result = false;
                }
                else if (YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.Exists(this.m_Facility.FacilityId) == true)
                {
                    result = false;
                }

                if(result == false)
                {
                    MessageBox.Show("The name did not create a valid Id." + Environment.NewLine + "You can add consonants to the name then change the name when a valid id is created.");
                }
            }
            else
            {
                result = false;
                MessageBox.Show("You need to enter a facility name.");
            }
            return result;
        }

        private bool CanSave()
        {
            bool result = true;
            YellowstonePathology.Business.Audit.Model.CanSaveFacilityAudit canSaveFacilityAudit = new Business.Audit.Model.CanSaveFacilityAudit(this.m_Facility);
            canSaveFacilityAudit.Run();
            if(canSaveFacilityAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                result = false;
                MessageBox.Show(canSaveFacilityAudit.Message.ToString());
            }
            return result;
        }
    }
}
