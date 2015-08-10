using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.MessageQueues
{
	public class CommandQueues : ObservableCollection<YellowstonePathology.Business.MessageQueues.CommandQueueBase>
	{        
        public CommandQueues()
        {
            YellowstonePathology.Business.MessageQueues.ExecuteCommandQueue executeCommandQueue = new ExecuteCommandQueue();
            YellowstonePathology.Business.MessageQueues.ExecuteCommandErrorQueue executeCommandErrorQueue = new ExecuteCommandErrorQueue();

            this.Add(executeCommandQueue);
            this.Add(executeCommandErrorQueue);
        }        
    }
}
