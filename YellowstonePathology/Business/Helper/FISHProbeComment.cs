using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Helper
{
    public class FISHProbeComment
    {
        private YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection m_PanelSetOrderCPTCodeCollection;
        private string m_Comment;
        private bool m_Success;
        private string m_Message;
        private List<Business.Billing.Model.CptCode> m_FISHCPTCodes;

        public FISHProbeComment(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            this.m_PanelSetOrderCPTCodeCollection = panelSetOrderCPTCodeCollection;

            this.m_FISHCPTCodes = new List<Billing.Model.CptCode>();
            Business.Billing.Model.CptCodeCollection allCPTCodes = Store.AppDataStore.Instance.CPTCodeCollection.Clone();
            this.m_FISHCPTCodes.Add(Billing.Model.CptCode.)

            if (this.OKToCreateComment() == true)
            {
                StringBuilder result = new StringBuilder();
                StringBuilder temp = new StringBuilder();

                int quantity88374 = GetCodeCount(panelSetOrderCPTCodeCollection, "88374");
                int quantity88377 = GetCodeCount(panelSetOrderCPTCodeCollection, "88377");
                int quantity88368 = GetCodeCount(panelSetOrderCPTCodeCollection, "88368");
                int quantity88369 = GetCodeCount(panelSetOrderCPTCodeCollection, "88369");
                int quantity88367 = GetCodeCount(panelSetOrderCPTCodeCollection, "88367");
                int quantity88373 = GetCodeCount(panelSetOrderCPTCodeCollection, "88373");

                GetCodeResult(temp, quantity88374, " of computer assisted, multiplex stains or probe sets");
                GetCodeResult(temp, quantity88377, " of manual, multiplex stains or probe sets");
                GetCodeResult(temp, quantity88368 + quantity88369, " of manual, single stains or probe sets");
                GetCodeResult(temp, quantity88367 + quantity88373, " of computer assisted, single stains or probe sets");

                if (temp.Length > 0)
                {
                    result.Append("This analysis was performed using ");
                    result.Append(temp);
                    result.Append(".");
                }

                this.m_Comment = result.ToString();
            }            
        }        

        public string Comment
        {
            get { return this.m_Comment; }            
        }

        public bool Success
        {
            get { return this.m_Success; }
        }

        public string Message
        {
            get { return this.m_Message; }
        }

        private bool OKToCreateComment()
        {
            bool result = true;
            if(this.m_PanelSetOrderCPTCodeCollection.Exists("asdf")
            return result;
        }

        private int GetCodeCount(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection, string code)
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

        private void GetCodeResult(StringBuilder temp, int quantity, string comment)
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
