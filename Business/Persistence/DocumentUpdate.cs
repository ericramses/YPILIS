using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentUpdate : Document
    {
        public DocumentUpdate(DocumentId documentId) 
            : base (documentId)
        {

        }

        public override YellowstonePathology.Business.Persistence.SubmissionResult Submit()
        {
            if (this.Value is YellowstonePathology.Business.Test.AccessionOrder)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)this.m_Value;
                if (accessionOrder.LockAquired == true)
                {
                    accessionOrder.LockAquiredByHostName = null;
                    accessionOrder.LockAquiredById = null;
                    accessionOrder.LockAquiredByUserName = null;
                    accessionOrder.TimeLockAquired = null;
                }
            }

            YellowstonePathology.Business.Persistence.SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(this.m_Value);
            YellowstonePathology.Business.Persistence.SubmissionResult result = sqlCommandSubmitter.SubmitChanges();

            return result;
        }             
    }
}
