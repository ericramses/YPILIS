﻿using System;
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
	public partial class SurgicalClientOrderInformationPage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
        
		private YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder m_ClientOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public SurgicalClientOrderInformationPage(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder clientOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {			
			this.m_ClientOrder = clientOrder;
			this.m_ObjectTracker = objectTracker;

			InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(SurgicalClientOrderInformationPage_Loaded);            
        }

		private void SurgicalClientOrderInformationPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxReportCopyTo.Focus();
        }

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Validation.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrder);
			dataValidator.UpdateValidBindingSources();
			YellowstonePathology.Business.Validation.ValidationResult validationResult = dataValidator.ValidateDataTypes();
			if (validationResult.IsValid == false)
			{
				MessageBox.Show(validationResult.Message);
			}
			else
			{
				YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
				Return(this, args);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Validation.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrder);
			dataValidator.UpdateValidBindingSources();
			YellowstonePathology.Business.Validation.ValidationResult validationResult = dataValidator.ValidateDataTypes();
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
					YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next, null);
					Return(this, args);
				}
			}
		}

		private YellowstonePathology.Business.Validation.DataValidator CreateDataValidator(YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder clientOrder)
		{
			YellowstonePathology.Business.Validation.DataValidator dataValidator = new YellowstonePathology.Business.Validation.DataValidator();

			BindingExpression reportCopyToBindingExpression = this.TextBoxReportCopyTo.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Business.Validation.ValidationResult reportCopyToDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrder.IsReportCopyToDataTypeValid(this.TextBoxReportCopyTo.Text);
			dataValidator.Add(new YellowstonePathology.Business.Validation.DataValidatorItem(reportCopyToDataTypeValidationResult, reportCopyToBindingExpression, clientOrder.IsReportCopyToDomainValid));

			BindingExpression clinicalHistoryBindingExpression = this.TextBoxClinicalHistory.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Business.Validation.ValidationResult clinicalHistoryDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.ClientOrder.IsClinicalHistoryDataTypeValid(this.TextBoxClinicalHistory.Text);
			dataValidator.Add(new YellowstonePathology.Business.Validation.DataValidatorItem(clinicalHistoryDataTypeValidationResult, clinicalHistoryBindingExpression, clientOrder.IsClinicalHistoryDomainValid));

			BindingExpression preOpDiagnosisBindingExpression = this.TextBoxPreOpDiagnosis.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Business.Validation.ValidationResult preOpDiagnosisDataTypeValidationResult = YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder.IsPreOpDiagnosisDataTypeValid(this.TextBoxPreOpDiagnosis.Text);
			dataValidator.Add(new YellowstonePathology.Business.Validation.DataValidatorItem(preOpDiagnosisDataTypeValidationResult, preOpDiagnosisBindingExpression, clientOrder.IsPreOpDiagnosisDomainValid));

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

		public void Save(bool releaseLock)
		{
            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(this.m_ClientOrder, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);
		}

		public void UpdateBindingSources()
		{
			YellowstonePathology.Business.Validation.DataValidator dataValidator = this.CreateDataValidator(this.m_ClientOrder);
			dataValidator.UpdateValidBindingSources();
		}
	}
}
