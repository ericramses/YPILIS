using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace YellowstonePathology.Business.Domain.Billing
{    
    public class ProfessionalCharge
    {        
        string m_Description;

        public ProfessionalCharge()
        {

        }
        
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }        
    }
}
