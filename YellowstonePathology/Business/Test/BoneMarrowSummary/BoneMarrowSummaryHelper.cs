using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class BoneMarrowSummaryHelper
    {
        public BoneMarrowSummaryHelper() { }

        public static List<Business.Test.PanelSetOrder> GetAccessionSummaryList(Business.Test.AccessionOrder accessionOrder)
        {
            List<Business.Test.PanelSetOrder> result = new List<PanelSetOrder>();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

            Business.Test.PanelSetOrderCollection flow = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection cyto = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection fish = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection molecular = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection other = new PanelSetOrderCollection();

            List<int> exclusionList = new List<int>();
            exclusionList.Add(13);
            exclusionList.Add(197);
            exclusionList.Add(262);
            exclusionList.Add(268);

            foreach (Business.Test.PanelSetOrder pso in accessionOrder.PanelSetOrderCollection)
            {
                if (exclusionList.IndexOf(pso.PanelSetId) == -1)
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSets.GetPanelSet(pso.PanelSetId);
                    if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FlowCytometry) flow.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.Cytogenetics) cyto.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FISH) fish.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.Molecular) molecular.Insert(0, pso);
                    else other.Insert(0, pso);
                }
            }

            result.AddRange(other);
            result.AddRange(molecular);
            result.AddRange(fish);
            result.AddRange(cyto);
            result.AddRange(flow);
            return result;
        }

        public static OtherReportViewCollection GetOtherReports(Business.Test.AccessionOrder accessionOrder)
        {
            OtherReportViewCollection result = Business.Gateway.AccessionOrderGateway.GetOtherReportViewCollection(accessionOrder.PatientId, accessionOrder.MasterAccessionNo);
            return result;
        }
    }
}
