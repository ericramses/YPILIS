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
        private string m_PDL122C3Result;
        private string m_EGFRMutationAnalysisResult;
        private string m_ROS1ByFISHResult;
        private string m_ALKForNSCLCByFISHResult;
        private string m_BRAFMutationAnalysisResult;
        private string m_PDL1SP142Result;

        private EGFRToALKReflexAnalysisElementStatusEnum m_PDL122C3Status;
        private EGFRToALKReflexAnalysisElementStatusEnum m_EGFRMutationAnalysisStatus;
        private EGFRToALKReflexAnalysisElementStatusEnum m_ROS1ByFISHStatus;
        private EGFRToALKReflexAnalysisElementStatusEnum m_ALKForNSCLCByFISHStatus;
        private EGFRToALKReflexAnalysisElementStatusEnum m_BRAFMutationAnalysisStatus;
        private EGFRToALKReflexAnalysisElementStatusEnum m_PDL1SP142Status;

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
            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoPDL122C3 = new TestOrderInfo(pdl122C3Test, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorPDL122C3 = new Visitor.OrderTestOrderVisitor(testOrderInfoPDL122C3);
                accessionOrder.TakeATrip(orderTestOrderVisitorPDL122C3);
            }

            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoEGFR = new TestOrderInfo(egfrMutationAnalysisTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorEGFR = new Visitor.OrderTestOrderVisitor(testOrderInfoEGFR);
                accessionOrder.TakeATrip(orderTestOrderVisitorEGFR);
            }

            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoRos1 = new TestOrderInfo(ros1ByfishTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorRos1 = new Visitor.OrderTestOrderVisitor(testOrderInfoRos1);
                accessionOrder.TakeATrip(orderTestOrderVisitorRos1);
            }

            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoALK = new TestOrderInfo(alkTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorALK = new Visitor.OrderTestOrderVisitor(testOrderInfoALK);
                accessionOrder.TakeATrip(orderTestOrderVisitorALK);
            }

            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoBRAF = new TestOrderInfo(brafTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorBRAF = new Visitor.OrderTestOrderVisitor(testOrderInfoBRAF);
                accessionOrder.TakeATrip(orderTestOrderVisitorBRAF);
            }
        }

        /*public void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
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
        }*/

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

        [PersistentProperty()]
        public string PDL122C3Result
        {
            get { return this.m_PDL122C3Result; }
            set
            {
                this.m_PDL122C3Result = value;
                NotifyPropertyChanged("PDL122C3Result");
            }
        }

        [PersistentProperty()]
        public string EGFRMutationAnalysisResult
        {
            get { return this.m_EGFRMutationAnalysisResult; }
            set
            {
                this.m_EGFRMutationAnalysisResult = value;
                NotifyPropertyChanged("EGFRMutationAnalysisResult");
            }
        }

        [PersistentProperty()]
        public string ROS1ByFISHResult
        {
            get { return this.m_ROS1ByFISHResult; }
            set
            {
                this.m_ROS1ByFISHResult = value;
                NotifyPropertyChanged("ROS1ByFISHResult");
            }
        }

        [PersistentProperty()]
        public string ALKForNSCLCByFISHResult
        {
            get { return this.m_ALKForNSCLCByFISHResult; }
            set
            {
                this.m_ALKForNSCLCByFISHResult = value;
                NotifyPropertyChanged("ALKForNSCLCByFISHResult");
            }
        }
        
        [PersistentProperty()]
        public string BRAFMutationAnalysisResult
        {
            get { return this.m_BRAFMutationAnalysisResult; }
            set
            {
                this.m_BRAFMutationAnalysisResult = value;
                NotifyPropertyChanged("BRAFMutationAnalysisResult");
            }
        }

        [PersistentProperty()]
        public string PDL1SP142Result
        {
            get { return this.m_PDL1SP142Result; }
            set
            {
                this.m_PDL1SP142Result = value;
                NotifyPropertyChanged("PDL1SP142Result");
            }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum PDL122C3Status
        {
            get { return this.m_PDL122C3Status; }
            set
            {
                this.m_PDL122C3Status = value;
                NotifyPropertyChanged("PDL122C3Status");
            }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum EGFRMutationAnalysisStatus
        {
            get { return this.m_EGFRMutationAnalysisStatus; }
            set
            {
                this.m_EGFRMutationAnalysisStatus = value;
                NotifyPropertyChanged("EGFRMutationAnalysisStatus");
            }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum ROS1ByFISHStatus
        {
            get { return this.m_ROS1ByFISHStatus; }
            set
            {
                this.m_ROS1ByFISHStatus = value;
                NotifyPropertyChanged("ROS1ByFISHStatus");
            }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum ALKForNSCLCByFISHStatus
        {
            get { return this.m_ALKForNSCLCByFISHStatus; }
            set
            {
                this.m_ALKForNSCLCByFISHStatus = value;
                NotifyPropertyChanged("ALKForNSCLCByFISHStatus");
            }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum BRAFMutationAnalysisStatus
        {
            get { return this.m_BRAFMutationAnalysisStatus; }
            set
            {
                this.m_BRAFMutationAnalysisStatus = value;
                NotifyPropertyChanged("BRAFMutationAnalysisStatus");
            }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum PDL1SP142Status
        {
            get { return this.m_PDL1SP142Status; }
            set
            {
                this.m_PDL1SP142Status = value;
                NotifyPropertyChanged("PDL1SP142Status");
            }
        }

        public string PDL122C3TestAbbreviation
        {
            get { return new Test.PDL122C3.PDL122C3Test().Abbreviation; }
        }

        public string EGFRMutationAnalysisTestAbbreviation
        {
            get { return new Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest().Abbreviation; }
        }

        public string ROS1ByFISHTestAbbreviation
        {
            get { return new Test.ROS1ByFISH.ROS1ByFISHTest().Abbreviation; }
        }

        public string ALKForNSCLCByFISHTestAbbreviation
        {
            get { return new Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest().Abbreviation; }
        }

        public string BRAFMutationAnalysisTestAbbreviation
        {
            get { return new Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest().Abbreviation; }
        }
        public string PDL1SP142TestAbbreviation
        {
            get { return new Test.PDL1SP142.PDL1SP142Test().Abbreviation; }
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

        /*protected override void CheckResults(AccessionOrder accessionOrder, object clone)
        {
            EGFRToALKReflexAnalysisTestOrder testOrderToCheck = (EGFRToALKReflexAnalysisTestOrder)clone;
            testOrderToCheck.SetResults(accessionOrder);
        }*/

        public override void SetPreviousResults(PanelSetOrder panelSetOrder)
        {
            EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (EGFRToALKReflexAnalysisTestOrder)panelSetOrder;
            egfrToALKReflexAnalysisTestOrder.Method = this.m_Method;
            egfrToALKReflexAnalysisTestOrder.Interpretation = this.m_Interpretation;
            egfrToALKReflexAnalysisTestOrder.TumorNucleiPercentage = this.m_TumorNucleiPercentage;
            egfrToALKReflexAnalysisTestOrder.QNSForALK = this.m_QNSForALK;
            egfrToALKReflexAnalysisTestOrder.QNSForROS1 = this.m_QNSForROS1;
            egfrToALKReflexAnalysisTestOrder.PDL122C3Result = this.m_PDL122C3Result;
            egfrToALKReflexAnalysisTestOrder.EGFRMutationAnalysisResult = this.m_EGFRMutationAnalysisResult;
            egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult = this.m_ROS1ByFISHResult;
            egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult = this.m_ALKForNSCLCByFISHResult;
            egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult = this.m_BRAFMutationAnalysisResult;
            egfrToALKReflexAnalysisTestOrder.PDL1SP142Result = this.m_PDL1SP142Result;
            base.SetPreviousResults(panelSetOrder);
    }

    public override void ClearPreviousResults()
        {
            this.m_Method = null;
            this.m_Interpretation = null;
            this.m_TumorNucleiPercentage = null;
            this.m_QNSForALK = false;
            this.m_QNSForROS1 = false;
            this.m_PDL122C3Result = null;
            this.m_EGFRMutationAnalysisResult = null;
            this.m_ROS1ByFISHResult = null;
            this.m_ALKForNSCLCByFISHResult = null;
            this.m_BRAFMutationAnalysisResult = null;
            this.m_PDL1SP142Result = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoComponentTestResultsMatchPreviousResults(accessionOrder, (EGFRToALKReflexAnalysisTestOrder)panelSetOrder, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskSetPreviousResults;
                }
            }

            return result;
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreTestResultsPresent(accessionOrder, result);
                if (result.Status == Audit.Model.AuditStatusEnum.OK)
                {
                    this.DoComponentTestResultsMatchPreviousResults(accessionOrder, this, result);
                    if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                    {
                        result.Message += AskAccept;
                    }
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreTestResultsPresent(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreComponentTestOrdersFinal(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoComponentTestResultsMatchPreviousResults(accessionOrder, this, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskFinal;
                }
            }
            return result;
        }

        private void DoComponentTestResultsMatchPreviousResults(AccessionOrder accessionOrder, EGFRToALKReflexAnalysisTestOrder panelSetOrder, Audit.Model.AuditResult result)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3.PDL122C3TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl122C3Test.PanelSetId);
                if (pdl122C3TestOrder.Result != panelSetOrder.PDL122C3Result)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(pdl122C3TestOrder.PanelSetName, pdl122C3TestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder  egfrMutationAnalysisTestOrder = (EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrMutationAnalysisTest.PanelSetId);
                if (egfrMutationAnalysisTestOrder.Result != panelSetOrder.EGFRMutationAnalysisResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(egfrMutationAnalysisTestOrder.PanelSetName, egfrMutationAnalysisTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (ROS1ByFISH.ROS1ByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ros1ByfishTest.PanelSetId);
                if (ros1ByFISHTestOrder.Result != panelSetOrder.ROS1ByFISHResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(ros1ByFISHTestOrder.PanelSetName, ros1ByFISHTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkTestOrder = (ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(alkTest.PanelSetId);
                if (alkTestOrder.Result != panelSetOrder.ALKForNSCLCByFISHResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(alkTestOrder.PanelSetName, alkTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafTest.PanelSetId);
                if (brafTestOrder.Result != panelSetOrder.BRAFMutationAnalysisResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(brafTestOrder.PanelSetName, brafTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                Test.PDL1SP142.PDL1SP142TestOrder pdl1SP142TestOrder = (PDL1SP142.PDL1SP142TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl1SP142Test.PanelSetId);
                if (pdl1SP142TestOrder.Result != panelSetOrder.PDL1SP142Result)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(pdl1SP142TestOrder.PanelSetName, pdl1SP142TestOrder.Result);
                }
            }
        }

        private void AreComponentTestOrdersFinal(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3.PDL122C3TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl122C3Test.PanelSetId);
                if (pdl122C3TestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(pdl122C3TestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrMutationAnalysisTest.PanelSetId);
                if (egfrMutationAnalysisTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(egfrMutationAnalysisTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (ROS1ByFISH.ROS1ByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ros1ByfishTest.PanelSetId);
                if (ros1ByFISHTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(ros1ByFISHTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkTestOrder = (ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(alkTest.PanelSetId);
                if (alkTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(alkTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafTest.PanelSetId);
                if (brafTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(brafTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                Test.PDL1SP142.PDL1SP142TestOrder pdl1SP142TestOrder = (PDL1SP142.PDL1SP142TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl1SP142Test.PanelSetId);
                if (pdl1SP142TestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(pdl1SP142TestOrder.PanelSetName);
                }
            }
        }

        private void AreTestResultsPresent(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_PDL122C3Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("PDL122C3Result");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_EGFRMutationAnalysisResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("EGFRMutationAnalysisResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_ROS1ByFISHResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("ROS1ByFISHResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_ALKForNSCLCByFISHResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("ALKForNSCLCByFISHResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_BRAFMutationAnalysisResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("BRAFMutationAnalysisResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_PDL1SP142Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("PDL1SP142Result");
                }
            }
        }

        public void DoesPDL122C3ResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_PDL122C3Result != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_PDL122C3Result);
            }
        }

        public void DoesEGFRMutationAnalysisResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_EGFRMutationAnalysisResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_EGFRMutationAnalysisResult);
            }
        }

        public void DoesROS1ByFISHResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_ROS1ByFISHResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_ROS1ByFISHResult);
            }
        }

        public void DoesALKForNSCLCByFISHResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_ALKForNSCLCByFISHResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_ALKForNSCLCByFISHResult);
            }
        }

        public void DoesBRAFMutationAnalysisResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_BRAFMutationAnalysisResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_BRAFMutationAnalysisResult);
            }
        }

        public void DoesPDL1SP142ResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_PDL1SP142Result != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_PDL1SP142Result);
            }
        }

        public override void SetStatus(PanelSetOrderCollection panelSetOrderCollection)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (panelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3.PDL122C3TestOrder)panelSetOrderCollection.GetPanelSetOrder(pdl122C3Test.PanelSetId);
                this.m_PDL122C3Status = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                if (pdl122C3TestOrder.Accepted == true) this.m_PDL122C3Status = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (pdl122C3TestOrder.Final == true) this.m_PDL122C3Status = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_PDL122C3Status = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }

            if (panelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)panelSetOrderCollection.GetPanelSetOrder(egfrMutationAnalysisTest.PanelSetId);
                this.m_EGFRMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                if (egfrMutationAnalysisTestOrder.Accepted == true) this.m_EGFRMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (egfrMutationAnalysisTestOrder.Final == true) this.m_EGFRMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_EGFRMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }

            if (panelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (ROS1ByFISH.ROS1ByFISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(ros1ByfishTest.PanelSetId);
                this.m_ROS1ByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                if (ros1ByFISHTestOrder.Accepted == true) this.m_ROS1ByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (ros1ByFISHTestOrder.Final == true) this.m_ROS1ByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_ROS1ByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }

            if (panelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkTestOrder = (ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(alkTest.PanelSetId);
                this.m_ALKForNSCLCByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                if (alkTestOrder.Accepted == true) this.m_ALKForNSCLCByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (alkTestOrder.Final == true) this.m_ALKForNSCLCByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_ALKForNSCLCByFISHStatus = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }

            if (panelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)panelSetOrderCollection.GetPanelSetOrder(brafTest.PanelSetId);
                this.m_BRAFMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                if (brafTestOrder.Accepted == true) this.m_BRAFMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (brafTestOrder.Final == true) this.m_BRAFMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_BRAFMutationAnalysisStatus = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }

            if (panelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                Test.PDL1SP142.PDL1SP142TestOrder pdl1SP142TestOrder = (PDL1SP142.PDL1SP142TestOrder)panelSetOrderCollection.GetPanelSetOrder(pdl1SP142Test.PanelSetId);
                this.m_PDL1SP142Status = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                if (pdl1SP142TestOrder.Accepted == true) this.m_PDL1SP142Status = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (pdl1SP142TestOrder.Final == true) this.m_PDL1SP142Status = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_PDL1SP142Status = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }
        }
    }
}
