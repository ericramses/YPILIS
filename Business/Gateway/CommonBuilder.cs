using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace YellowstonePathology.Business.Gateway
{
	public class CommonBuilder : Domain.Persistence.IBuilder
	{
		private object m_ResultObject;

		public CommonBuilder()
		{
		}

		public object ResultObject
		{
			get { return this.m_ResultObject; }
		}

		public void Build(XElement sourceElement)
		{
			if (sourceElement != null)
			{
				switch (sourceElement.Name.ToString())
				{
					/*case "Client":
						this.BuildClient(sourceElement);
						break;*/
					/*case "PhysicianClient":
						this.BuildPhysicianClient(sourceElement);
						break;
					case "PhysicianClientList":
						this.BuildPhysicianClientList(sourceElement);
						break;*/
					/*case "OrderCommentLog":
						this.BuildOrderCommentLog(sourceElement);
						break;
					case "OrderCommentLogCollection":
						this.BuildOrderCommentLogCollection(sourceElement);
						break;*/
					/*case "HpvRequisitionInstruction":
						this.BuildHpvRequisitionInstruction(sourceElement);
						break;
					case "HpvRequisitionInstructionCollection":
						this.BuildHpvRequisitionInstructionCollection(sourceElement);
						break;*/
					/*case "OrderableTestCptCode":
						this.BuildOrderableTestCptCode(sourceElement);
						break;
					case "OrderableTestCptCodeCollection":
						this.BuildOrderableTestCptCodeCollection(sourceElement);
						break;
					case "OrderableTestIcd9Code":
						this.BuildOrderableTestIcd9Code(sourceElement);
						break;
					case "OrderableTestIcd9CodeCollection":
						this.BuildOrderableTestIcd9CodeCollection(sourceElement);
						break;
					case "CommentListItem":
						this.BuildCommentListItem(sourceElement);
						break;
					case "CommentList":
						this.BuildCommentList(sourceElement);
						break;					
					case "CptBillingCode":
						this.BuildCptBillingCode(sourceElement);
						break;
					case "CptBillingCodeCollection":
						this.BuildCptBillingCodeCollection(sourceElement);
						break;										
					case "ReportNo":
						this.BuildReportNo(sourceElement);
						break;
					case "ReportNoCollection":
						this.BuildReportNoCollection(sourceElement);
						break;*/
					default:
						break;
				}
			}
		}

		private void BuildClient(XElement sourceElement)
		{
			YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			client.WriteProperties(xmlPropertyWriter);
			this.m_ResultObject = client;
		}

		private void BuildPhysicianClient(XElement sourceElement)
		{
			Domain.PhysicianClient physicianClient = new Domain.PhysicianClient();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			physicianClient.WriteProperties(xmlPropertyWriter);

            /*
			XElement clientElement = sourceElement.Element("Client");
			if (clientElement != null)
			{
				this.BuildClient(clientElement);
				physicianClient.Client = this.ResultObject as YellowstonePathology.Business.Client.Model.Client;
			}

			XElement physicianElement = sourceElement.Element("Physician");
			if (physicianElement != null)
			{
				this.BuildPhysician(physicianElement);
				physicianClient.Physician = this.ResultObject as Domain.Physician;
			}
            */

			this.m_ResultObject = physicianClient;
		}

		private void BuildPhysicianClientList(XElement sourceElement)
		{
			Domain.PhysicianClientList physicianClientList = new Domain.PhysicianClientList();
			foreach (XElement physicianClientElement in sourceElement.Elements("PhysicianClient"))
			{
				this.BuildPhysicianClient(physicianClientElement);
				physicianClientList.Add(this.m_ResultObject as Domain.PhysicianClient);
			}
			this.m_ResultObject = physicianClientList;
		}

		private void BuildOrderCommentLog(XElement sourceElement)
		{
			Domain.OrderCommentLog orderCommentLog = new Domain.OrderCommentLog();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			orderCommentLog.WriteProperties(xmlPropertyWriter);
			this.m_ResultObject = orderCommentLog;
		}

		private void BuildOrderCommentLogCollection(XElement sourceElement)
		{
			Domain.OrderCommentLogCollection orderCommentLogCollection = new Domain.OrderCommentLogCollection();
			foreach (XElement orderCommentLogElement in sourceElement.Elements("OrderCommentLog"))
            {
				this.BuildOrderCommentLog(orderCommentLogElement);
				orderCommentLogCollection.Add(this.m_ResultObject as Domain.OrderCommentLog);
            }
			this.m_ResultObject = orderCommentLogCollection;
		}

		private void BuildHpvRequisitionInstruction(XElement sourceElement)
		{
			Domain.HpvRequisitionInstruction hpvRequisitionInstruction = new Domain.HpvRequisitionInstruction();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			hpvRequisitionInstruction.WriteProperties(xmlPropertyWriter);
			this.m_ResultObject = hpvRequisitionInstruction;
		}

		private void BuildHpvRequisitionInstructionCollection(XElement sourceElement)
		{
			Domain.HpvRequisitionInstructionCollection hpvRequisitionInstructionCollection = new Domain.HpvRequisitionInstructionCollection();
			foreach (XElement hpvRequisitionInstructionElement in sourceElement.Elements("HpvRequisitionInstruction"))
			{
				this.BuildHpvRequisitionInstruction(hpvRequisitionInstructionElement);
				hpvRequisitionInstructionCollection.Add(this.m_ResultObject as Domain.HpvRequisitionInstruction);
			}
			this.m_ResultObject = hpvRequisitionInstructionCollection;
		}

		private void BuildOrderableTestCptCode(XElement sourceElement)
		{
			Domain.OrderableTestCptCode orderableTestCptCode = new Domain.OrderableTestCptCode();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			orderableTestCptCode.WriteProperties(xmlPropertyWriter);
			this.m_ResultObject = orderableTestCptCode;
		}

		private void BuildOrderableTestCptCodeCollection(XElement sourceElement)
		{
			Domain.OrderableTestCptCodeCollection orderableTestCptCodeCollection = new Domain.OrderableTestCptCodeCollection();
			foreach (XElement orderableTestCptCodeElement in sourceElement.Elements("OrderableTestCptCode"))
			{
				this.BuildOrderableTestCptCode(orderableTestCptCodeElement);
				orderableTestCptCodeCollection.Add(this.m_ResultObject as Domain.OrderableTestCptCode);
			}
			this.m_ResultObject = orderableTestCptCodeCollection;
		}

		private void BuildOrderableTestIcd9Code(XElement sourceElement)
		{
			Domain.OrderableTestIcd9Code orderableTestIcd9Code = new Domain.OrderableTestIcd9Code();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			orderableTestIcd9Code.WriteProperties(xmlPropertyWriter);
			this.m_ResultObject = orderableTestIcd9Code;
		}

		private void BuildOrderableTestIcd9CodeCollection(XElement sourceElement)
		{
			Domain.OrderableTestIcd9CodeCollection orderableTestIcd9CodeCollection = new Domain.OrderableTestIcd9CodeCollection();
			foreach (XElement orderableTestIcd9CodeElement in sourceElement.Elements("OrderableTestIcd9Code"))
			{
				this.BuildOrderableTestIcd9Code(orderableTestIcd9CodeElement);
				orderableTestIcd9CodeCollection.Add(this.m_ResultObject as Domain.OrderableTestIcd9Code);
			}
			this.m_ResultObject = orderableTestIcd9CodeCollection;
		}

		private void BuildCptBillingCode(XElement sourceElement)
		{
			Domain.CptBillingCode cptBillingCode = new Domain.CptBillingCode();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			cptBillingCode.WriteProperties(xmlPropertyWriter);			
			this.m_ResultObject = cptBillingCode;
		}

		private void BuildCptBillingCodeCollection(XElement sourceElement)
		{
			Domain.CptBillingCodeCollection cptBillingCodeCollection = new Domain.CptBillingCodeCollection();
			foreach (XElement cptBillingCodeElement in sourceElement.Elements("CptBillingCode"))
			{
				this.BuildCptBillingCode(cptBillingCodeElement);
				cptBillingCodeCollection.Add(this.m_ResultObject as Domain.CptBillingCode);
			}
			this.m_ResultObject = cptBillingCodeCollection;
		}        		

		private void BuildReportNo(XElement sourceElement)
		{
			ReportNo reportNo = new ReportNo();
			Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
			reportNo.WriteProperties(xmlPropertyWriter);
			this.m_ResultObject = reportNo;
		}

		private void BuildReportNoCollection(XElement sourceElement)
		{
			ReportNoCollection reportNoCollection = new ReportNoCollection();
			foreach (XElement reportNoElement in sourceElement.Elements("ReportNo"))
			{
				this.BuildReportNo(reportNoElement);
				reportNoCollection.Add(this.m_ResultObject as ReportNo);
			}
			this.m_ResultObject = reportNoCollection;
		}
	}
}
