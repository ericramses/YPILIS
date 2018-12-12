using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.MDSByFish
{
    public class ProbeCommentResolver
    {
        public ProbeCommentResolver()
        { }

        public static string ResolveComment(PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            StringBuilder result = new StringBuilder();
            int quantity88374 = CodeCount(panelSetOrderCPTCodeCollection, "88374");
            int quantity88377 = CodeCount(panelSetOrderCPTCodeCollection, "88377");
            string result88374 = CodeResult(quantity88374, " of computer assisted, multiplex stains or probe sets");
            string result88377 = CodeResult(quantity88377, " of manual, multiplex stains or probe sets");

            if (string.IsNullOrEmpty(result88374) == false || string.IsNullOrEmpty(result88377) == false)
            {
                result.Append("This analysis was performed using ");
                if(string.IsNullOrEmpty(result88374) == false)
                {
                    result.Append(result88374);
                    if (string.IsNullOrEmpty(result88377) == false)
                    {
                        result.Append(" and ");
                        result.Append(result88377);
                    }
                }
                else
                {
                    result.Append(result88377);
                }
                result.Append(".");
            }

            return result.ToString();
        }

        private static int CodeCount(PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection, string code)
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

        private static string CodeResult(int quantity, string comment)
        {
            string result = string.Empty;
            if (quantity > 0)
            {
                result = quantity.ToString();
                result += quantity == 1 ? " set" : " sets";
                result +=comment;
            }
            return result;
        }
    }
}
