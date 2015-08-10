using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Deployment.Internal;

namespace YellowstonePathology.Business.Rules.Creation
{
	public class RuleCheckTestOrderConflict : BaseRules
	{
		private static RuleCheckTestOrderConflict m_Instance;
		private List<YellowstonePathology.Domain.Test.Model.Test> m_OrderTestList;
		private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
		private string m_AliquotTypeFromFirstTest;
		private string m_AliquotTypeFromOtherTest;
		private string m_AliquotType;

		private RuleCheckTestOrderConflict()
			: base(typeof(YellowstonePathology.Business.Rules.Creation.RuleCheckTestOrderConflict))
		{ }

		public static RuleCheckTestOrderConflict Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new RuleCheckTestOrderConflict();
				}
				return m_Instance;
			}
		}

		public List<YellowstonePathology.Domain.Test.Model.Test> OrderTestList
		{
			get { return this.m_OrderTestList; }
			set { this.m_OrderTestList = value; }
		}

		public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
		{
			get { return this.m_AliquotOrder; }
			set
			{
				this.m_AliquotOrder = value;
				m_AliquotTypeFromFirstTest = string.Empty;
				m_AliquotTypeFromOtherTest = string.Empty;
				m_AliquotType = "Normal";
			}
		}

		private bool AliquotHasType()
		{
			m_AliquotType = m_AliquotOrder.AliquotType;
			return m_AliquotType == string.Empty || m_AliquotType == "Normal" ? false : true;
		}

		private bool NewTestsConflictWithEachOther()
		{
			foreach (YellowstonePathology.Domain.Test.Model.Test test in m_OrderTestList)
			{
				if (test.TestId == 44)
				{
					continue;
				}
				if (m_AliquotTypeFromFirstTest == string.Empty)
				{
					m_AliquotTypeFromFirstTest = test.AliquotType;
				}
				else if (test.AliquotType != m_AliquotTypeFromFirstTest)
				{
					m_AliquotTypeFromOtherTest = test.AliquotType;
					return true;
				}
			}           
			return false;
		}

		private bool NewTestsConflictAliquotType()
		{
            /* Removed by SEH on 11/23/09
			if (m_AliquotType != string.Empty && m_AliquotType != "Normal")
			{
				if (m_AliquotTypeFromFirstTest != m_AliquotType)
				{
					return true;
				}
			}
            */
			return false;
		}
	}
}
