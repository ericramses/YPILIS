using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.JAK2Exon1214
{
    [PersistentClass("tblJAK2Exon1214TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class JAK2Exon1214TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private string m_Interpretation;
        private string m_Method;
        private string m_ASRComment;

        public JAK2Exon1214TestOrder()
        {

        }

        public JAK2Exon1214TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {

        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    this.NotifyPropertyChanged("ASRComment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            return result.ToString();
        }

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder panelSetOrder = (Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.ASRComment = this.m_ASRComment;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Interpretation = null;
            this.m_Method = null;
            this.m_ASRComment = null;
            base.ClearPreviousResults();
        }

        public override Rules.MethodResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Rules.MethodResult result = this.CheckResults(accessionOrder);

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
                Rules.MethodResult methodResult = this.CheckResults(accessionOrder);
                if (methodResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message = methodResult.Message + "Are you sure you want to accept the results?";
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult auditResult = base.IsOkToFinalize(accessionOrder);
            if (auditResult.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    auditResult.Status = Audit.Model.AuditStatusEnum.Failure;
                    auditResult.Message = "This case cannot be finalized because the results have not been set.";
                    return auditResult;
                }

                Rules.MethodResult methodResult = this.CheckResults(accessionOrder);
                if (methodResult.Success == false)
                {
                    auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                    auditResult.Message = methodResult.Message + "Are you sure you want to finalize this report?";
                }
            }
            return auditResult;
        }

        private Rules.MethodResult CheckResults(AccessionOrder accessionOrder)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            Business.Test.MPNStandardReflex.MPNStandardReflexTest mpnStandardReflexTest = new MPNStandardReflex.MPNStandardReflexTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(mpnStandardReflexTest.PanelSetId) == true)
            {
                Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex panelSetOrderMPNStandardReflex = (Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mpnStandardReflexTest.PanelSetId);
                if (panelSetOrderMPNStandardReflex.Final == true && panelSetOrderMPNStandardReflex.JAK2Exon1214Result != this.Result)
                {
                    result.Success = false;
                    result.Message = "The finaled " + panelSetOrderMPNStandardReflex.PanelSetName + " result (" + panelSetOrderMPNStandardReflex.JAK2Exon1214Result +
                        ") does not match this result (" + this.Result + ")." + Environment.NewLine;
                }
            }
            return result;
        }
    }
}
