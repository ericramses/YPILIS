using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class LeukemiaLyphomaNavigationGroup : NavigationGroup
    {
        private static LeukemiaLyphomaNavigationGroup s_Instance;

        private LeukemiaLyphomaNavigationGroup()
        {            
            this.Add(typeof(LeukemiaLymphomaGatingPage));
            this.Add(typeof(LeukemiaLymphomaMarkersPage));
            this.Add(typeof(LeukemiaLymphomaResultPage));
            this.Add(typeof(LeukemiaLymphomaSignoutPage));
            //this.Add(typeof(PatientInformationPage));
		}

        public static LeukemiaLyphomaNavigationGroup Instance
        {
            get 
            {
                if (s_Instance == null)
                {
                    s_Instance = new LeukemiaLyphomaNavigationGroup();
                }
                return s_Instance; 
            }
        }
    }
}
