using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Facility.Model
{
    public class EasternColoradoHealthcare : Facility
    {
        public EasternColoradoHealthcare()
        {
            this.m_FacilityId = "ESTRNCOLHC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Eastern Colorado Healthcare";
            this.m_Address1 = "1055 Clearmont Street #112";      
            this.m_City = "Denver";
            this.m_State = "CO";
            this.m_ZipCode = "80220";
            this.m_PhoneNumber = "(303)399-8020";
            this.m_FedexAccountNo = null;
            this.m_FedexPaymentType = null;

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
