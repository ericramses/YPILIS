using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FlowAccessionGateway : YellowstonePathology.YpiConnect.Contract.Flow.IFlowAccessionGateway
	{
        public XElement GetAccessionDocument(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession flowAccession)
        {
            return null;
        }

		public XElement GetReportDistributionByMasterAccessionNo(string masterAccessionNo)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Declare @XmlResult XML; " +
                "select @XmlResult = " +
	            "(Select * " +
	            "from tblReportDistribution " +
	            "where MasterAccessionNo = @MasterAccessionNo " +
	            "for xml Path('ReportDistribution'), type, root('ReportDistributionCollection')) " +
                "Select case when @XmlResult is not null then @XmlResult else '<ReportDistributionCollection></ReportDistributionCollection>' end"; 
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

            XElement result = YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlReaderCommand(cmd, YellowstonePathology.Business.Domain.Persistence.DataLocationEnum.ProductionData);                                
            return result;           
        }

		public XElement GetReportDocument(string masterAccessionNo)
        {   
            YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection = this.GetFlowAccession(masterAccessionNo);            
            YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader flowAccessionReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
            flowAccessionReader.Initialize(typeof(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession));
            flowAccessionCollection[0].ReadProperties(flowAccessionReader);
            XElement accessionDocument = flowAccessionReader.Document;

            YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader flowLLPReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
            flowLLPReader.Initialize(typeof(YellowstonePathology.YpiConnect.Contract.Flow.FlowLeukemia));
			YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)flowAccessionCollection[0].PanelSetOrderCollection[0];
			YellowstonePathology.YpiConnect.Contract.Flow.FlowLeukemia flowLeukemia = new Contract.Flow.FlowLeukemia(panelSetOrderLeukemiaLymphoma);
            flowLeukemia.ReadProperties(flowLLPReader);
            accessionDocument.Add(flowLLPReader.Document);

            XElement markerCollectionElement = new XElement("FlowMarkerCollection");
            accessionDocument.Add(markerCollectionElement);
            foreach (YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker flowMarker in panelSetOrderLeukemiaLymphoma.FlowMarkerCollection)
            {
                YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader flowMarkerReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
                flowMarkerReader.Initialize(typeof(YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker));
                flowMarker.ReadProperties(flowMarkerReader);
                markerCollectionElement.Add(flowMarkerReader.Document);
            }
                          
            XElement reportDistributionCollectionElement = this.GetReportDistributionByMasterAccessionNo(masterAccessionNo);            
            accessionDocument.Add(reportDistributionCollectionElement);            

            XElement casehistoryElement = this.GetCaseHistoryDocument(masterAccessionNo);
            accessionDocument.Add(casehistoryElement);
            
            return accessionDocument;
        }

		public XElement GetCaseHistoryDocument(string masterAccessionNo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "pGetCaseHistoryXmlFromMasterAccessionNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            XElement result = YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteCommand(cmd, Business.Domain.Persistence.DataLocationEnum.ProductionData);			
			return result;
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection GetFlowAccession(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws GetFlowAccessionOrderByMasterAccessionNo";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
			FlowAccessionBuilder flowAccessionBuilder = new FlowAccessionBuilder(cmd);
			YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection = flowAccessionBuilder.Build();
			return flowAccessionCollection;
		}         

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection GetFlowComments()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetFlowComments";
			cmd.CommandType = CommandType.StoredProcedure;
			return BuildFlowCommentCollection(cmd);
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection GetMarkers(string reportNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "ws_GetMarkers";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			return BuildMarkerCollection(cmd);
		}

		public void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession> collection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum)
		{
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader xmlPropertyReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
			if (propertyReaderFilterEnum == Business.Domain.Persistence.PropertyReaderFilterEnum.External)
			{
				YellowstonePathology.YpiConnect.Service.FlowAccessionExternalPropertyReaderFilter filter = new FlowAccessionExternalPropertyReaderFilter();
				xmlPropertyReader.SetFilter(filter);
			}
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges(collection, Business.Domain.Persistence.DataLocationEnum.ProductionData, xmlPropertyReader);
		}

		public void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrder> collection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum)
		{
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader xmlPropertyReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
			if (propertyReaderFilterEnum == Business.Domain.Persistence.PropertyReaderFilterEnum.External)
			{
				YellowstonePathology.YpiConnect.Service.PanelSetOrderExternalPropertyReaderFilter filter = new PanelSetOrderExternalPropertyReaderFilter();
				xmlPropertyReader.SetFilter(filter);
			}
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges(collection, Business.Domain.Persistence.DataLocationEnum.ProductionData, xmlPropertyReader);
		}

		public void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowLeukemia> collection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum)
		{
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader xmlPropertyReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
			if (propertyReaderFilterEnum == Business.Domain.Persistence.PropertyReaderFilterEnum.External)
			{
				YellowstonePathology.YpiConnect.Service.FlowLeukemiaExternalPropertyReaderFilter filter = new FlowLeukemiaExternalPropertyReaderFilter();
				xmlPropertyReader.SetFilter(filter);
			}
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges(collection, Business.Domain.Persistence.DataLocationEnum.ProductionData, xmlPropertyReader);
		}

		public void SubmitChanges(Collection<YellowstonePathology.YpiConnect.Contract.Flow.FlowMarker> collection, YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilterEnum propertyReaderFilterEnum)
		{
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader xmlPropertyReader = new Business.Domain.Persistence.SqlXmlPropertyReader();
			if (propertyReaderFilterEnum == Business.Domain.Persistence.PropertyReaderFilterEnum.External)
			{
				YellowstonePathology.YpiConnect.Service.FlowMarkerExternalPropertyReaderFilter filter = new FlowMarkerExternalPropertyReaderFilter();
				xmlPropertyReader.SetFilter(filter);
			}
			YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges(collection, Business.Domain.Persistence.DataLocationEnum.ProductionData, xmlPropertyReader);
		}

		private YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection BuildFlowCommentCollection(SqlCommand cmd)
		{
			YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection flowCommentCollection = new YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection();

			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.YpiConnect.Contract.Flow.FlowComment flowComment = new Contract.Flow.FlowComment();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						flowComment.WriteProperties(propertyWriter);
						flowCommentCollection.Add(flowComment);
					}
				}
			}

			return flowCommentCollection;
		}

		private YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection BuildMarkerCollection(SqlCommand cmd)
		{
			YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection markerCollection = new YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection();

			using (SqlConnection cn = new SqlConnection(YpiConnect.Service.Properties.Settings.Default.ServerSqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.YpiConnect.Contract.Flow.Marker marker = new Contract.Flow.Marker();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						marker.WriteProperties(propertyWriter);
						markerCollection.Add(marker);
					}
				}
			}

			return markerCollection;
		}
	}
}
