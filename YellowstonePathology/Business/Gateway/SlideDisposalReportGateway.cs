using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Gateway
{
	public class SlideDisposalReportGateway
	{
		public static XElement GetCytologySlideDisposalReport(DateTime disposalDate)
		{
			XElement result = null;
			SqlCommand cmd = new SqlCommand("pCytologySlideDisposalReport");
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@DisposalDate", System.Data.SqlDbType.DateTime).Value = disposalDate;

			StringBuilder xmlString = new StringBuilder();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					while (xmlReader.Read())
					{
						xmlString.Append(xmlReader.ReadOuterXml());
					}
				}
			}

			if (xmlString.Length > 0)
			{
				result = XElement.Parse(xmlString.ToString());
			}
			return result;
		}

        public static Reports.DisposalReportData GetCytologySlideDisposalReport_1(DateTime disposalDate)
        {
            SqlCommand cmd = new SqlCommand("pCytologySlideDisposalReport_1");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@DisposalDate", System.Data.SqlDbType.DateTime).Value = disposalDate;
            Reports.DisposalReportData result = BuildDisposalReportData(cmd);
            return result;
        }

        public static XElement GetSpecimenDisposalReport(DateTime disposalDate)
		{
			XElement result = null;
			SqlCommand cmd = new SqlCommand("pDailySpecimenDisposalReport");
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@DisposalDate", System.Data.SqlDbType.DateTime).Value = disposalDate;

			StringBuilder xmlString = new StringBuilder();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					while (xmlReader.Read())
					{
						xmlString.Append(xmlReader.ReadOuterXml());
					}
				}
			}

			if (xmlString.Length > 0)
			{
				result = XElement.Parse(xmlString.ToString());
			}
			return result;
		}

        public static Reports.DisposalReportData GetSpecimenDisposalReport_1(DateTime disposalDate)
        {
            SqlCommand cmd = new SqlCommand("pDailySpecimenDisposalReport_1");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@DisposalDate", System.Data.SqlDbType.DateTime).Value = disposalDate;
            Reports.DisposalReportData result = BuildDisposalReportData(cmd);
            return result;
        }

        private static Reports.DisposalReportData BuildDisposalReportData(SqlCommand cmd)
        {
            Reports.DisposalReportData result = new Reports.DisposalReportData();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.DisposalDate = (DateTime)dr[0];
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        string reportNo = dr[0].ToString();
                        result.AddToHold.Add(reportNo);
                    }
                    if (result.AddToHold.Count == 0) result.AddToHold.Add(Reports.DisposalReportData.NoCasesFound);
                    dr.NextResult();

                    while (dr.Read())
                    {
                        result.DisposeOf = dr[0].ToString();
                    }
                    if (string.IsNullOrEmpty(result.DisposeOf) == true) result.DisposeOf = Reports.DisposalReportData.NoCasesFound;
                    dr.NextResult();

                    while (dr.Read())
                    {
                        string reportNo = dr[0].ToString();
                        result.DisposeOfFromHold.Add(reportNo);
                    }
                    if (result.DisposeOfFromHold.Count == 0) result.DisposeOfFromHold.Add(Reports.DisposalReportData.NoCasesFound);
                }
            }

            return result;
        }

        public static Reports.POCRetensionReportData GetPOCRetensionReport(DateTime startDate, DateTime endDate)
        {
            Reports.POCRetensionReportData result = new Reports.POCRetensionReportData();
            SqlCommand cmd = new SqlCommand("prcPOCRetensionReport_1");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@StartDate", System.Data.SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", System.Data.SqlDbType.DateTime).Value = endDate;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.StartDate = (DateTime)dr[0];
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Reports.POCRetensionReportDataItem pocRetensionReportDataItem = new Reports.POCRetensionReportDataItem();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(pocRetensionReportDataItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.POCRetensionReportDataItems.Add(pocRetensionReportDataItem);
                    }
                }
            }

            if (result.POCRetensionReportDataItems.Count == 0)
            {
                Reports.POCRetensionReportDataItem pocRetensionReportDataItem = new Reports.POCRetensionReportDataItem();
                pocRetensionReportDataItem.Status = "No Cases Found.";
                result.POCRetensionReportDataItems.Add(pocRetensionReportDataItem);
            }

            return result;
        }
    }
}
