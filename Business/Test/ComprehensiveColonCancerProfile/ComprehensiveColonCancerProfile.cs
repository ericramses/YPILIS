using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	[PersistentClass("tblComprehensiveColonCancerProfile", "tblPanelSetOrder", "YPIDATA")]
	public class ComprehensiveColonCancerProfile : PanelSetOrder
	{
        private string m_Interpretation;
        private bool m_IncludeTestsPerformedOnOtherBlocks;

		public ComprehensiveColonCancerProfile() 
        {
            
        }

		public ComprehensiveColonCancerProfile(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{
            
		}        

        [PersistentProperty()]
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
        public bool IncludeTestsPerformedOnOtherBlocks
        {
            get { return this.m_IncludeTestsPerformedOnOtherBlocks; }
            set
            {
                if (this.m_IncludeTestsPerformedOnOtherBlocks != value)
                {
                    this.m_IncludeTestsPerformedOnOtherBlocks = value;
                    this.NotifyPropertyChanged("IncludeTestsPerformedOnOtherBlocks");
                }
            }
        }    

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return "Interpretation: " + this.m_Interpretation;
		}
	}
}
