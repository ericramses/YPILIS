using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Helper
{
    public class ProbeCommentResolver
    {
        public ProbeCommentResolver()
        { }

        public static string ResolveComment(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            int quantity88374 = CodeCount(panelSetOrderCPTCodeCollection, "88374");
            int quantity88377 = CodeCount(panelSetOrderCPTCodeCollection, "88377");
            int quantity88368 = CodeCount(panelSetOrderCPTCodeCollection, "88368");
            int quantity88369 = CodeCount(panelSetOrderCPTCodeCollection, "88369");
            int quantity88367 = CodeCount(panelSetOrderCPTCodeCollection, "88367");
            int quantity88373 = CodeCount(panelSetOrderCPTCodeCollection, "88373");

            CodeResult(temp, quantity88374, " of computer assisted, multiplex stains or probe sets");
            CodeResult(temp, quantity88377, " of manual, multiplex stains or probe sets");
            CodeResult(temp, quantity88368 + quantity88369, " of manual, single stains or probe sets");
            CodeResult(temp, quantity88367 + quantity88373, " of computer assisted, single stains or probe sets");

            if (temp.Length > 0)
            {
                result.Append("This analysis was performed using ");
                result.Append(temp);
                result.Append(".");
            }

            return result.ToString();
        }

        private static int CodeCount(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection, string code)
        {
            int result = 0;

            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrderCPTCodeCollection)
            {
                if (panelSetOrderCPTCode.CPTCode == code)
                {
                    result += panelSetOrderCPTCode.Quantity;
                }
            }
            return result;
        }

        private static void CodeResult(StringBuilder temp, int quantity, string comment)
        {
            if (quantity > 0)
            {
                if (temp.Length > 0) temp.Append("; ");
                temp.Append(quantity.ToString());
                temp.Append(quantity == 1 ? " set" : " sets");
                temp.Append(comment);
            }
        }
    }
}
