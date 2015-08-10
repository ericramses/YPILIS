using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Service.Log
{
    public class ClientServicesLogFactory
    {
        public static YellowstonePathology.YpiConnect.Contract.Log.ClientServicesLog GetClientServicesLog(int eventId)
        {
            WindowsIdentity windowsIdentity = (WindowsIdentity)ServiceSecurityContext.Current.PrimaryIdentity;
            YellowstonePathology.YpiConnect.Contract.Log.ClientServicesLog clientServicesLog = new Contract.Log.ClientServicesLog();
            clientServicesLog.LoggedBy = windowsIdentity.Name;

            switch (eventId)
            {
                case 1055:                    
                    clientServicesLog.EventId = 1055;
                    clientServicesLog.Description = "Client Lookup was attempted.";                    
                    break;
                case 1060:
                    clientServicesLog.EventId = 1060;
                    clientServicesLog.Description = "Client authenticated.";
                    break;
                case 1061:
                    clientServicesLog.EventId = 1061;
                    clientServicesLog.Description = "Client authenticated failed at web service.";
                    break;
                case 1062:
                    clientServicesLog.EventId = 1062;
                    clientServicesLog.Description = "Maximum sign in attempt threshold achieved.";
                    break;
            }            
            return clientServicesLog;
        }
    }
}
