using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
    public class Icd9BillingCodeList : ObservableCollection<Icd9BillingCodeListItem>
    {
        private SqlCommand m_Cmd;

        public Icd9BillingCodeList()
        {
            this.m_Cmd = new SqlCommand();
        }

        public void Fill()
        {
            this.Clear();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (SqlDataReader dr = this.m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Icd9BillingCodeListItem item = new Icd9BillingCodeListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
		}

		public XElement ToXml()
		{
			XElement result = new XElement("Icd9BillingCodeList");
			foreach(Icd9BillingCodeListItem item in this)
			{
				result.Add(item.ToXml());
			}
			return result;
		}

        public void SetFillCommandByAccessionNo(string reportNo)
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblIcd9BillingCodes icd9 left outer join tblCptIcd9BillingCode bcicd on icd9.Icd9BillingId = bcicd.Icd9BillingCodeId Where ReportNo = @ReportNo";
			this.m_Cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar, 20).Value = reportNo;
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

		public void SetFillCommandByAccessionNoBillingDate(string reportNo, DateTime startDate, DateTime endDate)
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblIcd9BillingCodes icd9 left outer join tblCptIcd9BillingCode bcicd on icd9.Icd9BillingId = bcicd.Icd9BillingCodeId Where ReportNo = @ReportNo and BillingDate between @StartDate and @EndDate";
			this.m_Cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar, 20).Value = reportNo;
            this.m_Cmd.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = startDate.ToShortDateString();
            this.m_Cmd.Parameters.Add("@EndDate", SqlDbType.VarChar).Value = endDate.ToShortDateString();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

		public void SetFillCommandBySpecimenOrderId(string specimenOrderId)
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblIcd9BillingCodes icd9 left outer join tblCptIcd9BillingCode bcicd on icd9.Icd9BillingId = bcicd.Icd9BillingCodeId Where SpecimenOrderId = @SpecimenOrderId";
            this.m_Cmd.Parameters.Add("@SpecimenOrderId", SqlDbType.VarChar).Value = specimenOrderId;
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }
    }
}
