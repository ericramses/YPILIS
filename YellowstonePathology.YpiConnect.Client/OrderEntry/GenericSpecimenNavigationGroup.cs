using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class GenericSpecimenNavigationGroup : NavigationGroup
	{
		private static GenericSpecimenNavigationGroup s_Instance;

		private GenericSpecimenNavigationGroup()
        {
			this.Add(typeof(ScanContainerPage));
			this.Add(typeof(SpecimenDescriptionOtherPage));
		}

		public static GenericSpecimenNavigationGroup Instance
        {
            get 
            {
                if (s_Instance == null)
                {
					s_Instance = new GenericSpecimenNavigationGroup();
                }
                return s_Instance; 
            }
        }
	}
}
