using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ZPLPrinterUSB
    {        

        public ZPLPrinterUSB()
        {            
            
        }

        public void Print(string slideOrderId, string reportNo, string patientLastName, string testName, string label, string location)
        {
            string printerName = "ZDesigner GX430t";
            Business.Label.Model.HistologySlidePaperZPLLabel histologySlidePaperZPLLabel = new Business.Label.Model.HistologySlidePaperZPLLabel(slideOrderId, reportNo, patientLastName, testName, label, location);
            StringBuilder zpl = new StringBuilder();
            histologySlidePaperZPLLabel.AppendCommands(zpl, 20);                        
            RawPrinterHelper.SendStringToPrinter(printerName, "^XA" + zpl.ToString() + "^XZ");
        }

    }
}
