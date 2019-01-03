using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.OrderAssociation
{
    [PersistentClass("tblOrderAssociationTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class OrderAssociationTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Account;
        private string m_MedicalRecord;

        public OrderAssociationTestOrder()
        { }

        public OrderAssociationTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_NoCharge = true;
        }

        [PersistentProperty()]
        public string Account
        {
            get { return this.m_Account; }
            set { this.m_Account = value; }
        }

        [PersistentProperty()]
        public string MedicalRecord
        {
            get { return this.m_MedicalRecord; }
            set { this.m_MedicalRecord = value; }
        }

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
        {
            return "No results for Order Association.";
        }

    }
}
