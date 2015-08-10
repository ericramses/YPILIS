using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.IO;
using System.IO.Packaging;
using System.Windows.Documents;
using System.Windows.Markup;

namespace YellowstonePathology.YpiConnect.Client
{
    public class XpsDocumentHelper
    {
        public static XpsDocument FromMemoryStream(System.IO.MemoryStream memoryStream)
        {
            string tempPath = "pack://" + Guid.NewGuid().ToString() + ".xps";
            System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(memoryStream);
            Uri uri = new Uri(tempPath);
            System.IO.Packaging.PackageStore.AddPackage(uri, package);
            XpsDocument xpsDocument = new XpsDocument(package, System.IO.Packaging.CompressionOption.Maximum, tempPath);
            return xpsDocument;
        }

		public static void ConvertToXps(FixedDocument fixedDocument, Stream outputStream)
		{
			Package package = Package.Open(outputStream, FileMode.Create);
			XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Normal);
			XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
			FixedDocumentSequence fixedDocSeq = new FixedDocumentSequence();
			DocumentReference docRef = new DocumentReference();
			docRef.BeginInit();
			docRef.SetDocument(fixedDocument);
			docRef.EndInit();
			((IAddChild)fixedDocSeq).AddChild(docRef);
			xpsDocumentWriter.Write(fixedDocSeq.DocumentPaginator);
			xpsDocument.Close();
			package.Close();
		}
	}
}
