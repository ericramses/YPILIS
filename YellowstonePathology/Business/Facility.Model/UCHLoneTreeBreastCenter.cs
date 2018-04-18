using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UCHLoneTreeBreastCenter : Facility
    {
        public UCHLoneTreeBreastCenter()
        {
            this.m_FacilityId = "UCHLTBC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "UCH Lone Tree Breast Center";
            this.m_Address1 = "9544 Park Meadows Dr.";
            this.m_Address2 = "Suite 100";
            this.m_City = "Lone Tree";
            this.m_State = "CO";
            this.m_ZipCode = "80124";
            this.m_PhoneNumber = "(720)553-1200";
            this.m_FedexAccountNo = "568695402";
            this.m_FedexPaymentType = "RECIPIENT";

            //this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
