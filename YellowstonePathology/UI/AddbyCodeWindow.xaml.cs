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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AddbyCodeWindow.xaml
    /// </summary>
    public partial class AddbyCodeWindow : Window
    {
        public AddbyCodeWindow()
        {
            InitializeComponent();
            Loaded += AddbyCodeWindow_Loaded;
        }

        private void AddbyCodeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ColumnDefinition coldef1 = new ColumnDefinition();
            GridLength gl1 = new GridLength(0, GridUnitType.Auto);
            coldef1.Width = gl1;

            ColumnDefinition colDef2 = new ColumnDefinition();
            GridLength gl2 = new GridLength(100, GridUnitType.Star);
            colDef2.Width = gl2;

            RowDefinition rd1 = new RowDefinition();
            rd1.Height = new GridLength(100, GridUnitType.Pixel);

            RowDefinition rd2 = new RowDefinition();
            rd2.Height = new GridLength(0, GridUnitType.Auto);

            RowDefinition rd3 = new RowDefinition();
            rd3.Height = new GridLength(30, GridUnitType.Pixel);

            this.MainGrid.ColumnDefinitions.Add(coldef1);
            this.MainGrid.ColumnDefinitions.Add(colDef2);
            this.MainGrid.RowDefinitions.Add(rd1);
            this.MainGrid.RowDefinitions.Add(rd2);
            this.MainGrid.RowDefinitions.Add(rd3);

            Button b = new Button();
            b.Width = 80;
            b.Height = 25;
            b.Click += Button_Click;
            b.Content = "Add";
            Grid.SetRow(b, 2);
            Grid.SetColumn(b, 0);
            this.MainGrid.Children.Add(b);

            Button c = new Button();
            c.Width = 80;
            c.Height = 25;
            c.Click += Button1_Click;
            c.Content = "Close";
            Grid.SetRow(c, 2);
            Grid.SetColumn(c, 1);
            this.MainGrid.Children.Add(c);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Added";
            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, 0);
            this.MainGrid.Children.Add(textBlock);

            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            textBox.Text = "This is added too and can be changed.";
            Grid.SetRow(textBox, 1);
            Grid.SetColumn(textBox, 1);
            this.MainGrid.Children.Add(textBox);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
