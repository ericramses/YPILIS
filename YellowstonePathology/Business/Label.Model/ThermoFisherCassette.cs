using System;
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

        public override void Print()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);

            StringBuilder line = new StringBuilder(this.m_Prefix + this.m_Delimeter);
            line.Append(this.m_CassetteColumnDelimiter + this.m_CassetteColumn.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            line.Append(this.BlockTitle + this.m_Delimeter);
            line.Append(this.PatientInitials + this.m_Delimeter);
            line.Append(this.CompanyId + this.m_Delimeter);
            line.Append(this.ScanningId + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());            

            string path = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter + System.Guid.NewGuid().ToString() + ".txt";
            
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {                                                            
                    file.Write(line + "\r\n");
                    this.m_AliquotOrder.Printed = true;                    
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(path + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
        }
    }
}
