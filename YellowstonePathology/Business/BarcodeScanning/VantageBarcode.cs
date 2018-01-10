using System;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class VantageBarcode
    {
        private string m_ReportNo;
        private string m_Specimen;
        private string m_Block;
        private string m_Slide;

        public VantageBarcode()
        {

        }

        public VantageBarcode(string reportNo, string specimen, string block, string slide)
        {
            this.m_ReportNo = reportNo;
            this.m_Specimen = specimen;
            this.m_Block = block;
            this.m_Slide = slide;
        }

        public VantageBarcode(string scanData)
        {
            string [] splits = scanData.Split(new char[] { ';' });
            this.m_ReportNo = splits[0];
            this.m_Specimen = splits[1];
            this.m_Block = splits[2];
            this.m_Slide = splits[3];
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public string Specimen
        {
            get { return this.m_Specimen; }
            set { this.m_Specimen = value; }
        }

        public string Block
        {
            get { return this.m_Block; }
            set { this.m_Block = value; }
        }

        public string Slide
        {
            get { return this.m_Slide; }
            set { this.m_Slide = value; }
        }

        public string GetFormated()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.m_ReportNo);
            result.Append(";");
            result.Append(this.m_Specimen);
            result.Append(";");
            result.Append(this.m_Block);
            result.Append(";");
            result.Append(this.m_Slide);

            return result.ToString();
        }

        public static void SimulateSavingSlideTrackingSpecimen()
        {
            VantageBarcode vantageBarcode = new BarcodeScanning.VantageBarcode("18-1234.S", "18-1234.1", "18-1234.1.1", "1");
            string key = vantageBarcode.GetFormated();
            Store.RedisDB vantageDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlideScan);
            vantageDb.DataBase.SetAdd("vantageSlideScan", key);
        }
    }
}
