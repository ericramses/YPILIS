using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace YellowstonePathology.Business.MessageQueues
{
	public class ExecuteCommandErrorQueue : CommandQueueBase
	{
		public ExecuteCommandErrorQueue() : base(@"TestSql\ExecuteCommandError", "Execute Error")
        {			
        
        }        
	}
}
