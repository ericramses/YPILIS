using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Client
{
	public class PackingSlip
	{
		YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
		PackingSlipData m_PackingSlipData;

		public PackingSlip(PackingSlipData packingSlipData)
		{
			this.m_PackingSlipData = packingSlipData;


			YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Packing Slip");
			YellowstonePathology.Document.Xps.NoFooter footer = new YellowstonePathology.Document.Xps.NoFooter();

			this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

			this.WriteHeader();
			this.WriteShipmentDetails();
			this.WritePatientDetails();
		}

		public FixedDocument FixedDocument
		{
			get { return this.m_ReportDocument.FixedDocument; }
		}

		private void WriteHeader()
		{
			Grid headerGrid = new Grid();
			RowDefinition row = new RowDefinition();
			headerGrid.RowDefinitions.Add(row);

			TextBlock label = new TextBlock();
			label.Text = "Packing Slip";
			label.Margin = new Thickness(2, 2, 2, 10);
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 0);
			headerGrid.Children.Add(label);
		}

		private void WriteShipmentDetails()
		{
			this.WriteShipmentDetailLine("Shipped To:", this.m_PackingSlipData.Element("ShipmentTo").Value);
			this.WriteShipmentDetailLine("Shipped From:", this.m_PackingSlipData.Element("ShipmentFrom").Value);
			this.WriteShipmentDetailLine("Ship Date:", this.m_PackingSlipData.Element("ShipDate").Value);
			this.WriteShipmentDetailLine("Prepared By:", this.m_PackingSlipData.Element("ShipmentPreparedBy").Value);
		}

		private void WritePatientDetails()
		{
			TextBlock label = new TextBlock();
			label.Width = 96 * 1.5;
			//label.Text = "Specimen Description";
			label.Margin = new Thickness(5);
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			this.m_ReportDocument.WriteRowContent(label);

			foreach (XElement clientOrderElement in this.m_PackingSlipData.Element("ClientOrderCollection").Elements("ClientOrder"))
			{
				this.WritePatientLine(clientOrderElement);
				foreach (XElement specimenElement in clientOrderElement.Element("ClientOrderDetailCollection").Elements("ClientOrderDetail"))
				{
					this.WriteSpecimenDescriptionLine(specimenElement);
					this.WriteContainerIdLine(specimenElement);
				}
			}
		}

		private void WriteShipmentDetailLine(string labelText, string textValue)
		{
			Grid grid = new Grid();
			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);
			ColumnDefinition column1 = new ColumnDefinition();
			column1.Width = new GridLength(96 * .95, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column1);
			ColumnDefinition column2 = new ColumnDefinition();
			column2.Width = new GridLength(96 * 3, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column2);

			TextBlock label = new TextBlock();
			label.Text = labelText;
			label.Margin = new Thickness(2, 2, 2, 2);
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 0);
			grid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = textValue;
			text.Margin = new Thickness(2, 2, 2, 2);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 0);
			grid.Children.Add(text);
			this.m_ReportDocument.WriteRowContent(grid);
		}

		private void WritePatientLine(XElement clientOrderElement)
		{
			Grid grid = new Grid();
			//grid.Margin = new Thickness(0, 10, 0, 0);
			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);
			ColumnDefinition column1 = new ColumnDefinition();
			column1.Width = new GridLength(96 * .72, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column1);
			ColumnDefinition column2 = new ColumnDefinition();
			column2.Width = new GridLength(96 * 1.5, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column2);
			ColumnDefinition column3 = new ColumnDefinition();
			column3.Width = new GridLength(96 * 1.1, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column3);
			ColumnDefinition column4 = new ColumnDefinition();
			column4.Width = new GridLength(96 * 1.25, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column4);

			TextBlock label1 = new TextBlock();
			label1.Text = "Patient:";
			label1.Margin = new Thickness(0, 10, 2, 2);
			label1.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label1.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label1, 0);
			Grid.SetRow(label1, 0);
			grid.Children.Add(label1);

			TextBlock text1 = new TextBlock();
			text1.Text = clientOrderElement.Element("PatientName").Value;
			text1.Margin = new Thickness(2, 10, 2, 2);
			text1.FontWeight = FontWeight.FromOpenTypeWeight(700);
			text1.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text1, 1);
			Grid.SetRow(text1, 0);
			grid.Children.Add(text1);

			TextBlock label2 = new TextBlock();
			label2.Text = "Birthdate:";
			label2.Margin = new Thickness(2, 10, 2, 2);
			label2.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label2.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label2, 2);
			Grid.SetRow(label2, 0);
			grid.Children.Add(label2);

			TextBlock text2 = new TextBlock();
			text2.Text = clientOrderElement.Element("PBirthdate").Value;
			text2.Margin = new Thickness(2, 10, 2, 2);
			text2.FontWeight = FontWeight.FromOpenTypeWeight(700);
			text2.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text2, 3);
			Grid.SetRow(text2, 0);
			grid.Children.Add(text2);
			this.m_ReportDocument.WriteRowContent(grid);
		}

		private void WriteSpecimenDescriptionLine(XElement clientOrderDetailElement)
		{
			Grid grid = new Grid();
			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);
			ColumnDefinition column1 = new ColumnDefinition();
			column1.Width = new GridLength(96 * .5, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column1);
			ColumnDefinition column2 = new ColumnDefinition();
			column2.Width = new GridLength(96 * 4, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column2);
			ColumnDefinition column3 = new ColumnDefinition();
			column3.Width = new GridLength(96, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column3);
			ColumnDefinition column4 = new ColumnDefinition();
			column4.Width = new GridLength(96 * 2, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column4);

			TextBlock text1 = new TextBlock();
			text1.Text = clientOrderDetailElement.Element("SpecimenNumber").Value;
			text1.Margin = new Thickness(20, 2, 2, 2);
			text1.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text1, 0);
			Grid.SetColumnSpan(text1, 2);
			Grid.SetRow(text1, 0);
			grid.Children.Add(text1);

			/*TextBlock text2 = new TextBlock();
			text2.Text = clientOrderDetailElement.Element("Description").Value;
			text2.Margin = new Thickness(2, 2, 2, 2);
			text2.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text2, 1);
			Grid.SetRow(text2, 0);
			grid.Children.Add(text2);*/

			TextBlock label3 = new TextBlock();
			label3.Text = "Collected:";
			label3.Margin = new Thickness(2, 2, 2, 2);
			label3.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label3, 2);
			Grid.SetRow(label3, 0);
			grid.Children.Add(label3);

			TextBlock text3 = new TextBlock();
			text3.Text = clientOrderDetailElement.Element("CollectionDate").Value;
			text3.Margin = new Thickness(2, 2, 2, 2);
			text3.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text3, 3);
			Grid.SetRow(text3, 0);
			grid.Children.Add(text3);
			this.m_ReportDocument.WriteRowContent(grid);
		}

		private void WriteContainerIdLine(XElement clientOrderDetailElement)
		{
			Grid grid = new Grid();
			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);
			ColumnDefinition column1 = new ColumnDefinition();
			column1.Width = new GridLength(96 * 1.3, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column1);
			ColumnDefinition column2 = new ColumnDefinition();
			column2.Width = new GridLength(96 * 4.1, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column2);
			ColumnDefinition column3 = new ColumnDefinition();
			column3.Width = new GridLength(96 * .2, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column3);
			ColumnDefinition column4 = new ColumnDefinition();
			column4.Width = new GridLength(96 * 1.5, GridUnitType.Pixel);
			grid.ColumnDefinitions.Add(column4);

			TextBlock label1 = new TextBlock();
			label1.Text = "Container Id:";
			label1.Margin = new Thickness(2, 2, 2, 2);
			label1.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label1, 0);
			Grid.SetRow(label1, 0);
			grid.Children.Add(label1);

			TextBlock text1 = new TextBlock();
			text1.Text = clientOrderDetailElement.Element("ContainerId").Value;
			text1.Margin = new Thickness(2, 2, 2, 2);
			text1.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text1, 1);
			Grid.SetRow(text1, 0);
			grid.Children.Add(text1);

			TextBlock label2 = new TextBlock();
			label2.Text = "By:";
			label2.Margin = new Thickness(2, 2, 2, 2);
			label2.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label2, 2);
			Grid.SetRow(label2, 0);
			grid.Children.Add(label2);

			TextBlock text2 = new TextBlock();
			text2.Text = clientOrderDetailElement.Element("OrderedBy").Value;
			text2.Margin = new Thickness(2, 2, 2, 2);
			text2.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text2, 3);
			Grid.SetRow(text2, 0);
			grid.Children.Add(text2);
			this.m_ReportDocument.WriteRowContent(grid);
		}
	}
}
