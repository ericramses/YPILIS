using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICFT1View
    {

        public EPICFT1View()
        { }

        public void ToXml(XElement document)
        {
            XElement ft1Element = new XElement("FT1");
            document.Add(ft1Element);

        }
    }
}
