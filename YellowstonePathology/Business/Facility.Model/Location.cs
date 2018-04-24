using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Facility.Model
{
    [PersistentClass("tblLocation", "YPIDATA")]
    public class Location
    {
        private string m_LocationId;
        private string m_Description;
        private string m_FriendlyName;

        public Location()
        {
            
        }

        [PersistentPrimaryKeyProperty(false)]
        public string LocationId
        {
            get { return this.m_LocationId; }
            set { this.m_LocationId = value; }
        }        

        [PersistentProperty()]
		public string Description
        {
			get { return this.m_Description; }
			set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public string FriendlyName
        {
            get { return this.m_FriendlyName; }
            set { this.m_FriendlyName = value; }
        }
    }
}
