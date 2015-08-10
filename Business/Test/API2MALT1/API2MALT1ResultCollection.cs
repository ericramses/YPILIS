using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.API2MALT1
{
	public class API2MALT1ResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public API2MALT1ResultCollection()
		{
			this.Add(new API2MALT1NegativeResult());
			this.Add(new API2MALT1PositiveResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
