using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence
{
	public interface IPersistable
	{
		void FromXml(XElement xml);
		XElement ToXml();
		void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter);
		void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader);
	}
}
