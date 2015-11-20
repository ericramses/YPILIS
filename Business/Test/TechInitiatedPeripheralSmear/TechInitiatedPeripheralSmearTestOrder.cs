using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
	[PersistentClass("tblTechInitiatedPeripheralSmearTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class TechInitiatedPeripheralSmearTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{

        private string m_TechComment;
        private string m_PathologistComment;

		public TechInitiatedPeripheralSmearTestOrder()
		{
            
		}

		/*public TechInitiatedPeripheralSmearTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute, systemIdentity)
		{
            this.Accept(systemIdentity.User);
            this.Finalize(systemIdentity.User);
		}*/

        public TechInitiatedPeripheralSmearTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            :base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
        {

        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return string.Empty;
		}

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToFinalize()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (this.Final == true)
            {
                result.Success = false;
                result.Message = "This case cannot be finalized because it is already finalized.";
            }
            return result;
        }

        [PersistentProperty()]
        public string TechComment
        {
            get { return this.m_TechComment; }
            set
            {
                if (this.m_TechComment != value)
                {
                    this.m_TechComment = value;
                    this.NotifyPropertyChanged("TechComment");
                }
            }
        }

        [PersistentProperty()]
        public string PathologistComment
        {
            get { return this.m_PathologistComment; }
            set
            {
                if (this.m_PathologistComment != value)
                {
                    this.m_PathologistComment = value;
                    this.NotifyPropertyChanged("PathologistComment");
                }
            }
        }
    }
}
