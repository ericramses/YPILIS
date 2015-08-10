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
	public partial class CytologyScreeningTypePage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder m_CytologyClientOrderClone;
		private List<string> m_ScreeningTypes;

        public CytologyScreeningTypePage(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder cytologyClientOrderClone)
        {
            this.m_CytologyClientOrderClone = cytologyClientOrderClone;

			this.m_ScreeningTypes = new List<string>();
			this.m_ScreeningTypes.Add("Routine Screening Pap (V76.2)");
            this.m_ScreeningTypes.Add("Routine Screening Pap (V72.31)");
			this.m_ScreeningTypes.Add("Vaginal Screening, No Cervix (V76.47)");
			this.m_ScreeningTypes.Add("Screen Other Sites (V76.49)");
			this.m_ScreeningTypes.Add("High Risk Screening Pap (V15.89)");
			this.m_ScreeningTypes.Add("Diagnostic Pap");
            this.m_ScreeningTypes.Add("Other");

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(CytologyIcd9CodingPage_Loaded);            
        }

		private void CytologyIcd9CodingPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			BindingExpression bindingExpression = this.ListBoxScreeningType.GetBindingExpression(ListBox.SelectedValueProperty);
			bindingExpression.UpdateTarget();
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public List<string> ScreeningTypes
		{
			get { return this.m_ScreeningTypes; }
		}

        public YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder ClientOrderClone
		{
            get { return this.m_CytologyClientOrderClone; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            DataValidator dataValidator = this.CreateDataValidator(this.m_CytologyClientOrderClone);
			dataValidator.UpdateValidBindingSources();
			YellowstonePathology.Shared.ValidationResult validationResult = dataValidator.ValidateDataTypes();
			if (validationResult.IsValid == false)
			{
				MessageBox.Show(validationResult.Message);
			}
			else
			{
				Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
				Return(this, args);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{            
            DataValidator dataValidator = this.CreateDataValidator(this.m_CytologyClientOrderClone);
			dataValidator.UpdateValidBindingSources();
			YellowstonePathology.Shared.ValidationResult validationResult = dataValidator.ValidateDataTypes();

			if (validationResult.IsValid == false)
			{
				MessageBox.Show(validationResult.Message);
			}
			else
			{
				validationResult = dataValidator.ValidateDomain();
				if (validationResult.IsValid == false)
				{
					MessageBox.Show(validationResult.Message);
				}
				else
				{
                    Shared.PageNavigationReturnEventArgs args = null;
                    if (this.m_CytologyClientOrderClone.ScreeningType == "Diagnositic Pap" || this.m_CytologyClientOrderClone.ScreeningType == "Other")
                    {
                        args = new CytologyScreeningTypePageEventArgs(true, Shared.PageNavigationDirectionEnum.Next, null);
                    }
                    else
                    {
                        args = new CytologyScreeningTypePageEventArgs(false, Shared.PageNavigationDirectionEnum.Next, null);
                    }
					Return(this, args);              
				}
			}             
		}

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder clientOrderClone)
		{
			DataValidator dataValidator = new DataValidator();

			BindingExpression screeningTypeBindingExpression = this.ListBoxScreeningType.GetBindingExpression(ListBox.SelectedValueProperty);
			string valueToValidate = null;
			if (this.ListBoxScreeningType.SelectedItem != null)
			{
				valueToValidate = this.ListBoxScreeningType.SelectedItem.ToString();
			}
			YellowstonePathology.Shared.ValidationResult screeningTypeDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsScreeningTypeDataTypeValid(valueToValidate);
			dataValidator.Add(new DataValidatorItem(screeningTypeDataTypeValidationResult, screeningTypeBindingExpression, clientOrderClone.IsScreeningTypeDomainValid));

			return dataValidator;
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			bool result = true;
			if (CytologySpecimenNavigationGroup.Instance.IsInGroup(pageNavigatingTo) == true)
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
			DataValidator dataValidator = this.CreateDataValidator(this.m_CytologyClientOrderClone);
			dataValidator.UpdateValidBindingSources();
		}
	}
}
