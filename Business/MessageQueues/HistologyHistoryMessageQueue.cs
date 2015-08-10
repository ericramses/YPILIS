using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace YellowstonePathology.Business.MessageQueues
{
	public class HistologyHistoryMessageQueue : CommandQueueBase
	{
		public HistologyHistoryMessageQueue() : base(@"TestSql\HistologyHistoryMessageCommand", "Histology History Messages")
        {
			
        }
	}
}
