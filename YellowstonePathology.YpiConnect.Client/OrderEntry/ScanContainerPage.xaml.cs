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
using System.Text.RegularExpressions;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OrderEntryWindow.xaml
    /// </summary>
	public partial class ScanContainerPage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetailClone;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private bool m_ContainerIsValid;        

		public ScanContainerPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone, YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
			this.m_ClientOrderDetailClone = clientOrderDetailClone;
            this.m_ClientOrder = clientOrder;            

            InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(ScanContainerPage_Loaded);
			this.TextBlockContainerId.TextChanged += TextBlockContainerId_TextChanged;
		}

		private void ScanContainerPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
			this.SetContainerIdValidity(false);
			if (this.ContainerIdValid == false)
			{
				this.TextBlockContainerId.Focus();
			}
		}        

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetailClone
		{
			get { return this.m_ClientOrderDetailClone; }
		}

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

		public bool ContainerIdValid
		{
			get { return this.m_ContainerIsValid; }
			set
			{
				this.m_ContainerIsValid = value;
				NotifyPropertyChanged("ContainerIdValid");
				NotifyPropertyChanged("ValidContainerIdVisibility");
			}
		}

		public Visibility ValidContainerIdVisibility
		{
			get
			{
				Visibility result = System.Windows.Visibility.Visible;
				if (this.m_ContainerIsValid == false)
				{
					result = System.Windows.Visibility.Hidden;
				}
				return result;
			}
		}

		private void SetContainerIdValidity(bool showMessage)
		{
			bool result = true;
			if (this.ContainerExists() == true)
			{
				result = false;
				if (showMessage == true)
				{
					MessageBox.Show("The container ID is a duplicate.");
				}
				this.TextBlockContainerId.Focus();
				this.TextBlockContainerId.Select(0, this.TextBlockContainerId.Text.Length);
			}
			else
			{
				YellowstonePathology.Business.Validation.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
				dataValidator.UpdateValidBindingSources();
				YellowstonePathology.Business.Validation.ValidationResult validationResult = dataValidator.ValidateDataTypes();
				if (validationResult.IsValid == false)
				{
					result = false;
					if (showMessage == true)
					{
						MessageBox.Show(validationResult.Message);
					}
					this.TextBlockContainerId.Focus();
					this.TextBlockContainerId.Select(0, this.TextBlockContainerId.Text.Length);
				}
				else
				{
					validationResult = dataValidator.ValidateDomain();
					if (validationResult.IsValid == false)
					{
						result = false;
						if (showMessage == true)
						{
							MessageBox.Show(validationResult.Message);
						}
						this.TextBlockContainerId.Focus();
						this.TextBlockContainerId.Select(0, this.TextBlockContainerId.Text.Length);
					}
				}
			}
			this.ContainerIdValid = result;
		}

		private bool ContainerExists()
		{
			YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
			YellowstonePathology.Business.ClientOrder.Model.ContainerIdLookupResponse containerIdLookupResponse = proxy.DoesContainerIdExist(this.TextBlockContainerId.Text, this.m_ClientOrderDetailClone.ClientOrderDetailId);
			return containerIdLookupResponse.Found;
		}

		private void ButtonClear_Click(object sender, RoutedEventArgs e)
		{
			this.TextBlockContainerId.Text = string.Empty;
			this.ClientOrderDetailClone.ContainerId = string.Empty;
			this.SetContainerIdValidity(false);
			this.TextBlockContainerId.Focus();
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.ContainerExists() == true)
			{
				this.TextBlockContainerId.TextChanged -= this.TextBlockContainerId_TextChanged;
				this.TextBlockContainerId.Text = this.m_ClientOrderDetailClone.ContainerId;
				this.TextBlockContainerId.TextChanged += this.TextBlockContainerId_TextChanged;
			}

			YellowstonePathology.Business.Validation.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
			dataValidator.UpdateValidBindingSources();
			YellowstonePathology.Business.Validation.ValidationResult validationResult = dataValidator.ValidateDataTypes();
			if (validationResult.IsValid == false)
			{
				MessageBox.Show(validationResult.Message);
				this.TextBlockContainerId.Focus();
				this.TextBlockContainerId.Select(0, this.TextBlockContainerId.Text.Length);
			}
			else
			{
				YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
				Return(this, args);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.SetContainerIdValidity(true);
			if (this.ContainerIdValid == true)
			{
				YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next, null);
				Return(this, args);
			}
		}

		private YellowstonePathology.Business.Validation.DataValidator CreateDataValidator(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetailClone)
		{
			YellowstonePathology.Business.Validation.DataValidator dataValidator = new YellowstonePathology.Business.Validation.DataValidator();

			BindingExpression containerIdBindingExpression = this.TextBlockContainerId.GetBindingExpression(TextBox.TextProperty);
			//YellowstonePathology.Shared.ValidationResult containerIdDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail.IsContainerIdDataTypeValid(this.TextBlockContainerId.Text);
            //dataValidator.Add(new YellowstonePathology.Shared.DataValidatorItem(containerIdDataTypeValidationResult, containerIdBindingExpression, clientOrderDetailClone.IsContainerIdDomainValid));

			return dataValidator;
		}

		private void TextBlockContainerId_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.TextBlockContainerId.Text.Length > 0)
			{
				int stopCount = 40;
				if (this.TextBlockContainerId.Text.StartsWith("\\") == true)
				{
					stopCount = 42;
				}
				if (this.TextBlockContainerId.Text.Length == stopCount)
				{
					this.TextBlockContainerId.TextChanged -= TextBlockContainerId_TextChanged;
					this.TextBlockContainerId.Text = this.TextBlockContainerId.Text.Replace(@"\", "");
					this.TextBlockContainerId.TextChanged += TextBlockContainerId_TextChanged;
					this.SetContainerIdValidity(true);
				}
			}
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
			YellowstonePathology.Business.Validation.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrderDetailClone);
			dataValidator.UpdateValidBindingSources();
		}

        private void ButtonEnterNewContainerId_Click(object sender, RoutedEventArgs e)
        {
            //string containerId = "CTNR" + System.Guid.NewGuid().ToString().ToUpper();
            //string containerId = "CTNRD3CFEA94-032E-4C23-86AD-E148C4253803";
            //this.TextBlockContainerId.Text = containerId;
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
