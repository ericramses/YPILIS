using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.TechnicalOnly
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class TechnicalOnlyTestOrder : PanelSetOrder
	{
		public TechnicalOnlyTestOrder()
		{
            
		}

		public TechnicalOnlyTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
