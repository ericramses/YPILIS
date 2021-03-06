﻿using System;
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

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for AmendmentListPage.xaml
	/// </summary>
	public partial class AmendmentListPage : Page, INotifyPropertyChanged
	{		
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ContentChangedEventHandler(object sender, EventArgs e);
        public event ContentChangedEventHandler ContentChanged;

        private AmendmentUI m_AmendmentUI;

		public AmendmentListPage(AmendmentUI amendmentUI)
		{
			this.m_AmendmentUI = amendmentUI;
			InitializeComponent();
			DataContext = this;
            if(this.m_AmendmentUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
            {
                this.ButtonAdd.IsEnabled = false;
                this.ButtonDelete.IsEnabled = false;
                this.ButtonEdit.IsEnabled = false;
                this.ListViewAmendments.MouseDoubleClick -= this.ListViewAmendments_MouseDoubleClick;
            }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection Amendments
		{
			get { return this.m_AmendmentUI.AmendmentCollection; }
		}

		private void ListViewAmendments_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            this.m_AmendmentUI.SelectedAmendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.ListViewAmendments.SelectedItem;
		}

		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AmendmentUI.AccessionOrder.AddAmendment(this.m_AmendmentUI.PanelSetOrder.ReportNo);
			amendment.TestResultAmendmentFill(this.m_AmendmentUI.ReportNo, this.m_AmendmentUI.AssignedToId, "???");

			NotifyPropertyChanged("Amendments");
			this.ListViewAmendments.SelectedIndex = 0;
		}

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_AmendmentUI.SelectedAmendment != null)
			{
				this.ShowEditPage();
			}
		}

		private void ButtonDelete_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewAmendments.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this amendment?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.Yes)
				{
                    YellowstonePathology.Business.Amendment.Model.Amendment selectedAmendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.ListViewAmendments.SelectedItem;					
					this.m_AmendmentUI.AccessionOrder.DeleteAmendment(selectedAmendment.AmendmentId);
					this.ListViewAmendments.SelectedIndex = -1;
				}
			}
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			Window window = Window.GetWindow(this);
			window.Close();
		}

		private void ListViewAmendments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
            if (this.ListViewAmendments.SelectedItem != null)
            {
                this.ShowEditPage();
            }
        }

        private void ShowEditPage()
        {
            AmendmentEditPage amendmentEditPage = new AmendmentEditPage(this.m_AmendmentUI);
            amendmentEditPage.ContentChanged += AmendmentEditPage_ContentChanged;
            this.NavigationService.Navigate(amendmentEditPage);
            this.ContentChanged(this, new EventArgs());
        }

        private void AmendmentEditPage_ContentChanged(object sender, EventArgs e)
        {
            this.ContentChanged(this, new EventArgs());
        }
    }
}
