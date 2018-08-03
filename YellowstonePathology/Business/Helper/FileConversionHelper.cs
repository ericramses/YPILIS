﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps.Serialization;
using System.Windows.Xps.Packaging;
using System.IO.Packaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Helper
{
    public class FileConversionHelper
    {
        public static void ConvertDocumentTo(YellowstonePathology.Business.OrderIdParser orderIdParser, CaseDocumentTypeEnum caseDocumentType,
            CaseDocumentFileTypeEnnum fromType, CaseDocumentFileTypeEnnum toType)
        {            
            string filePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo;

            switch (caseDocumentType)
            {
                case CaseDocumentTypeEnum.CaseReport:
                    //do nothing
                    break;
                case CaseDocumentTypeEnum.AdditionalTestingNotification:
                    filePath = filePath + ".notify";
                    break;
                case CaseDocumentTypeEnum.PreauthorizationRequest:
                    filePath = filePath + ".preauth";
                    break;
            }

            if (fromType == CaseDocumentFileTypeEnnum.xml && toType == CaseDocumentFileTypeEnnum.doc)
            {
                ConvertXMLToDoc(filePath + ".xml", filePath + ".doc");
            }
            else if (fromType == CaseDocumentFileTypeEnnum.doc && toType == CaseDocumentFileTypeEnnum.xps)
            {
                ConvertDocToXPS(filePath + ".doc", filePath + ".xps");
            }
            else if (fromType == CaseDocumentFileTypeEnnum.xps && toType == CaseDocumentFileTypeEnnum.tif)
            {
                ConvertXPSToTIF(filePath + ".xps", filePath + ".tif");
            }
            else if (fromType == CaseDocumentFileTypeEnnum.xml && toType == CaseDocumentFileTypeEnnum.pdf)
            {
                ConvertXMLToPDF(filePath + ".xml", filePath + ".pdf");
            }
        }

        public static void ConvertXMLToPDF(object xmlFilename, object pdfFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            try
            {
                File.Delete(pdfFileName.ToString());
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("This file is locked and cannot be published at this time: " + reportNo);
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFilename, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            doc.SaveAs(ref pdfFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            
            CaseDocument.ReleaseComObject(oFmt);
            CaseDocument.ReleaseComObject(doc);
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            CaseDocument.ReleaseComObject(oWord);
        }

        public static void ConvertXMLToDoc(object xmlFileName, object docFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            try
            {
                File.Delete(docFileName.ToString());
            }
            catch (Exception)
            {
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Object fileFormat = "wdFormatDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;

            doc.SaveAs(ref docFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            CaseDocument.ReleaseComObject(oFmt);
            CaseDocument.ReleaseComObject(doc);
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            CaseDocument.ReleaseComObject(oWord);
        }

        public static void ConvertDocToXPS(object docFileName, object xpsFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            string currentPrinter = oWord.ActivePrinter;
            oWord.ActivePrinter = "Microsoft XPS Document Writer";           

            Object fileFormat = "wdFormatDocument";

            if (System.IO.File.Exists(docFileName.ToString()) == true)
            {
                Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref docFileName, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                object oOutputFile = xpsFileName;
                doc.PrintOut(ref oFalse, ref oFalse, ref oMissing, ref oOutputFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);                
            }

            oWord.ActivePrinter = currentPrinter;
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);            
        }

        public static bool ConvertXPSToPDF(string xpsFileName, string pdfFileName)
        {
            bool result = true;
			
            string gxpsFilePath = "C:\\Program Files\\Yellowstone Pathology Institute\\gxps.exe";
            string arguments =  " -sDEVICE=pdfwrite -sOutputFile=" + xpsFileName + " -dNOPAUSE " + pdfFileName;

            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = gxpsFilePath;
            processStartInfo.Arguments = arguments;

            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();

            return result;
        }       

        static public void ConvertXPSToTIF(string xpsFileName, string tifFileName)
        {			
            if (File.Exists(xpsFileName) == true)
            {                
                XpsDocument xpsDoc = new XpsDocument(xpsFileName, System.IO.FileAccess.Read);                
                FixedDocumentSequence docSeq = xpsDoc.GetFixedDocumentSequence();
                int pages = docSeq.DocumentPaginator.PageCount;

                TiffBitmapEncoder encoder = new TiffBitmapEncoder();                
				encoder.Compression = TiffCompressOption.Default;				
			    encoder.Compression = TiffCompressOption.Ccitt4;				    
                
                for (int pageNum = 0; pageNum < pages; pageNum++)
                {
                    DocumentPage docPage = docSeq.DocumentPaginator.GetPage(pageNum);
                    RenderTargetBitmap renderTarget =
						new RenderTargetBitmap((int)(docPage.Size.Width * 300 / 96),
												(int)(docPage.Size.Height * 300 / 96),
                                                300d, 
                                                300d,
                                                System.Windows.Media.PixelFormats.Default);

                    renderTarget.Render(docPage.Visual);
                    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                }

                FileStream pageOutStream = new FileStream(tifFileName, FileMode.Create, FileAccess.Write);
                encoder.Save(pageOutStream);
                pageOutStream.Close();

                xpsDoc.Close();
            }
        }

		public static void SaveXpsAsMultiPageTif(string reportNo, object visual)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string outputFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
			FrameworkElement frameworkElement = (FrameworkElement)visual;

			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)frameworkElement.ActualWidth,
									  (int)frameworkElement.ActualHeight, 96d, 96d, PixelFormats.Default);
			renderTargetBitmap.Render(frameworkElement);

			TiffBitmapEncoder tiffBitmapEncoder = new TiffBitmapEncoder();
			tiffBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

			using (Stream stream = File.Create(outputFileName))
			{
				tiffBitmapEncoder.Save(stream);
			}
		}        

        public static void SaveFixedDocumentAsTiff(FixedDocument document, string outputFileName)
		{
			int pages = document.DocumentPaginator.PageCount;

			TiffBitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Compression = TiffCompressOption.Ccitt4;

			for (int pageNum = 0; pageNum < pages; pageNum++)
			{
				DocumentPage docPage = document.DocumentPaginator.GetPage(pageNum);

				RenderTargetBitmap renderTarget =
					new RenderTargetBitmap((int)(docPage.Size.Width * 300 / 96),
											(int)(docPage.Size.Height * 300 / 96),
											300d,
											300d,
											System.Windows.Media.PixelFormats.Default);

				renderTarget.Render(docPage.Visual);
				encoder.Frames.Add(BitmapFrame.Create(renderTarget));
			}

			FileStream outputFileStream = new FileStream(outputFileName, FileMode.Create);
			encoder.Save(outputFileStream);
			outputFileStream.Close();
		}
	}
}
