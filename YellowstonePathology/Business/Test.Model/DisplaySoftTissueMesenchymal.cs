using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplaySoftTissueMesenchymal : DisplayGroupIHC
    {
        public DisplaySoftTissueMesenchymal()
        {
            this.m_GroupName = "Soft Tissue Mesenchymal";

            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("164")); // Vimentin());            
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("153")); // SmoothMuscleActin());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("92")); // Desmin());
            this.m_List.Add(new FactorVIII());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("101")); // FactorXIIIa());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("62")); // CD117());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("298")); // DOG1());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)Business.Test.Model.TestCollection.Instance.GetTest("299")); // BetaCatenin());            
        }
    }
}
