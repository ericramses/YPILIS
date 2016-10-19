using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class GenomicHealth : Facility
    {
        public GenomicHealth()
        {
            this.m_FacilityId = "GNMCHLTH";
            this.m_FacilityName = "Genomic Health";
            this.m_City = "Redwood City";
            this.m_Address1 = "301 Penobscot Dr.";
            this.m_Address2 = string.Empty;
            this.m_State = "CA";
            this.m_ZipCode = "94063-4700";
            this.m_PhoneNumber = "(866)662-6897";
            this.m_IsReferenceLab = true;            
            this.m_FedexAccountNo = "229534823";
            this.m_FedexPaymentType = "RECIPIENT";

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
