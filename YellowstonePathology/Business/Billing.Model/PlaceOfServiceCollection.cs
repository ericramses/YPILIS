using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PlaceOfServiceCollection : ObservableCollection<PlaceOfService>
    {
        public PlaceOfServiceCollection()
        {
            this.Add(new PlaceOfService("VOR", "21", "IP"));
            this.Add(new PlaceOfService("VPERI", "21", "IP"));
            this.Add(new PlaceOfService("V6TO", "21", "IP"));
            this.Add(new PlaceOfService("V3FO", "21", "IP"));
            this.Add(new PlaceOfService("VED", "23", "OP"));
            this.Add(new PlaceOfService("VBRST", "11", "OP"));
            this.Add(new PlaceOfService("VGI", "11", "OP"));
            this.Add(new PlaceOfService("VAP", "21", "IP"));
        }

        public PlaceOfService Get(string name)
        {
            PlaceOfService result = null;
            foreach(PlaceOfService placeOfService in this)
            {
                if(placeOfService.Name == name)
                {
                    result = placeOfService;
                    break;
                }
            }
            return result;
        }
    }
}
