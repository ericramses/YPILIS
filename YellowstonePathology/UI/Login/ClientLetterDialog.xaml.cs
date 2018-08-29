using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for ClientLetterDialog.xaml
    /// </summary>
    public partial class ClientLetterDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;
        
		StringBuilder m_AbnEventComment;
		StringBuilder m_InfoEventComment;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.PanelSetOrder m_PanelSetOrder;
        private string m_PatientNameWithBirthDate;                            
				
		string m_TestName;		

		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public ClientLetterDialog(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;                                              			
            this.m_SystemIdentity = systemIdentity;

            InitializeComponent();
            this.DataContext = this;

            this.m_AbnEventComment = new StringBuilder();
            this.m_InfoEventComment = new StringBuilder();
		}

        public string PatientName
        {
            get { return this.m_AccessionOrder.PatientDisplayName; }
        }

        public string ClientName
        {
            get { return this.m_AccessionOrder.ClientName; }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }		

        private void CreateLetterBody()
        {			
			this.m_PatientNameWithBirthDate = this.m_AccessionOrder.PatientDisplayName;
			if (this.m_AccessionOrder.PBirthdate.HasValue) this.m_PatientNameWithBirthDate = this.m_PatientNameWithBirthDate + " (DOB:" + this.m_AccessionOrder.PBirthdate.Value.ToShortDateString() +")";
			StringBuilder result = new StringBuilder();
			bool created = CreateMissingABN(result);
			if(!created) created = CreateMissingInfo(result);
			if (!created) created = CreateMissingSignature(result);
			this.TextBoxLetterBody.Text = result.ToString();			
        }

        private void ButtonCreateLetterBody_Click(object sender, RoutedEventArgs e)
        {
			this.CreateLetterBody();		
        }

        private void ButtonFaxLetter_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

            if (this.m_AccessionOrder.ClientId != 0)
            {
				YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
				string letterBody = this.TextBoxLetterBody.Text;
				
                Business.Test.MissingInformation.MissingInformationWordDocument missingInformationWordDocument = new Business.Test.MissingInformation.MissingInformationWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal, letterBody);
                missingInformationWordDocument.Render();									

                YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(client.Fax, "Missing Information", YellowstonePathology.Properties.Settings.Default.ClientMissingInformationLetterFileName);                								               
            }
            else
            {
                MessageBox.Show("Client must be selected before a fax can be generated.");
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }        

		private void AppendToEventDescription(StringBuilder stringBuilder, string msg)
		{
			if (stringBuilder.Length > 9)
			{
				stringBuilder.Append("; ");
			}
			stringBuilder.Append(msg);
		}        

		private bool CreateMissingABN(StringBuilder result)
		{
			bool res = false;
			if (this.CheckBoxABNDate.IsChecked == true ||
				this.CheckBoxABNEstimatedCost.IsChecked == true ||
				this.CheckBoxABNIdentificationNumber.IsChecked == true ||
				this.CheckBoxABNLaboratory.IsChecked == true ||
				this.CheckBoxABNNotifier.IsChecked == true ||
				this.CheckBoxABNOptionBoxChecked.IsChecked == true ||
				this.CheckBoxABNPatientName.IsChecked == true)
			{
				MissingABNLetterBody missingABNLetterBody = new MissingABNLetterBody();
				missingABNLetterBody.GetLetterBody(result, this.m_PatientNameWithBirthDate,
					this.CheckBoxABNDate.IsChecked == true,
					this.CheckBoxABNEstimatedCost.IsChecked == true,
					this.CheckBoxABNIdentificationNumber.IsChecked == true,
					this.CheckBoxABNLaboratory.IsChecked == true,
					this.CheckBoxABNNotifier.IsChecked == true,
					this.CheckBoxABNOptionBoxChecked.IsChecked == true,
					this.CheckBoxABNPatientName.IsChecked == true);

				res = true;

				if (this.CheckBoxABNDate.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Date");
				if (this.CheckBoxABNEstimatedCost.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Estimate Cost ");
				if (this.CheckBoxABNIdentificationNumber.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Identification Number ");
				if (this.CheckBoxABNLaboratory.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Laboratory Tests ");
				if (this.CheckBoxABNNotifier.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Notifier ");
				if (this.CheckBoxABNOptionBoxChecked.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Option Box Checked ");
				if (this.CheckBoxABNPatientName.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Patient Name ");
			}
			return res;
		}

		private bool CreateMissingInfo(StringBuilder result)
		{
			bool res = false;
			if (this.CheckBoxABN.IsChecked == true ||
				this.CheckBoxCytologyDXCode.IsChecked == true ||
				this.CheckBoxPatientDemographics.IsChecked == true ||
				this.CheckBoxNGCTDXCode.IsChecked == true ||
				this.CheckBoxTestType.IsChecked == true ||
				this.CheckBoxOrderingPhysician.IsChecked == true ||
				this.CheckBoxCollectionDate.IsChecked == true)
			{
				MissingInfoLetterBody missingInfoLetterBody = new MissingInfoLetterBody();
				missingInfoLetterBody.GetLetterBody(result, this.m_PatientNameWithBirthDate,
					this.CheckBoxABN.IsChecked == true,
					this.CheckBoxCytologyDXCode.IsChecked == true,
					this.CheckBoxPatientDemographics.IsChecked == true,
					this.CheckBoxNGCTDXCode.IsChecked == true,
					this.CheckBoxTestType.IsChecked == true,
					this.CheckBoxOrderingPhysician.IsChecked == true,
					this.CheckBoxCollectionDate.IsChecked == true);

				res = true;

				if (this.CheckBoxABN.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "ABN");
				if (this.CheckBoxCytologyDXCode.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Diagnosis Code");
				if (this.CheckBoxPatientDemographics.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Patient Demographics");
				if (this.CheckBoxNGCTDXCode.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "NG/CT Diagnosis Code");
				if (this.CheckBoxTestType.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Test Type");
				if (this.CheckBoxOrderingPhysician.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Ordering Physician");
				if (this.CheckBoxCollectionDate.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Collection Date");
			}            
			return res;
		}

		private bool CreateMissingSignature(StringBuilder result)
		{
			bool res = false;
			this.m_TestName = string.Empty;
			if (this.CheckBoxMissingSignatureSurgical.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureSurgical.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureCytology.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureCytology.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureFlow.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureFlow.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureMolecular.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureMolecular.Tag.ToString();
			}
			if (this.m_TestName.Length > 0)
			{
				MissingSignatureLetterBody missingSignatureLetterBody = new MissingSignatureLetterBody();
				missingSignatureLetterBody.GetLetterBody(result, this.m_TestName, this.m_PatientNameWithBirthDate, this.m_AccessionOrder.PhysicianName);				
			}
			return res;
		}
	}
}
