using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayCollection : ObservableCollection<PantherAssay>
    {
        public PantherAssayCollection()
        {            
            PantherAssayHPV hpv = new PantherAssayHPV();
            this.Add(hpv);

            PantherAssay ngct = new PantherAssay("AptimaCombo2", "CT/GC", 3);
            ngct.AnalyteList.Add("CT");
            ngct.AnalyteList.Add("GC");
            this.Add(ngct);

            PantherAssay hpv1618 = new PantherAssay("GT HPV", "GT HPV", 62);
            hpv1618.AnalyteList.Add("HPV 16 18/45");
            this.Add(hpv1618);

            PantherAssay trich = new PantherAssay("Aptima TV", "TRICH", 61);
            trich.AnalyteList.Add("TRICH");
            this.Add(trich);
        }
    }
}
