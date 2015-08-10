using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Deployment.Internal;

namespace YellowstonePathology.Business.Rules.Creation
{
	public class RulesDetermineAliquotType : BaseRules
	{
		private static RulesDetermineAliquotType m_Instance;
		private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private List<int> m_TestIdList;
		private string m_AliquotType;
		private string m_AliquotDescription;
		private string m_LabelPrefix;

		private Test.SpecimenOrder m_SpecimenOrder;


		private RulesDetermineAliquotType()
			: base(typeof(YellowstonePathology.Business.Rules.Creation.RulesDetermineAliquotType))
		{ }

		public static RulesDetermineAliquotType Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new RulesDetermineAliquotType();
				}
				return m_Instance;
			}
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
		{
			get { return this.m_AliquotOrder; }
			set
			{
				this.m_AliquotOrder = value;				
			}
		}

		private void GetSpecimenOrderItem()
		{
			this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetCurrent(this.m_AliquotOrder.SpecimenOrderId);			
		}

		private void DetermineAliquotType()
		{
			this.m_AliquotType = string.Empty;
			YellowstonePathology.Domain.Test.Model.TestCollection testCollection = YellowstonePathology.Domain.Test.Model.TestCollection.GetAllTests();
			foreach (YellowstonePathology.Domain.Test.Model.TestOrderListItem testOrderListItem in m_AliquotOrder.TestOrderItemList)
			{
				if (testOrderListItem.TestId == 44)
				{
					continue;
				}

				YellowstonePathology.Domain.Test.Model.Test test = testCollection.GetTest(testOrderListItem.TestId);
				m_AliquotType = test.AliquotType;
				break;
			}
		}

		private void ListTestsOrdered()
		{
			m_TestIdList = (from t in m_AliquotOrder.TestOrderItemList
							from pso in this.m_AccessionOrder.PanelSetOrderCollection
							from po in pso.PanelOrderCollection
							from to in ((YellowstonePathology.Business.Test.PanelOrder)po).TestOrderCollection
							where to.TestOrderId == t.TestOrderId
							select to.TestId).ToList<int>();
		}

		private void DetermineLabelPrefixFromTest()
		{
			this.m_LabelPrefix = string.Empty;
			if (m_TestIdList.Contains(45))
			{
				m_LabelPrefix = "FS";
				m_AliquotType = "FrozenBlock";
			}
			//195	Cell block
			else if (m_TestIdList.Contains(195))
			{
				m_AliquotType = "CellBlock";
				m_LabelPrefix = "CB";
			}
		}

		private void DetermineAliquotDescriptionFromTest()
		{
			this.m_AliquotDescription = string.Empty;
			//47 Tissue Smear; 131 Oil Red O; 196 Touch Prep; 204 Fine Needle Aspirate; 205 Wrights Stain; 206 Non-Gyn Cytology; 207 Wet Mount; 219 also Non-Gyn Cytology
			if (m_TestIdList.Contains(47) || m_TestIdList.Contains(131) || m_TestIdList.Contains(196) || m_TestIdList.Contains(204) ||
					m_TestIdList.Contains(205) || m_TestIdList.Contains(206) || m_TestIdList.Contains(207) || m_TestIdList.Contains(219))
			{				
				m_AliquotDescription = this.m_SpecimenOrder.Description;
			}
			//48 Gross Only; 194 Intraoperative Consultation
			else if (m_TestIdList.Contains(48) || m_TestIdList.Contains(194))
			{				
				m_AliquotDescription = this.m_SpecimenOrder.Description;
			}
		}

		private void SetData()
		{
			if (m_AliquotType == string.Empty && m_AliquotDescription == string.Empty)
			{
				GetSpecimenInfo();
			}
			this.m_AliquotOrder.AliquotType = m_AliquotType;
			this.m_AliquotOrder.Description = m_AliquotDescription;
			this.m_AliquotOrder.LabelPrefix = m_LabelPrefix;
			//this.m_AccessionOrderGateway.SubmitItemChanges(this.m_AliquotOrder);
		}

		private void GetSpecimenInfo()
		{			
			if (this.m_SpecimenOrder == null)
			{
				m_AliquotDescription = "Aliquot";
				return;
			}
		
			for (int idx = 0; idx < this.m_SpecimenOrder.AliquotOrderCollection.Count; idx++)
			{				
				if (this.m_AliquotOrder.AliquotOrderId == this.m_SpecimenOrder.AliquotOrderCollection[idx].AliquotOrderId)
				{
					m_AliquotDescription = "Aliquot " + (idx + 1).ToString();
					return;
				}
			}
		}
	}
}
