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
            : base(documentId)
        {            
            ObjectCloner objectCloner = new ObjectCloner();
            this.m_Clone = objectCloner.Clone(this.m_Value);            
        }             

        public override YellowstonePathology.Business.Persistence.SubmissionResult Submit()
        {            
            YellowstonePathology.Business.Persistence.SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(this.m_Value);
            YellowstonePathology.Business.Persistence.SubmissionResult result = sqlCommandSubmitter.SubmitChanges();

            ObjectCloner objectCloner = new ObjectCloner();
            this.m_Clone = objectCloner.Clone(this.m_Value);

            return result;
        }             
    }
}
