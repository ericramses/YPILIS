using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.Data.SqlXml;

namespace YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence
{
	public class BeforeElement
	{
		public BeforeElement()
		{}

		public XElement GetBeforeElement(XDocument schema, XElement values)
		{
			XElement result = new XElement(values.Name);
			XElement schemaElement = schema.Root.Element("{http://www.w3.org/2001/XMLSchema}schema");
			result.Add(this.GetKeyElement(schemaElement, values));
			return result;
		}

		public XElement GetKeyElement(XElement schemaElement, XElement values)
		{
			XElement result = null;
			foreach (XElement element in schemaElement.Elements("{http://www.w3.org/2001/XMLSchema}element"))
			{
				XAttribute attribute = element.Attribute("name");
				if (attribute.Value == values.Name.ToString())
				{
					XAttribute valueAttribute = element.Attribute("{urn:schemas-microsoft-com:mapping-schema}key-fields");
					result = values.Element(valueAttribute.Value);
					break;
				}
			}
			return result;
		}
	}
}
