﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class BlockLabelPrinter
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<Business.Label.Model.HistologyBlockPaperZPLLabel> m_BlockLabelQueue;        

        public BlockLabelPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_BlockLabelQueue = new Queue<Business.Label.Model.HistologyBlockPaperZPLLabel>();
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
                            Business.Label.Model.HistologyBlockPaperZPLLabel blockLabel = new Business.Label.Model.HistologyBlockPaperZPLLabel(aliquotOrder.AliquotOrderId, initials, aliquotOrder.Label, accessionOrder.MasterAccessionNo);                            
                            this.m_BlockLabelQueue.Enqueue(blockLabel);
                            aliquotOrder.Printed = true;
                        }
                        else
                        {
                            string reportNo = accessionOrder.PanelSetOrderCollection[0].ReportNo;
                            string initials = Business.Helper.PatientHelper.GetPatientInitials(accessionOrder.PFirstName, accessionOrder.PLastName);
                            Business.Label.Model.HistologyBlockPaperZPLLabel blockLabel = new Business.Label.Model.HistologyBlockPaperZPLLabel(aliquotOrder.AliquotOrderId, initials, aliquotOrder.Label, reportNo);
                            this.m_BlockLabelQueue.Enqueue(blockLabel);
                            aliquotOrder.Printed = true;
                        }
                    }
                }
            }       
        }

        public BlockLabelPrinter(string aliquotOrderId, string aliquotLabel, string masterAccessionNo, string pLastName, string pFirstName)
        {
            this.m_BlockLabelQueue = new Queue<Business.Label.Model.HistologyBlockPaperZPLLabel>();
            string initials = Business.Helper.PatientHelper.GetPatientInitials(pFirstName, pLastName);
            Business.Label.Model.HistologyBlockPaperZPLLabel blockLabel = new Business.Label.Model.HistologyBlockPaperZPLLabel(aliquotOrderId, initials, aliquotLabel, masterAccessionNo);
            this.m_BlockLabelQueue.Enqueue(blockLabel);
        }        

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_BlockLabelQueue.Count > 0) result = true;
            return result;
        }        

        public void Print()
        {            
            while (this.m_BlockLabelQueue.Count != 0)
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
                if(this.m_BlockLabelQueue.Count != 0)
                {
                    Business.Label.Model.HistologyBlockPaperZPLLabel label = this.m_BlockLabelQueue.Dequeue();
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
