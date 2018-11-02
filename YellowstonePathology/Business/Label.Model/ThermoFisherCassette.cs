﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThermoFisherCassette : Cassette
    {
        private string m_Delimeter = "#";
        private string m_Prefix = "$";
        private string m_CassetteColumnDelimiter = "H";

        public override string GetFileExtension()
        {
            return ".txt";
        }

        public override string GetLine()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);

            StringBuilder line = new StringBuilder(this.m_Prefix + this.m_Delimeter);

            CassettePrinterCollection printers = new CassettePrinterCollection();
            CassettePrinter printer = printers.GetPrinter(Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter);
            CarouselColumn carouselColumn = printer.Carousel.GetColumn(this.m_CassetteColor);

            line.Append(this.m_CassetteColumnDelimiter + carouselColumn.PrinterCode + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            line.Append(this.BlockTitle + this.m_Delimeter);
            line.Append(this.PatientInitials + this.m_Delimeter);
            line.Append(this.CompanyId + this.m_Delimeter);
            line.Append(this.ScanningId + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
            return line.ToString();            
        }
    }
}
