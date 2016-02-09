using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class ClientDocumentBuilder : DocumentBuilder
    {
        private SqlCommand m_SQLCommand;

        public ClientDocumentBuilder(SqlCommand sqlCommand)
        {
            this.m_SQLCommand = sqlCommand;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Client.Model.Client client = new Client.Model.Client();
            this.BuildClient(client);
            return client;
        }

        public override void Refresh(object o)
        {
            YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)o;
            this.BuildClient(client);
        }

        private void BuildClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (XmlReader xmlReader = this.m_SQLCommand.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        XElement result = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
                        YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(result, client);
                        xmlPropertyWriter.Write();

                        BuildLocation(client, result);
                    }
                }
            }
        }


        private void BuildLocation(YellowstonePathology.Business.Client.Model.Client client, XElement clientElement)
        {
            client.ClientLocationCollection.Clear();
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
