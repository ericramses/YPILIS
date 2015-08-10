using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing
{
	public class PQRIBarrettsEsophagus
	{
        List<string> m_CptCodeList = new List<string>();
        List<string> m_ICD9CodeList = new List<string>();

        public PQRIBarrettsEsophagus()
        {
            this.m_CptCodeList = new List<string>();
            this.m_CptCodeList.Add("88305");            

            this.m_ICD9CodeList = new List<string>();
            this.m_ICD9CodeList.Add("530.85");            
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
