using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class MPLResultCollection : TestResultCollection
	{
        public MPLResultCollection()
		{
            this.Add(new MPLResultDetected());
            this.Add(new MPLResultNotDetected());                        
            this.Add(new TestResultNoResult());
		}               
	}
}
