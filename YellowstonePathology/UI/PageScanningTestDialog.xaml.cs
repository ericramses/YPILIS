﻿using System;
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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using YellowstonePathology.Business.Twain;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PageScanningTestDialog.xaml
    /// </summary>
    public partial class PageScanningTestDialog : Window
    {
        Twain _twain;
        ScanSettings _settings;

        public PageScanningTestDialog()
        {            
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PageScanningTestDialog_Loaded);
        }

        private void PageScanningTestDialog_Loaded(object send, RoutedEventArgs e)
        {            
            _twain = new Twain(new WpfWindowMessageHook(this));
            _twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    image1.Source = Imaging.CreateBitmapSourceFromHBitmap(
                            new System.Drawing.Bitmap(args.Image).GetHbitmap(),
                            IntPtr.Zero,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
                }
            };
            _twain.ScanningComplete += delegate
            {
                IsEnabled = true;
            };

            var sourceList = _twain.SourceNames;
            ManualSource.ItemsSource = sourceList;

            if (sourceList != null && sourceList.Count > 0)
            {
                ManualSource.SelectedItem = sourceList[0];
            }           
        }

        private void selectSourceButton_Click(object sender, RoutedEventArgs e)
        {
            _twain.SelectSource();
        }

        private void scanButton_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            _settings = new ScanSettings()
            {
                UseDocumentFeeder = useAdfCheckBox.IsChecked == true,
                ShowTwainUI = useTwainUICheckBox.IsChecked == true
            };

            try
            {
                if (SourceUserSelected.IsChecked == true)
                {
                    _twain.SelectSource(ManualSource.SelectedItem.ToString());
                }

                _twain.StartScanning(_settings);
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message);
            }

            IsEnabled = true;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (image1.Source != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                if (sfd.ShowDialog() == true)
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image1.Source));

                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.OpenOrCreate))
                    {
                        encoder.Save(stream);
                    }
                }
            }
            */
        }
    }
}
