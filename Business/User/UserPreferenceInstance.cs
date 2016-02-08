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

		private UserPreferenceInstance()
		{
            this.m_UserPreference = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetUserPreference();
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
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_UserPreference, false);
        }
	}
}
