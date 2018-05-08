using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	public class EGFRToALKReflexAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public EGFRToALKReflexAnalysisTest()
		{
			this.m_PanelSetId = 124;
			this.m_PanelSetName = "EGFR, ALK, ROS1, PD-L1 Analysis";
            this.m_Abbreviation = "EGFRALKRFLX";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ReflexTesting;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument).AssemblyQualifiedName;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(14);            
            this.m_EpicDistributionIsImplemented = true;
            this.m_EnforceOrderTarget = true;

            string task1Description = "Cut H&E slide and give to pathologist to circle tumor for tech only. Give the paraffin block to Flow so they can send to NEO.";
            string task1ClientSpecificDescription = "Give provided slides to pathologist to circle tumor for tech only. Give the paraffin block to Flow so they can send to NEO.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description, task1ClientSpecificDescription, new Business.Client.Model.IdahoClientIdList()));

            string task2Description = "Collect circled H&E after slide, curls and paraffin block from pathologist and send to NEO.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}

        public override YellowstonePathology.Business.Rules.MethodResult OrderTargetIsOk(YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            methodResult.Success = true;

            if (orderTarget.GetType().Name != "AliquotOrder")
            {
                methodResult.Success = false;
                methodResult.Message = "EGFR, ALK, ROS1, PD-L1 Analysis must be ordered on an aliquot.";
            }

            return methodResult;
        }
    }
}
