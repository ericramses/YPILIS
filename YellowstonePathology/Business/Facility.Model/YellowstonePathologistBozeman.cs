using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Facility.Model
{
    public class YellowstonePathologistBozeman : Facility
    {
        public YellowstonePathologistBozeman()
        {
            this.m_FacilityName = "Yellowstone Pathologist Bozeman";
            this.m_FacilityId = "YPBZMN";
            this.m_FacilityIdOLD = "35C4D327-2623-4AF1-90F9-7F48900A5A96";
            this.m_Address1 = "Bozeman Health Deaconess Hospital";
            this.m_Address2 = "915 Highland BLVD";
            this.m_City = "Bozeman";
            this.m_State = "MT";
            this.m_ZipCode = "59715";
            this.m_IsReferenceLab = false;
            this.m_AccessioningLocation = "Billings";
            this.m_LocationAbbreviation = "YPI Bzmn, Mt";
            this.m_ClientId = 1613;

            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrEmerickOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrNeroOffice());

            this.m_CliaLicense = new CLIALicense(this, "27D0410647");
        }
    }
}
