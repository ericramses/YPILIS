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
	public partial class SpecimenDescriptionPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail m_ClientOrderDetailClone;
		private SurgicalOrderTypeCollection m_SurgicalOrderTypeCollection;
		private bool m_OrderFrozenSectionEnabled;

		public SpecimenDescriptionPage(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail clientOrderDetailClone)
        {
			this.m_ClientOrderDetailClone = clientOrderDetailClone;
			this.m_SurgicalOrderTypeCollection = new SurgicalOrderTypeCollection();

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(SpecimenDescriptionPage_Loaded);            
        }

		private void SpecimenDescriptionPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
        }		

		public YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail ClientOrderDetailClone
		{
			get { return this.m_ClientOrderDetailClone; }
		}

		public SurgicalOrderTypeCollection SurgicalOrderTypeCollection
		{
			get { return this.m_SurgicalOrderTypeCollection; }
		}

		private void TextBoxSpecimenDescription_Loaded(object sender, RoutedEventArgs e)
		{
			var textbox = sender as TextBox;
			if (textbox == null) return;
			textbox.Focus();
		}

		public bool OrderFrozenSectionEnabled
		{
			get { return this.m_OrderFrozenSectionEnabled; }
		}

		private void ListBoxOrderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ListBoxOrderType.SelectedIndex == 0)
			{
				this.CheckBoxOrderFrozenSection.IsChecked = true;
				this.m_OrderFrozenSectionEnabled = true;
			}
			else
			{
				this.CheckBoxOrderFrozenSection.IsChecked = false;
				this.m_OrderFrozenSectionEnabled = false;
			}
			NotifyPropertyChanged("OrderFrozenSectionEnabled");
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Shared.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
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
            YellowstonePathology.Shared.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
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

        private YellowstonePathology.Shared.DataValidator CreateDataValidator(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail clientOrderDetailClone)
		{
            YellowstonePathology.Shared.DataValidator dataValidator = new YellowstonePathology.Shared.DataValidator();

			//BindingExpression descriptionBindingExpression = this.TextBoxSpecimenDescription.GetBindingExpression(TextBox.TextProperty);
			//YellowstonePathology.Shared.ValidationResult descriptionDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail.IsDescriptionDataTypeValid(this.TextBoxSpecimenDescription.Text);
            //dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(descriptionDataTypeValidationResult, descriptionBindingExpression, clientOrderDetailClone.IsDescriptionDomainValid));

			//BindingExpression specialInstructionsBindingExpression = this.ComboBoxSpecialInstructions.GetBindingExpression(ComboBox.TextProperty);
			//YellowstonePathology.Shared.ValidationResult specialInstructionsDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail.IsSpecialInstructionsDataTypeValid(this.ComboBoxSpecialInstructions.Text);
            //dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(specialInstructionsDataTypeValidationResult, specialInstructionsBindingExpression, clientOrderDetailClone.IsSpecialInstructionsDomainValid));

			//BindingExpression collectionDateBindingExpression = this.TextBoxCollectionDate.GetBindingExpression(TextBox.TextProperty);
			//YellowstonePathology.Shared.ValidationResult collectionDateDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail.IsCollectionDateDataTypeValid(this.TextBoxCollectionDate.Text);
            //dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(collectionDateDataTypeValidationResult, collectionDateBindingExpression, clientOrderDetailClone.IsCollectionDateDomainValid));

			if(this.m_ClientOrderDetailClone.OrderTypeCode == "SRGCL")
			{
				BindingExpression immediateExamBindingExpression = this.ListBoxOrderType.GetBindingExpression(ListBox.SelectedValueProperty);
				bool? valueToValidate = null;
				if (this.ListBoxOrderType.SelectedItem != null)
				{
					SurgicalOrderType surgicalOrderType = (SurgicalOrderType)this.ListBoxOrderType.SelectedItem;
					valueToValidate = surgicalOrderType.Value;
				}
				Shared.ValidationResult immediateExamDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail.IsOrderImmediateExamDataTypeValid(valueToValidate);
                dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(immediateExamDataTypeValidationResult, immediateExamBindingExpression, clientOrderDetailClone.IsOrderImmediateExamDomainValid));

				BindingExpression frozenSectionBindingExpression = this.CheckBoxOrderFrozenSection.GetBindingExpression(CheckBox.IsCheckedProperty);
				Shared.ValidationResult frozenSectionDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail.IsOrderFrozenSectionDataTypeValid(this.CheckBoxOrderFrozenSection.IsChecked);
                dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(frozenSectionDataTypeValidationResult, frozenSectionBindingExpression, clientOrderDetailClone.IsOrderFrozenSectionDomainValid));

				BindingExpression callbackNumberBindingExpression = this.TextBoxCallbackNumber.GetBindingExpression(TextBox.TextProperty);
				Shared.ValidationResult callbackNumberDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrderDetail.IsCallbackNumberDataTypeValid(this.TextBoxCallbackNumber.Text);
                dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(callbackNumberDataTypeValidationResult, callbackNumberBindingExpression, clientOrderDetailClone.IsCallbackNumberDomainValid));
			}
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
            YellowstonePathology.Shared.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
			dataValidator.UpdateValidBindingSources();
		}

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
