using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Gateway
{
	
	//we need to be careful not to change how this single class structure is setup because this structure is thread safe.
    public sealed class AOGW
    {
        //Properties in AO: LockAquiredById, LockAquiredByUserName, LockAquiredByHostName, TimeLockAquired
        //Need a stored procedure that takes the above parameters and a masteraccessionno  and sets them if they are not null.
        //Assume if LockAuqiredById is null then they are all null.

        private static readonly AOGW instance = new AOGW();

        private YellowstonePathology.Business.Test.AccessionOrderCollection m_AccessionOrderCollection;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        static AOGW()
        {

        }

        private AOGW()
        {
            //This collection will hold all AO's in use by the application.
            //AO's will be removed from the collection when they are released.
            //saving an AO will not cause it to be removed from the collection.
            this.m_AccessionOrderCollection = new Test.AccessionOrderCollection();
            this.m_ObjectTracker = new Persistence.ObjectTracker();
        }

        public AOSaveResult Save(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            //This will be the only place that an AO can be saved.
            //We need to devise a way to make sure AO's are not saved directly throught the OT
            AOSaveResult result = new AOSaveResult();
            return result;
        }

        public AOSaveResult Release(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            //used to release lock, side affect is that all data will be saved as well.
            //If lock is aquired (property in accessionorder) then
            //this method will change the LockedById, LockedByUserName, TimeLockeAquired to null and then call save.            
            //this method will remove the AO from the collection
            //If the AO does not exist in the collection then throw an error.
            AOSaveResult result = new AOSaveResult();
            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool aquireLock)
        {
            //If it is in the list remove it from the list, unregister it from the OT and then go get it from the database.  If it's not in the list then throw an error.    
            //Register it with OT if AO.LockAquired == true;        
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();            
            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByMasterAccessionNo(string masterAccessionNo, bool aquireLock)
        {
            //always see if you already have it before you go to the database
            //if it's alread here then return it
            //go get it, put it in the collection and return it
            //If AO.LockAquired == true then register it in the OT
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();            
            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByReportNo(string reportNo, bool aquireLock)
        {
            //always see if you already have it before you go to the database
            //if it's alread here then return it
            //go get it, put it in the collection and return it
            //If AO.LockAquired == true then register it in the OT            
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();            
            return result;
        }

        public static AOGW Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
