using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class SurgicalOrderType
	{
		private string m_Display;
		private bool m_Value;

		public SurgicalOrderType(string display, bool value)
		{
			this.m_Display = display;
			this.m_Value = value;
		}

		public string Display
		{
			get { return this.m_Display; }
		}

		public bool Value
		{
			get { return this.m_Value; }
		}
	}
}
