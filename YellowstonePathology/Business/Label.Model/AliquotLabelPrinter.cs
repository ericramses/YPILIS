using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class AliquotLabelPrinter
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<Business.Label.Model.AliquotPaperZPLLabel> m_AliquotLabelQueue;        

        public AliquotLabelPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_AliquotLabelQueue = new Queue<Business.Label.Model.AliquotPaperZPLLabel>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel == true)
                    {
                        YellowstonePathology.Business.OrderIdParser orderIdParser = new OrderIdParser(accessionOrder.MasterAccessionNo);
                        if (orderIdParser.IsLegacyMasterAccessionNo == false)
                        {
                            string initials = Business.Helper.PatientHelper.GetPatientInitials(accessionOrder.PFirstName, accessionOrder.PLastName);
                            Business.Label.Model.AliquotPaperZPLLabel aliquotLabel = new Business.Label.Model.AliquotPaperZPLLabel(aliquotOrder.AliquotOrderId, accessionOrder.PLastName, accessionOrder.PFirstName, aliquotOrder.Label, accessionOrder.MasterAccessionNo);                            
                            this.m_AliquotLabelQueue.Enqueue(aliquotLabel);
                            aliquotOrder.Printed = true;
                        }
                        else
                        {
                            string reportNo = accessionOrder.PanelSetOrderCollection[0].ReportNo;
                            string initials = Business.Helper.PatientHelper.GetPatientInitials(accessionOrder.PFirstName, accessionOrder.PLastName);
                            Business.Label.Model.AliquotPaperZPLLabel aliquotLabel = new Business.Label.Model.AliquotPaperZPLLabel(aliquotOrder.AliquotOrderId, accessionOrder.PFirstName, accessionOrder.PLastName, aliquotOrder.Label, reportNo);
                            this.m_AliquotLabelQueue.Enqueue(aliquotLabel);
                            aliquotOrder.Printed = true;
                        }
                    }
                }
            }       
        }

        public AliquotLabelPrinter(string aliquotOrderId, string aliquotLabel, string masterAccessionNo, string pLastName, string pFirstName)
        {
            this.m_AliquotLabelQueue = new Queue<Business.Label.Model.AliquotPaperZPLLabel>();
            Business.Label.Model.AliquotPaperZPLLabel blockLabel = new Business.Label.Model.AliquotPaperZPLLabel(aliquotOrderId, pLastName, pFirstName, aliquotLabel, masterAccessionNo);
            this.m_AliquotLabelQueue.Enqueue(blockLabel);
        }        

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_AliquotLabelQueue.Count > 0) result = true;
            return result;
        }        

        public void Print()
        {            
            while (this.m_AliquotLabelQueue.Count != 0)
            {
                this.PrintRow();
            }
        }

        private void PrintRow()
        {
            StringBuilder result = new StringBuilder();
            int xOffset = 0;

            result.Append("^XA");
            for (int i = 0; i < 4; i++)
            {
                if(this.m_AliquotLabelQueue.Count != 0)
                {
                    Business.Label.Model.AliquotPaperZPLLabel label = this.m_AliquotLabelQueue.Dequeue();
                    label.AppendCommands(result, xOffset);
                    xOffset += 325;
                }                
            }

            result.Append("^XZ");

            Business.Label.Model.ZPLPrinter printer = new ZPLPrinter("10.1.1.21");
            printer.Print(result.ToString());
        }        
    }
}
