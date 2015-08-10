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
	public partial class CallbackNumberPage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail m_SurgicalClientOrderDetailClone;

		public CallbackNumberPage(YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail surgicalClientOrderDetailClone)
        {
			this.m_SurgicalClientOrderDetailClone = surgicalClientOrderDetailClone;

            InitializeComponent();

			this.DataContext = this.SurgicalClientOrderDetailClone;
			this.Loaded += new RoutedEventHandler(CallbackNumberPage_Loaded);            
        }

		private void CallbackNumberPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxCallbackNumber.Focus();
        }

        public YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail SurgicalClientOrderDetailClone
		{
			get { return this.m_SurgicalClientOrderDetailClone; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			DataValidator dataValidator = this.CreateDataValidator(this.m_SurgicalClientOrderDetailClone);
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
			DataValidator dataValidator = this.CreateDataValidator(this.m_SurgicalClientOrderDetailClone);
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

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail surgicalClientOrderDetailClone)
		{
			DataValidator dataValidator = new DataValidator();
			BindingExpression callbackNumberBindingExpression = this.TextBoxCallbackNumber.GetBindingExpression(TextBox.TextProperty);
			Shared.ValidationResult callbackNumberDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.SurgicalClientOrderDetail.IsCallbackNumberDataTypeValid(this.TextBoxCallbackNumber.Text);
			dataValidator.Add(new DataValidatorItem(callbackNumberDataTypeValidationResult, callbackNumberBindingExpression, surgicalClientOrderDetailClone.IsCallbackNumberDomainValid));
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
			DataValidator dataValidator = this.CreateDataValidator(this.m_SurgicalClientOrderDetailClone);
			dataValidator.UpdateValidBindingSources();
		}
	}
}
