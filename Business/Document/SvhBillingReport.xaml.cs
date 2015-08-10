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
using System.IO;
using System.IO.Packaging;
using System.Windows.Markup;
using System.Reflection;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Document
{
	public class SvhBillingReport
	{
		private YellowstonePathology.Business.Billing.SvhIncomingBillingData m_SvhIncomingBillingData;
		private XElement m_BillingElement;
		private FixedDocument m_Document;
		private System.Windows.Threading.DispatcherPriority m_DispatcherPriority;
		private const string TemplatePath = "YellowstonePathology.Business.Document.SvhBillingReport.xaml";

		public SvhBillingReport(YellowstonePathology.Business.Billing.SvhIncomingBillingData svhIncomingBillingData)
		{
			this.m_SvhIncomingBillingData = svhIncomingBillingData;
			m_BillingElement = XElement.Parse(svhIncomingBillingData.BillingData);

			this.m_DispatcherPriority = DispatcherPriority.SystemIdle;
			this.LoadTemplate();
			this.InjectData();
		}

		private void LoadTemplate()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			m_Document = (FixedDocument)XamlReader.Load(assembly.GetManifestResourceStream(TemplatePath));
		}

		private void InjectData()
		{
			this.m_Document.DataContext = this;
			var dispatcher = Dispatcher.CurrentDispatcher;
			dispatcher.Invoke(this.m_DispatcherPriority, new DispatcherOperationCallback(delegate { return null; }), null);
		}

		public YellowstonePathology.Business.Billing.SvhIncomingBillingData SvhIncomingBillingData
		{
			get { return this.m_SvhIncomingBillingData; }
		}

		public XElement BillingElement
		{
			get { return this.m_BillingElement; }
		}

		public FixedDocument Document
		{
			get { return this.m_Document; }
		}
	}
}
