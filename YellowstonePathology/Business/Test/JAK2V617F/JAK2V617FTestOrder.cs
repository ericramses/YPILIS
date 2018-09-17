using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	[PersistentClass("tblJAK2V617FTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class JAK2V617FTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{

		private string m_Result;
		private string m_Interpretation;
		private string m_Comment;
		private string m_Method;
		private string m_Reference;
        private string m_Disclosure;

        public JAK2V617FTestOrder()
        {
            
        }

		public JAK2V617FTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Disclosure = "The performance characteristics of this test have been determined by NeoGenomics Laboratories.  This test has not " +
                "been approved by the FDA.  The FDA has determined such clearance or approval is not necessary.  This laboratory is CLIA certified " +
                "to perform high complexity clinical testing.";
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
            if(string.IsNullOrEmpty(this.m_Result) == true)
            {
                result.Success = false;
                result.Message = "A Result must be selected before the result can be set.";
            }
            else if(this.m_Accepted == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the results already have been accepted.";
			}
			return result;
		}

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
            if (result.Success == true)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Success = false;
                    result.Message = "The results cannot be accepted because the Result is not set.";
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
                }
            }
            return auditResult;
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Reference
		{
			get { return this.m_Reference; }
			set
			{
				if (this.m_Reference != value)
				{
					this.m_Reference = value;
					this.NotifyPropertyChanged("Reference");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Disclosure
        {
            get { return this.m_Disclosure; }
            set
            {
                if (this.m_Disclosure != value)
                {
                    this.m_Disclosure = value;
                    this.NotifyPropertyChanged("Disclosure");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Result: " + this.Result);
            result.AppendLine();

            result.AppendLine("Comment: " + this.m_Comment);
            result.AppendLine();
            
			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

            result.AppendLine("Method: " + this.m_Method);
            result.AppendLine();

            return result.ToString();
        }

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            Business.Test.JAK2V617F.JAK2V617FTestOrder panelSetOrder = (Business.Test.JAK2V617F.JAK2V617FTestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.Method = this.Method;
            panelSetOrder.Disclosure = this.Disclosure;
            panelSetOrder.Comment = this.Comment;
            panelSetOrder.Reference = this.Reference;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Interpretation = null;
            this.m_Method = null;
            this.m_Disclosure = null;
            this.m_Comment = null;
            this.m_Reference = null;
            base.ClearPreviousResults();
        }
    }
}
