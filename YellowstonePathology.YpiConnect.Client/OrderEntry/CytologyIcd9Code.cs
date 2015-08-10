using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class CytologyIcd9Code
	{
		private string m_Icd9Code;
		private string m_Description;

		public CytologyIcd9Code()
		{
		}

		public CytologyIcd9Code(string icd9Code, string description)
		{
			this.m_Icd9Code = icd9Code;
			this.m_Description = description;
		}

		public string Icd9Code
		{
			get { return this.m_Icd9Code; }
		}

		public string Description
		{
			get { return this.m_Description; }
		}

		public string Display
		{
			get
			{
				string result = this.m_Description;
				if (string.IsNullOrEmpty(this.m_Icd9Code) == false)
				{
					result = this.m_Description + " (" + this.m_Icd9Code + ")";
				}
				return result;
			}
		}
	}
}
