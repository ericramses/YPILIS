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

namespace YellowstonePathology.UI.MySql
{
    /// <summary>
    /// Interaction logic for SqlServerRefreshDialog.xaml
    /// </summary>
    public partial class SqlServerRefreshDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MySQLMigration.MigrationStatusCollection m_MigrationStatusCollection;
        private MySQLMigration.NonpersistentTableDefCollection m_NonpersistentTableDefCollection;
        private YellowstonePathology.MySQLMigration.SqlServerFromMySqlRefresher m_SqlServerFromMySqlRefresher;
        private string m_StatusMessage;

        public SqlServerRefreshDialog()
        {
            this.m_MigrationStatusCollection = MySQLMigration.MigrationStatusCollection.GetAll();
            this.m_SqlServerFromMySqlRefresher = new MySQLMigration.SqlServerFromMySqlRefresher();

            InitializeComponent();

            DataContext = this;
        }

        public MySQLMigration.MigrationStatusCollection MigrationStatusCollection
        {
            get { return this.m_MigrationStatusCollection; }
            private set
            {
                this.m_MigrationStatusCollection = value;
                this.NotifyPropertyChanged("MigrationStatusCollection");
            }
        }

        public MySQLMigration.NonpersistentTableDefCollection NonpersistentTableDefCollection
        {
            get { return this.m_NonpersistentTableDefCollection; }
            private set
            {
                this.m_NonpersistentTableDefCollection = value;
                this.NotifyPropertyChanged("NonpersistentTableDefCollection");
            }
        }

        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
            set
            {
                this.m_StatusMessage = value;
                this.NotifyPropertyChanged("StatusMessage");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void SetStatusMessage(Business.Rules.MethodResult methodResult)
        {
            if (string.IsNullOrEmpty(methodResult.Message) == true)
            {
                this.StatusMessage = "Success";
            }
            else
            {
                this.StatusMessage = methodResult.Message;
            }
        }

        private void MenuItemGetRowCounts_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTables.SelectedItems.Count > 0)
            {
                this.StatusMessage = "Working on it.";
                foreach (MySQLMigration.MigrationStatus migrationStatus in this.ListViewTables.SelectedItems)
                {
                    this.m_SqlServerFromMySqlRefresher.GetRowCounts(migrationStatus);
                }
            }
            else
            {
                MessageBox.Show("Select a class.");
            }
        }

        private void MenuItemRefreshData_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            foreach (MySQLMigration.MigrationStatus migrationStatus in this.m_MigrationStatusCollection)
            {
                if (/*migrationStatus.TableName == "tblSurgicalAudit" || migrationStatus.TableName == "tblSurgicalSpecimen" ||
                    migrationStatus.TableName == "tblSurgicalSpecimenAudit" ||*/ migrationStatus.TableName == "tblSurgicalTestOrder" /*||
                    migrationStatus.TableName == "tblGrossOnlyResult" || migrationStatus.TableName == "tblBCellEnumerationTestOrder" ||
                    migrationStatus.TableName == "tblTCellNKProfileTestOrder" || migrationStatus.TableName == "tblTCellSubsetAnalysisTestOrder"*/)
                {
                    Business.Rules.MethodResult methodResult = m_SqlServerFromMySqlRefresher.CompareTables(migrationStatus);
                    if (methodResult.Success == false)
                    {
                        overallResult.Success = false;
                        overallResult.Message += methodResult.Message;
                    }
                }
            }
            this.SetStatusMessage(overallResult);
        }
    }
}
