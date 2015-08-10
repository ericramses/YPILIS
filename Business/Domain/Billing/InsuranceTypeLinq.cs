using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace YellowstonePathology.Business.Domain.Billing
{    
    public class InsuranceType
    {
        string m_Description;

        public InsuranceType()
        {

        }
 
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}