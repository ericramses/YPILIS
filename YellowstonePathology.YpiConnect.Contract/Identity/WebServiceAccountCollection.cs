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
    [XmlType("WebServiceAccountCollection")]
    public class WebServiceAccountCollection : Collection<WebServiceAccount>
    {
        public WebServiceAccountCollection()
        {

        }        
    }
}
