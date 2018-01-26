﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FetalHemoglobin
{
	public class FetalHemoglobinTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
		public FetalHemoglobinTest()
        {
            this.m_PanelSetId = 28;
            this.m_PanelSetName = "Fetal Hemoglobin";
            //this.m_TestId = 7;
			this.m_AllowMultiplePerAccession = true;

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform fetal hemoglobin testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86356", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);                  
		}
    }
}
