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
	public partial class PlacentaDescriptionPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, Shared.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail m_ClientOrderDetailClone;
		private SurgicalOrderTypeCollection m_SurgicalOrderTypeCollection;

		public PlacentaDescriptionPage(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail clientOrderDetailClone)
        {
			this.m_ClientOrderDetailClone = clientOrderDetailClone;
			this.m_SurgicalOrderTypeCollection = new SurgicalOrderTypeCollection();

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(PlacentaDescriptionPage_Loaded);            
        }

		private void PlacentaDescriptionPage_Loaded(object sender, RoutedEventArgs e)
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

		public YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail ClientOrderDetailClone
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

        private YellowstonePathology.Shared.DataValidator CreateDataValidator(YellowstonePathology.Business.ClientOrder.Model.PlacentaClientOrderDetail clientOrderDetailClone)
		{
            YellowstonePathology.Shared.DataValidator dataValidator = new YellowstonePathology.Shared.DataValidator();

			//BindingExpression descriptionBindingExpression = this.TextBoxSpecimenDescription.GetBindingExpression(TextBox.TextProperty);
			//YellowstonePathology.Shared.ValidationResult descriptionDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail.IsDescriptionDataTypeValid(this.TextBoxSpecimenDescription.Text);
            //dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(descriptionDataTypeValidationResult, descriptionBindingExpression, clientOrderDetailClone.IsDescriptionDomainValid));

			//BindingExpression specialInstructionsBindingExpression = this.ComboBoxSpecialInstructions.GetBindingExpression(ComboBox.TextProperty);
			//YellowstonePathology.Shared.ValidationResult specialInstructionsDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail.IsSpecialInstructionsDataTypeValid(this.ComboBoxSpecialInstructions.Text);
            //dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(specialInstructionsDataTypeValidationResult, specialInstructionsBindingExpression, clientOrderDetailClone.IsSpecialInstructionsDomainValid));

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
	}
}
