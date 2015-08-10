using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Log
{    
    [ServiceContract]    
    public interface IClientServicesLogService
    {
        [OperationContract]
        string GetCallingUser();

        [OperationContract]
        void LogEvent(int eventId, string details);        
    }
}
