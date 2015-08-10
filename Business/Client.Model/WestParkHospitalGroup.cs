using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class WestParkHospitalGroup : ClientGroup
    {
        public WestParkHospitalGroup()
        {
            this.Members.Add(553);
            this.Members.Add(1341);
            this.Members.Add(1088);
            this.Members.Add(1439);

            //this.Members.Add(1063);
            //this.Members.Add(1288);            
            //this.Members.Add(935);            
            //this.Members.Add(1399);
        }
    }
}
