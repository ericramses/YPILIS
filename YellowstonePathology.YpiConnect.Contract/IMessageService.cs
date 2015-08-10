using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Contract
{
	[ServiceContract]
	public interface IMessageService
    {
		[OperationContract]
		bool Ping();

		[OperationContract]
		void Send(Message message);                       
    }
}
