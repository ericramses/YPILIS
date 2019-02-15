using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization
{
    public class ExtractAndHoldForPreauthorizationTest : YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowTest
    {
        public ExtractAndHoldForPreauthorizationTest()
        {
            this.m_PanelSetName = "Extract And Hold For Preauthorization";
            this.m_Abbreviation = "Extract And Hold For Preauthorization";
            this.m_ExpectedDuration = new TimeSpan(7, 0, 0, 0);
            this.m_AllowMultiplePerAccession = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument).AssemblyQualifiedName;

            YellowstonePathology.Business.Task.Model.TaskFax taskFax = new YellowstonePathology.Business.Task.Model.TaskFax(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, "Preauthorization Fax", "PreauthorizationNotification");
            this.m_TaskCollection.Add(taskFax);
        }
    }
}
