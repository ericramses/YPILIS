using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ClientWebServices.Domain
{
    [Table(Name="tblClient")]
    public class Client 
	{        
        int m_ClientId;
        string m_ClientName;        

        public Client()
        {

        }

        [Column(Name="ClientId", Storage="m_ClientId", IsPrimaryKey=true)]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        [Column(Name="ClientName", Storage ="m_ClientName")]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }        
	}
}
