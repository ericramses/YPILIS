using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Billing.Model;

namespace YellowstonePathology.Business.Test.Model
{
    public class DualStain : ImmunoHistochemistryTest
    {        
		protected Test m_FirstTest;
		protected Test m_SecondTest;
		protected string m_DepricatedFirstTestId;
		protected string m_DepricatedSecondTestId;        

        public DualStain()
        {
            
        }        

		public Test FirstTest
        {
            get { return this.m_FirstTest; }            
        }

		public Test SecondTest
        {
            get { return this.m_SecondTest; }
        }        

		public string DepricatedFirstTestId
		{
			get { return this.m_DepricatedFirstTestId; }
		}

		public string DepricatedSecondTestId
		{
			get { return this.m_DepricatedSecondTestId; }
		}

        public override CptCode GetGradedCptCode(bool isTechnicalOnly)
        {
            return Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88344", null);
        }

        public YellowstonePathology.Business.Test.Model.TestOrder GetTestOrder(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection, int stainNumber)
        {
            YellowstonePathology.Business.Test.Model.TestOrder result = null;
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in testOrderCollection)
            {
                if (stainNumber == 1)
                {
                    if (testOrder.TestId == this.m_FirstTest.TestId || testOrder.TestId == this.m_DepricatedFirstTestId)
                    {
                        result = testOrder;
                    }
                }
                else if (stainNumber == 2)
                {
                    if (testOrder.TestId == this.m_SecondTest.TestId || testOrder.TestId == this.m_DepricatedSecondTestId)
                    {
                        result = testOrder;
                    }
                }
            }
            return result;
        }

		public static int CompareByTestName(DualStain x, DualStain y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				else
				{
					return -1;
				}
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				else
				{
					int retval = x.m_TestName.CompareTo(y.m_TestName);
					if (retval != 0)
					{
						return retval;
					}
					else
					{
						return x.m_TestName.CompareTo(y.m_TestName);
					}
				}
			}
		}
	}
}
