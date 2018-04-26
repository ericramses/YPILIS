﻿using System;
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
using System.IO;

namespace YellowstonePathology.UI.Common
{
    /// <summary>
    /// Interaction logic for UserPreferencesList.xaml
    /// </summary>
    public partial class UserPreferencesList : Window
    {
        private List<YellowstonePathology.Business.User.UserPreference> m_UserPreferenceList;
        public UserPreferencesList()
        {
            this.m_UserPreferenceList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllUserPreferences();
            InitializeComponent();
            this.DataContext = this;
        }

        public List<YellowstonePathology.Business.User.UserPreference> UserPreferenceList
        {
            get { return this.m_UserPreferenceList; }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            UserPreferences dlg = new Common.UserPreferences();
            dlg.ShowDialog();
            Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewUserPreferences.SelectedItem != null)
            {
                YellowstonePathology.Business.User.UserPreference userPreference = (YellowstonePathology.Business.User.UserPreference)this.ListViewUserPreferences.SelectedItem;
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ypilis.json";
                File.WriteAllText(path, "{'location': '" + userPreference.Location + "'}");
                Close();
            }
            else
            {
                MessageBox.Show("Select a User Preference for a location or add a new User Preference.");
            }
        }
    }
}
