using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ICDCode
    {
        string m_ICDCodeId;
        string m_Code;
        string m_Category;
        string m_Description;

        public ICDCode()
        {

        }

        public string ICDCodeId
        {
            get { return this.m_ICDCodeId; }
            set { this.m_ICDCodeId = value; }
        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public string Category
        {
            get { return this.m_Category; }
            set { this.m_Category = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public static ICDCode Clone(ICDCode icdCodeIn)
        {
            return (ICDCode)icdCodeIn.MemberwiseClone();
        }
    }
}
