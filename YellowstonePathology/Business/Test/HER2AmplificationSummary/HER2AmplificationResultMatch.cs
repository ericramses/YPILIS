using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultMatch
    {
        private string m_Result;
        private bool m_IsAMatch;

        public HER2AmplificationResultMatch()
        { }

        public string Result
        {
            get { return this.m_Result; }
            set { this.m_Result = value; }
        }

        public bool IsAMatch
        {
            get { return this.m_IsAMatch; }
            set { this.m_IsAMatch = value; }
        }
    }
}
