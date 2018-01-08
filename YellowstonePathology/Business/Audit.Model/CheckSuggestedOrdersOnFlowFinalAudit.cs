using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CheckSuggestedOrdersOnFlowFinalAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma m_PanelSetOrder;
        private bool m_HasIndication;
        private bool m_HasCLLByFish;
        private bool m_HasIGVH;
        private bool m_IsNewDiagnosis;
        private bool m_UseFCCRule;
        private bool m_UseTCIRule;

        private List<int> m_FCCPhysicians;
        private List<int> m_TCIPhysicians;

        public CheckSuggestedOrdersOnFlowFinalAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;

            this.m_HasIndication = false;
            if(string.IsNullOrEmpty(this.m_PanelSetOrder.Impression) == false)
            {
                this.m_HasIndication = this.m_PanelSetOrder.Impression.Contains("chronic lymphocytic leukemia");
            }
            
            this.m_FCCPhysicians = new List<int>();
            this.m_FCCPhysicians.Add(83);
            this.m_FCCPhysicians.Add(2337);
            this.m_FCCPhysicians.Add(20);
            this.m_FCCPhysicians.Add(3124);
            this.m_FCCPhysicians.Add(2610);
            this.m_FCCPhysicians.Add(3345);
            this.m_FCCPhysicians.Add(574);
            this.m_FCCPhysicians.Add(3386);
            this.m_FCCPhysicians.Add(3537);
            this.m_FCCPhysicians.Add(3446);
            this.m_FCCPhysicians.Add(3939);
            this.m_FCCPhysicians.Add(2623);
            this.m_FCCPhysicians.Add(4294);
            this.m_FCCPhysicians.Add(2045);

            this.m_TCIPhysicians = new List<int>();
            this.m_TCIPhysicians.Add(999);
            this.m_TCIPhysicians.Add(3258);
            this.m_TCIPhysicians.Add(3359);
            this.m_TCIPhysicians.Add(3227);
            this.m_TCIPhysicians.Add(2135);
            this.m_TCIPhysicians.Add(2736);
            this.m_TCIPhysicians.Add(2558);
            this.m_TCIPhysicians.Add(618);
            this.m_TCIPhysicians.Add(3866);
            this.m_TCIPhysicians.Add(3641);
            this.m_TCIPhysicians.Add(3980);
            this.m_TCIPhysicians.Add(4204);
        }

        public override void Run()
        {
            this.HasIndication();
            this.CaseForClient();
            this.HasOrder();
            this.CreateResult();
        }

        private void HasIndication()
        {
            this.m_HasIndication = false;
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.Impression) == false)
            {
                if (this.m_PanelSetOrder.Impression.Contains("chronic lymphocytic leukemia") == true ||
                this.m_PanelSetOrder.InterpretiveComment.Contains("chronic lymphocytic leukemia") == true)
                {
                    this.m_HasIndication = true;
                }
            }            
        }

        private void CaseForClient()
        {
            if (this.m_HasIndication == true)
            {
                if (this.m_FCCPhysicians.IndexOf(this.m_AccessionOrder.PhysicianId) > -1) this.m_UseFCCRule = true;
                else if (this.m_TCIPhysicians.IndexOf(this.m_AccessionOrder.PhysicianId) > -1) this.m_UseTCIRule = true;
            }
        }

        private void HasOrder()
        {
            if (this.m_HasIndication == true)
            {
                Test.CLLByFish.CLLByFishTest cllByFishTest = new Test.CLLByFish.CLLByFishTest();
                PanelSet.Model.PanelSetIGVH panelSetIGVH = new PanelSet.Model.PanelSetIGVH();
                foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
                {
                    if (panelSetOrder.PanelSetId == cllByFishTest.PanelSetId) this.m_HasCLLByFish = true;
                    if (panelSetOrder.PanelSetId == panelSetIGVH.PanelSetId) this.m_HasIGVH = true;
                }
            }
        }

        private void CreateResult()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if (this.m_UseFCCRule == true && this.m_HasIndication == true) CreateFCCResult();
            else if (this.m_UseTCIRule == true && this.m_HasIndication == true) CreateTetonCancerResult();
        }

        private void CreateFCCResult()
        {
            if (this.m_HasCLLByFish == false)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("A CLL By Fish is suggested.");
            }
        }

        private void CreateTetonCancerResult()
        {
            this.IsNewDiagnosis();
            if (this.m_IsNewDiagnosis == true)
            {
                if(this.m_HasCLLByFish == false && this.m_HasIGVH == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("A CLL By Fish and an IgVH Mutation Analysis are suggested.");
                }
                else if(this.m_HasCLLByFish == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("A CLL By Fish is suggested.");
                }
                else if (this.m_HasIGVH == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("An IgVH Mutation Analysis is suggested.");
                }
            }
        }

        private void IsNewDiagnosis()
        {
            string patientId = this.m_AccessionOrder.PatientId;
            int previousCases = Gateway.FlowGateway.PreviousFlowCasesAbnormalCLL(this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.PatientId);
            if (previousCases == 0) this.m_IsNewDiagnosis = true;
        }
    }
}
