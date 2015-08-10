using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels; 
using System.Security.Principal;

namespace YellowstonePathology.YpiConnect.Service.Log
{
    public class ClientServicesLogService : YellowstonePathology.YpiConnect.Contract.Log.IClientServicesLogService
    {
        public string GetCallingUser()
        {            
            WindowsIdentity windowsIdentity = (WindowsIdentity)ServiceSecurityContext.Current.PrimaryIdentity;            
            return windowsIdentity.Name;
        }

        public void LogEvent(int eventId, string details)
        {
            OperationContext context = OperationContext.Current; 
            MessageProperties prop = context.IncomingMessageProperties; 
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty; 
            string ipAddress = endpoint.Address; 

            YellowstonePathology.YpiConnect.Contract.Log.ClientServicesLog clientServicesLog = YellowstonePathology.YpiConnect.Service.Log.ClientServicesLogFactory.GetClientServicesLog(eventId);
            clientServicesLog.Details = details;
            clientServicesLog.IpAddress = ipAddress;
            YellowstonePathology.YpiConnect.Service.Log.ClientServicesLogGateway.Insert(clientServicesLog);   
        }
    }
}
