using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class Document
    {
        protected object m_Key;
        protected object m_Value;
        protected Type m_Type;
        protected object m_Clone;                

        protected List<object> m_Writers;

        public Document()
        {

        }

        public Document(DocumentId documentId)
        {
            this.m_Type = documentId.Type;
            this.m_Value = Activator.CreateInstance(documentId.Type);

            ObjectCloner objectCloner = new ObjectCloner();
            this.m_Clone = objectCloner.Clone(this.m_Value);

            PropertyInfo keyProperty = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            this.m_Key = keyProperty.GetValue(documentId.Key, null);

            this.m_Writers.Add(documentId.Writer);
        }

        public void ReleaseLock()
        {
            if (this.m_Value is YellowstonePathology.Business.Test.AccessionOrder)
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
        }

        public void SetLock(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            if (this.m_Value is YellowstonePathology.Business.Test.AccessionOrder)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)this.m_Value;                                
                accessionOrder.LockAquiredByHostName = Environment.MachineName;
                accessionOrder.LockAquiredById = systemIdentity.User.UserId;
                accessionOrder.LockAquiredByUserName = systemIdentity.User.UserName;
                accessionOrder.TimeLockAquired = DateTime.Now;             
            }
        }        

        public List<object> Writers
        {
            get { return this.m_Writers;  }
        }

        public object Key
        {
            get { return this.m_Key; }
            set { this.m_Key = value; }
        }

        public object Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }
        public Type Type
        {
            get { return this.m_Type; }
            set { this.m_Type = value; }
        }

        public object Clone
        {
            get { return this.m_Clone; }
            set { this.m_Clone = value; }
        }                             

        public virtual YellowstonePathology.Business.Persistence.SubmissionResult Submit()
        {
            throw new Exception("Not implemented here");
        }

        public void RemoveWriter(object writer)
        {
            for(int i=0; i<this.m_Writers.Count; i++)
            {
                if(this.m_Writers[i] == writer)
                {
                    this.m_Writers.Remove(this.m_Writers[i]);
                    break;
                }
            }
        }

        public SqlCommandSubmitter GetSqlCommands(object objectToSubmit)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)objectToSubmit.GetType().GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            Type objectType = objectToSubmit.GetType();

            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToSubmit, null);

            this.HandleUpdateSubmission(objectToSubmit, this.m_Value, keyPropertyValue, objectSubmitter);

            ObjectCloner objectCloner = new ObjectCloner();
            this.Clone = objectCloner.Clone(objectToSubmit);

            return objectSubmitter;
        }

        private void HandleUpdateSubmission(object objectToSubmit, object originalValues, object keyPropertyValue, SqlCommandSubmitter objectSubmitter)
        {
            UpdateCommandBuilder updateCommandBuilder = new UpdateCommandBuilder();
            updateCommandBuilder.Build(objectToSubmit, originalValues, objectSubmitter.SqlUpdateCommands);

            InsertedObjectFinder insertedObjectFinder = new InsertedObjectFinder();
            insertedObjectFinder.Run(originalValues, objectToSubmit);

            while (insertedObjectFinder.InsertedObjects.Count != 0)
            {
                object objectToInsert = insertedObjectFinder.InsertedObjects.Dequeue();
                InsertCommandBuilder insertCommandBuilder = new InsertCommandBuilder();
                insertCommandBuilder.Build(objectToInsert, objectSubmitter.SqlInsertCommands, objectSubmitter.SqlInsertLastCommands);
            }

            DeletedObjectFinder deletedObjectFinder = new DeletedObjectFinder();
            deletedObjectFinder.Run(originalValues, objectToSubmit);
            while (deletedObjectFinder.DeletedObjects.Count != 0)
            {
                object objectToDelete = deletedObjectFinder.DeletedObjects.Dequeue();
                DeleteCommandBuilder deleteCommandBuilder = new DeleteCommandBuilder();
                deleteCommandBuilder.Build(objectToDelete, objectSubmitter.SqlDeleteFirstCommands, objectSubmitter.SqlDeleteCommands);
            }
        }        
    }
}
