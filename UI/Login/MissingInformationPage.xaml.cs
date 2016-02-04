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

namespace YellowstonePathology.UI.Login
{	
	public partial class MissingInformationPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
	{
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_NewText;     

		public MissingInformationPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{            
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            
			InitializeComponent();

			DataContext = this;

            Loaded += MissingInformationPage_Loaded;
            Unloaded += MissingInformationPage_Unloaded;        
		}

        private void MissingInformationPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void MissingInformationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        public string NewText
        {
            get { return this.m_NewText; }
            set { this.m_NewText = value; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}        

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            this.Next(this, new EventArgs());
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(this.m_AccessionOrder, false);
        }

        public void UpdateBindingSources()
		{

		}

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.m_NewText) == false)
            {
                string newDialog = this.m_SystemIdentity.User.DisplayName + ": " + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + Environment.NewLine + this.m_NewText;
                if (string.IsNullOrEmpty(this.m_AccessionOrder.CaseDialog) == false)
                {
                    this.m_AccessionOrder.CaseDialog = newDialog + Environment.NewLine + Environment.NewLine + this.m_AccessionOrder.CaseDialog;
                }
                else
                {
                    this.m_AccessionOrder.CaseDialog = newDialog;
                }

                this.m_NewText = null;               
            }            
        }
    }
}
