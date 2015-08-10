using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.YpiConnect.Client
{
	public class ApplicationNavigator
	{
		private static Frame m_ApplicationContentFrame;

		public static Frame ApplicationContentFrame
		{
			get { return m_ApplicationContentFrame; }
			set
			{
				if (m_ApplicationContentFrame == null)
				{
					m_ApplicationContentFrame = value;
				}
			}
		}        
	}
}