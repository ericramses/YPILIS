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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string m_TouchDown;
        private string m_TouchUp;
        private List<string> m_TestList;
        private System.Windows.Threading.DispatcherTimer m_TouchDownTimer;        

        public Window2()
        {            
            this.m_TestList = new List<string>();
            this.m_TestList.Add("aaaaaaaaaaaaaaaaaaaaaaaaa");
            this.m_TestList.Add("bbbbbbbbbbbbbbbbbbbbbbbbb");

            InitializeComponent();

            this.m_TouchDown = "No Touch";
            this.m_TouchUp = "No Touch";            
            this.DataContext = this;

            this.m_TouchDownTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_TouchDownTimer.Interval = new TimeSpan(0, 0, 0, 0, 750);
            this.m_TouchDownTimer.Tick += new EventHandler(TouchDownTimer_Tick);
        }

        private void TouchDownTimer_Tick(object sender, EventArgs e)
        {
            this.m_TouchDownTimer.Stop();
            string item = (string)this.List.SelectedItem;
            MessageBox.Show(item);            
        }

        public List<string> TestList
        {
            get { return this.m_TestList; }
        }

        public string TouchDown
        {
            get { return this.m_TouchDown; }
            set
            {
                if(this.m_TouchDown != value)
                {
                    this.m_TouchDown = value;
                    this.NotifyPropertyChanged("TouchDown");
                }                
            }
        }

        public string TouchUp
        {
            get { return this.m_TouchUp; }
            set
            {
                if (this.m_TouchUp != value)
                {
                    this.m_TouchUp = value;
                    this.NotifyPropertyChanged("TouchUp");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.m_TouchDown = "No Touch";
            this.m_TouchUp = "No Touch";

            this.NotifyPropertyChanged("TouchDown");
            this.NotifyPropertyChanged("TouchUp");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListView_TouchDown(object sender, TouchEventArgs e)
        {
            this.m_TouchDownTimer.Start();
            this.m_TouchDown = "Touch Down";
            this.NotifyPropertyChanged("TouchDown");
        }

        private void ListView_TouchUp(object sender, TouchEventArgs e)
        {
            this.m_TouchDownTimer.Stop();
            this.m_TouchUp = "Touch Up";
            this.NotifyPropertyChanged("TouchUp");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.List.SelectedItem != null)
            {                             
                
            }
        }
    }
}
