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
	public partial class CytologyIcd9EntryPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder m_CytologyClientOrderClone;

		public CytologyIcd9EntryPage(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder cytologyClientOrderClone)
        {
			this.m_CytologyClientOrderClone = cytologyClientOrderClone;
            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(CytologyIcd9EntryPage_Loaded);            
        }

		private void CytologyIcd9EntryPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxIcd9Code.Focus();
        }

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
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
					Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
					Return(this, args);
				}
			}
            
		}

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder cytologyClientOrderClone)
		{
			DataValidator dataValidator = new DataValidator();

			BindingExpression icd9CodeBindingExpression = this.TextBoxIcd9Code.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Shared.ValidationResult icd9CodeDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsIcd9CodeDataTypeValid(this.TextBoxIcd9Code.Text);
			dataValidator.Add(new DataValidatorItem(icd9CodeDataTypeValidationResult, icd9CodeBindingExpression, cytologyClientOrderClone.IsIcd9CodeDomainValid));

			return dataValidator;
		}

		private bool ValidateAndUpdateBindingSources(bool showMessages)
		{
			bool result = true;
			if (this.ValidateIcd9Code(showMessages) == true)
			{
				BindingExpression bindingExpression = this.TextBoxIcd9Code.GetBindingExpression(TextBox.TextProperty);
				bindingExpression.UpdateSource();
			}
			else
			{
				result = false;
				if (showMessages == true)
				{
					return result;
				}
			}

			return result;
		}

		private bool ValidateIcd9Code(bool showMessages)
		{
			bool result = true;
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.IsIcd9CodeNotBlank();
			if (methodResult.Success == false)
			{
				result = false;
				if (showMessages == true)
				{
					MessageBox.Show(methodResult.Message);
				}
			}
			return result;
		}

		private YellowstonePathology.Business.Rules.MethodResult IsIcd9CodeNotBlank()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			result.Success = true;

			if (string.IsNullOrEmpty(this.TextBoxIcd9Code.Text) == true)
			{
				result.Message = "Please enter an ICD9 Code.";
				result.Success = false;
			}

			return result;
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
