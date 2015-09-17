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

        public HPVTWIResult GetResult(string resultCode)
        {
            HPVTWIResult result = null;
            foreach (HPVTWIResult hpvTWIResult in this)
            {
                if (hpvTWIResult.ResultCode == resultCode)
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
            result.Add(new HPVTWINegativeResult());
            result.Add(new HPVTWIPositiveResult());            
            return result;
        }
    }
}
