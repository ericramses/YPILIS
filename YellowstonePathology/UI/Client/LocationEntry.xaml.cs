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
    /// Interaction logic for LocationEntry.xaml
    /// </summary>
    public partial class LocationEntry : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Facility.Model.Location m_Location;
        private bool m_NewButtonEnabled;

        public LocationEntry(YellowstonePathology.Business.Facility.Model.Location location, bool isNewLocation)
        {
            if (isNewLocation == true)
            {
                this.m_Location = new Business.Facility.Model.Location();
            }
            else
            {
                this.m_Location = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullLocation(location.LocationId, this);
            }
            this.m_NewButtonEnabled = isNewLocation;

            InitializeComponent();
            this.DataContext = this;
            Closing += LocationEntry_Closing;
        }

        private void LocationEntry_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            YellowstonePathology.Business.Facility.Model.LocationCollection.Refresh();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Facility.Model.Location Location
        {
            get { return this.m_Location; }
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
                this.NotifyPropertyChanged("Location");
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_Location, this);
            }
        }

        private bool CanAdd()
        {
            bool result = true;
            if (string.IsNullOrEmpty(m_Location.Description) == false)
            {
                this.m_Location.LocationId = YellowstonePathology.Business.Helper.IdHelper.CapitalConsonantsInString(m_Location.Description);
                if (string.IsNullOrEmpty(this.m_Location.LocationId) == true)
                {
                    result = false;
                }
                else if (YellowstonePathology.Business.Facility.Model.LocationCollection.Instance.Exists(this.m_Location.LocationId) == true)
                {
                    result = false;
                }

                if (result == false)
                {
                    MessageBox.Show("The name did not create a valid Id." + Environment.NewLine + "You can add consonants to the name then change the name when a valid id is created.");
                }
            }
            else
            {
                result = false;
                MessageBox.Show("You need to enter a Location name.");
            }
            return result;
        }

        private bool CanSave()
        {
            bool result = true;
            //YellowstonePathology.Business.Audit.Model.CanSaveFacilityAudit canSaveFacilityAudit = new Business.Audit.Model.CanSaveFacilityAudit(this.m_Facility);
            //canSaveFacilityAudit.Run();
            //if (canSaveFacilityAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            //{
            //    result = false;
            //    MessageBox.Show(canSaveFacilityAudit.Message.ToString());
            //}
            return result;
        }
    }
}
