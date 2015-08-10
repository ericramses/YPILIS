using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class OrderCommentGateway
    {
		public static Domain.OrderCommentLogCollection GetOrderCommentLogCollectionByClientOrderId(string clientOrderId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwOrderCommentsByClientOrderId";
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@ClientOrderId", System.Data.SqlDbType.VarChar).Value = clientOrderId;			
			XElement xelement = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlReaderCommand(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
			return BuildOrderCommentLogCollection(xelement);
		}

		public static Domain.OrderCommentLogCollection GetOrderCommentLogCollectionByMasterAccessionNo(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwOrderCommentsByMasterAccessionNo";
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@MasterAccessionNo", System.Data.SqlDbType.VarChar).Value = masterAccessionNo;
			XElement xelement = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlReaderCommand(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
			return BuildOrderCommentLogCollection(xelement);
		}

		public static Domain.OrderCommentLogCollection GetOrderCommentsForSpecimenLogId(int specimenLogId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblOrderCommentLog where SpecimenLogId = @SpecimenLogId order by LogDate desc for xml Path('OrderCommentLog'), root('OrderCommentLogCollection')";
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@SpecimenLogId", System.Data.SqlDbType.Int).Value = specimenLogId;
            XElement xelement = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlReaderCommand(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
			return BuildOrderCommentLogCollection(xelement);
		}

		public static Domain.OrderCommentLogCollection OrderCommentLogCollectionFromCaseNotesKeys(Domain.CaseNotesKeyCollection caseNotesKeyCollection)
		{
			Domain.OrderCommentLogCollection result = new Domain.OrderCommentLogCollection();
			foreach (Domain.CaseNotesKey caseNotesKey in caseNotesKeyCollection)
			{
				SqlCommand cmd = new SqlCommand();				
				cmd.CommandType = System.Data.CommandType.Text;
				switch (caseNotesKey.CaseNotesKeyName)
				{
					case Domain.CaseNotesKeyNameEnum.MasterAccessionNo:
						cmd.CommandText = "SELECT * from tblOrderCommentLog where MasterAccessionNo = '" + caseNotesKey.Key + "' order by LogDate for xml Path('OrderCommentLog'), root('OrderCommentLogCollection')";
						break;
					case Domain.CaseNotesKeyNameEnum.ClientOrderId:
						cmd.CommandText = "SELECT * from tblOrderCommentLog where ClientOrderId = '" + caseNotesKey.Key + "' order by LogDate for xml Path('OrderCommentLog'), root('OrderCommentLogCollection')";
						break;
					case Domain.CaseNotesKeyNameEnum.ContainerId:
						cmd.CommandText = "SELECT * from tblOrderCommentLog where ContainerId = '" + caseNotesKey.Key + "' order by LogDate for xml Path('OrderCommentLog'), root('OrderCommentLogCollection')";
						break;
					case Domain.CaseNotesKeyNameEnum.SpecimenLogId:
						cmd.CommandText = "SELECT * from tblOrderCommentLog where SpecimenLogId = " + caseNotesKey.Key + " order by LogDate for xml Path('OrderCommentLog'), root('OrderCommentLogCollection')";
						break;
				}

				XElement xelement = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteXmlReaderCommand(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
				Domain.OrderCommentLogCollection midCollection = BuildOrderCommentLogCollection(xelement);

				foreach (Domain.OrderCommentLog orderCommentLog in midCollection)
				{
					result.AddUnique(orderCommentLog);
				}
			}
			return result;
		}

		private static YellowstonePathology.Business.Domain.OrderCommentLogCollection BuildOrderCommentLogCollection(XElement sourceElement)
        {
			Domain.OrderCommentLogCollection orderCommentLogCollection = new Domain.OrderCommentLogCollection();
            if (sourceElement != null)
            {
				foreach (XElement orderCommentLogElement in sourceElement.Elements("OrderCommentLog"))
                {
					Domain.OrderCommentLog orderCommentLog = BuildOrderCommentLog(orderCommentLogElement);
					orderCommentLogCollection.Add(orderCommentLog);
                }
            }
			return orderCommentLogCollection;
        }

		private static Domain.OrderCommentLog BuildOrderCommentLog(XElement sourceElement)
        {
			Domain.OrderCommentLog orderCommentLog = new Domain.OrderCommentLog();
            if (sourceElement != null)
            {
                Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
				orderCommentLog.WriteProperties(xmlPropertyWriter);
            }
			return orderCommentLog;
        }
	}
}
