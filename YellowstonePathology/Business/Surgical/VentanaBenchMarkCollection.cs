﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Surgical
{
    public class VentanaBenchMarkCollection : ObservableCollection<VentanaBenchMark>
    {
        string[] erTestIds =  new string[2] { "98", "99" };
        string[] prTestIds = new string[2] { "144", "145" };

        public VentanaBenchMarkCollection()
        {

        }

        public VentanaBenchMark GetByVentanaTestId(string ventanaTestId)
        {
            VentanaBenchMark result = null;
            foreach (VentanaBenchMark item in this)
            {
                if (item.BarcodeNumber.ToString() == ventanaTestId)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public VentanaBenchMark GetByYPITestId(string ypiTestId, bool useWetProtocol)
        {
            VentanaBenchMark result = null;

            if(Array.Exists(erTestIds, element => element == ypiTestId))
            {
                if (useWetProtocol == false)
                {
                    result = this.GetByVentanaTestId("259");
                }
                else
                {
                    result = this.GetByVentanaTestId("280");
                }
            }
            else if(Array.Exists(prTestIds, element => element == ypiTestId))
            {
                if(useWetProtocol == false)
                {
                    result = this.GetByVentanaTestId("265");
                }
                else
                {
                    result = this.GetByVentanaTestId("281");
                }                
            }
            else
            {
                foreach (VentanaBenchMark item in this)
                {
                    if (item.YPITestId == ypiTestId && item.IsWetProtocol == useWetProtocol)
                    {
                        result = item;
                        break;
                    }
                }
            }
            
            return result;
        }
    }
}
