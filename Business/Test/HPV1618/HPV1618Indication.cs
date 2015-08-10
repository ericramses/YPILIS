using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618Indication
    {
        public static string SquamousCellCarcinoma = "Squamous Cell Carcinoma of Head and Neck";
        public static string AbnormalPap = "Abnormal Pap";

        public static List<string> GetIndicationList()
        {
            List<string> result = new List<string>();
            result.Add(null);
            result.Add(SquamousCellCarcinoma);
            result.Add(AbnormalPap);
            return result;
        }
        
    }
}
