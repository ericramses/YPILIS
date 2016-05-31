using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing
{
	public class PQRIColorectalCancer
	{
        List<string> m_CptCodeList = new List<string>();
        List<string> m_ICD9CodeList = new List<string>();        

        public PQRIColorectalCancer()
        {
            this.m_CptCodeList = new List<string>();            
            this.m_CptCodeList.Add("88309");

            this.m_ICD9CodeList = new List<string>();
            this.m_ICD9CodeList.Add("153.0");
            this.m_ICD9CodeList.Add("153.1");
            this.m_ICD9CodeList.Add("153.2");
            this.m_ICD9CodeList.Add("153.3");
            this.m_ICD9CodeList.Add("153.4");
            this.m_ICD9CodeList.Add("153.5");
            this.m_ICD9CodeList.Add("153.6");
            this.m_ICD9CodeList.Add("153.7");
            this.m_ICD9CodeList.Add("153.8");
            this.m_ICD9CodeList.Add("153.9");
            this.m_ICD9CodeList.Add("154.0");
            this.m_ICD9CodeList.Add("154.1");
            this.m_ICD9CodeList.Add("154.8");         
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
