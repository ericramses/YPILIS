using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using YellowstonePathology.Business.Twain;

namespace YellowstonePathology.UI
{
    public class ScanDocumentsV2
    {
        private List<Image> m_ImageList;

        private Twain m_Twain;
        private YellowstonePathology.Business.Common.PageScannerCollection m_PageScannerCollection;

        public ScanDocumentsV2(Window window)
        {
            this.m_ImageList = new List<Image>();
            this.m_Twain = new Twain(new WpfWindowMessageHook(window));
            this.m_Twain.TransferImage += new EventHandler<TransferImageEventArgs>(Twain_TransferImage);
            this.m_Twain.ScanningComplete += new EventHandler<ScanningCompleteEventArgs>(Twain_ScanningComplete);
        }

        private void Twain_ScanningComplete(object sender, ScanningCompleteEventArgs e)
        {
            //IsEnabled = true;
        }

        private void Twain_TransferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null)
            {
                Image image = new Image();
                image.Source = Imaging.CreateBitmapSourceFromHBitmap(
                        new System.Drawing.Bitmap(e.Image).GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                image.Margin = new Thickness(2);
                //this.StackPanelImages.Children.Add(image);
                this.m_ImageList.Add(image);
            }
        }
        
        public void Scan()
        {
            YellowstonePathology.Business.Common.PageScanner pageScanner = this.m_PageScannerCollection.SelectedPageScanner;
            this.m_Twain.SelectSource(pageScanner.ScannerName);
            this.m_Twain.StartScanning(pageScanner.ScanSettings);
        }
    }
}
