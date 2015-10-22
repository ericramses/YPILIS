using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class NGCTResult
    {        
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string InvalidResult = "Invalid";

        protected string m_NGResultCode;
        protected string m_NGResult;
        protected string m_CTResultCode;
        protected string m_CTResult;

        public NGCTResult()
        {

        } 
        
        public string NGResultCode
        {
            get { return this.m_NGResultCode; }
        }

        public string NGResult
        {
            get { return this.m_NGResult; }
        }

        public string CTResultCode
        {
            get { return this.m_CTResultCode; }
        }

        public string CTResult
        {
            get { return this.m_CTResult; }
        }
    }
}
