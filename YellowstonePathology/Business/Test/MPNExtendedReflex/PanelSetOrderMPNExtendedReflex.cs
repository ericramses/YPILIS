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

        public override Rules.MethodResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Rules.MethodResult result = this.CheckResults(accessionOrder, "Match");

            if (result.Success == false)
            {
                result.Message += "Are you sure you want to use the selected results?";
            }

            return result;
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                Rules.MethodResult methodResult = this.CheckResults(accessionOrder, "Filled");
                if (methodResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = methodResult.Message;
                }
                else
                {
                    methodResult = this.CheckResults(accessionOrder, "Match");
                    if (methodResult.Success == false)
                    {
                        result.Status = Audit.Model.AuditStatusEnum.Warning;
                        result.Message = methodResult.Message + "Are you sure you want to accept the results?";
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
                Rules.MethodResult methodResult = this.CheckResults(accessionOrder, "Filled");
                if (methodResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = methodResult.Message;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                Rules.MethodResult methodResult = this.CheckResults(accessionOrder, "Final");
                if (methodResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = methodResult.Message;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                Rules.MethodResult methodResult = this.CheckResults(accessionOrder, "Match");
                if (methodResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message = methodResult.Message + "Are you sure you want to finalize this report?";
                }
            }
            return result;
        }

        private Rules.MethodResult CheckResults(AccessionOrder accessionOrder, string action)
        {
            Rules.MethodResult result = this.MatchJAK2V617FResult(accessionOrder, action);

            Rules.MethodResult tmpResult = this.MatchMPLResult(accessionOrder, action);
            if (tmpResult.Success == false)
            {
                result.Success = false;
                result.Message += tmpResult.Message;
            }

            tmpResult = this.MatchCalreticulinMutationAnalysisResult(accessionOrder, action);
            if (tmpResult.Success == false)
            {
                result.Success = false;
                result.Message += tmpResult.Message;
            }
            return result;
        }

        private Rules.MethodResult MatchJAK2V617FResult(AccessionOrder accessionOrder, string action)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                if (action == "Filled" && string.IsNullOrEmpty(this.JAK2V617FResult) == true)
                {
                    result.Success = false;
                    result.Message = this.NotFilledMessage(jak2V617FTest.PanelSetName);
                }
                if (action == "Match" && jak2V617FTestOrder.Result != this.JAK2V617FResult)
                {
                    result.Success = false;
                    result.Message += this.MismatchMessage(jak2V617FTest.PanelSetName, jak2V617FTestOrder.Result);
                }
                if (action == "Final" && jak2V617FTestOrder.Final == false)
                {
                    result.Success = false;
                    result.Message += this.NotFinaledMessage(jak2V617FTest.PanelSetName);
                }
            }
            return result;
        }

        private Rules.MethodResult MatchMPLResult(AccessionOrder accessionOrder, string action)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            Test.MPL.MPLTest test = new MPL.MPLTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId) == true)
            {
                Test.MPL.PanelSetOrderMPL testOrder = (MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId);
                if (action == "Filled" && string.IsNullOrEmpty(this.JAK2V617FResult) == true)
                {
                    result.Success = false;
                    result.Message = this.NotFilledMessage(test.PanelSetName);
                }
                if (action == "Match" && testOrder.Result != this.JAK2V617FResult)
                {
                    result.Success = false;
                    result.Message += this.MismatchMessage(test.PanelSetName, testOrder.Result);
                }
                if (action == "Final" && testOrder.Final == false)
                {
                    result.Success = false;
                    result.Message += this.NotFinaledMessage(test.PanelSetName);
                }
            }
            return result;
        }

        private Rules.MethodResult MatchCalreticulinMutationAnalysisResult(AccessionOrder accessionOrder, string action)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest test = new CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId) == true)
            {
                Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder testOrder = (CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId);
                if (action == "Filled" && string.IsNullOrEmpty(this.JAK2V617FResult) == true)
                {
                    result.Success = false;
                    result.Message = this.NotFilledMessage(test.PanelSetName);
                }
                if (action == "Match" && testOrder.Result != this.JAK2V617FResult)
                {
                    result.Success = false;
                    result.Message += this.MismatchMessage(test.PanelSetName, testOrder.Result);
                }
                if (action == "Final" && testOrder.Final == false)
                {
                    result.Success = false;
                    result.Message += this.NotFinaledMessage(test.PanelSetName);
                }
            }
            return result;
        }

        private string NotFilledMessage(string panelSetName)
        {
            return "The " + panelSetName + " result is not set." + Environment.NewLine;
        }

        private string MismatchMessage(string panelSetName, string panelSetResult)
        {
            return "The " + panelSetName + " result(" + panelSetResult + ") does not match." + Environment.NewLine;
        }

        private string NotFinaledMessage(string panelSetName)
        {
            return "The " + panelSetName + " is not finaled." + Environment.NewLine;
        }
    }
}
