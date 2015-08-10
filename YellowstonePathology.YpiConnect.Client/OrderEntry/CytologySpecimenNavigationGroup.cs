using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class CytologySpecimenNavigationGroup : NavigationGroup
	{
		private static CytologySpecimenNavigationGroup s_Instance;

		private CytologySpecimenNavigationGroup()
        {
			this.Add(typeof(CytologyTestOrderPage));
			this.Add(typeof(CytologyScreeningTypePage));
			this.Add(typeof(CytologyClinicalHistoryPage));
			this.Add(typeof(CytologyIcd9EntryPage));
			this.Add(typeof(ScanContainerPage));
			this.Add(typeof(CytologySpecimenSourcePage));
		}

		public static CytologySpecimenNavigationGroup Instance
        {
            get 
            {
                if (s_Instance == null)
                {
					s_Instance = new CytologySpecimenNavigationGroup();
                }
                return s_Instance; 
            }
        }
	}
}
