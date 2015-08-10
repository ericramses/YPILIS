using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence
{
    public class ElementHelper
    {
		public static XElement GetUpdategramRootElement()
		{
			XElement root = new XElement("ROOT");
			return root;
		}

        public static XAttribute GetUpdgNamespaceAttribute()
        {
            return new XAttribute(XNamespace.Xmlns + "updg", "urn:schemas-microsoft-com:xml-updategram");
        }

        public static XAttribute GetXsdNamespaceAttribute()
        {
            return new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema");
        }

        public static XAttribute GetSqlNamespaceAttribute()
        {
            return new XAttribute(XNamespace.Xmlns + "sql", "urn:schemas-microsoft-com:xml-sql");
        }

		public static XElement GetSyncElement()
		{
			XNamespace updg = "urn:schemas-microsoft-com:xml-updategram";
			XElement sync = new XElement(updg + "sync",
				new XAttribute(XNamespace.Xmlns + "updg", "urn:schemas-microsoft-com:xml-updategram"),
				new XAttribute("mapping-schema", "#InlineSchema"));
			return sync;
		}

        public static XElement GetBeforeElement(XElement body)
        {
            XNamespace updg = "urn:schemas-microsoft-com:xml-updategram";
            XElement beforeElement = new XElement(updg + "before", new XAttribute(XNamespace.Xmlns + "updg", "urn:schemas-microsoft-com:xml-updategram"));
            beforeElement.Add(body);
            return beforeElement;
        }

        public static XElement GetAfterElement(XElement body)
        {
            XNamespace updg = "urn:schemas-microsoft-com:xml-updategram";
            XElement afterElement = new XElement(updg + "after", new XAttribute(XNamespace.Xmlns + "updg", "urn:schemas-microsoft-com:xml-updategram"));
            afterElement.Add(body);
            return afterElement;
        }
    }
}
