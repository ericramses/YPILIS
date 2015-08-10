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

namespace YellowstonePathology.UI.Monitor
{
    /// <summary>
    /// Interaction logic for PendingTestDelayDialog.xaml
    /// </summary>
    public partial class PendingTestDelayDialog : Window
    {
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObectTracker;

        public PendingTestDelayDialog(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_SystemIdentity = systemIdentity;

			this.m_ObectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            this.m_ObectTracker.RegisterObject(this.m_PanelSetOrder);

            InitializeComponent();

            this.DataContext = this;                        
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOKToClose() == true)
            {
                this.Close();
                this.m_ObectTracker.SubmitChanges(this.m_PanelSetOrder);
            }
        }

        private bool IsOKToClose()
        {
            bool result = true;
            if (this.m_PanelSetOrder.Delayed == true)
            {
                if (string.IsNullOrEmpty(this.m_PanelSetOrder.DelayComment) == true)
                {
                    result = false;
                    MessageBox.Show("Use must enter a comment explaining why this case is delayed.");
                }
            }
            return result;
        }

        private void ComboBoxDelayDays_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)this.ComboBoxDelayDays.SelectedItem;
            int daysToDelay = Convert.ToInt32(comboBoxItem.Tag.ToString());
            TimeSpan timeSpanDelay = new TimeSpan(daysToDelay,0,0,0);
            this.m_PanelSetOrder.Delayed = true;
            this.m_PanelSetOrder.DelayedBy = this.m_SystemIdentity.User.DisplayName;
            this.m_PanelSetOrder.DelayedDate = DateTime.Now;

            this.m_PanelSetOrder.ExpectedFinalTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetEndDateConsideringWeekends(this.m_PanelSetOrder.ExpectedFinalTime.Value, timeSpanDelay);
        }
    }
}
