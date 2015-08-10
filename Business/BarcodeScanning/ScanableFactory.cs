using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Core.BarcodeScanning
{
    public class ScanableFactory
    {
        /*
        public static IScanable GetScanable(BarcodePrefixEnum barcodePrefix)
        {
            IScanable scanable = null;
            switch (barcodePrefix)
            {
                case BarcodePrefixEnum.HBLK:
                case BarcodePrefixEnum.ALQ:
                    scanable = new HistologyBlock();
                    break;
                case BarcodePrefixEnum.SPN:
                case BarcodePrefixEnum.HSLD:
                case BarcodePrefixEnum.SLD:
                    scanable = new HistologySlide();
                    break;
                case BarcodePrefixEnum.SBDG:
                case BarcodePrefixEnum.BDG:
                case BarcodePrefixEnum.YSU:
                    scanable = new SecurityBadge();
                    break;
                case BarcodePrefixEnum.CTNR:
                    scanable = new Container();
                    break;
                case BarcodePrefixEnum.DCMT:
                    scanable = new Document();
                    break;
                case BarcodePrefixEnum.CLNT:
                    scanable = new Client();
                    break;                
            }            
            return scanable;
        }

        public static IScanable GetScanable(Barcode barcode)
        {
            IScanable scanable = null;
            switch (barcode.Prefix)
            {
                case BarcodePrefixEnum.HBLK:
                case BarcodePrefixEnum.ALQ:
                    scanable = new HistologyBlock();
                    break;
                case BarcodePrefixEnum.SPN:
                case BarcodePrefixEnum.HSLD:
                case BarcodePrefixEnum.SLD:
                    scanable = new HistologySlide();
                    break;
                case BarcodePrefixEnum.SBDG:
                case BarcodePrefixEnum.BDG:
                case BarcodePrefixEnum.YSU:
                    scanable = new SecurityBadge();
                    break;
                case BarcodePrefixEnum.CTNR:
                    scanable = new Container();
                    break;
                case BarcodePrefixEnum.DCMT:
                    scanable = new Document();
                    break;
                case BarcodePrefixEnum.CLNT:
                    scanable = new Client();
                    break;                
                case BarcodePrefixEnum.UNDEFINED:
                    scanable = ScanableFactory.SpecialScanableCheck(barcode);
                    break;
            }
            scanable.FromBarcode(barcode);
            return scanable;
        }

        private static IScanable SpecialScanableCheck(Barcode barcode)
        {
            IScanable scanable = null;

            //Cytology Slides
            if (barcode.Body.Contains(",") == true)
            {
                scanable = new YellowstonePathology.Core.BarcodeScanning.CytologySlide();
            }
            else if (barcode.Body.StartsWith("00") == true) // SVH Medical Record Number
            {
                scanable = new YellowstonePathology.Core.BarcodeScanning.SvhMedicalRecordNo();
            }
			else if (barcode.Body.StartsWith("12") == true) // SVH Medical Record Number
			{
				scanable = new YellowstonePathology.Core.BarcodeScanning.SvhMedicalRecordNo();
			}
			else if (barcode.Body.StartsWith("700") == true) // SVH Account Number
			{
				scanable = new YellowstonePathology.Core.BarcodeScanning.SvhAccountNo();
			}
            else if (barcode.Body.Trim().Length == 20 && barcode.Body.StartsWith("701")) // US Postal Service Certified Mail
            {
                scanable = new YellowstonePathology.Core.BarcodeScanning.USPostalServiceCertifiedMail();
            }
			else
			{
				scanable = new YellowstonePathology.Core.BarcodeScanning.UnknownScan();
			}
            return scanable;
        }
        */
    }
}
