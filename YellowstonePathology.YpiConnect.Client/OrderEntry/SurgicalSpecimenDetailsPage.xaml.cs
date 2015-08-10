using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OrderEntryWindow.xaml
    /// </summary>
	public partial class SurgicalSpecimenDetailsPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail m_ClientOrderDetailClone;
		private List<string> m_FixationTypes;

		public SurgicalSpecimenDetailsPage(YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail clientOrderDetailClone)
        {
			this.m_ClientOrderDetailClone = clientOrderDetailClone;

			this.m_FixationTypes = new List<string>();
			this.m_FixationTypes.Add("Fresh");
			this.m_FixationTypes.Add("Formalin");
			this.m_FixationTypes.Add("B+ Fixative");
			this.m_FixationTypes.Add("Cytolyt");
			this.m_FixationTypes.Add("95% IPA");
			this.m_FixationTypes.Add("Thin Prep");

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(SurgicalSpecimenDetailsPage_Loaded);            
        }

		private void SurgicalSpecimenDetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
        }

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public List<string> FixationTypes
		{
			get { return this.m_FixationTypes; }
		}

		public YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail ClientOrderDetailClone
		{
			get { return this.m_ClientOrderDetailClone; }
		}

		private void TextBoxSpecimenDescription_Loaded(object sender, RoutedEventArgs e)
		{
			var textbox = sender as TextBox;
			if (textbox == null) return;
			textbox.Focus();
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
		}

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone)
		{
			DataValidator dataValidator = new DataValidator();
			return dataValidator;
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			bool result = true;
			if (SurgicalSpecimenNavigationGroup.Instance.IsInGroup(pageNavigatingTo) == true)
			{
				result = false;
			}
			return result;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
		}

		public void UpdateBindingSources()
		{
		}
	}
}
