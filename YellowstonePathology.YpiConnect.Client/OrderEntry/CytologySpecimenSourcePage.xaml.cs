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
	public partial class CytologySpecimenSourcePage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetailClone;
		private List<string> m_SourceTypes;

		public CytologySpecimenSourcePage(YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone)
        {
			this.m_ClientOrderDetailClone = clientOrderDetailClone;

			this.m_SourceTypes = new List<string>();
			this.m_SourceTypes.Add("Cervical/EndoCervical");
			this.m_SourceTypes.Add("Vaginal");

			InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(CytologySpecimenSourcePage_Loaded);            
        }

		private void CytologySpecimenSourcePage_Loaded(object sender, RoutedEventArgs e)
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

		public List<string> SourceTypes
		{
			get { return this.m_SourceTypes; }
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
			DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
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
			DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
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

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone)
		{
			DataValidator dataValidator = new DataValidator();

			string valueToValidate = string.Empty;
			if (this.ListBoxSpecimenSource.SelectedItem != null)
			{
				valueToValidate = this.ListBoxSpecimenSource.SelectedItem.ToString();
			}

			BindingExpression specimenSourceBindingExpression = this.ListBoxSpecimenSource.GetBindingExpression(ListBox.SelectedValueProperty);
			YellowstonePathology.Shared.ValidationResult specimenSourceDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail.IsSpecimenSourceDataTypeValid(valueToValidate);
			dataValidator.Add(new DataValidatorItem(specimenSourceDataTypeValidationResult, specimenSourceBindingExpression, clientOrderDetailClone.IsSpecimenSourceDomainValid));

			return dataValidator;
		}

		private bool ValidateAndUpdateBindingSources(bool showMessages)
		{
			bool result = true;
			if (this.ValidateSpecimenSource(showMessages) == true)
			{
				BindingExpression bindingExpression = this.ListBoxSpecimenSource.GetBindingExpression(ListBox.SelectedValueProperty);
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

        private bool ValidateSpecimenSource(bool showMessages)
        {
            bool result = true;
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.IsSpecimenSourceSelected();
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

        private YellowstonePathology.Business.Rules.MethodResult IsSpecimenSourceSelected()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = true;

            if (this.ListBoxSpecimenSource.SelectedItem == null)
            {
                result.Message = "Please select a specimen source.";
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
			DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
			dataValidator.UpdateValidBindingSources();
		}
	}
}
