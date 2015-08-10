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
	public partial class PlacentalPathologyQuestionnaireTestOrderPage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail m_PlacentaClientOrderDetail;

		public PlacentalPathologyQuestionnaireTestOrderPage(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail placentaClientOrderDetail)
        {
			this.m_PlacentaClientOrderDetail = placentaClientOrderDetail;
            InitializeComponent();

			this.DataContext = this.m_PlacentaClientOrderDetail;
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Shared.DataValidator dataValidator = this.CreateDataValidator(this.m_PlacentaClientOrderDetail);
			dataValidator.UpdateValidBindingSources();
			YellowstonePathology.Shared.ValidationResult validationResult = dataValidator.ValidateDataTypes();
			if (validationResult.IsValid == false)
			{
				MessageBox.Show("A Pathology Test Order must be selected.");
			}
			else
			{
				validationResult = dataValidator.ValidateDomain();
				if (validationResult.IsValid == false)
				{
					MessageBox.Show("A Pathology Test Order must be selected.");
				}
				else
				{
					if (this.m_PlacentaClientOrderDetail.DateSubmitted.HasValue == false)
					{
						this.m_PlacentaClientOrderDetail.DateSubmitted = DateTime.Now;
						this.m_PlacentaClientOrderDetail.SubmittedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
					}
					Shared.PageNavigationReturnEventArgs args = new Shared.PageNavigationReturnEventArgs(Shared.PageNavigationDirectionEnum.Next, null);
					Return(this, args);
				}
			}
		}

        private YellowstonePathology.Shared.DataValidator CreateDataValidator(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail clientOrderDetailClone)
		{
            YellowstonePathology.Shared.DataValidator dataValidator = new YellowstonePathology.Shared.DataValidator();

			BindingExpression grossExamBindingExpression = this.CheckBoxGrossExam.GetBindingExpression(CheckBox.IsCheckedProperty);
			YellowstonePathology.Shared.ValidationResult grossExamDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail.IsGrossExamDataTypeValid(this.CheckBoxGrossExam.IsChecked.Value);
            dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(grossExamDataTypeValidationResult, grossExamBindingExpression, clientOrderDetailClone.IsGrossExamDomainValid));

			BindingExpression cytogeneticsBindingExpression = this.CheckBoxCytogenetics.GetBindingExpression(CheckBox.IsCheckedProperty);
			YellowstonePathology.Shared.ValidationResult cytogeneticsDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail.IsCytogeneticsDataTypeValid(this.CheckBoxCytogenetics.IsChecked.Value);
            dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(cytogeneticsDataTypeValidationResult, cytogeneticsBindingExpression, clientOrderDetailClone.IsCytogeneticsDomainValid));

			BindingExpression completeExamBindingExpression = this.CheckBoxCompleteExam.GetBindingExpression(CheckBox.IsCheckedProperty);
			YellowstonePathology.Shared.ValidationResult completeExamDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail.IsCompleteExamDataTypeValid(this.CheckBoxCompleteExam.IsChecked.Value);
            dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(completeExamDataTypeValidationResult, completeExamBindingExpression, clientOrderDetailClone.IsCompleteExamDomainValid));

			BindingExpression otherExamBindingExpression = this.TextBoxOtherExam.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Shared.ValidationResult otherExamDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail.IsOtherExamDataTypeValid(this.TextBoxOtherExam.Text);
            dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(otherExamDataTypeValidationResult, otherExamBindingExpression, clientOrderDetailClone.IsOtherExamDomainValid));

			return dataValidator;
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
		}

		public void UpdateBindingSources()
		{
		}
	}
}
