using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Rules;

namespace YellowstonePathology.Business.Test.MPNStandardReflex
{
    [PersistentClass("tblPanelSetOrderMPNStandardReflex", "tblPanelSetOrder", "YPIDATA")]
    public class PanelSetOrderMPNStandardReflex : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
    {
        private string m_JAK2V617FResult;
        private string m_JAK2Exon1214Result;
        private string m_Comment;
        private string m_Interpretation;
        private string m_Method;

        public PanelSetOrderMPNStandardReflex()
        {

        }

        public PanelSetOrderMPNStandardReflex(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {

        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string JAK2Exon1214Result
        {
            get { return this.m_JAK2Exon1214Result; }
            set
            {
                if (this.m_JAK2Exon1214Result != value)
                {
                    this.m_JAK2Exon1214Result = value;
                    this.NotifyPropertyChanged("JAK2Exon1214Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        public override void OrderInitialTests(AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest panelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetJAK2V617F.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetJAK2V617F, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
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

            result.AppendLine("JAK2 V617F");
            result.AppendLine(this.m_JAK2V617FResult);
            result.AppendLine();

            result.AppendLine("JAK2 Exon 1214");
            result.AppendLine(this.m_JAK2Exon1214Result);
            result.AppendLine();            

			return result.ToString();
		}

        public override void SetPreviousResults(PanelSetOrder panelSetOrder)
        {
            Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex panelSetOrderMPNStandardReflex = (Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)panelSetOrder;
            panelSetOrderMPNStandardReflex.JAK2V617FResult = this.JAK2V617FResult;
            panelSetOrderMPNStandardReflex.JAK2Exon1214Result = this.m_JAK2Exon1214Result;
            panelSetOrderMPNStandardReflex.Comment = this.m_Comment;
            panelSetOrderMPNStandardReflex.Interpretation = this.m_Interpretation;
            panelSetOrderMPNStandardReflex.Method = this.m_Method;
            base.SetPreviousResults(panelSetOrder);
        }

        public override void ClearPreviousResults()
        {
            this.m_JAK2V617FResult = null;
            this.m_JAK2Exon1214Result = null;
            this.m_Comment = null;
            this.m_Interpretation = null;
            this.m_Method = null;
            base.ClearPreviousResults();
        }

        public override MethodResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            MethodResult result = new Rules.MethodResult();
            Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            Test.JAK2Exon1214.JAK2Exon1214Test jak2Exon1214Test = new JAK2Exon1214.JAK2Exon1214Test();
            if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
            {
                Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                if(jak2V617FTestOrder.Result != this.JAK2V617FResult)
                {
                    result.Success = false;
                    result.Message += "The " + jak2V617FTest.PanelSetName + " result does not match the selected result." + Environment.NewLine;
                }
            }
            if (accessionOrder.PanelSetOrderCollection.Exists(jak2Exon1214Test.PanelSetId) == true)
            {
                Test.JAK2Exon1214.JAK2Exon1214TestOrder jakExon1214TestOrder = (JAK2Exon1214.JAK2Exon1214TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2Exon1214Test.PanelSetId);
                if (jakExon1214TestOrder.Result != this.JAK2Exon1214Result)
                {
                    result.Success = false;
                    result.Message += "The " + jak2Exon1214Test.PanelSetName + " result does not match the selected result." + Environment.NewLine;
                }
            }

            if(result.Success == false) result.Message += "Are you sure you want to use the selected results?";

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult auditResult = base.IsOkToFinalize(accessionOrder);
            if (auditResult.Status == Audit.Model.AuditStatusEnum.OK)
            {
                /*if (string.IsNullOrEmpty(this.m_JAK2Exon1214Result) == true || string.IsNullOrEmpty(this.m_JAK2Exon1214Result) == true)
                {
                    auditResult.Status = Audit.Model.AuditStatusEnum.Failure;
                    auditResult.Message = "This case cannot be finalized because the results have not been set.";
                    return auditResult;
                }

                Business.Test.MPNStandardReflex.MPNStandardReflexTest mpnStandardReflexTest = new MPNStandardReflex.MPNStandardReflexTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(mpnStandardReflexTest.PanelSetId) == true)
                {
                    Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex panelSetOrderMPNStandardReflex = (Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mpnStandardReflexTest.PanelSetId);
                    if (panelSetOrderMPNStandardReflex.Final == true && panelSetOrderMPNStandardReflex.JAK2V617FResult != this.Result)
                    {
                        auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                        auditResult.Message = "The finaled " + panelSetOrderMPNStandardReflex.PanelSetName + " result (" + panelSetOrderMPNStandardReflex.JAK2V617FResult +
                            ") does not match this result (" + this.Result + ")." + Environment.NewLine + "Are you sure you want to use the selected results?";
                    }
                }*/
                Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
                Test.JAK2Exon1214.JAK2Exon1214Test jak2Exon1214Test = new JAK2Exon1214.JAK2Exon1214Test();
                if (accessionOrder.PanelSetOrderCollection.Exists(jak2V617FTest.PanelSetId) == true)
                {
                    Test.JAK2V617F.JAK2V617FTestOrder jak2V617FTestOrder = (JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
                    if (jak2V617FTestOrder.Result != this.JAK2V617FResult)
                    {
                        auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                        auditResult.Message += "The " + jak2V617FTest.PanelSetName + " result does not match this result." + Environment.NewLine;
                    }
                }
                if (accessionOrder.PanelSetOrderCollection.Exists(jak2Exon1214Test.PanelSetId) == true)
                {
                    Test.JAK2Exon1214.JAK2Exon1214TestOrder jakExon1214TestOrder = (JAK2Exon1214.JAK2Exon1214TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2Exon1214Test.PanelSetId);
                    if (jakExon1214TestOrder.Result != this.JAK2Exon1214Result)
                    {
                        auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                        auditResult.Message += "The " + jak2Exon1214Test.PanelSetName + " result does not match this result." + Environment.NewLine;
                    }
                }
            }

            if (auditResult.Status == Audit.Model.AuditStatusEnum.Warning) auditResult.Message += "Are you sure you want to finalize this report?";

            return auditResult;
        }
    }
}
