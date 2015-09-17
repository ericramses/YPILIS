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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for PathologistSignoutAuditMessagePage.xaml
    /// </summary>
    public partial class PathologistSignoutAuditMessagePage : UserControl, Business.Interface.IPersistPageChanges
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        YellowstonePathology.Business.Audit.Model.AuditResult m_AuditResult;

        public PathologistSignoutAuditMessagePage(YellowstonePathology.Business.Audit.Model.AuditResult auditResult)
        {
            this.m_AuditResult = auditResult;

            InitializeComponent();
            DataContext = this;
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return false;
        }

        public bool OkToSaveOnClose()
        {
            return false;
        }

        public void Save()
        {
        }

        public void UpdateBindingSources()
        {

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        public YellowstonePathology.Business.Audit.Model.AuditResult AuditResult
        {
            get { return this.m_AuditResult; }
        }
    }
}
