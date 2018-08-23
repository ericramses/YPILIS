using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class IndicationMetastaticMelanoma : Indication
    {
        public IndicationMetastaticMelanoma() 
            : base("Metastatic Melanoma", "Metastatic Melanoma")
        {

        }

        public override void SetComment(string resultCode, Interface.ICommonResult commonResult)
        {
            if(resultCode == "BRAFMTTNANLNTDTCTD")
            {
                commonResult.IndicationComment = "Note:  False negatives may occur if the sample does not contain a sufficient quantity of tumor DNA.  " +
                    "If this test was performed on a small/scant sample and subsequent surgical excision confirms the presence of papillary " +
                    "thyroid carcinoma, retesting on the surgical specimen is recommended.";
            }
            else
            {
                base.SetComment(resultCode, commonResult);
            }
        }
    }
}
