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

        public PQRSCode()
        {
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
                //return this.m_FormattedReportingDefinition;
                StringBuilder result = new StringBuilder(this.m_Code);
                if (this.m_Modifier != null) result.Append("-" + this.m_Modifier.Modifier);
                result.Append(":  ");
                result.Append(this.m_ReportingDefinition);
                return result.ToString();
            }
            set { this.m_FormattedReportingDefinition = value; }
        }

        public override string GetModifier(BillingComponentEnum billingComponent)
        {
            string result = null;
            if (this.m_Modifier != null) result = this.m_Modifier.Modifier;
            return result;
        }

        public override CptCode Clone(CptCode cptCodeIn)
        {
            PQRSCode codeIn = (PQRSCode)cptCodeIn;
            return (CptCode)codeIn.MemberwiseClone();
        }

        public override void SetModifier(string modifier)
        {
            if (string.IsNullOrEmpty(modifier) == false)
            {
                foreach (CptCodeModifier codeModifier in this.Modifiers)
                {
                    if (codeModifier.Modifier == modifier)
                    {
                        this.ReportingDefinition = codeModifier.Description;
                        this.Modifier = codeModifier;
                        break;
                    }
                }
                if (this.Modifier == null)
                {
                    throw new Exception("trying to get PQRS Code " + this.Code + " with modifier " + modifier + " not available for the code.");
                }

            }
        }
    }
}
