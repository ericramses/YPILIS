using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{    
    public class PID
    {
        Business.Patient.Model.Address m_Address;

        public PID()
        {
            this.m_Address = new Patient.Model.Address();
        }

        public void FromHL7(string pidSegment)
        {
            string[] split = pidSegment.Split('|');
            string[] addressField = split[11].Split('^');
            
            this.m_Address.PAddress1 = addressField[0];
            this.m_Address.PAddress2 = addressField[1];
            this.m_Address.PCity = addressField[2];
            this.m_Address.PState = addressField[3];
            this.m_Address.PZipCode = addressField[4];
        }

        public Business.Patient.Model.Address Address
        {
            get { return this.m_Address; }
        }
    }
}
