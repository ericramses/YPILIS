using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Patient.Model
{
    public class SVHImportFile 
    {
        private SVHImportFileName m_SVHImportFileName;
		private SVHBillingDataCollection m_SVHBillingDataCollection;

        public SVHImportFile(SVHImportFileName svhImportFilename)
        {
            this.m_SVHImportFileName = svhImportFilename;
			this.m_SVHBillingDataCollection = new SVHBillingDataCollection();            
        }

        public void ParseAndPersist()
        {
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Persistence.ObjectTracker();
			objectTracker.RegisterObject(this.m_SVHBillingDataCollection);
			using (StreamReader sr = new StreamReader(this.m_SVHImportFileName.FullPath))
            {                
                DateTime dateProcessed = DateTime.Now;

                // read the first 2 lines - ||| and then header
                string line = sr.ReadLine();                                          

                while ((line = sr.ReadLine()) != null)
                {
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					SVHBillingData sVHBillingData = new SVHBillingData(objectId, line, dateProcessed, this.m_SVHImportFileName.FileDate);
					sVHBillingData.SVHBillingDataId = Guid.NewGuid().ToString();
					this.m_SVHBillingDataCollection.AddOnlyMostRecent(sVHBillingData);
					objectTracker.RegisterRootInsert(sVHBillingData);
                }
			}
			objectTracker.SubmitChanges(this.m_SVHBillingDataCollection);
		}        
    }
}
