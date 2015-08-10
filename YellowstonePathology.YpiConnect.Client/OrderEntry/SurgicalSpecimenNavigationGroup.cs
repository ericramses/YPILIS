using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class SurgicalSpecimenNavigationGroup : NavigationGroup
	{
		private static SurgicalSpecimenNavigationGroup s_Instance;

		private SurgicalSpecimenNavigationGroup()
        {
			this.Add(typeof(SurgicalClientOrderInformationPage));			
			this.Add(typeof(ScanContainerPage));
			this.Add(typeof(SpecimenDescriptionPage));
		}

		public static SurgicalSpecimenNavigationGroup Instance
        {
            get 
            {
                if (s_Instance == null)
                {
					s_Instance = new SurgicalSpecimenNavigationGroup();
                }
                return s_Instance; 
            }
        }
	}
}
