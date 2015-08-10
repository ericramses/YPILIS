using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
	public class NameSearchEventArgs : EventArgs
	{
		private PatientNameSearch m_PatientNameSearch;

		public NameSearchEventArgs()
		{
		}

		public NameSearchEventArgs(PatientNameSearch patientNameSearch)
		{
			this.m_PatientNameSearch = patientNameSearch;
		}

		public PatientNameSearch PatientNameSearch
		{
			get { return this.m_PatientNameSearch; }
			set { this.m_PatientNameSearch = value; }
		}
	}
}
