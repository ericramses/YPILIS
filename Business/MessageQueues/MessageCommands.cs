using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Messaging;

namespace YellowstonePathology.Business.MessageQueues
{
	public class MessageCommands : ObservableCollection<YellowstonePathology.Business.MessageQueues.MessageCommand>
	{        
        public MessageCommands(string messageQueueName)
        {
            System.Messaging.MessageQueue messageQueue = new MessageQueue(messageQueueName);
            ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes(); 

            Message[] messages = messageQueue.GetAllMessages();            
            foreach (System.Messaging.Message message in messages)
            {
                Console.WriteLine("Adding to Message Comamnd List: " + message.Label);
                
                YellowstonePathology.Business.MessageQueues.MessageCommand messageCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)message.Body;
                this.Add(messageCommand);                
            }            
        }        
    }
}
