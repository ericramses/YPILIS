using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing
{
	public class PQRIBreastCancer
	{
        List<string> m_CptCodeList = new List<string>();
        List<string> m_ICD9CodeList = new List<string>();        

        public PQRIBreastCancer()
        {
            this.m_CptCodeList = new List<string>();
            this.m_CptCodeList.Add("88307");
            this.m_CptCodeList.Add("88309");

            this.m_ICD9CodeList = new List<string>();
            this.m_ICD9CodeList.Add("174.0");
            this.m_ICD9CodeList.Add("174.1");
            this.m_ICD9CodeList.Add("174.2");
            this.m_ICD9CodeList.Add("174.3");
            this.m_ICD9CodeList.Add("174.4");
            this.m_ICD9CodeList.Add("174.5");
            this.m_ICD9CodeList.Add("174.6");
            this.m_ICD9CodeList.Add("174.7");
            this.m_ICD9CodeList.Add("174.8");
            this.m_ICD9CodeList.Add("174.9");
            this.m_ICD9CodeList.Add("175.0");
            this.m_ICD9CodeList.Add("175.9");            
        }

        public List<string> CptCodeList
        {
            get { return this.m_CptCodeList; }
        }

        public List<string> ICD9CodeList
        {
            get { return this.m_ICD9CodeList; }
        }
	}
}
