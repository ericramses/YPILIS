using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Panther
{
	public class PantherORC
	{        		
		private string m_DateFormat = "yyyyMMddHHmm";
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public PantherORC(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_AliquotOrder = aliquotOrder;
            this.m_PanelSetOrder = panelSetOrder;
        }       

        public void ToXml(XElement document)
        {
            XElement orcElement = new XElement("ORC");
            document.Add(orcElement);

            XElement orc01Element = new XElement("ORC.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.1.1", PantherActionCode.NewSample, orc01Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc01Element);
            
            XElement orc03Element = new XElement("ORC.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.3.1", this.m_AliquotOrder.AliquotOrderId, orc03Element);            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc03Element);            
                        
            //XElement orc07Element = new XElement("ORC.7");
            //YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.7.1", "Dilution", orc07Element);
            //YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc07Element);
            
            XElement orc09Element = new XElement("ORC.9");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.9.1", this.m_SpecimenOrder.CollectionDate.Value.ToString("yyyyMMddHHmmss"), orc09Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc09Element);

            XElement orc10Element = new XElement("ORC.10");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.10.1", this.m_PanelSetOrder.OrderedByInitials, orc10Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc10Element);
        }
	}
}
