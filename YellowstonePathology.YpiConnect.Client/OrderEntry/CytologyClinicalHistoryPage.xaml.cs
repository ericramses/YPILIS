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
	public partial class CytologyClinicalHistoryPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder m_CytologyClientOrderClone;

        public CytologyClinicalHistoryPage(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder cytologyClientOrderClone)
        {
			this.m_CytologyClientOrderClone = cytologyClientOrderClone;

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(CytologyClinicalHistoryPage_Loaded);            
        }

		private void CytologyClinicalHistoryPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxLMP.Focus();
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

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder clientOrderClone)
		{
			DataValidator dataValidator = new DataValidator();

			BindingExpression lMPBindingExpression = this.TextBoxLMP.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Shared.ValidationResult lMPBindingDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsLMPBindingDataTypeValid(this.TextBoxLMP.Text);
			dataValidator.Add(new DataValidatorItem(lMPBindingDataTypeValidationResult, lMPBindingExpression, clientOrderClone.IsLMPDomainValid));

			BindingExpression hysterectomyBindingExpression = this.CheckBoxHysterectomy.GetBindingExpression(CheckBox.IsCheckedProperty);
			YellowstonePathology.Shared.ValidationResult hysterectomyDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsHysterectomyDataTypeValid(this.CheckBoxHysterectomy.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(hysterectomyDataTypeValidationResult, hysterectomyBindingExpression, clientOrderClone.IsHysterectomyDomainValid));

			BindingExpression cervixPresentBindingExpression = this.CheckBoxCervixPresent.GetBindingExpression(CheckBox.IsCheckedProperty);
			YellowstonePathology.Shared.ValidationResult cervixPresentDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsCervixPresentDataTypeValid(this.CheckBoxCervixPresent.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(cervixPresentDataTypeValidationResult, cervixPresentBindingExpression, clientOrderClone.IsCervixPresentDomainValid));

			BindingExpression abnormalBleedingBindingExpression = this.CheckBoxAbnormalBleeding.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult abnormalBleedingDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsAbnormalBleedingDataTypeValid(this.CheckBoxAbnormalBleeding.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(abnormalBleedingDataTypeValidationResult, abnormalBleedingBindingExpression, clientOrderClone.IsAbnormalBleedingDomainValid));

			BindingExpression birthControlBindingExpression = this.CheckBoxBirthControl.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult birthControlDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsBirthControlDataTypeValid(this.CheckBoxBirthControl.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(birthControlDataTypeValidationResult, birthControlBindingExpression, clientOrderClone.IsBirthControlDomainValid));

			BindingExpression hormoneTherapyBindingExpression = this.CheckBoxHormoneTherapy.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult hormoneTherapyDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsHormoneTherapyDataTypeValid(this.CheckBoxHormoneTherapy.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(hormoneTherapyDataTypeValidationResult, hormoneTherapyBindingExpression, clientOrderClone.IsHormoneTherapyDomainValid));

			BindingExpression previousNormalPapBindingExpression = this.CheckBoxPreviousNormalPap.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult previousNormalPapDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPreviousNormalPapDataTypeValid(this.CheckBoxPreviousNormalPap.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(previousNormalPapDataTypeValidationResult, previousNormalPapBindingExpression, clientOrderClone.IsPreviousNormalPapDomainValid));

			BindingExpression previousNormalPapDateBindingExpression = this.TextBoxPreviousNormalPapDate.GetBindingExpression(TextBox.TextProperty);
            YellowstonePathology.Shared.ValidationResult previousNormalPapDateDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPreviousNormalPapDateDataTypeValid(this.TextBoxPreviousNormalPapDate.Text);
			dataValidator.Add(new DataValidatorItem(previousNormalPapDateDataTypeValidationResult, previousNormalPapDateBindingExpression, clientOrderClone.IsPreviousNormalPapDateDomainValid));

			BindingExpression previousAbnormalPapBindingExpression = this.CheckBoxPreviousAbnormalPap.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult previousAbnormalPapDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPreviousAbnormalPapDataTypeValid(this.CheckBoxPreviousAbnormalPap.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(previousAbnormalPapDataTypeValidationResult, previousAbnormalPapBindingExpression, clientOrderClone.IsPreviousAbnormalPapDomainValid));

			BindingExpression previousAbnormalPapDateBindingExpression = this.TextBoxPreviousAbnormalPapDate.GetBindingExpression(TextBox.TextProperty);
            YellowstonePathology.Shared.ValidationResult previousAbnormalPapDateDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPreviousAbnormalPapDateDataTypeValid(this.TextBoxPreviousAbnormalPapDate.Text);
			dataValidator.Add(new DataValidatorItem(previousAbnormalPapDateDataTypeValidationResult, previousAbnormalPapDateBindingExpression, clientOrderClone.IsPreviousAbnormalPapDateDomainValid));

			BindingExpression previousBiopsyBindingExpression = this.CheckBoxPreviousBiopsy.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult previousBiopsyDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPreviousBiopsyDataTypeValid(this.CheckBoxPreviousBiopsy.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(previousBiopsyDataTypeValidationResult, previousBiopsyBindingExpression, clientOrderClone.IsPreviousBiopsyDomainValid));

			BindingExpression previousBiopsyDateBindingExpression = this.TextBoxPreviousBiopsyDate.GetBindingExpression(TextBox.TextProperty);
            YellowstonePathology.Shared.ValidationResult previousBiopsyDateDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPreviousBiopsyDateDataTypeValid(this.TextBoxPreviousBiopsyDate.Text);
			dataValidator.Add(new DataValidatorItem(previousBiopsyDateDataTypeValidationResult, previousBiopsyDateBindingExpression, clientOrderClone.IsPreviousBiopsyDateDomainValid));

			BindingExpression prenatalBindingExpression = this.CheckBoxPrenatal.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult prenatalDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPrenatalDataTypeValid(this.CheckBoxPrenatal.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(prenatalDataTypeValidationResult, prenatalBindingExpression, clientOrderClone.IsPrenatalDomainValid));

			BindingExpression postpartumBindingExpression = this.CheckBoxPostpartum.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult postpartumDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPostpartumDataTypeValid(this.CheckBoxPostpartum.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(postpartumDataTypeValidationResult, postpartumBindingExpression, clientOrderClone.IsPostpartumDomainValid));

			BindingExpression postmenopausalBindingExpression = this.CheckBoxPostmenopausal.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult postmenopausalDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsPostmenopausalDataTypeValid(this.CheckBoxPostmenopausal.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(postmenopausalDataTypeValidationResult, postmenopausalBindingExpression, clientOrderClone.IsPostmenopausalDomainValid));

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
