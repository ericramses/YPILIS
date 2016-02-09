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

        public override void Build(object o)
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
                        YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(result, o);
                        xmlPropertyWriter.Write();

                        BuildLocation((YellowstonePathology.Business.Client.Model.Client)o, result);
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
