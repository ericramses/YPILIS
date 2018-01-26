﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfile
{
	public class ThrombocytopeniaProfileTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
        public ThrombocytopeniaProfileTest()
        {
            this.m_PanelSetId = 21;
            this.m_PanelSetName = "Thrombocytopenia Profile";            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);
            this.m_EpicDistributionIsImplemented = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ThombocytopeniaProfile.ThombocytopeniaProfileWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform thrombocytopenia profile testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86023", null), 2);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("85055", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);            
		}
    }
}
