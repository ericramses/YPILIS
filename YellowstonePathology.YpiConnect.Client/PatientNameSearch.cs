using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class PatientNameSearch
    {
		string m_PatientNameSearchString;

		public PatientNameSearch()
        {

        }

		public string PatientNameSearchString
		{
			get { return this.m_PatientNameSearchString; }
			set { this.m_PatientNameSearchString = value; }
		}

		public string FirstNameSearchString
		{
			get
			{
				string result = string.Empty;
				if (string.IsNullOrEmpty(this.PatientNameSearchString) == false)
				{
					string[] commaSplit = this.m_PatientNameSearchString.Split(',');
					if (commaSplit.Length == 2)
					{
						result = commaSplit[1].Trim().ToUpper();
					}
				}
				return result;
			}
		}

		public string LastNameSearchString
		{
			get
			{
				string result = string.Empty;
				if (string.IsNullOrEmpty(this.PatientNameSearchString) == false)
				{
					string[] commaSplit = this.m_PatientNameSearchString.Split(',');
					if (commaSplit.Length == 2)
					{
						result = commaSplit[0].Trim().ToUpper();
					}
					else
					{
						result = this.PatientNameSearchString;
					}
				}
				return result;
			}
		}
	}
}
