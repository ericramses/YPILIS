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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for StainOrder.xaml
    /// </summary>
    public partial class StainOrder : Window
    {
        List<CheckBox> m_DualStains;

        public StainOrder()
        {            
            InitializeComponent();
            this.Loaded += StainOrder_Loaded;
        }

        private void StainOrder_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_DualStains = new List<CheckBox>();
            this.InitializeCheckboxList("DualStainOrder", this.m_DualStains, this.MainGrid);
        }

        private void InitializeCheckboxList(string name, List<CheckBox> list, DependencyObject depObj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if(child is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)child;
                    if(checkBox.Name.Contains(name))
                    {
                        list.Add(checkBox);
                    }
                }
                this.InitializeCheckboxList(name, list, child);
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
