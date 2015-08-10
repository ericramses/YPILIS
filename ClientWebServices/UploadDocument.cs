using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ClientWebServices
{
    [DataContract]    
    public class UploadDocument
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] FileStream { get; set; }

    }
}