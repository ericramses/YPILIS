using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.YpiConnect.Service
{
	public class BillingBuilder
	{
		private SqlCommand m_Cmd;

		public BillingBuilder(SqlCommand cmd)
		{
			this.m_Cmd = cmd;
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection Build()
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection billingAccessionCollection = new Contract.Billing.BillingAccessionCollection();
			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				m_Cmd.Connection = cn;
				using (SqlDataReader dr = m_Cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession billingAccession = new Contract.Billing.BillingAccession();
						billingAccession.WriteProperties(propertyWriter);
						billingAccessionCollection.Add(billingAccession);
					}
				}
			}
			return billingAccessionCollection;
		}
	}
}
