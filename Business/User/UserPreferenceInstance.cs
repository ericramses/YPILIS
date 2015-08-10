using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.User
{
	public class UserPreferenceInstance
	{
		private static UserPreferenceInstance instance;
		private UserPreference m_UserPreference;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		private UserPreferenceInstance()
		{
            this.m_ObjectTracker = new Persistence.ObjectTracker();
			this.m_UserPreference = YellowstonePathology.Business.User.SystemUserGateway.GetUserPreference();            
            this.m_ObjectTracker.RegisterObject(this.m_UserPreference);
		}

		public static UserPreferenceInstance Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new UserPreferenceInstance();
				}
				return instance;
			}
		}

		public UserPreference UserPreference
		{
			get { return m_UserPreference; }
		}

        public void SubmitChanges()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_UserPreference);
        }
	}
}
