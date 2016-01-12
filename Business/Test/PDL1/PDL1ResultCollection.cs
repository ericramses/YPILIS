using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
    public class PDL1ResultCollection : TestResultCollection
    {
        public PDL1ResultCollection()
        {
            this.Add(new PDL1NegativeResult());
            this.Add(new PDL1PositiveResult());
            this.Add(new PDL1NoResult());
        }

        public PDL1Result GetByResultCode(string resultCode)
        {
            PDL1Result result = null;
            foreach(PDL1Result pdl1Result in this)
            {
                if (pdl1Result.ResultCode == resultCode)
                {
                    result = pdl1Result;
                    break;
                }
            }
            return result;
        }
    }
}
