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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.ReceiveSpecimen
{
	public partial class PlacentalPathologyPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Placental Pathology Questionnaire Page";
        private string m_ClientOrderId;
        //private YellowstonePathology.YpiConnect.Contract.Order.PlacentalPathologyQuestionnaireCollection m_PlacentalPathologyQuestionnaireCollection;

		public PlacentalPathologyPage(string clientOrderId)
		{
            this.m_ClientOrderId = clientOrderId;

			InitializeComponent();
            
			DataContext = this;
			Loaded += new RoutedEventHandler(PlacentalFormDisplayPage_Loaded);
		}

		private void PlacentalFormDisplayPage_Loaded(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData placentalPathologyQuestionnaireData = YellowstonePathology.Business.Gateway.XmlGateway.GetPlacentalPathologyQuestionnaire(this.m_ClientOrderId);
			YellowstonePathology.Document.PlacentalPathologyQuestionnaire placentalPathologyQuestionnare = new Document.PlacentalPathologyQuestionnaire(placentalPathologyQuestionnaireData);
			this.Viewer.Document = placentalPathologyQuestionnare.FixedDocument;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save(bool releaseLock)
		{

		}

		public void UpdateBindingSources()
		{
		}
	}
}
