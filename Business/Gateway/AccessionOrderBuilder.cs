using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace YellowstonePathology.Business.Gateway
{
	public class AccessionOrderBuilder : Domain.Persistence.IBuilder 
	{
		private Test.AccessionOrder m_AccessionOrder;        

		public AccessionOrderBuilder()
		{
        
		}

		public Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public void Build(XElement accessionOrderElement)
		{
            if (accessionOrderElement != null)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = new Test.AccessionOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(accessionOrderElement, accessionOrder);
				xmlPropertyWriter.Write();

                BuildSpecimenOrder(accessionOrder, accessionOrderElement);                
				BuildTaskOrder(accessionOrder, accessionOrderElement);
				BuildIcdBillingCode(accessionOrder, accessionOrderElement);
                BuildPanelSetOrder(accessionOrder, accessionOrderElement);				

                this.m_AccessionOrder = accessionOrder;
            }
            else
            {
                this.m_AccessionOrder = null;
            }
		}        		

		private void BuildSpecimenOrder(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
            accessionOrder.SpecimenOrderCollection.IsLoading = true;
			List<XElement> specimenOrderElements = (from item in accessionOrderElement.Elements("SpecimenOrderCollection")
													select item).ToList<XElement>();
			foreach (XElement specimenOrderElement in specimenOrderElements.Elements("SpecimenOrder"))
			{
				Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(specimenOrderElement, specimenOrder);
				xmlPropertyWriter.Write();
				BuildAliquotOrderLeftSide(specimenOrder, specimenOrderElement);
				accessionOrder.SpecimenOrderCollection.Add(specimenOrder);
			}
            accessionOrder.SpecimenOrderCollection.IsLoading = false;
		}

		private void BuildAliquotOrderLeftSide(Specimen.Model.SpecimenOrder specimenOrder, XElement specimenOrderElement)
		{
			List<XElement> aliquotOrderElements = (from item in specimenOrderElement.Elements("AliquotOrderCollection")
												   select item).ToList<XElement>();
			foreach (XElement aliquotOrderElement in aliquotOrderElements.Elements("AliquotOrder"))
			{
				YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(aliquotOrderElement, aliquotOrder);
				xmlPropertyWriter.Write();
              
                BuildAliquotOrderTestOrder(aliquotOrder, aliquotOrderElement);
                BuildAliquotOrderSlideOrderCollection(aliquotOrder, aliquotOrderElement);
				specimenOrder.AliquotOrderCollection.Add(aliquotOrder);
			}
		}

        private void BuildTestOrderAliquotOrder(YellowstonePathology.Business.Test.Model.TestOrder testOrder, XElement testOrderElement)
        {
            XElement aliquotOrderElement = testOrderElement.Element("AliquotOrder");
            if (aliquotOrderElement != null)
            {
                YellowstonePathology.Business.Test.AliquotOrder_Base aliquotOrder = new Test.AliquotOrder_Base();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(aliquotOrderElement, aliquotOrder);
				xmlPropertyWriter.Write();
				testOrder.AliquotOrder = aliquotOrder;
            }
        }

        private void BuildSlideOrderTestOrder(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder, XElement slideOrderElement)
        {
            XElement testOrderElement = slideOrderElement.Element("TestOrder");
            if (testOrderElement != null)
            {
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
				xmlPropertyWriter.Write();
                slideOrder.TestOrder = testOrder;
            }
        }

        private void BuildAliquotOrderTestOrder(Test.AliquotOrder aliquotOrder, XElement aliquotOrderElement)
        {
            List<XElement> aliquotOrderElements = (from item in aliquotOrderElement.Elements("TestOrderCollection")
                                                   select item).ToList<XElement>();
            foreach (XElement testOrderElement in aliquotOrderElements.Elements("TestOrder"))
            {
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
				xmlPropertyWriter.Write();
				aliquotOrder.TestOrderCollection.Add(testOrder);
            }
        }		

        private void BuildTestOrderSlideOrderCollection(YellowstonePathology.Business.Test.Model.TestOrder testOrder, XElement testOrderElement)
        {
            List<XElement> slideOrderElements = (from item in testOrderElement.Elements("SlideOrderCollection")
                                                           select item).ToList<XElement>();
            foreach (XElement slideOrderElement in slideOrderElements.Elements("SlideOrder"))
            {
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(slideOrderElement, slideOrder);
                xmlPropertyWriter.Write();                
                testOrder.SlideOrderCollection.Add(slideOrder);
            }
        }

        private void BuildAliquotOrderSlideOrderCollection(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, XElement aliquotOrderElement)
        {
            List<XElement> slideOrderElements = (from item in aliquotOrderElement.Elements("SlideOrderCollection")
                                                 select item).ToList<XElement>();
            foreach (XElement slideOrderElement in slideOrderElements.Elements("SlideOrder"))
            {
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(slideOrderElement, slideOrder);
                xmlPropertyWriter.Write();
                aliquotOrder.SlideOrderCollection.Add(slideOrder);
            }
        }   

		private void BuildPanelSetOrder(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

			List<XElement> panelSetOrderElements = (from psoc in accessionOrderElement.Elements("PanelSetOrderCollection")
														select psoc).ToList<XElement>();
			foreach (XElement panelSetOrderElement in panelSetOrderElements.Elements("PanelSetOrder"))
			{
                int panelSetId = Convert.ToInt32(panelSetOrderElement.Element("PanelSetId").Value);
                string reportNo = panelSetOrderElement.Element("ReportNo").Value;

                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetId);
				Test.PanelSetOrder panelSetOrder = Test.PanelSetOrderFactory.CreatePanelSetOrder(panelSet);
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderElement, panelSetOrder);
				xmlPropertyWriter.Write();

                YellowstonePathology.Business.Builder.PanelSetOrderBuilder builder = YellowstonePathology.Business.Builder.PanelSetOrderBuilderFactory.GetBuilder(panelSetId);
				builder.Build(panelSetOrder, panelSetOrderElement);
                this.SetOrderedOnDescription(panelSetOrder, accessionOrder);
				BuildAmendment(panelSetOrder, panelSetOrderElement);
				BuildPanelSetOrderComment(panelSetOrder, panelSetOrderElement);
                BuildPanelSetOrderCPTCode(panelSetOrder, panelSetOrderElement);
                BuildPanelSetOrderCPTCodeBill(panelSetOrder, panelSetOrderElement);
                BuildTestOrderReportDistribution(panelSetOrder, panelSetOrderElement);
                BuildTestOrderReportDistributionLog(panelSetOrder, panelSetOrderElement);
				BuildPanelOrder(panelSetOrder, panelSetOrderElement);
				BuildSurgicalSpecific(accessionOrder, panelSetOrder, panelSetOrderElement);
				accessionOrder.PanelSetOrderCollection.Add(panelSetOrder);
			}
		}

        private void SetOrderedOnDescription(Test.PanelSetOrder panelSetOrder, Test.AccessionOrder accessionOrder)
        {            
            if (panelSetOrder.OrderedOn != null)
            {
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
                if (specimenOrder != null)
                {
                    switch (panelSetOrder.OrderedOn)
                    {
                        case YellowstonePathology.Business.OrderedOn.Specimen:
                        case YellowstonePathology.Business.OrderedOn.ThinPrepFluid:
                            panelSetOrder.OrderedOnDescription = specimenOrder.Description;
                            break;
                        case YellowstonePathology.Business.OrderedOn.Aliquot:
                            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrder(panelSetOrder.OrderedOnId);
                            panelSetOrder.OrderedOnDescription = specimenOrder.Description + " - " + aliquotOrder.Label;
                            break;
                        default:
                            throw new Exception("Must be Specimen or Aliquot");
                    }
                }
            }            
        }

		private void BuildAmendment(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{
			List<XElement> amendmentElements = (from item in panelSetOrderElement.Elements("AmendmentCollection")
												 select item).ToList<XElement>();
			foreach (XElement amendmentElement in amendmentElements.Elements("Amendment"))
			{
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = new YellowstonePathology.Business.Amendment.Model.Amendment();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(amendmentElement, amendment);
				xmlPropertyWriter.Write();
				panelSetOrder.AmendmentCollection.Add(amendment);
			}
		}

		private void BuildPanelSetOrderComment(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{
			List<XElement> panelSetOrderCommentElements = (from item in panelSetOrderElement.Elements("PanelSetOrderCommentCollection")
														   select item).ToList<XElement>();
			foreach (XElement panelSetOrderCommentElement in panelSetOrderCommentElements.Elements("PanelSetOrderComment"))
			{
				YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = new Test.PanelSetOrderComment();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderCommentElement, panelSetOrderComment);
				xmlPropertyWriter.Write();
				panelSetOrder.PanelSetOrderCommentCollection.Add(panelSetOrderComment);                
			}
		}

        private void BuildTestOrderReportDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> testOrderReportDistributionElements = (from item in panelSetOrderElement.Elements("TestOrderReportDistributionCollection")
                                                           select item).ToList<XElement>();
            foreach (XElement testOrderReportDistributionElement in testOrderReportDistributionElements.Elements("TestOrderReportDistribution"))
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(testOrderReportDistributionElement, testOrderReportDistribution);
                xmlPropertyWriter.Write();
                panelSetOrder.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
            }
        }

        private void BuildTestOrderReportDistributionLog(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> testOrderReportDistributionLogElements = (from item in panelSetOrderElement.Elements("TestOrderReportDistributionLogCollection")
                                                                      select item).ToList<XElement>();
            foreach (XElement testOrderReportDistributionLogElement in testOrderReportDistributionLogElements.Elements("TestOrderReportDistributionLog"))
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog testOrderReportDistributionLog = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(testOrderReportDistributionLogElement, testOrderReportDistributionLog);
                xmlPropertyWriter.Write();
                panelSetOrder.TestOrderReportDistributionLogCollection.Add(testOrderReportDistributionLog);
            }
        }

        private void BuildPanelSetOrderCPTCode(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> panelSetOrderCPTCodeElements = (from item in panelSetOrderElement.Elements("PanelSetOrderCPTCodeCollection")
                                                           select item).ToList<XElement>();
            foreach (XElement panelSetOrderCPTCodeElement in panelSetOrderCPTCodeElements.Elements("PanelSetOrderCPTCode"))
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = new Test.PanelSetOrderCPTCode();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderCPTCodeElement, panelSetOrderCPTCode);
				xmlPropertyWriter.Write();
                panelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }
        }

        private void BuildPanelSetOrderCPTCodeBill(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> panelSetOrderCPTCodeBillElements = (from item in panelSetOrderElement.Elements("PanelSetOrderCPTCodeBillCollection")
                                                           select item).ToList<XElement>();
            foreach (XElement panelSetOrderCPTCodeBillElement in panelSetOrderCPTCodeBillElements.Elements("PanelSetOrderCPTCodeBill"))
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = new Test.PanelSetOrderCPTCodeBill();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderCPTCodeBillElement, panelSetOrderCPTCodeBill);
				xmlPropertyWriter.Write();
                panelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }
        }

		private void BuildPanelOrder(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{			
			List<XElement> panelOrderElements = (from poc in panelSetOrderElement.Elements("PanelOrderCollection")
												 select poc).ToList<XElement>();

            YellowstonePathology.Business.Panel.Model.PanelCollection panelCollection = YellowstonePathology.Business.Panel.Model.PanelCollection.GetAll();
            

			foreach (XElement panelOrderElement in panelOrderElements.Elements("PanelOrder"))
			{
				int panelId = Convert.ToInt32(panelOrderElement.Element("PanelId").Value);
                YellowstonePathology.Business.Panel.Model.Panel panel = panelCollection.GetPanel(panelId);
				Test.PanelOrder panelOrder = Test.PanelOrderFactory.GetPanelOrder(panel);

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelOrderElement, panelOrder);
				xmlPropertyWriter.Write();
                BuildTestOrderRightSide(panelOrder, panelOrderElement);
				panelSetOrder.PanelOrderCollection.Add(panelOrder);
			}
		}

		private void BuildTestOrderRightSide(Test.PanelOrder panelOrder, XElement panelOrderElement)
		{
			List<XElement> testOrderElements = (from item in panelOrderElement.Elements("TestOrderCollection")
												select item).ToList<XElement>();
			foreach (XElement testOrderElement in testOrderElements.Elements("TestOrder"))
			{
				YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
				xmlPropertyWriter.Write();

                BuildTestOrderAliquotOrder(testOrder, testOrderElement);
                BuildTestOrderSlideOrderCollection(testOrder, testOrderElement);
				panelOrder.TestOrderCollection.Add(testOrder);
			}
		}				

		public void SetSurgicalAuditAmendment(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
			{
				panelSetOrder.SurgicalAuditCollection.SetAmendmentReference(amendment);
			}
		}

		public void SetSurgicalSpecimenSpecimenOrder(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrder.SurgicalSpecimenCollection)
			{
				foreach (Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
				{
					if (specimenOrder.SpecimenOrderId == surgicalSpecimen.SpecimenOrderId)
					{
						surgicalSpecimen.SpecimenOrder = specimenOrder;
						break;
					}
				}				
			}
		}

		public void SetSurgicalSpecimenAuditSpecimenOrder(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrder.SurgicalAuditCollection)
			{
				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
				{
					Specimen.Model.SpecimenOrder specimenOrder = (from so in accessionOrder.SpecimenOrderCollection
																where so.SpecimenOrderId == surgicalSpecimenAudit.SpecimenOrderId
																  select so).Single<Specimen.Model.SpecimenOrder>();
					surgicalSpecimenAudit.SpecimenOrder = specimenOrder;
				}
			}         
		}

		public void SetSurgicalSpecimenOrderItemCollection(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{

			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrder.SurgicalSpecimenCollection)
			{
				foreach (Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
				{
					if (specimenOrder.SpecimenOrderId == surgicalSpecimen.SpecimenOrderId)
					{
						panelSetOrder.SpecimenOrderCollection.IsLoading = true;
						panelSetOrder.SpecimenOrderCollection.Add(specimenOrder);
						panelSetOrder.SpecimenOrderCollection.IsLoading = false;
					}
				}
			}            
		}

		private void BuildTaskOrder(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
			List<XElement> taskOrderElements = (from item in accessionOrderElement.Elements("TaskOrderCollection")
														 select item).ToList<XElement>();
			foreach (XElement taskOrderElement in taskOrderElements.Elements("TaskOrder"))
			{
				YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderElement, taskOrder);
				xmlPropertyWriter.Write();
				this.BuildTaskOrderDetail(taskOrder, taskOrderElement);
				accessionOrder.TaskOrderCollection.Add(taskOrder);
			}
		}

		private void BuildTaskOrderDetail(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder, XElement taskOrderElement)
		{
			List<XElement> taskOrderDetailElements = (from item in taskOrderElement.Elements("TaskOrderDetailCollection")
												select item).ToList<XElement>();
			foreach (XElement taskOrderDetailElement in taskOrderDetailElements.Elements("TaskOrderDetail"))
			{
				YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new YellowstonePathology.Business.Task.Model.TaskOrderDetail();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderDetailElement, taskOrderDetail);
				xmlPropertyWriter.Write();
				taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
			}
		}

		private void BuildIcdBillingCode(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
			List<XElement> icd9BillingCodeElements = (from item in accessionOrderElement.Elements("ICD9BillingCodeCollection")
													 select item).ToList<XElement>();
			foreach (XElement icd9BillingCodeElement in icd9BillingCodeElements.Elements("ICD9BillingCode"))
			{
				YellowstonePathology.Business.Billing.ICD9BillingCode icd9BillingCode = new Billing.ICD9BillingCode();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(icd9BillingCodeElement, icd9BillingCode);
				xmlPropertyWriter.Write();
				accessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
			}
		}

		private void BuildSurgicalSpecific(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{
			YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
			if (panelSetOrder.PanelSetId == panelSetSurgical.PanelSetId)
			{
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)panelSetOrder;
				List<XElement> surgicalSpecimenElements = (from item in panelSetOrderElement.Elements("SurgicalSpecimenCollection")
																	 select item).ToList<XElement>();
				foreach (XElement surgicalSpecimenElement in surgicalSpecimenElements.Elements("SurgicalSpecimen"))
				{
					YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen();
					YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(surgicalSpecimenElement, surgicalSpecimen);
					xmlPropertyWriter.Write();
					BuildICD9Code(surgicalSpecimen, surgicalSpecimenElement);
					BuildIntraoperativeConsultationResult(surgicalSpecimen, surgicalSpecimenElement);
					BuildStainResult(surgicalSpecimen, surgicalSpecimenElement);
					panelSetOrderSurgical.SurgicalSpecimenCollection.Add(surgicalSpecimen);
				}

				BuildSurgicalAudit(panelSetOrderSurgical, panelSetOrderElement);
				SetSurgicalAuditAmendment(panelSetOrderSurgical);
				SetSurgicalSpecimenSpecimenOrder(accessionOrder, panelSetOrderSurgical);
				SetSurgicalSpecimenAuditSpecimenOrder(accessionOrder, panelSetOrderSurgical);
				SetSurgicalSpecimenOrderItemCollection(accessionOrder, panelSetOrderSurgical);
			}

		}

		private void BuildSurgicalAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder, XElement panelSetOrderElement)
		{
			List<XElement> collectionElements = (from item in panelSetOrderElement.Elements("SurgicalAuditCollection")
												 select item).ToList<XElement>();
			foreach (XElement surgicalAuditElement in collectionElements.Elements("SurgicalAudit"))
			{
				YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit = new YellowstonePathology.Business.Test.Surgical.SurgicalAudit();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(surgicalAuditElement, surgicalAudit);
				xmlPropertyWriter.Write();
				BuildSurgicalSpecimenAuditResult(surgicalAudit, surgicalAuditElement);
				panelSetOrder.SurgicalAuditCollection.Add(surgicalAudit);
			}
		}

		private void BuildSurgicalSpecimenAuditResult(YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit, XElement surgicalAuditElement)
		{
			List<XElement> collectionElements = (from item in surgicalAuditElement.Elements("SurgicalSpecimenAuditCollection")
												 select item).ToList<XElement>();
			foreach (XElement surgicalSpecimenResultAuditElement in collectionElements.Elements("SurgicalSpecimenAudit"))
			{
				YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(surgicalSpecimenResultAuditElement, surgicalSpecimenAudit);
				xmlPropertyWriter.Write();
				surgicalAudit.SurgicalSpecimenAuditCollection.Add(surgicalSpecimenAudit);
			}
		}

		private void BuildICD9Code(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, XElement surgicalSpecimenElement)
		{
			List<XElement> collectionElements = (from item in surgicalSpecimenElement.Elements("ICD9BillingCodeCollection")
												 select item).ToList<XElement>();
			foreach (XElement icd9BillingElement in collectionElements.Elements("ICD9BillingCode"))
			{
				YellowstonePathology.Business.Billing.ICD9BillingCode icd9Billing = new YellowstonePathology.Business.Billing.ICD9BillingCode();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(icd9BillingElement, icd9Billing);
				xmlPropertyWriter.Write();
				surgicalSpecimen.ICD9BillingCodeCollection.Add(icd9Billing);
			}
		}

		private void BuildIntraoperativeConsultationResult(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, XElement surgicalSpecimenElement)
		{
			List<XElement> collectionElements = (from item in surgicalSpecimenElement.Elements("IntraoperativeConsultationResultCollection")
												 select item).ToList<XElement>();
			foreach (XElement intraoperativeConsultationResultElement in collectionElements.Elements("IntraoperativeConsultationResult"))
			{
				YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult = new YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(intraoperativeConsultationResultElement, intraoperativeConsultationResult);
				xmlPropertyWriter.Write();
				surgicalSpecimen.IntraoperativeConsultationResultCollection.Add(intraoperativeConsultationResult);
			}
		}

		private void BuildStainResult(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, XElement surgicalSpecimenElement)
		{
			List<XElement> collectionElements = (from item in surgicalSpecimenElement.Elements("StainResultItemCollection")
												 select item).ToList<XElement>();
			foreach (XElement stainResultElement in collectionElements.Elements("StainResultItem"))
			{
				YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem = new SpecialStain.StainResultItem();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(stainResultElement, stainResultItem);
				xmlPropertyWriter.Write();
				surgicalSpecimen.StainResultItemCollection.Add(stainResultItem);
			}
		}		
	}
}
