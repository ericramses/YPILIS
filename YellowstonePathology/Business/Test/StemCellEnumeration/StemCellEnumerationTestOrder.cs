using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.StemCellEnumeration
{
    [PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
    public class StemCellEnumerationTestOrder : PanelSetOrder
    {

        private Flow.FlowMarkerCollection m_FlowMarkerCollection;

        public StemCellEnumerationTestOrder()
        {
            this.m_FlowMarkerCollection = new Flow.FlowMarkerCollection();
        }

        public StemCellEnumerationTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_FlowMarkerCollection = new Flow.FlowMarkerCollection();
        }

        [PersistentCollection()]
        public Flow.FlowMarkerCollection FlowMarkerCollection
        {
            get { return this.m_FlowMarkerCollection; }
            set
            {
                this.m_FlowMarkerCollection = value;
                NotifyPropertyChanged("FlowMarkerCollection");
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            if (this.m_FlowMarkerCollection.Count == 0)
            {
                result.AppendLine("No Markers selected.");
                result.AppendLine();
            }
            else
            {
                foreach (Flow.FlowMarkerItem item in this.m_FlowMarkerCollection)
                {
                    result.AppendLine(item.Name + ": " + item.Result);
                    result.AppendLine();
                }
            }

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                foreach (Flow.FlowMarkerItem markerItem in this.FlowMarkerCollection)
                {
                    switch (markerItem.Name)
                    {
                        case "Stem Cell Enumeration":
                        case "Viability":
                        case "WBC Count":
                            if (string.IsNullOrEmpty(markerItem.Result) == true)
                            {
                                result.Status = AuditStatusEnum.Failure;
                                result.Message += markerItem.Name + Environment.NewLine;
                            }
                            break;
                    }
                }
                if( result.Status == AuditStatusEnum.Failure)
                {
                    result.Message = "Unable to accept as the following result/s are not set: " + Environment.NewLine + result.Message;
                }
            }
            return result;
        }
    }
}
