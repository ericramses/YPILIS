using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace YellowstonePathology.Business.Domain.Billing
{    
    public class TypingCptCode
    {
        int m_TypingCptCodeId;
        string m_CptCode;
        string m_FeeType;
        int m_Quantity;

        public TypingCptCode()
        {

        }

        public int TypingCptCodeId
        {
            get { return this.m_TypingCptCodeId; }
            set { this.m_TypingCptCodeId = value; }
        }

        public string CptCode
        {
            get { return this.m_CptCode; }
            set { this.m_CptCode = value; }
        }

        public string FeeType
        {
            get { return this.m_FeeType; }
            set { this.m_FeeType = value; }
        }

        public int Quantity
        {
            get { return this.m_Quantity; }
            set { this.m_Quantity = value; }
        }      
    }
}
