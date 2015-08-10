using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace YellowstonePathology.Business.Domain.Billing
{
    [Table(Name = "tblTechnicalCharge")]
    public class TechnicalCharge
    {        
        string m_Description;

        public TechnicalCharge()
        {

        }

        [Column(Name = "Description", Storage = "m_Description")]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }        
    }
}
