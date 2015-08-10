using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FlowAccessionBuilder
	{
		private SqlCommand m_Cmd;

		public FlowAccessionBuilder(SqlCommand cmd)
		{
			this.m_Cmd = cmd;
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection Build()
		{
			YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection = new Contract.Flow.FlowAccessionCollection();
			YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession flowAccession = new Contract.Flow.FlowAccession();
			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				m_Cmd.Connection = cn;
				using (SqlDataReader dr = m_Cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						flowAccession.WriteProperties(propertyWriter);
						flowAccessionCollection.Load(flowAccession);
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.YpiConnect.Contract.Domain.SpecimenOrder specimenOrder = new Contract.Domain.SpecimenOrder();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						specimenOrder.WriteProperties(propertyWriter);
						flowAccession.SpecimenOrderCollection.Load(specimenOrder);
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = new Contract.Domain.PanelSetOrderLeukemiaLymphoma();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						panelSetOrderLeukemiaLymphoma.WriteProperties(propertyWriter);
						flowAccession.PanelSetOrderCollection.Load(panelSetOrderLeukemiaLymphoma);
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker flowMarker = new Contract.Flow.FlowMarker();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						flowMarker.WriteProperties(propertyWriter);
						((YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)flowAccession.PanelSetOrderCollection[0]).FlowMarkerCollection.Load(flowMarker);
					}

				}
			}

			flowAccession.CaseDocumentList = new Contract.RemoteFileList(flowAccession.PanelSetOrderCollection[0].ReportNo, false);
            flowAccession.CaseDocumentList.Load();

			return flowAccessionCollection;
		}
	}
}
