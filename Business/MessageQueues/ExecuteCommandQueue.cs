using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace YellowstonePathology.Business.MessageQueues
{
	public class ExecuteCommandQueue : CommandQueueBase
	{
		public ExecuteCommandQueue() : base(@"TestSql\ExecuteCommand", "Execute")
        {
			
        }        
	}
}
