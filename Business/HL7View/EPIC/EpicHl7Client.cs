using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EpicHl7Client : Hl7Client
    {
        public EpicHl7Client() : base("COPATH", "SVH")
        {

        }
    }
}
