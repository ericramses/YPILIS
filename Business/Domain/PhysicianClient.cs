using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
    [PersistentClass("tblPhysicianClient", "YPIDATA")]
	public partial class PhysicianClient
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
        private int m_PhysicianClientId;
        private int m_PhysicianId;
        private int m_ClientId;		

		public PhysicianClient()
		{

		}

		public PhysicianClient(string objectId, int physicianId, int clientId)
		{
			this.m_ObjectId = objectId;
			this.m_PhysicianId = physicianId;
			this.m_ClientId = clientId;
		}

		[PersistentDocumentIdProperty()]
		public String ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

        [PersistentPrimaryKeyProperty(true)]
        public int PhysicianClientId
        {
            get { return this.m_PhysicianClientId; }
            set
            {
                if (this.m_PhysicianClientId != value)
                {
                    this.m_PhysicianClientId = value;
                    this.NotifyPropertyChanged("PhysicianClientId");                    
                }
            }
        }

        [PersistentProperty()]
        public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set
            {
                if (this.m_PhysicianId != value)
                {
                    this.m_PhysicianId = value;
                    this.NotifyPropertyChanged("PhysicianId");                    
                }
            }
        }

        [PersistentProperty()]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");                    
                }
            }
        }

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_PhysicianClientId = propertyWriter.WriteInt("PhysicianClientId");
            this.m_PhysicianId = propertyWriter.WriteInt("PhysicianId");
            this.m_ClientId = propertyWriter.WriteInt("ClientId");
			this.m_ObjectId = propertyWriter.WriteString("ObjectId");
		}

        public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
        {
            propertyReader.ReadInt("PhysicianClientId", PhysicianClientId);
            propertyReader.ReadInt("PhysicianId", PhysicianId);
            propertyReader.ReadInt("ClientId", ClientId);
			propertyReader.ReadString("ObjectId", ObjectId);
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }   
	}
}
