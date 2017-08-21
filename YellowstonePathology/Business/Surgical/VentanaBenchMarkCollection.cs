using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Surgical
{
    public class VentanaBenchMarkCollection : ObservableCollection<VentanaBenchMark>
    {
        public VentanaBenchMarkCollection()
        {

        }

        public VentanaBenchMark GetByYPITestId(string ypiTestId)
        {
            VentanaBenchMark result = null;
            foreach (VentanaBenchMark item in this)
            {
                if (item.YPITestId == ypiTestId)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
    }
}
