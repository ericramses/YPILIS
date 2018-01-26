﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PlateletAssociatedAntibodies
{
	public class PlateletAssociatedAntibodiesTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
		public PlateletAssociatedAntibodiesTest()
        {
            this.m_PanelSetId = 22;
            this.m_PanelSetName = "Platelet Associated Antibodies";
            //this.m_TestId = 4;
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform PAA testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86023", null), 2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);                  
		}
    }
}
