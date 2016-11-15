using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.User
{
	[XmlType("SystemUser")]
	[PersistentClass("tblSystemUser", "YPIDATA")]
	public class SystemUser : INotifyPropertyChanged
	{
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		int m_UserId;
		string m_FirstName;
		string m_LastName;
        string m_MiddleInitial;
		string m_DisplayName;
		string m_UserName;
		bool m_Active;
		private string m_Signature;
		string m_Initials;
        string m_NationalProviderId;
        string m_EmailAddress;        

		SystemUserRoleCollection m_SystemUserRoleCollection;

		public SystemUser()
		{
			m_SystemUserRoleCollection = new SystemUserRoleCollection();
		}

		[PersistentCollection()]
		public SystemUserRoleCollection SystemUserRoleCollection
		{
			get { return this.m_SystemUserRoleCollection; }
			set { this.m_SystemUserRoleCollection = value; }
		}

		[PersistentPrimaryKeyProperty(true)]
		[PersistentDataColumnProperty(false, "11", "null", "int")]
		public int UserId
		{
			get { return this.m_UserId; }
			set
			{
				if (this.m_UserId != value)
				{
					this.m_UserId = value;
					this.NotifyPropertyChanged("UserId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string FirstName
		{
			get { return this.m_FirstName; }
			set
			{
				if (this.m_FirstName != value)
				{
					this.m_FirstName = value;
					this.NotifyPropertyChanged("FirstName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string LastName
		{
			get { return this.m_LastName; }
			set
			{
				if (this.m_LastName != value)
				{
					this.m_LastName = value;
					this.NotifyPropertyChanged("LastName");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MiddleInitial
        {
            get { return this.m_MiddleInitial; }
            set
            {
                if (this.m_MiddleInitial != value)
                {
                    this.m_MiddleInitial = value;
                    this.NotifyPropertyChanged("MiddleInitial");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "varchar")]
		public string DisplayName
		{
			get { return this.m_DisplayName; }
			set
			{
				if (this.m_DisplayName != value)
				{
					this.m_DisplayName = value;
					this.NotifyPropertyChanged("DisplayName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string UserName
		{
			get { return this.m_UserName; }
			set
			{
				if (this.m_UserName != value)
				{
					this.m_UserName = value;
					this.NotifyPropertyChanged("UserName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "1", "tinyint")]
		public bool Active
		{
			get { return this.m_Active; }
			set
			{
				if (this.m_Active != value)
				{
					this.m_Active = value;
					this.NotifyPropertyChanged("Active");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string Signature
		{
			get { return this.m_Signature; }
			set
			{
				if (this.m_Signature != value)
				{
					this.m_Signature = value;
					this.NotifyPropertyChanged("Signature");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string Initials
		{
			get { return this.m_Initials; }
			set
			{
				if (this.m_Initials != value)
				{
					this.m_Initials = value;
					this.NotifyPropertyChanged("Initials");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string NationalProviderId
        {
            get { return this.m_NationalProviderId; }
            set
            {
                if (this.m_NationalProviderId != value)
                {
                    this.m_NationalProviderId = value;
                    this.NotifyPropertyChanged("NationalProviderId");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string EmailAddress
        {
            get { return this.m_EmailAddress; }
            set
            {
                if (this.m_EmailAddress != value)
                {
                    this.m_EmailAddress = value;
                    this.NotifyPropertyChanged("EmailAddress");
                }
            }
        }

		public bool IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum systemRole)
		{
			return SystemUserRoleCollection.IsUserInRole(systemRole);
		}

		public bool IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionList systemUserRoleDescriptionList)
		{
			return SystemUserRoleCollection.IsUserInRole(systemUserRoleDescriptionList);
		}

        public string GetWPHMneumonic()
        {
            string result = null;
            result = this.m_LastName.Substring(0, 3) + this.m_FirstName.Substring(0, 2);
            return result.ToUpper();
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
