using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Service.Search
{
	public class ClientBuilder
	{
		YellowstonePathology.Business.Client.Model.Client m_Client;

		public ClientBuilder()
		{
		}

		public YellowstonePathology.Business.Client.Model.Client Client
		{
			get { return this.m_Client; }
		}

		public void Build(XElement rootElement)
		{
            if (rootElement != null)
            {
				YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Business.Persistence.XmlPropertyWriter(rootElement, client);
				xmlPropertyWriter.Write();

                BuildLocation(client, rootElement);
                this.m_Client = client;
            }
            else
            {
                this.m_Client = null;
            }
		}

		private void BuildLocation(YellowstonePathology.Business.Client.Model.Client client, XElement clientElement)
		{
			List<XElement> clientLocationElements = (from item in clientElement.Elements("ClientLocationCollection")
														 select item).ToList<XElement>();
			foreach (XElement clientLocationElement in clientLocationElements.Elements("ClientLocation"))
			{
				YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(clientLocationElement, clientLocation);
				xmlPropertyWriter.Write();
				client.ClientLocationCollection.Add(clientLocation);
			}
		}
	}
}
