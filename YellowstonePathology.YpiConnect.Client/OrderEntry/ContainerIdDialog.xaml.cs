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
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for SvhStartOrderDialog.xaml
    /// </summary>
    public partial class ContainerIdDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        StringBuilder m_KeyboardInput;
        string m_ContainerId;

        YellowstonePathology.Domain.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;

        public ContainerIdDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Domain.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            this.m_ClientOrderCollection = clientOrderCollection;
            this.m_ClientOrder = clientOrder;
            this.m_KeyboardInput = new StringBuilder();            

            InitializeComponent();

            this.TextBlockContainerId.TextChanged +=new TextChangedEventHandler(TextBlockContainerId_TextChanged);
            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(ContainerIdDialog_Loaded);
        }

        private void ContainerIdDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBlockContainerId.Focus();
        }

        public string ContainerId
        {
            get { return this.m_ContainerId; }
            set 
            {
                if (this.m_ContainerId != value)
                {
                    this.m_ContainerId = value;
                    this.NotifyPropertyChanged("ContainerId");
                }
            }
        }        

        private void ValidateContainerId()
		{
			int cnt = this.m_ClientOrderCollection[0].ClientOrderDetailCollection.Count - 1;

            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new Business.Rules.ExecutionStatus();
            Rules.ContainerIdValidation containerIdValidation = new Rules.ContainerIdValidation(executionStatus);
			containerIdValidation.Execute(this.m_ContainerId, this.m_ClientOrder, this.m_ClientOrderCollection);

            if (executionStatus.Halted == true)
            {
                this.TextBlockMessage.Text = executionStatus.GetResultMessage();                                
            }
            else
            {                
                this.DialogResult = true;
                this.Close();                
            }            
        }        
        
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }        

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            this.ContainerId = string.Empty;
			this.TextBlockMessage.Text = string.Empty;
        }

        private void TextBlockContainerId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ContainerId.Length > 0)
            {
                int stopCount = 40;
                if (this.ContainerId.StartsWith("\\") == true)
                {
                    stopCount = 42;
                }
                if (this.ContainerId.Length == stopCount)
                {
                    this.TextBlockContainerId.TextChanged -= TextBlockContainerId_TextChanged;
                    this.ContainerId = this.ContainerId.Replace(@"\", "");
                    this.ValidateContainerId();
                    this.TextBlockContainerId.TextChanged += TextBlockContainerId_TextChanged;
                }
            }            
        }        
    }
}
