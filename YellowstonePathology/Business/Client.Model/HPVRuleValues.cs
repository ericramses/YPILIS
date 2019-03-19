using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleValues
    {
        public static string Any = "Any";
        public static string NotSet = "Not Set";
        public static string NotUsed = "N/A";
        // Age
        public static string AgeLessThan20 = "Younger than 20 y.o.";
        public static string AgeOlderThan20 = "Older than 20 y.o.";
        public static string Age25andOlder = "25 y.o. and older";
        public static string AgeOlderThan30 = "Older than 30 y.o.";
        public static string AgeBetween21and29 = "Between 21 and 29 y.o.";

        //Pap result
        public static string PAPResultNormal = "Normal";
        public static string PAPResultAbnormal = "Abnormal";
        public static string PAPResultNormalorReactive = "Normal or Reactive";
        public static string PAPResultASCUS = "ASCUS";
        public static string PAPResultASCUSorAGUS = "ASCUS or AGUS";
        public static string PAPResultASCUSorLSIL = "ASCUS or LSIL";
        public static string PAPResultASCUSAGUSLSILorHSIL = "ASCUS, AGUS, LSIL or HSIL";
        public static string PAPResultASCUSorHigher = "ASCUS or higher";

        //HPV result
        public static string HPVResultPositive = "Positive";
    }
}
