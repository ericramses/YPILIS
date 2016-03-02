using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.Business.XPSDocument.Result.Xps
{
    public class CytologyScreeningListReport
    {
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
        private Data.CytologyScreeningListReportData m_CytologyScreeningListReportData;

        public CytologyScreeningListReport(Data.CytologyScreeningListReportData cytologyScreeningListReportData)
        {
            this.m_CytologyScreeningListReportData = cytologyScreeningListReportData;

            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Cytology Screening List");
            YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(string.Empty);

            this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

            this.WriteHeaderGrid();
            this.WriteDetailInfo();
        }

        public FixedDocument FixedDocument
        {
            get { return this.m_ReportDocument.FixedDocument; }
        }

        private void WriteHeaderGrid()
        {
            Grid grid = SetupDetailGrid();
            this.WriteHeaderText("Report No", grid, 0, 0);
            this.WriteHeaderText("Patient Name", grid, 0, 1);
            this.WriteHeaderText("Screened By", grid, 0, 2);
            this.WriteHeaderText("Accessioned", grid, 0, 3);
            this.WriteHeaderText("Finaled", grid, 0, 4);

            this.m_ReportDocument.WriteRowContent(grid);

            Border border = new Border()
            {
                BorderBrush = System.Windows.Media.Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            Grid.SetColumn(border, 0);
            Grid.SetRow(border, 0);

            m_ReportDocument.WriteBorder(border);
        }

        private void WriteHeaderText(string text, Grid grid, int row, int column)
        {
            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = text;
            textBlockText.Margin = new Thickness(2, 0, 2, 0);
            textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockText.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(textBlockText, column);
            Grid.SetRow(textBlockText, row);
            grid.Children.Add(textBlockText);
        }

        public void WriteDetailInfo()
        {
            foreach (XElement specimenElement in this.m_CytologyScreeningListReportData.Elements("CytologyScreening"))
            {
                Grid detailGrid = this.SetupDetailGrid();
                this.WriteDetailReportNo(specimenElement, detailGrid);
                this.WriteDetailPatientName(specimenElement, detailGrid);
                this.WriteDetailScreenedByName(specimenElement, detailGrid);
                this.WriteDetailAccessionTime(specimenElement, detailGrid);
                this.WriteDetailScreeningFinalTime(specimenElement, detailGrid);
                this.m_ReportDocument.WriteRowContent(detailGrid);
            }
        }

        private Grid SetupDetailGrid()
        {
            Grid detailGrid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 0.65);
            detailGrid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 2.5);
            detailGrid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 1.65);
            detailGrid.ColumnDefinitions.Add(col3);

            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(96 * 1.15);
            detailGrid.ColumnDefinitions.Add(col4);

            ColumnDefinition col5 = new ColumnDefinition();
            col5.Width = new GridLength(96 * 1.15);
            detailGrid.ColumnDefinitions.Add(col5);

            RowDefinition row = new RowDefinition();
            detailGrid.RowDefinitions.Add(row);

            return detailGrid;
        }

        private void WriteDetailReportNo(XElement detailElement, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = detailElement.Element("ReportNo").Value;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailPatientName(XElement detailElement, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = detailElement.Element("PatientName").Value;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailScreenedByName(XElement detailElement, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = detailElement.Element("ScreenedByName").Value;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 2);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailAccessionTime(XElement detailElement, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = detailElement.Element("AccessionTime").Value;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 3);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailScreeningFinalTime(XElement detailElement, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = detailElement.Element("ScreeningFinalTime").Value;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 4);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }
    }
}
