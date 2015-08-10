using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderTechnicalOnly : PanelSetOrder
	{
		public PanelSetOrderTechnicalOnly()
		{
            
		}

		public PanelSetOrderTechnicalOnly(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute, systemIdentity)
		{
			
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return string.Empty;
		}
	}
}
