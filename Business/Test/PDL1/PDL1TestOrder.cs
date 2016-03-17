using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.PDL1
{
    [PersistentClass("tblPDL1TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class PDL1TestOrder : PanelSetOrder
    {
        private string m_Result;
        private string m_StainPercent;
        private string m_Method;
        private string m_Comment;

        public PDL1TestOrder()
        {
        	
        }
        
        public PDL1TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {

        }

        [PersistentProperty()]
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
        public string StainPercent
        {
            get { return this.m_StainPercent; }
            set
            {
                if (this.m_StainPercent != value)
                {
                    this.m_StainPercent = value;
                    this.NotifyPropertyChanged("StainPercent");
                }
            }
        }

        [PersistentProperty()]
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

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Stain Percent: " + this.m_StainPercent);
            result.AppendLine();

            return result.ToString();
        }

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if(string.IsNullOrEmpty(this.StainPercent) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the stain percent is not set.";
				}
				else if(string.IsNullOrEmpty(this.m_Result) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the result is not set.";
				}
			}
			return result;
		}
    }
}
