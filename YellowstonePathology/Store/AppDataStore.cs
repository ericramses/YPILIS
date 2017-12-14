using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Store
{
    public class AppDataStore
    {        
        //private static string MODE = "DEV";
        private static string MODE = "PROD";     

        public AppDataStore()
        {
            if(MODE == "PROD")
            {
               
            }
            else
            {

            }
        }        
    }
}
