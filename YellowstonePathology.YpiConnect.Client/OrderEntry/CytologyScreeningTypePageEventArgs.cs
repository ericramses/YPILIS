using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class CytologyScreeningTypePageEventArgs : Shared.PageNavigationReturnEventArgs
    {
        private bool m_ManualEntryOfIcd9CodeRequired;

        public CytologyScreeningTypePageEventArgs(bool manualEntryOfIcd9CodeRequired, Shared.PageNavigationDirectionEnum pageNavigationDirectionEnum, object data) 
            : base(pageNavigationDirectionEnum, data)
        {
            this.m_ManualEntryOfIcd9CodeRequired = manualEntryOfIcd9CodeRequired;
        }

        public bool ManualEntryOfIcd9CodeRequired
        {
            get { return this.m_ManualEntryOfIcd9CodeRequired; }            
        }
    }
}
