using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupCytochemical
    {
        protected string m_GroupName;
        protected List<Test> m_List;

        public DisplayGroupCytochemical()
        {
            this.m_GroupName = "Cytochemical";

            this.m_List = new List<Test>();
            this.m_List.Add(TestCollection.Instance.GetTest("50")); // AcidFast());
            this.m_List.Add(TestCollection.Instance.GetTest("51")); // AlcianBlue());
            this.m_List.Add(TestCollection.Instance.GetTest("84")); // CongoRed());
            this.m_List.Add(new CopperRhodanine());
            this.m_List.Add(TestCollection.Instance.GetTest("96")); // Elastic());
            this.m_List.Add(new Fites());
            this.m_List.Add(new Giemsa());
            this.m_List.Add(TestCollection.Instance.GetTest("106")); // GMS());
            this.m_List.Add(new HuckerTwort());
            this.m_List.Add(TestCollection.Instance.GetTest("115")); // Iron());
            this.m_List.Add(TestCollection.Instance.GetTest("119")); // MelanA());
            this.m_List.Add(new Mucin());
            this.m_List.Add(new OilRedO());
            this.m_List.Add(TestCollection.Instance.GetTest("137")); // PAS());
            this.m_List.Add(TestCollection.Instance.GetTest("138")); // PASAlcianBlue());
            this.m_List.Add(TestCollection.Instance.GetTest("139")); // PASforFungus());
            this.m_List.Add(TestCollection.Instance.GetTest("140")); // PASWithDiastase());
            this.m_List.Add(TestCollection.Instance.GetTest("151")); // Reticulin());
            this.m_List.Add(TestCollection.Instance.GetTest("155")); // SteinerandSteiner());
            this.m_List.Add(TestCollection.Instance.GetTest("160")); // Trichrome());
            this.m_List.Add(new AlphaNaphthylAcetateEsterase());            
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
        }

        public List<Test> List
        {
            get { return this.m_List; }
        }
    }
}
