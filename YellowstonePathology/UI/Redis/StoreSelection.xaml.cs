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

namespace YellowstonePathology.UI.Redis
{
    /// <summary>
    /// Interaction logic for StoreSelection.xaml
    /// </summary>
    public partial class StoreSelection : Window
    {
        List<string> m_RedisDatabases;

        public StoreSelection()
        {
            this.m_RedisDatabases = new List<string>();
            foreach (string name in Enum.GetNames(typeof(YellowstonePathology.Store.AppDBNameEnum)))
            {
                this.m_RedisDatabases.Add(name);
            }

            InitializeComponent();
            DataContext = this;
        }

        public List<string> RedisDatabases
        {
            get { return this.m_RedisDatabases; }
        }

        private void ListViewRedisDatabases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewRedisDatabases.SelectedItems.Count > 0)
            {
                foreach(string name in this.ListViewRedisDatabases.SelectedItems)
                {
                    YellowstonePathology.Store.AppDBNameEnum db;
                    if(Enum.TryParse<YellowstonePathology.Store.AppDBNameEnum>(name, out db) == true)
                    {
                       switch(db)
                        {
                            case YellowstonePathology.Store.AppDBNameEnum.CPTCode:
                                this.HandleJSON(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.Lock:
                                this.HandleHash(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.ICDCode:
                                this.HandleJSON(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.PQRS:
                                this.HandleJSON(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.Stain:
                                this.HandleJSON(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.VantageSlide:
                                this.HandleJSON(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.EmbeddingScan:
                                this.HandleHash(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.BozemanBlockCount:
                                this.HandleString(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.Specimen:
                                this.HandleJSON(db);
                                break;
                            case YellowstonePathology.Store.AppDBNameEnum.DictationTemplate:
                                this.HandleJSON(db);
                                break;
                            default:
                                MessageBox.Show("This database is not handled at this time.");
                                break;
                        }
                    }
                }
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HandleJSON(YellowstonePathology.Store.AppDBNameEnum db)
        {

        }

        private void HandleHash(YellowstonePathology.Store.AppDBNameEnum db)
        {

        }

        private void HandleString(YellowstonePathology.Store.AppDBNameEnum db)
        {

        }
    }
}
