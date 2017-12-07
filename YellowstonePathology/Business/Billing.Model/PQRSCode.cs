using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCode : CptCode
    {
        protected string m_ReportingDefinition;
        protected string m_FormattedReportingDefinition;
        protected List<CptCodeModifier> m_CptCodeModifiers;

        public PQRSCode()
        {
            this.m_CptCodeModifiers = new List<Model.CptCodeModifier>();
        }

        public List<CptCodeModifier> CptCodeModifiers
        {
            get { return this.m_CptCodeModifiers; }
            set { this.m_CptCodeModifiers = value; }
        }

        [PersistentProperty()]
        public string ReportingDefinition
        {
            get { return this.m_ReportingDefinition; }
            set { this.m_ReportingDefinition = value; }
        }

        public string FormattedReportingDefinition
		{
			get
			{
                return this.m_FormattedReportingDefinition;
				/*StringBuilder result = new StringBuilder(this.m_Code);
				if (this.m_Modifier != null) result.Append("-" + this.m_Modifier);
				result.Append(":  ");
				result.Append(this.m_ReportingDefinition);
				return result.ToString();*/
			}
            set { this.m_FormattedReportingDefinition = value; }
		}

        public override string GetModifier(BillingComponentEnum billingComponent)
        {
            return this.m_Modifier;
        }
    }
}
