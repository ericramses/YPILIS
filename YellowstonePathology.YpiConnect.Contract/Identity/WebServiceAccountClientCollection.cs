using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
	public class WebServiceAccountClientCollection : Collection<WebServiceAccountClient>
	{
		public WebServiceAccountClientCollection()
		{

		}

        public string ToIdString()
        {
            string result = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                result = result + (this[i].ClientId);
                if (i != this.Count - 1)
                {
                    result = result + (", ");
                }
            }
            return result;
        }
	}
}
