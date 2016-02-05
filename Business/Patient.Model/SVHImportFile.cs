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
                    YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitRootInsert(sVHBillingData);					
                }
			}
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(this.m_SVHBillingDataCollection, true);			
		}        
    }
}
