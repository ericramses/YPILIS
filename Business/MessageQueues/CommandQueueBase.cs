using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace YellowstonePathology.Business.MessageQueues
{
	public class CommandQueueBase
	{
        protected string m_QueueName;
        protected string m_DisplayName;

        public CommandQueueBase(string queueName, string displayName)
        {
            this.m_QueueName = queueName;
            this.m_DisplayName = displayName;
        }

        public string QueueName
        {
            get { return this.m_QueueName; }
            set { this.m_QueueName = value; }
        }

        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set { this.m_DisplayName = value; }
        }

        public int GetCommandCount()
        {
            System.Messaging.MessageQueue messageQueue = new MessageQueue(this.m_QueueName);
            Message [] messages = messageQueue.GetAllMessages();
            return messages.Length;
        }

        public int GetRunnableCommandCount()
        {
            System.Messaging.MessageQueue messageQueue = new MessageQueue(this.m_QueueName);
            ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();

            Message[] messages = messageQueue.GetAllMessages();
            int runnableMessageCount = 0;
            foreach (System.Messaging.Message message in messages)
            {
                YellowstonePathology.Business.MessageQueues.MessageCommand messageCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)message.Body;
                if (messageCommand.RetryCount == 0)
                {
                    runnableMessageCount += 1;
                }
                else
                {
                    if (messageCommand.TimeMessageSent < DateTime.Now.AddMinutes(-1))
                    {
                        runnableMessageCount += 1;
                    }
                }
            }
            Console.WriteLine("Runnable Command Count: " + runnableMessageCount.ToString());
            return runnableMessageCount;
        }        

        public virtual YellowstonePathology.Business.MessageQueues.MessageCommand PeekAtFirstCommand()
        {
            YellowstonePathology.Business.MessageQueues.MessageCommand peekCommand;
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {                
                Cursor cursor = messageQueue.CreateCursor();
                TimeSpan timeSpan = new TimeSpan(0, 0, 1);

                ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();
               
                Message peekMessage = messageQueue.Peek(timeSpan, cursor, PeekAction.Current);
                peekCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)peekMessage.Body;
            }
            return peekCommand;
        }


        public virtual YellowstonePathology.Business.MessageQueues.MessageCommand ReceiveFirstCommand()
        {
            YellowstonePathology.Business.MessageQueues.MessageCommand command = null;
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                Message[] messages = messageQueue.GetAllMessages();
                if (messages.Length != 0)
                {
                    ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();
                    using (Message message = messageQueue.Receive())
                    {
                        command = (YellowstonePathology.Business.MessageQueues.MessageCommand)message.Body;
                    }
                }
            }
            return command;
        }
        public void RecieveCommand(YellowstonePathology.Business.MessageQueues.MessageCommand messageCommand)
        {
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                Cursor cursor = messageQueue.CreateCursor();
                PeekAction peekAction = PeekAction.Current;

                ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();
                TimeSpan timeSpan = new TimeSpan(0, 0, 1);

                try
                {
                    while (true)
                    {
                        Message peekedMessage = messageQueue.Peek(timeSpan, cursor, peekAction);
                        YellowstonePathology.Business.MessageQueues.MessageCommand peekedCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)peekedMessage.Body;

                        if (peekedCommand.MessageCommandId == messageCommand.MessageCommandId)
                        {
                            messageQueue.Receive(timeSpan, cursor);     
                       
                            //TODO: removed variable because it's not being used.
                            //System.Messaging.Message receiveMessage = messageQueue.Receive(timeSpan, cursor);                            
                            break;
                        }
                        else
                        {
                            peekAction = PeekAction.Next;
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(messageQueue.QueueName + " " + e.Message);
                }
            }
        }

        public void ExecuteCommand(YellowstonePathology.Business.MessageQueues.MessageCommand messageCommand, CommandQueueBase errorQueue)
        {
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                Cursor cursor = messageQueue.CreateCursor();
                PeekAction peekAction = PeekAction.Current;

                ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();
                TimeSpan timeSpan = new TimeSpan(0, 0, 1);

                try
                {
                    while (true)
                    {
                        Message peekedMessage = messageQueue.Peek(timeSpan, cursor, peekAction);
                        YellowstonePathology.Business.MessageQueues.MessageCommand peekedCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)peekedMessage.Body;

                        if (peekedCommand.MessageCommandId == messageCommand.MessageCommandId)
                        {
                            System.Messaging.Message receiveMessage = messageQueue.Receive(timeSpan, cursor);
                            YellowstonePathology.Business.MessageQueues.MessageCommand receiveCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)receiveMessage.Body;

                            Console.WriteLine("Executing Command: " + receiveCommand.Label);
                            receiveCommand.Execute();
                            if (receiveCommand.ErrorInExecution == true)
                            {
                                receiveCommand.RetryCount += 1;
                                errorQueue.SendCommand(receiveCommand);
                                Console.WriteLine("Error in Execution: " + receiveCommand.Label + " - " + receiveCommand.ErrorMessage + ", Retry Count: " + receiveCommand.RetryCount.ToString());
                            }
                            if (receiveCommand.ResubmitAfterExecution == true)
                            {
                                this.SendCommand(receiveCommand);
                                Console.WriteLine("Resubmitted After Execution: " + receiveCommand.Label);
                            }
                            break;
                        }
                        else
                        {
                            peekAction = PeekAction.Next;
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(messageQueue.QueueName + " " + e.Message);                    
                }    
            }        
        }

        public virtual void ExecuteCommands(CommandQueueBase errorQueue, TimeSpan executionDelay, TimeSpan lastExecutionLookback)
        {                        
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                Cursor cursor = messageQueue.CreateCursor();                

                ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();                                           
                TimeSpan timeSpan = new TimeSpan(0, 0, 1);

                List<YellowstonePathology.Business.MessageQueues.MessageCommand> commandsToResubmit = new List<MessageCommand>();

				try
                {
                    while (true)
                    {                        
                        //Message peekedMessage = messageQueue.Peek(timeSpan, cursor, peekAction);                        
                        //YellowstonePathology.Business.MessageQueues.MessageCommand peekedCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)peekedMessage.Body;
                        //if (peekedCommand.RetryCount == 0 || peekedCommand.TimeMessageSent < DateTime.Now.AddMinutes(-5))
                        //{
                            System.Messaging.Message receiveMessage = messageQueue.Receive(timeSpan, cursor);
                            YellowstonePathology.Business.MessageQueues.MessageCommand receiveCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)receiveMessage.Body;

							Console.WriteLine("Executing Command: " + receiveCommand.Label);
                            receiveCommand.Execute();                            
                            if (receiveCommand.ErrorInExecution == true)
                            {
                                receiveCommand.RetryCount += 1;
                                errorQueue.SendCommand(receiveCommand);                                
                                Console.WriteLine("Error in Execution: " + receiveCommand.Label + " - " + receiveCommand.ErrorMessage + ", Retry Count: " + receiveCommand.RetryCount.ToString() );
                            }
                            if (receiveCommand.ResubmitAfterExecution == true)
                            {
                                //this.SendCommand(receiveCommand);
                                commandsToResubmit.Add(receiveCommand);
                                Console.WriteLine("Resubmitted After Execution: " + receiveCommand.Label);
                            }
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Not Runnable: " + peekedCommand.Label);
                        //    peekAction = PeekAction.Next;
                        //    System.Threading.Thread.Sleep(executionDelay);
                        //}
                    }                    
                }
                catch (Exception)
                {
                    this.SubmitCommands(commandsToResubmit);
                    //System.Windows.MessageBox.Show(messageQueue.QueueName + " " + e.Message);                    
                }                                    
            }            
        }

        private void SubmitCommands(List<YellowstonePathology.Business.MessageQueues.MessageCommand> messageCommands)
        {
            foreach (YellowstonePathology.Business.MessageQueues.MessageCommand messageCommand in messageCommands)
            {
                this.SendCommand(messageCommand);
            }
        }

		public virtual void ExecuteCommands(List<MessageCommand> commands, object[] valuesToSet)
		{
			using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
			{
				((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();

				Cursor cursor = messageQueue.CreateCursor();
				PeekAction peekAction = PeekAction.Current;
				TimeSpan timeSpan = new TimeSpan(0, 0, 1);
				try
				{
					while (true)
					{
						Message peekedMessage = messageQueue.Peek(timeSpan, cursor, peekAction);
						YellowstonePathology.Business.MessageQueues.MessageCommand peekedCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)peekedMessage.Body;
						bool received = false;
						foreach (MessageCommand command in commands)
						{
							if (command.MessageCommandId == peekedCommand.MessageCommandId)
							{
								System.Messaging.Message receiveMessage = messageQueue.Receive(timeSpan, cursor);
								YellowstonePathology.Business.MessageQueues.MessageCommand receiveCommand = (YellowstonePathology.Business.MessageQueues.MessageCommand)receiveMessage.Body;
								receiveCommand.SetExecutionData(valuesToSet);
								receiveCommand.Execute();
								received = true;
								break;
							}
						}
						if (!received)
						{
							peekAction = PeekAction.Next;
						}
					}
				}
				catch(Exception ex)
				{
					string s = ex.Message;
				}
			}
		}        

        public virtual void SendCommand(YellowstonePathology.Business.MessageQueues.MessageCommand command)
        {
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                using (Message message = new Message())
                {
                    command.TimeMessageSent = DateTime.Now;
                    message.Recoverable = true;                    
                    message.Label = command.Label;
                    message.Body = command;
					message.Recoverable = true;
                    messageQueue.Send(message);
                }
            }
        }

		public virtual void SendCommand(YellowstonePathology.Business.MessageQueues.MessageCommand command, MessagePriority priority)
		{
			using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
			{
				using (Message message = new Message())
				{
					message.Recoverable = true;
					message.Label = command.Label;
					message.Body = command;
					message.Recoverable = true;
					message.Priority = priority;
					messageQueue.Send(message);
				}
			}
		}

        public virtual void SendMessage(string messageLabel, string messageText)
        {
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                using (Message message = new Message())
                {
                    message.Recoverable = true;                    
                    message.Label =messageLabel;
                    message.Body = messageText;
					message.Recoverable = true;
                    messageQueue.Send(message);
				}
            }
        }

        public virtual void Purge()
        {            
            using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
            {
                messageQueue.Purge();                
            }            
        }

		public List<YellowstonePathology.Business.MessageQueues.MessageCommand> Commands
		{
			get
			{
				List<YellowstonePathology.Business.MessageQueues.MessageCommand> commands = new List<MessageCommand>();				

				using (MessageQueue messageQueue = new MessageQueue(this.m_QueueName))
				{
                    ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = CommandQueueBase.GetTargetTypes();
					Message[] messages = messageQueue.GetAllMessages();
					foreach(Message message in messages)
					{
						string s = message.Body.ToString();
						MessageCommand command = (MessageCommand)message.Body;
						commands.Add(command);
					}
				}
				return commands;
			}
		}

        public static Type[] GetTargetTypes()
        {
            Type[] targetTypes = 
            { 
                typeof(string),
				typeof(YellowstonePathology.Business.MessageQueues.OrderToAcknowledgeCommand),
                typeof(YellowstonePathology.Business.MessageQueues.CytologyScreeningAssignmentCommand),				
                typeof(YellowstonePathology.Business.MessageQueues.DailyAMScheduleCommand),                
                typeof(YellowstonePathology.Business.MessageQueues.CytologySlideDisposalCommand),
                typeof(YellowstonePathology.Business.MessageQueues.TestRequestedCommand),
                typeof(YellowstonePathology.Business.MessageQueues.POCRetensionCommand),
				typeof(YellowstonePathology.Business.MessageQueues.SpecimenDisposalCommand),
				typeof(YellowstonePathology.Business.MessageQueues.SurgicalSpecimenDisposalCommand)
            };
            return targetTypes;
        }
	}
}
