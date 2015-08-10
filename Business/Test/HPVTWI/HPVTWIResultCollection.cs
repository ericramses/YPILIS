using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
    public class HPVTWIResultCollection : List<HPVTWIResult>
    {
        public HPVTWIResultCollection()
        {

        }

        public static HPVTWIResult ParseFromTcanResult(string tcanResult)
        {
            HPVTWIResult result = null;
            if (string.IsNullOrEmpty(tcanResult) == true)
            {
                result = new HPVTWINegativeResult();
            }
            else if (tcanResult == "A7")
            {
                result = new HPVTWIA7PositiveResult();
            }
            else if (tcanResult == "A9")
            {
                result = new HPVTWIA9PositiveResult();
            }
            else if (tcanResult == "A7, A9")
            {
                result = new HPVTWIA79PositiveResult();
            }
            else if (tcanResult == "A5/A6")
            {
                result = new HPVTWIA56PositiveResult();
            }
            else if (tcanResult == "A5/A6, A9")
            {
                result = new HPVTWIA569PositiveResult();
            }
            else if (tcanResult == "A5/A6, A7")
            {
                result = new HPVTWIA567PositiveResult();
            }
            else if (tcanResult == "A5/A6, A7, A9")
            {
                result = new HPVTWIA5679PositiveResult();
            }
            else if (tcanResult == "Low gDNA")
            {
                result = new HPVTWILowgDNAResult();
            }
            else if (tcanResult == "Low FAM FOZ")
            {
                result = new HPVTWILowFamFOZResult();
            }
            return result;
        }        

        public HPVTWIResult GetResultByPreliminaryResultCode(string resultCode)
        {
            HPVTWIResult result = null;
            foreach (HPVTWIResult hpvTWIResult in this)
            {
                if (hpvTWIResult.PreliminaryResultCode == resultCode)
                {
                    result = hpvTWIResult;
                    break;
                }
            }
            return result;
        }        

        public static HPVTWIResultCollection GetAllResults()
        {
            HPVTWIResultCollection result = new HPVTWIResultCollection();
            result.Add(new HPVTWIA5679PositiveResult());
            result.Add(new HPVTWIA567PositiveResult());
            result.Add(new HPVTWIA569PositiveResult());
            result.Add(new HPVTWIA56PositiveResult());
            result.Add(new HPVTWIA79PositiveResult());
            result.Add(new HPVTWIA7PositiveResult());
            result.Add(new HPVTWIA9PositiveResult());
            result.Add(new HPVTWIHighCVResult());
            result.Add(new HPVTWILowFamFOZResult());
            result.Add(new HPVTWILowgDNAResult());
            result.Add(new HPVTWILowgDNARepeatResult());
            result.Add(new HPVTWINegativeResult());
            result.Add(new HPVTWIQNSResult());            
            return result;
        }
    }
}
