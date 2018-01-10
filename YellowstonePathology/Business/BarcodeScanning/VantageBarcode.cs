using System;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class VantageBarcode
    {
        private string m_ScanData;
        private string m_ReportNo;
        private string m_Specimen;
        private string m_Block;
        private string m_Slide;

        public VantageBarcode()
        {

        }

        public VantageBarcode(string scanData)
        {
            this.m_ScanData = scanData;
            string [] splits = scanData.Split(new char[] { ';' });
            this.m_ReportNo = splits[0];
            this.m_Specimen = splits[1];
            this.m_Block = splits[2];
            this.m_Slide = splits[3];
        }

        public string ScanData
        {
            get { return this.m_ScanData; }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
        }

        public string Specimen
        {
            get { return this.m_Specimen; }
        }

        public string Block
        {
            get { return this.m_Block; }
        }

        public string Slide
        {
            get { return this.m_Slide; }
        }

        public static string SimulateScan()
        {
            StringBuilder result = new StringBuilder();
            DateTime date = DateTime.Now;
            result.Append("R");
            result.Append(date.Day.ToString());
            result.Append(";");
            result.Append("Sp");
            result.Append(date.Hour.ToString());
            result.Append(";");
            result.Append("B");
            result.Append(date.Minute.ToString());
            result.Append(";");
            result.Append("S");
            result.Append(date.Second.ToString());

            return result.ToString();
        }
    }
}
