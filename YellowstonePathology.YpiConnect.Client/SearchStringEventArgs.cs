using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
	public class SearchStringEventArgs : EventArgs
	{
		private string m_SearchString;

		public SearchStringEventArgs()
		{
		}

		public SearchStringEventArgs(string searchString)
		{
			this.m_SearchString = searchString;
		}

		public string SearchString
		{
			get { return this.m_SearchString; }
			set { this.m_SearchString = value; }
		}
	}
}
