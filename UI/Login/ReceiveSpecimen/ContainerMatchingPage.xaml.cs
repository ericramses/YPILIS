using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login.ReceiveSpecimen
{
	/// <summary>
	/// Interaction logic for ContainerMatchingPage.xaml
	/// </summary>
	public partial class ContainerMatchingPage : UserControl, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia m_ClientOrderMedia;
		private string m_PageHeaderText = "Container matching page.";
        private ObservableCollection<string> m_FixationTypeCollection;

		public ContainerMatchingPage(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			this.m_PageNavigator = pageNavigator;
            this.m_FixationTypeCollection = YellowstonePathology.Business.Specimen.Model.FixationType.GetFixationTypeCollection();
			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();

            if (string.IsNullOrEmpty(clientOrderDetail.SpecimenNumberMatchStatus) == true)
            {
                clientOrderDetail.SpecimenNumberMatchStatus = "Not Determined";
            }

            if (string.IsNullOrEmpty(clientOrderDetail.SpecimenDescriptionMatchStatus) == true)
            {
                clientOrderDetail.SpecimenDescriptionMatchStatus = "Not Determined";
            }

            this.m_ClientOrderMedia = new Business.ClientOrder.Model.ClientOrderMedia();
			this.m_ClientOrderMedia.SpecimenNumberMatchStatus = clientOrderDetail.SpecimenNumberMatchStatus;
			this.m_ClientOrderMedia.SpecimenDescriptionMatchStatus = clientOrderDetail.SpecimenDescriptionMatchStatus;
			this.m_ClientOrderDetail = clientOrderDetail;

			InitializeComponent();

			this.DataContext = this;
		}

        public ObservableCollection<string> FixationTypeCollection
        {
            get { return this.m_FixationTypeCollection; }
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia ClientOrderMedia
		{
			get { return this.m_ClientOrderMedia; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
		{
			get { return this.m_ClientOrderDetail; }
		}

		private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
		{
			Border border = sender as Border;
			ContentPresenter contentPresenter = border.TemplatedParent as ContentPresenter;
			contentPresenter.HorizontalAlignment = HorizontalAlignment.Stretch;
		}
		
		private void ButtonSpecimenNumberMatchStatus_Click(object sender, RoutedEventArgs e)
		{            
            switch (this.m_ClientOrderMedia.SpecimenNumberMatchStatus)
            {
                case "Not Determined":
                    this.m_ClientOrderMedia.SpecimenNumberMatchStatus = "Matched";
					this.m_ClientOrderMedia.SpecimenNumber = this.ClientOrderDetail.SpecimenNumber.ToString();                    
                    this.m_ClientOrderDetail.SpecimenNumberMatchStatus = "Matched";                    
                    break;
                case "Matched":
                    this.m_ClientOrderMedia.SpecimenNumberMatchStatus = "Not Numbered";
					this.m_ClientOrderMedia.SpecimenNumber = "Not Numbered";
                    this.m_ClientOrderDetail.SpecimenNumberMatchStatus = "Not Numbered";
                    break;
                case "Not Numbered":
                    this.m_ClientOrderMedia.SpecimenNumberMatchStatus = "Not Matched";
					this.m_ClientOrderMedia.SpecimenNumber = "Not Matched";
                    this.m_ClientOrderDetail.SpecimenNumberMatchStatus = "Not Matched";
                    break;
                case "Not Matched":
                    this.m_ClientOrderMedia.SpecimenNumberMatchStatus = "Not Determined";
					this.m_ClientOrderMedia.SpecimenNumber = string.Empty;
                    this.m_ClientOrderDetail.SpecimenNumberMatchStatus = "Not Determined";
                    break;
            }
		}

        private void ButtonSpecimenDescriptionMatchStatus_Click(object sender, RoutedEventArgs e)
        {
            switch (this.m_ClientOrderMedia.SpecimenDescriptionMatchStatus)
            {
                case "Not Determined":
                    this.m_ClientOrderMedia.SpecimenDescriptionMatchStatus = "Matched";
					this.m_ClientOrderMedia.Description = this.ClientOrderDetail.Description;
                    this.m_ClientOrderDetail.SpecimenDescriptionMatchStatus = "Matched";
                    if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == true)
                    {
                        this.m_ClientOrderDetail.DescriptionToAccession = this.SpecimenDescriptionHelper(this.m_ClientOrderDetail.Description);
                    }
                    break;
                case "Matched":
                    this.m_ClientOrderMedia.SpecimenDescriptionMatchStatus = "Not Labeled";
                    this.m_ClientOrderDetail.SpecimenDescriptionMatchStatus = "Not Labeled";
                    break;
                case "Not Labeled":
                    this.m_ClientOrderMedia.SpecimenDescriptionMatchStatus = "Not Matched";
					this.m_ClientOrderMedia.Description = string.Empty;
                    this.m_ClientOrderDetail.SpecimenDescriptionMatchStatus = "Not Matched";
                    break;
                case "Not Matched":
                    this.m_ClientOrderMedia.SpecimenDescriptionMatchStatus = "Not Determined";
                    this.m_ClientOrderDetail.SpecimenDescriptionMatchStatus = "Not Determined";
                    break;
            }

			this.TextBoxAccessionAs.Focus();
			this.TextBoxAccessionAs.SelectAll();
        }

		private void ButtonClientDetailFresh_Click(object sender, RoutedEventArgs e)
		{
			this.ClientOrderDetail.ClientFixation = "Fresh";
			this.ClientOrderDetail.LabFixation = "Formalin";			
		}

		private void ButtonClientDetailFormalin_Click(object sender, RoutedEventArgs e)
		{
			this.ClientOrderDetail.ClientFixation = "Formalin";
			this.ClientOrderDetail.LabFixation = "Formalin";
		}

		private void ButtonClientDetailBPlus_Click(object sender, RoutedEventArgs e)
		{
			this.ClientOrderDetail.ClientFixation = "B+ Fixative";
            this.ClientOrderDetail.LabFixation = "Formalin"; 
		}

		private void ButtonClientDetailCytolyt_Click(object sender, RoutedEventArgs e)
		{
            this.ClientOrderDetail.ClientFixation = "Cytolyt";
			this.ClientOrderDetail.LabFixation = "Cytolyt";
		}

		private void ButtonClientDetail95IPA_Click(object sender, RoutedEventArgs e)
		{
			this.ClientOrderDetail.ClientFixation = "95% IPA";
			this.ClientOrderDetail.LabFixation = "95% IPA";
		}

		private void ButtonClientDetailNotApplicable_Click(object sender, RoutedEventArgs e)
		{
			this.ClientOrderDetail.ClientFixation = "Not Applicable";
			this.ClientOrderDetail.LabFixation = "Not Applicable";
		}

        private string SpecimenDescriptionHelper(string incomingDescription)
        {
            string result = incomingDescription;
            if (incomingDescription.ToUpper() == "GALLBLADDER")
            {
                result = "Gallbladder, excision";
            }
            if (incomingDescription.ToUpper() == "APPENDIX")
            {
                result = "Appendix, excision";
            }
            return result;
        }

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.m_ClientOrderDetail.Received = true;
			this.m_ClientOrderDetail.DateReceived = DateTime.Now;

			this.m_ClientOrderMedia.HasBarcode = true;
			this.m_ClientOrderMedia.HasSpecimenId = true;
			this.m_ClientOrderMedia.Description = this.m_ClientOrderDetail.Description;
			this.m_ClientOrderMedia.SpecimenNumber = this.ClientOrderDetail.SpecimenNumber.ToString();
			this.m_ClientOrderMedia.CollectionDate = this.ClientOrderDetail.CollectionDate;
			if (!this.m_ClientOrderMedia.CollectionDate.HasValue)
			{
				this.m_ClientOrderMedia.CollectionDate = DateTime.Now;
			}

			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this).Close();
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save()
		{
            
		}

		public void UpdateBindingSources()
		{
		}
	}
}
