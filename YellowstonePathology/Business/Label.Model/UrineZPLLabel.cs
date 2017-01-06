using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class UrineZPLLabel
    {
        public UrineZPLLabel()
        {

        }

        public static string GetCommands()
        {                        
            StringBuilder result = new StringBuilder();
            int xOffset = 0;

            result.Append("^XA");
            for (int i = 0; i < 4; i++)
            {
                YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode = Business.BarcodeScanning.ContainerBarcode.Parse();
                GetOne(containerBarcode.ToString(), result, xOffset);
                xOffset += 325;
            }
            
            result.Append("^XZ");
            return result.ToString();
        }

        private static void GetOne(string containerId, StringBuilder result, int xOffset)
        {
            string line1 = containerId.Substring(0, 14);
            string line2 = containerId.Substring(14, 14);
            string line3 = containerId.Substring(28);


            result.Append("^FO" + (xOffset + 65) + ",030^ATN,50^FD" + "Urine" + "^FS");
            result.Append("^FO" + (xOffset + 30) + ",090^ATN,50^FD" + "84166-26" + "^FS");
            result.Append("^FO" + (xOffset + 170) + ",150^ATN,50^FD" + "MD" + "^FS");
            result.Append("^FO" + (xOffset + 30) + ",210^ATN,50^FD" + "    /    /17" + "^FS");
        }
    }
}