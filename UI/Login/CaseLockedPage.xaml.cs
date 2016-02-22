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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for CaseLockedPage.xaml
	/// </summary>
	public partial class CaseLockedPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void OKEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.AccessionOrderReturnEventArgs e);
		public event OKEventHandler OK;
                
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Case Lock Page";        
        private string m_PageMessage;
        

        public CaseLockedPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{            
            this.m_AccessionOrder = accessionOrder;
            this.m_PageMessage = "This case is locked by: " + this.m_AccessionOrder.LockAquiredByUserName + " on " + this.m_AccessionOrder.LockAquiredByHostName;                

			InitializeComponent();

			DataContext = this;            
		}

        public string PageMessage
        {
            get { return this.m_PageMessage; }            
        }       		

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            if (this.OK != null) this.OK(this, new CustomEventArgs.AccessionOrderReturnEventArgs(this.m_AccessionOrder));
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
