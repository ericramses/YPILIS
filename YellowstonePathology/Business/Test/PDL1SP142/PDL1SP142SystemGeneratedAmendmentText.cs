﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.PDL1SP142
{
    public class PDL1SP142SystemGeneratedAmendmentText
    {
        public PDL1SP142SystemGeneratedAmendmentText()
        {
        }

        public static string AmendmentText(PDL1SP142.PDL1SP142TestOrder testOrder)
        {
            string comment = "        Comment: This test utilizes PD - L1 antibody clone SP - 142.In general, higher levels of " +
                "PD - L1 expression are associated with better response to PD-1 antagonists.  For full details, refer to separate report.";

            StringBuilder result = new StringBuilder();
            result.Append("PD - L1 immunohistochemical stain was performed on the specimen(see YPI report # ");
            result.Append(testOrder.ReportNo);
            result.AppendLine("), which yielded the following result:");
            result.AppendLine();
            result.Append("        PD - L1 (SP142): ");
            result.Append(testOrder.StainPercent);            
            result.AppendLine();
            result.AppendLine(comment);

            return result.ToString();
        }
    }
}
