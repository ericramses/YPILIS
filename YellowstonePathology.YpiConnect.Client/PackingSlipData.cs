using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Client
{
	public class PackingSlipData : XElement
	{
		public PackingSlipData(XElement packingSlipDocument)
			: base(packingSlipDocument)
		{
			string shipDate = this.DateTimeStringFromXmlString(this.Element("ShipDate").Value);
			this.Element("ShipDate").SetValue(shipDate);

			foreach (XElement clientOrderElement in this.Element("ClientOrderCollection").Elements("ClientOrder"))
			{
				string birthdate = DateStringFromXmlString(clientOrderElement.Element("PBirthdate").Value);
				clientOrderElement.Element("PBirthdate").SetValue(birthdate);
				foreach (XElement clientOrderDetailElement in clientOrderElement.Element("ClientOrderDetailCollection").Elements("ClientOrderDetail"))
				{
					string collectionDate = DateTimeStringFromXmlString(clientOrderDetailElement.Element("CollectionDate").Value);
					clientOrderDetailElement.Element("CollectionDate").SetValue(collectionDate);

					string specimenNumber = clientOrderDetailElement.Element("SpecimenNumber").Value;
					string description = clientOrderDetailElement.Element("Description").Value;
					clientOrderDetailElement.Element("SpecimenNumber").SetValue(specimenNumber + ". " + description);
				}
			}
		}

		private string DateStringFromXmlString(string xmlDateString)
		{
			string result = string.Empty;
			DateTime? date = this.FromXmlString(xmlDateString);
			if (date.HasValue)
			{
				result = date.Value.ToShortDateString();
			}

			return result;
		}

		private string DateTimeStringFromXmlString(string xmlDateString)
		{
			string result = string.Empty;
			DateTime? date = this.FromXmlString(xmlDateString);
			if (date.HasValue)
			{
				result = date.Value.ToShortDateString() + " " + date.Value.ToShortTimeString();
			}

			return result;
		}

		private Nullable<DateTime> FromXmlString(string xmlDateString)
		{
			string dateStringNormalized = xmlDateString;
			if (string.IsNullOrEmpty(dateStringNormalized) == false && dateStringNormalized.IndexOf('Z') == dateStringNormalized.Length - 1)
			{
				dateStringNormalized = dateStringNormalized.Remove(dateStringNormalized.Length - 1, 1);
			}
			Nullable<DateTime> nullableResult = null;
			DateTime result = new DateTime();
			bool parsed = DateTime.TryParse(dateStringNormalized, out result);
			if (parsed == true)
			{
				nullableResult = result;
			}
			return nullableResult;
		}
	}
}
