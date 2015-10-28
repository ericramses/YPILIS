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

        public static HPVTWIResultCollection GetAllResults()
        {
            HPVTWIResultCollection result = new HPVTWIResultCollection();            
            result.Add(new HPVTWINegativeResult());
            result.Add(new HPVTWIPositiveResult());
            result.Add(new HPVTWIInvalidResult());
            result.Add(new HPVTWINoResult());
            return result;
        }
    }
}
