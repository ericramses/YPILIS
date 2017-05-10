using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class StLukesMagicValley
        : Facility
    {
        public StLukesMagicValley()
        {
            this.m_FacilityId = "STLKMV";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "St Lukes Magic Valley";
            this.m_Address1 = "801 Pole Line Road West";
            this.m_Address2 = "P.O. Box 409";
            this.m_City = "Twin Falls";
            this.m_State = "ID";
            this.m_ZipCode = "83303";
            this.m_PhoneNumber = "(208)814-0360";
            this.m_FedexAccountNo = "115533622";
            this.m_FedexPaymentType = "RECIPIENT";            
            this.m_IsReferenceLab = true;                        
        }
    }
}
