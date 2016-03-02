using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class CytologyScreeningListReportData : XElement
    {
        public CytologyScreeningListReportData(XElement cytologyScreeningListElement)
            : base("CytologyScreeningListReportData")
        {
            List<XElement> cytologyScreeningElements = (from item in cytologyScreeningListElement.Elements("CytologyScreeningSearchResult")
                                                    select item).ToList<XElement>();
            foreach (XElement cytologyScreeningElement in cytologyScreeningElements)
            {
                XElement reportElement = new XElement("CytologyScreening",
                new XElement("MasterAccessionNo", cytologyScreeningElement.Element("MasterAccessionNo").Value),
                new XElement("ReportNo", cytologyScreeningElement.Element("ReportNo").Value),
                new XElement("PatientName", cytologyScreeningElement.Element("PatientName").Value),
                new XElement("OrderedByName", cytologyScreeningElement.Element("OrderedByName").Value),
                new XElement("ScreenedByName", cytologyScreeningElement.Element("ScreenedByName").Value),
                new XElement("ScreeningType", cytologyScreeningElement.Element("ScreeningType").Value),
                new XElement("AssignedToName", cytologyScreeningElement.Element("AssignedToName").Value),
                new XElement("AccessionTime", cytologyScreeningElement.Element("AccessionTime").Value),
                new XElement("ScreeningFinalTime", cytologyScreeningElement.Element("ScreeningFinalTime").Value),
                new XElement("CaseFinalTime", cytologyScreeningElement.Element("CaseFinalTime").Value));

                this.Add(reportElement);
            }
        }
    }
}
