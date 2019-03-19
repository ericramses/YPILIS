using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.DNACellCycleAnalysis
{
	public class DNACellCycleAnalysisTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
        public DNACellCycleAnalysisTest()
        {
            this.m_PanelSetId = 29;
            this.m_PanelSetName = "DNA Content and Cell Cycle Analysis";            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);

            /*
            this.m_EpicDistributionIsImplemented = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ThombocytopeniaProfile.ThombocytopeniaProfileWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform thrombocytopenia profile testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;            
            */
		}
    }
}
