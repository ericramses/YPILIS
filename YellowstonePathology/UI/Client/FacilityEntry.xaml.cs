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
        public FacilityEntry(YellowstonePathology.Business.Facility.Model.Facility facility)
        {
            this.m_Facility = facility;

            InitializeComponent();
            this.DataContext = this;
            Closing += FacilityEntry_Closing;
        }

        private void FacilityEntry_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
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

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                Close();
            }
        }

        private bool CanSave()
        {
            bool result = true;

            /*Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection { new Business.Audit.Model.ProviderDisplayNameAudit(this.m_Physician.DisplayName),
                new YellowstonePathology.Business.Audit.Model.ProviderNpiAudit(this.m_Physician),
                new Business.Audit.Model.ProviderHomeBaseAudit(this.m_Physician),
                new Business.Audit.Model.ProviderClientsHaveDistributionSetAudit(this.m_Physician.ObjectId, this.m_PhysicianClientView) };

            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = auditCollection.Run2();
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditResult.Message + "  Do you want to continue?", "Missing Information", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }*/
            return result;
        }
    }
}
