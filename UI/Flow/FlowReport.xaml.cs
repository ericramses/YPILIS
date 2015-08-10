using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace YellowstonePathology.UI.Flow
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>

    public partial class FlowReport : System.Windows.Controls.UserControl
    {
        public FlowReport()
        {
            InitializeComponent();            
            
        }

        public void Test()
        {
            //System.Windows.Controls.PrintDialog kk = new PrintDialog();
            
            //kk.PrintTicket.
            /*
             FlowDocument doc = …;

                IDocumentPaginator paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;
                paginator.PageSize = new Size( ?, ?) ;
                paginator.ComputePageCount();
                StackPanel container = new StackPanel();
                for(int i=0; i < paginator.PageCount; i++) {
                DocumentPageView dpv = new DocumentPageView()
                dpv.DocumentPaginator = paginator;
                dpv.PageNumber = i;
                container.Add(dpv);

                }
                
                container.Arrange(new Rect(0, 0, +Infinity, +infinty)) ;               
                do bitmap capture of container (this *could* be quite a large bitmap)

             */

            /*
            DocumentPaginator paginator = ((IDocumentPaginatorSource)this.flowDocumentFlowReport).DocumentPaginator;
            paginator.PageSize = new Size(300, 300);
            paginator.ComputePageCount();
            StackPanel container = new StackPanel();
            for (int i = 0; i < paginator.PageCount; i++)
            {
                DocumentPageView dpv = new DocumentPageView();
                dpv.DocumentPaginator = paginator;
                dpv.PageNumber = i;
                container.Children.Add(dpv);
            }


            PixelFormat format = new PixelFormat();
            
            RenderTargetBitmap bmp = new RenderTargetBitmap(300, 300, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(container);
            
            using (FileStream outStream = new FileStream(@"c:\mybutton.bmp", FileMode.Create))
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bmp));
                enc.Save(outStream);
            }          
            */
        }

    }
}