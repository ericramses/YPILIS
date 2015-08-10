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
	public partial class FixationPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetailClone;

		public FixationPage(YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone)
        {
			this.m_ClientOrderDetailClone = clientOrderDetailClone;

			InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(FixationPage_Loaded);            
        }

		private void FixationPage_Loaded(object sender, RoutedEventArgs e)
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
			get { return YellowstonePathology.Domain.ClientOrder.Model.FixationTypes.Instance; }
		}

		public YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail ClientOrderDetailClone
        {
			get { return this.m_ClientOrderDetailClone; }
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

		private void HyperlinkSetFixationStartTimeToCurrent_Click(object sender, RoutedEventArgs e)
		{
			this.m_ClientOrderDetailClone.FixationStartTime = DateTime.Now;
			this.TextBoxFixationStartTime.Focus();
			this.TextBoxFixationStartTime.Select(TextBoxFixationStartTime.Text.Length, 0);            
		}

		private DataValidator CreateDataValidator(YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone)
        {
			DataValidator dataValidator = new DataValidator();

            /*
            BindingExpression collectionDateBindingExpression = this.TextBoxCollectionDate.GetBindingExpression(TextBox.TextProperty);
            YellowstonePathology.Shared.ValidationResult collectionDateDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail.IsCollectionDateDataTypeValid(this.TextBoxCollectionDate.Text);
			dataValidator.Add(new DataValidatorItem(collectionDateDataTypeValidationResult, collectionDateBindingExpression, clientOrderDetailClone.IsCollectionDateDomainValid));

			BindingExpression clientFixationBindingExpression = this.ListBoxFixationType.GetBindingExpression(ListBox.SelectedValueProperty);
			string valueToValidate = string.Empty;
			if (this.ListBoxFixationType.SelectedItem != null)
			{
				valueToValidate = this.ListBoxFixationType.SelectedItem.ToString();
			}
			YellowstonePathology.Shared.ValidationResult clientFixationDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail.IsClientFixationDataTypeValid(valueToValidate);
			dataValidator.Add(new DataValidatorItem(clientFixationDataTypeValidationResult, clientFixationBindingExpression, clientOrderDetailClone.IsClientFixationDomainValid));

			BindingExpression fixationStartTimeBindingExpression = this.TextBoxFixationStartTime.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Shared.ValidationResult fixationStartTimeDataTypeValidationResult = YellowstonePathology.Domain.ClientOrder.Model.ClientOrderDetail.IsFixationStartTimeDataTypeValid(this.TextBoxFixationStartTime.Text);
			dataValidator.Add(new DataValidatorItem(fixationStartTimeDataTypeValidationResult, fixationStartTimeBindingExpression, clientOrderDetailClone.IsFixationStartTimeDomainValid));
            */

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
			DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
			dataValidator.UpdateValidBindingSources();
		}
	}
}
