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
	public partial class CytologyTestOrderPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        private YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder m_CytologyClientOrderClone;

        public CytologyTestOrderPage(YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder cytologyClientOrderClone)
        {
            this.m_CytologyClientOrderClone = cytologyClientOrderClone;

            InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(CytologyTestOrderPage_Loaded);            
        }

		private void CytologyTestOrderPage_Loaded(object sender, RoutedEventArgs e)
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

			BindingExpression reflexHPVBindingExpression = this.CheckBoxReflexHPV.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult reflexHPVDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsReflexHPVDataTypeValid(this.CheckBoxReflexHPV.IsChecked.Value);
            dataValidator.Add(new DataValidatorItem(reflexHPVDataTypeValidationResult, reflexHPVBindingExpression, clientOrderClone.IsReflexHPVDomainValid));

			BindingExpression routineHPVTestingBindingExpression = this.CheckBoxRoutineHPVTesting.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult routineHPVTestingDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsRoutineHPVTestingDataTypeValid(this.CheckBoxRoutineHPVTesting.IsChecked.Value);
            dataValidator.Add(new DataValidatorItem(routineHPVTestingDataTypeValidationResult, routineHPVTestingBindingExpression, clientOrderClone.IsRoutineHPVTestingDomainValid));

			BindingExpression nGCTTestingBindingExpression = this.CheckBoxNGCTTesting.GetBindingExpression(CheckBox.IsCheckedProperty);
            YellowstonePathology.Shared.ValidationResult nGCTTestingDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsReflexHPVDataTypeValid(this.CheckBoxNGCTTesting.IsChecked.Value);
            dataValidator.Add(new DataValidatorItem(nGCTTestingDataTypeValidationResult, nGCTTestingBindingExpression, clientOrderClone.IsNGCTTestingDomainValid));

			BindingExpression trichomonasVaginalisBindingExpression = this.CheckBoxTrichomonasVaginalis.GetBindingExpression(CheckBox.IsCheckedProperty);
			YellowstonePathology.Shared.ValidationResult trichomonasVaginalisDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.CytologyClientOrder.IsTrichomonasVaginalisDataTypeValid(this.CheckBoxNGCTTesting.IsChecked.Value);
			dataValidator.Add(new DataValidatorItem(trichomonasVaginalisDataTypeValidationResult, trichomonasVaginalisBindingExpression, clientOrderClone.IsTrichomonasVaginalisDomainValid));

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
