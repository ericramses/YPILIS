using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupHematopoietic : DisplayGroupIHC
    {
        public DisplayGroupHematopoietic()
        {
            this.m_GroupName = "Hematopoietic";            

            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("68")); // CD3());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("71")); // CD4());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("75")); // CD5());            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("80")); // CD8());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("61")); // CD10());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("64")); // CD15());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("172")); // CD19());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("65")); // CD20());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("66")); // CD23());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("69")); // CD30());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("176")); // CD31());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("70")); // CD34());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("73")); // CD45());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("77")); // CD56()); x
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("78")); // CD68());
            this.m_List.Add(new CD79a());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("177")); // CD99());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("62")); // CD117());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("63")); // CD138());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("56")); // Bcl2());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("57")); // Bcl6());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("85")); // CyclinD1());            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("113")); // IgKappa());            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("114")); // IgLambda());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("126")); // Myeloperoxidase());
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("157")); // TdT());            
            this.m_List.Add((ImmunoHistochemistryTest)TestCollection.Instance.GetTest("141")); // PAX5());            
        }
    }
}
