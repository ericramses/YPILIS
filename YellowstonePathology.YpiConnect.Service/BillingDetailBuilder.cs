using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.YpiConnect.Service
{
	public class BillingDetailBuilder
	{
		private SqlCommand m_Cmd;
		private string m_ReportNo;
		private bool m_IncludeMemorystream;
		private YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;

		public BillingDetailBuilder(SqlCommand cmd, string reportNo, bool includeMemoryStream, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			this.m_Cmd = cmd;
			this.m_ReportNo = reportNo;
			this.m_IncludeMemorystream = includeMemoryStream;
			this.m_WebServiceAccount = webServiceAccount;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail Build()
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail billingDetail = new Contract.Billing.BillingDetail();
			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				m_Cmd.Connection = cn;
				using (SqlDataReader dr = m_Cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCode panelSetOrderCPTCode = new Contract.Billing.PanelSetOrderCPTCode();
						panelSetOrderCPTCode.WriteProperties(propertyWriter);
						billingDetail.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = new Contract.Billing.PanelSetOrderCPTCodeBill();
						panelSetOrderCPTCodeBill.WriteProperties(propertyWriter);
						billingDetail.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.YpiConnect.Contract.Billing.ICD9BillingCode icd9BillingCode = new Contract.Billing.ICD9BillingCode();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(icd9BillingCode, dr);
						propertyWriter.WriteProperties();
						billingDetail.ICD9BillingCodeCollection.Add(icd9BillingCode);
					}
				}
			}			

			billingDetail.RemoteFileList = new Contract.RemoteFileList(this.m_ReportNo, this.m_IncludeMemorystream);
			billingDetail.RemoteFileList.Load();
			
			return billingDetail;
		}
	}
}
