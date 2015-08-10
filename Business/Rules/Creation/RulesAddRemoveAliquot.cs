using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Internal;
using YellowstonePathology.Business.DataAccess;

namespace YellowstonePathology.Business.Rules.Creation
{
	/*public class RulesAddRemoveAliquot : BaseRules
	{
		private static RulesAddRemoveAliquot m_Instance;        

		private YellowstonePathology.Business.Test.PanelSetOrderItem m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.SpecimenOrderItem m_SpecimenOrderItem;		

		private RulesAddRemoveAliquot()
			: base(typeof(YellowstonePathology.Business.Rules.Creation.RulesAddRemoveAliquot))
		{

        }

		public static RulesAddRemoveAliquot Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new RulesAddRemoveAliquot();
				}
				return m_Instance;
			}
		}

		public YellowstonePathology.Business.Test.PanelSetOrderItem PanelSetOrderItem
		{
			get { return this.m_PanelSetOrderItem; }
			set	{ this.m_PanelSetOrderItem = value; }
		}

		public YellowstonePathology.Business.Test.SpecimenOrderItem SpecimenOrderItem
		{
			get { return this.m_SpecimenOrderItem; }
			set	{ this.m_SpecimenOrderItem = value;	}
		}

		public void AddAliquotsToRequested()
		{
			int difference = this.Difference();
            
            int currSeq = m_SpecimenOrderItem.AliquotOrderItemCollection.Count + 1;

			while (difference > 0)
			{
                YellowstonePathology.Business.Tools.ObjectTool.InsertAliquotOrder(m_PanelSetOrderItem.PanelSetId, m_SpecimenOrderItem.SpecimenOrderId, currSeq);
                difference--;			
                currSeq++;
            }
		}

		public void RemoveAliquotsToRequested()
		{
			int difference = this.Difference();
			int offset = m_SpecimenOrderItem.AliquotOrderItemCollection.Count - 1;            

			while (difference < 0)
			{
				foreach (YellowstonePathology.Business.Test.TestOrderListItem testOrderListItem in m_SpecimenOrderItem.AliquotOrderItemCollection[offset].TestOrderItemList)
				{					
					YellowstonePathology.Business.Interface.ITestOrder testOrderItem = (from po in m_PanelSetOrderItem.PanelOrderItemCollection
																					  from ts in ((YellowstonePathology.Business.Test.PanelOrderItem)po).TestOrderItemCollection
																					  where ts.TestOrderId == testOrderListItem.TestOrderId
																						select ts).Single<YellowstonePathology.Business.Interface.ITestOrder>();		
					if (((YellowstonePathology.Business.Test.TestOrderItem)testOrderItem).AliquotOrderItemList.Count > 0)
					{
						YellowstonePathology.Business.Tools.ObjectTool.DeleteTestOrder(testOrderListItem.TestOrderId, m_SpecimenOrderItem.AliquotOrderItemCollection[offset].AliquotOrderId);
					}
				}				
				YellowstonePathology.Business.Tools.ObjectTool.DeleteAliquotOrder(m_SpecimenOrderItem.AliquotOrderItemCollection[offset].AliquotOrderId);
				offset--;
				difference++;
			}
		}

        public int Difference()
        {            
            int result = m_SpecimenOrderItem.AliquotRequestCount - m_SpecimenOrderItem.AliquotOrderItemCollection.Count;
            return result;           
        }

	}*/
}
