using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace YellowstonePathology.Business.MessageQueues
{
	public class HistologyMessageQueue : CommandQueueBase
	{
		public HistologyMessageQueue() : base(@"TestSql\HistologyMessageCommand", "Histology Messages")
        {
			
        }
	}
}
