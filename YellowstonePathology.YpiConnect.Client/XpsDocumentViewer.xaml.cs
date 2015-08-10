using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using System.Xml;
using System.IO;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class XpsDocumentViewer : Window
    {        
        public XpsDocumentViewer()
        {            
            InitializeComponent();            
        }        

        public void LoadDocument(XpsDocument xpsDocument)
        {            
            FixedDocumentSequence fixedDocumentSequence = xpsDocument.GetFixedDocumentSequence();
            this.DocumentViewerReports.Document = fixedDocumentSequence as IDocumentPaginatorSource;
        }

        public void LoadDocument(FixedDocument fixedDocument)
        {
            this.DocumentViewerReports.Document = fixedDocument;
        }
    }
}
