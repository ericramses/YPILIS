using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupEpithelial : DisplayGroupIHC
    {
        public DisplayGroupEpithelial()
        {
            this.m_GroupName = "Epithelial";            

            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("136")); // Pancytokeratin());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("170")); // OSCAR());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("89")); // Cytokeratin56());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("90")); // Cytokeratin7());
            this.m_List.Add(new Cytokeratin17());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("87")); // Cytokeratin20());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("88")); // Cytokeratin34());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("97")); // EMA());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("94")); // Ecadherin());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("359")); // P40());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("215")); // MOC31());
        }
    }
}
