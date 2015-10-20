using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class TechInitiatedPeripheralSmearTestOrder : PanelSetOrder
	{
		public TechInitiatedPeripheralSmearTestOrder()
		{
            
		}

		public TechInitiatedPeripheralSmearTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute, systemIdentity)
		{
            this.Accept(systemIdentity.User);
            this.Finalize(systemIdentity.User);
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return string.Empty;
		}
	}
}
