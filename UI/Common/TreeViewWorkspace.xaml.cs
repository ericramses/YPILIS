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
using System.ComponentModel;

namespace YellowstonePathology.UI.Common
{
    public partial class TreeViewWorkspace : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public TreeViewWorkspace(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;
            this.m_ObjectTracker = objectTracker;
            this.m_AccessionOrder = accessionOrder;

            InitializeComponent();

            this.DataContext = this;
        }

		public YellowstonePathology.Business.User.SystemUser CurrentUser
        {
            get { return this.m_SystemIdentity.User; }
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        private void ContextMenuCancelTest_Opened(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = (ContextMenu)sender;
            bool enabled = true;
            if (contextMenu.Tag.GetType().Name == "TestOrder")
            {
                if (((YellowstonePathology.Business.Test.Model.TestOrder)contextMenu.Tag).TestName == "Surgical Diagnosis")
                {
                    enabled = false;
                }
            }
            else
            {
                enabled = false;
            }
            foreach (MenuItem menuItem in contextMenu.Items)
            {
                menuItem.IsEnabled = enabled;
            }
        }

        private void MenuItemCancelTest_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem.Tag.GetType().Name == "TestOrder")
            {
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = (YellowstonePathology.Business.Test.Model.TestOrder)menuItem.Tag;                
                YellowstonePathology.Business.Visitor.RemoveTestOrderVisitor removeTestOrderVisitor = new Business.Visitor.RemoveTestOrderVisitor(testOrder.TestOrderId);
                this.m_AccessionOrder.TakeATrip(removeTestOrderVisitor);
                this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
            }
        }        

        private void ContextMenuCancelPanel_Opened(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = (ContextMenu)sender;
            if (((YellowstonePathology.Business.Test.PanelOrder)contextMenu.Tag).TestOrderCollection.Count > 0)
            {
                foreach (MenuItem menuItem in contextMenu.Items)
                {
                    menuItem.IsEnabled = false;
                }
            }
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
