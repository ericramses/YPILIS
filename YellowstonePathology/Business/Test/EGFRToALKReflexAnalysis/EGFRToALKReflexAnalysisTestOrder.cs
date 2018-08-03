using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	[PersistentClass("tblEGFRToALKReflexAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class EGFRToALKReflexAnalysisTestOrder : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{
		private string m_Method;
		private string m_Interpretation;
        private string m_TumorNucleiPercentage;
        private bool m_QNSForALK;
        private bool m_QNSForROS1;

		public EGFRToALKReflexAnalysisTestOrder() 
        {
            
        }

		public EGFRToALKReflexAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            
		}

        public override void OrderInitialTests(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {                        
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoPDL122C3 = new TestOrderInfo(pdl122C3Test, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorPDL122C3 = new Visitor.OrderTestOrderVisitor(testOrderInfoPDL122C3);
            accessionOrder.TakeATrip(orderTestOrderVisitorPDL122C3);
            
            
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoEGFR = new TestOrderInfo(egfrMutationAnalysisTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorEGFR = new Visitor.OrderTestOrderVisitor(testOrderInfoEGFR);
            accessionOrder.TakeATrip(orderTestOrderVisitorEGFR);

            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoRos1 = new TestOrderInfo(ros1ByfishTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorRos1 = new Visitor.OrderTestOrderVisitor(testOrderInfoRos1);
            accessionOrder.TakeATrip(orderTestOrderVisitorRos1);

            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoALK = new TestOrderInfo(alkTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorALK = new Visitor.OrderTestOrderVisitor(testOrderInfoALK);
            accessionOrder.TakeATrip(orderTestOrderVisitorALK);

            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoBRAF = new TestOrderInfo(brafTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorBRAF = new Visitor.OrderTestOrderVisitor(testOrderInfoBRAF);
            accessionOrder.TakeATrip(orderTestOrderVisitorBRAF);
        }		

        public void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            StringBuilder interpretation = new StringBuilder();
            StringBuilder method = new StringBuilder();
            StringBuilder references = new StringBuilder();

            if (accessionOrder.PanelSetOrderCollection.Exists(60) == true)
            {
                YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
                interpretation.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.Interpretation);                
                references.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.ReportReferences);                
                method.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(131) == true)
            {                
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);

                references.AppendLine();
                references.AppendLine("ALK: " + alkForNSCLCByFISHTestOrder.ReportReferences);

                interpretation.AppendLine();
                interpretation.AppendLine("ALK: " + alkForNSCLCByFISHTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine("ALK: " + alkForNSCLCByFISHTestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(204) == true)
            {                
                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(204);                

                interpretation.AppendLine();
                interpretation.AppendLine("ROS1: " + ros1ByFISHTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine("ROS1: " + ros1ByFISHTestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(215) == true)
            {
                YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder pdl1sp142TestOrder = (YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(215);
                interpretation.AppendLine();
                interpretation.AppendLine(pdl1sp142TestOrder.PanelSetName + ": " + pdl1sp142TestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine(pdl1sp142TestOrder.PanelSetName + ": " + pdl1sp142TestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(245) == true)
            {
                YellowstonePathology.Business.Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (YellowstonePathology.Business.Test.PDL122C3.PDL122C3TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(245);
                interpretation.AppendLine();
                interpretation.AppendLine(pdl122C3TestOrder.PanelSetName + ": " + pdl122C3TestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine(pdl122C3TestOrder.PanelSetName + ": " + pdl122C3TestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(274) == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(274);
                interpretation.AppendLine();
                interpretation.AppendLine(brafMutationAnalysisTestOrder.PanelSetName + ": " + brafMutationAnalysisTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine(brafMutationAnalysisTestOrder.PanelSetName + ": " + brafMutationAnalysisTestOrder.Method);
            }

            char[] lineFeedCharacters = { '\r', '\n' };            
            this.Interpretation = interpretation.ToString().TrimEnd(lineFeedCharacters);
            this.Method = method.ToString().TrimEnd(lineFeedCharacters);
            this.ReportReferences = references.ToString().TrimEnd(lineFeedCharacters);
        }		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				this.m_Method = value;
				NotifyPropertyChanged("Method");
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				this.m_Interpretation = value;
				NotifyPropertyChanged("Interpretation");
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool QNSForALK
        {
            get { return this.m_QNSForALK; }
            set
            {
                this.m_QNSForALK = value;
                NotifyPropertyChanged("QNSForALK");
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool QNSForROS1
        {
            get { return this.m_QNSForROS1; }
            set
            {
                this.m_QNSForROS1 = value;
                NotifyPropertyChanged("QNSForROS1");
            }
        }

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("EGFR Mutation Analysis");
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
				result.AppendLine(egfrMutationAnalysisTestOrder.ToResultString(accessionOrder));
			}
			result.AppendLine();

			result.AppendLine("ALK For NSCLC By FISH");
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkForNSCLCByFISHTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
			if(accessionOrder.PanelSetOrderCollection.Exists(alkForNSCLCByFISHTest.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
				result.AppendLine(alkForNSCLCByFISHTestOrder.ToResultString(accessionOrder));
			}
			result.AppendLine();

			result.AppendLine("Interpretation:");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}

        protected override void CheckResults(AccessionOrder accessionOrder, object clone)
        {
            EGFRToALKReflexAnalysisTestOrder testOrderToCheck = (EGFRToALKReflexAnalysisTestOrder)clone;
            testOrderToCheck.SetResults(accessionOrder);
        }
    }
}
