using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	[PersistentClass("tblPanelSetOrderMPNExtendedReflex", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderMPNExtendedReflex : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
	{
		private string m_Comment;
		private string m_Interpretation;
		private string m_Method;
        private string m_JAK2V617FResult;
        private string m_CalreticulinMutationAnalysisResult;
        private string m_MPLResult;

        public PanelSetOrderMPNExtendedReflex()
		{

		}

		public PanelSetOrderMPNExtendedReflex(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

		}

		public override void OrderInitialTests(AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
		{
			YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest panelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetJAK2V617F.PanelSetId) == false)
			{
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetJAK2V617F, orderTarget, false);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
			}

            YellowstonePathology.Business.Test.MPL.MPLTest mplTest = new MPL.MPLTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(mplTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
            }

            YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calreticulinTest = new CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(calreticulinTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(calreticulinTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if (this.m_Interpretation != value)
				{
					this.m_Interpretation = value;
					this.NotifyPropertyChanged("Interpretation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

        [PersistentProperty()]
        public string JAK2V617FResult
        {
            get { return this.m_JAK2V617FResult; }
            set
            {
                if (this.m_JAK2V617FResult != value)
                {
                    this.m_JAK2V617FResult = value;
                    this.NotifyPropertyChanged("JAK2V617FResult");
                }
            }
        }

        [PersistentProperty()]
        public string CalreticulinMutationAnalysisResult
        {
            get { return this.m_CalreticulinMutationAnalysisResult; }
            set
            {
                if (this.m_CalreticulinMutationAnalysisResult != value)
                {
                    this.m_CalreticulinMutationAnalysisResult = value;
                    this.NotifyPropertyChanged("CalreticulinMutationAnalysisResult");
                }
            }
        }

        [PersistentProperty()]
        public string MPLResult
        {
            get { return this.m_MPLResult; }
            set
            {
                if (this.m_MPLResult != value)
                {
                    this.m_MPLResult = value;
                    this.NotifyPropertyChanged("MPLResult");
                }
            }
        }

        public override string ToResultString(AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
            result.AppendLine("Interpretation:");
            result.AppendLine(this.m_Interpretation);
            result.AppendLine();

            result.AppendLine("Comment:");
            result.AppendLine(this.m_Comment);
            result.AppendLine();

            YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
            YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calreticulinMutationAnalysisTest = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
            YellowstonePathology.Business.Test.MPL.MPLTest mplTest = new YellowstonePathology.Business.Test.MPL.MPLTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                result.AppendLine("JAK2 V617F");
                YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                result.AppendLine(jak2V617FTestOrder.ToResultString(accessionOrder));
                result.AppendLine();
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(calreticulinMutationAnalysisTest.PanelSetId) == true)
            {
                result.AppendLine("Calreticulin Mutation Analysis");
                YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder calreticulinMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(calreticulinMutationAnalysisTest.PanelSetId);
                result.AppendLine(calreticulinMutationAnalysisTestOrder.ToResultString(accessionOrder));
                result.AppendLine();
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == true)
            {
                result.AppendLine("MPL");
                YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL mplTestOrder = (YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mplTest.PanelSetId);
                result.AppendLine(mplTestOrder.ToResultString(accessionOrder));
                result.AppendLine();
            }

            return result.ToString();
        }

        public override void SetPreviousResults(PanelSetOrder panelSetOrder)
        {
            Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)panelSetOrder;
            panelSetOrderMPNExtendedReflex.JAK2V617FResult = this.JAK2V617FResult;
            panelSetOrderMPNExtendedReflex.MPLResult = this.m_MPLResult;
            panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult = this.m_CalreticulinMutationAnalysisResult;
            panelSetOrderMPNExtendedReflex.Comment = this.m_Comment;
            panelSetOrderMPNExtendedReflex.Interpretation = this.m_Interpretation;
            panelSetOrderMPNExtendedReflex.Method = this.m_Method;
            base.SetPreviousResults(panelSetOrder);
        }

        public override void ClearPreviousResults()
        {
            this.m_JAK2V617FResult = null;
            this.m_MPLResult = null;
            this.m_CalreticulinMutationAnalysisResult = null;
            this.m_Comment = null;
            this.m_Interpretation = null;
            this.m_Method = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreComponentTestOrdersAccepted(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoComponentTestResultsMatchPreviousResults(accessionOrder, (PanelSetOrderMPNExtendedReflex)panelSetOrder, result);
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

        private void DoComponentTestResultsMatchPreviousResults(AccessionOrder accessionOrder, PanelSetOrderMPNExtendedReflex panelSetOrder, Audit.Model.AuditResult result)
        {
            Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            Test.MPL.MPLTest mplTest = new MPL.MPLTest();
            Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calRTest = new CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                if (jak2V617FTestOrder.Result != panelSetOrder.JAK2V617FResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(jak2V617FTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == true)
            {
                Test.MPL.PanelSetOrderMPL panelSetOrderMPL = (MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mplTest.PanelSetId);
                if (panelSetOrderMPL.Result != panelSetOrder.MPLResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(panelSetOrderMPL.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(calRTest.PanelSetId) == true)
            {
                Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder calreticulinMutationAnalysisTestOrder = (CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(calRTest.PanelSetId);
                if (calreticulinMutationAnalysisTestOrder.Result != panelSetOrder.CalreticulinMutationAnalysisResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(calreticulinMutationAnalysisTestOrder.PanelSetName);
                }
            }
        }

        private void AreComponentTestOrdersAccepted(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            Test.MPL.MPLTest mplTest = new MPL.MPLTest();
            Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calRTest = new CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                if (jak2V617FTestOrder.Accepted == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotAcceptedMessage(jak2V617FTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == true)
            {
                Test.MPL.PanelSetOrderMPL panelSetOrderMPL = (MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mplTest.PanelSetId);
                if (panelSetOrderMPL.Accepted == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotAcceptedMessage(panelSetOrderMPL.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(calRTest.PanelSetId) == true)
            {
                Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder calreticulinMutationAnalysisTestOrder = (CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(calRTest.PanelSetId);
                if (calreticulinMutationAnalysisTestOrder.Accepted == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotAcceptedMessage(calreticulinMutationAnalysisTestOrder.PanelSetName);
                }
            }
        }

        private void AreComponentTestOrdersFinal(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            Test.MPL.MPLTest mplTest = new MPL.MPLTest();
            Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calRTest = new CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                if (jak2V617FTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(jak2V617FTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == true)
            {
                Test.MPL.PanelSetOrderMPL panelSetOrderMPL = (MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mplTest.PanelSetId);
                if (panelSetOrderMPL.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(panelSetOrderMPL.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(calRTest.PanelSetId) == true)
            {
                Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder calreticulinMutationAnalysisTestOrder = (CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(calRTest.PanelSetId);
                if (calreticulinMutationAnalysisTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(calreticulinMutationAnalysisTestOrder.PanelSetName);
                }
            }
        }

        private void AreTestResultsPresent(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            Test.MPL.MPLTest mplTest = new MPL.MPLTest();
            Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest calRTest = new CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_JAK2V617FResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("JAK2V617FResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(mplTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_MPLResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("MPLResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(calRTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_CalreticulinMutationAnalysisResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("CalreticulinMutationAnalysisResult");
                }
            }
        }

        public void DoesJAK2V617FResultMatch(string jak2V617FResult, Audit.Model.AuditResult result)
        {
            if(this.Final == true && this.JAK2V617FResult != jak2V617FResult)
            {
                result.Status = Audit.Model.AuditStatusEnum.Warning;
                result.Message += MismatchMessage(this.PanelSetName);
            }
        }

        public void DoesMPLResultMatch(string mplResult, Audit.Model.AuditResult result)
        {
            if(this.Final == true && this.MPLResult != mplResult)
            {
                result.Status = Audit.Model.AuditStatusEnum.Warning;
                result.Message += MismatchMessage(this.PanelSetName);
            }
        }

        public void DoesCalreticulinMutationAnalysisResultMatch(string calreticulinMutationAnalysisResult, Audit.Model.AuditResult result)
        {
            if (this.Final == true && this.CalreticulinMutationAnalysisResult != calreticulinMutationAnalysisResult)
            {
                result.Status = Audit.Model.AuditStatusEnum.Warning;
                result.Message += MismatchMessage(this.PanelSetName);
            }
        }
    }
}
