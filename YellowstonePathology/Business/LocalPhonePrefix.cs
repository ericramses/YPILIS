using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class LocalPhonePrefix : List<string>
    {
        public LocalPhonePrefix()
        {
            this.Add("123");
            this.Add("206");
            this.Add("237");
            this.Add("238");
            this.Add("245");
            this.Add("247");
            this.Add("248");
            this.Add("252");
            this.Add("254");
            this.Add("256");
            this.Add("259");
            this.Add("281");
            this.Add("294");
            this.Add("322");            
            this.Add("328");
            this.Add("373");
            this.Add("435");
            this.Add("446");
            this.Add("534");
            this.Add("545");
            this.Add("601");
            this.Add("628");
            this.Add("638");
            this.Add("651");
            this.Add("652");
            this.Add("655");
            this.Add("656");
            this.Add("657");
            this.Add("662");
            this.Add("665");
            this.Add("839");
            this.Add("867");
            this.Add("894");
            this.Add("896");
            this.Add("922");
            this.Add("962");
            this.Add("967");
        }

        public string HandleLongDistance(string phoneNumber)
        {
            string result = phoneNumber;
            string prefix = phoneNumber.Substring(3, 3);
            if(this.Exists(element => element == prefix) == false)
            {
                result = "1" + phoneNumber;
            }
            return result;
        }
    }
}
