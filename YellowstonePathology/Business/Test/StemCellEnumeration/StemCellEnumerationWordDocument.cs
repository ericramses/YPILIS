using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.StemCellEnumeration
{
	public class StemCellEnumerationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {        

        public StemCellEnumerationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\StemCellEnumeration.1.xml";        
            base.OpenTemplate();            

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationTestOrder stemCellEnumerationTestOrder = (StemCellEnumerationTestOrder)this.m_PanelSetOrder;

			YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(stemCellEnumerationTestOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(stemCellEnumerationTestOrder.OrderedOn, stemCellEnumerationTestOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			foreach (YellowstonePathology.Business.Flow.FlowMarkerItem markerItem in stemCellEnumerationTestOrder.FlowMarkerCollection)
			{
                switch(markerItem.Name)
                {
                    case "Stem Cell Enumeration":                        
                        this.SetXmlNodeData("stemcell_result", markerItem.Result);
                        break;
                    case "Viability":
                        this.SetXmlNodeData("viability_result", markerItem.Result);
                        break;
                    case "WBC Count":
                        this.SetXmlNodeData("wbccount_result", markerItem.Result);
                        break;                
                }
            }            
            this.SaveReport();
        }        
    }
}
