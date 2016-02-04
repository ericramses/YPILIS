using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectGatway
    {
        private static volatile ObjectGatway instance;
        private static object syncRoot = new Object();

        private Dictionary<object, object> m_RegisteredObjects;
        private Dictionary<object, object> m_RegisteredRootInserts;
        private Dictionary<object, object> m_RegisteredRootDeletes;

        static ObjectGatway()
        {

        }
        private ObjectGatway() 
        {            
            this.m_RegisteredObjects = new Dictionary<object, object>();
            this.m_RegisteredRootInserts = new Dictionary<object, object>();
            this.m_RegisteredRootDeletes = new Dictionary<object, object>();            
        }

        public static ObjectGatway Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ObjectGatway();
                    }
                }

                return instance;
            }
        }        

        public YellowstonePathology.Business.Test.AccessionOrder GetByMasterAccessionNo(string masterAccessionNo, bool aquireLock)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = null;
            
            if (this.m_RegisteredObjects.ContainsKey(masterAccessionNo) == true)
            {
                result = (YellowstonePathology.Business.Test.AccessionOrder)this.m_RegisteredObjects[masterAccessionNo];
                if (result.LockedAquired == false)
                {
                    this.m_RegisteredObjects.Remove(masterAccessionNo);
                    result = null;
                }
            }

            if (result == null)
            {                
                result = this.BuildFromSQL(masterAccessionNo, aquireLock);
                this.RegisterObject(result);                
            }            

            return result;
        }        

        private YellowstonePathology.Business.Test.AccessionOrder BuildFromSQL(string masterAccessionNo, bool aquireLock)
        {
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            YellowstonePathology.Business.Gateway.AccessionOrderBuilder accessionOrderBuilder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            XElement document = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AOGWGetByMasterAccessionNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            cmd.Parameters.Add("@AquireLock", SqlDbType.Bit).Value = aquireLock;
            cmd.Parameters.Add("@LockAquiredById", SqlDbType.VarChar).Value = systemIdentity.User.UserId;
            cmd.Parameters.Add("@LockAquiredByUserName", SqlDbType.VarChar).Value = systemIdentity.User.UserName;
            cmd.Parameters.Add("@LockAquiredByHostName", SqlDbType.VarChar).Value = System.Environment.MachineName;
            cmd.Parameters.Add("@TimeLockAquired", SqlDbType.DateTime).Value = DateTime.Now;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        document = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
                    }
                }
            }

            accessionOrderBuilder.Build(document);
            return accessionOrderBuilder.AccessionOrder;
        }

        public YellowstonePathology.Business.Persistence.SubmissionResult SubmitChanges(object objectToSubmit, bool releaseLock)
        {
            YellowstonePathology.Business.Persistence.SubmissionResult result = new YellowstonePathology.Business.Persistence.SubmissionResult();

            if(objectToSubmit is YellowstonePathology.Business.Test.AccessionOrder)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)objectToSubmit;
                if (accessionOrder.LockedAquired == true && releaseLock == true)
                {
                    accessionOrder.LockAquiredByHostName = null;
                    accessionOrder.LockAquiredById = null;
                    accessionOrder.LockAquiredByUserName = null;
                    accessionOrder.TimeLockAquired = null;
                }
            }            

            YellowstonePathology.Business.Persistence.SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(objectToSubmit);
            sqlCommandSubmitter.SubmitChanges();

            return result;
        }

        public void RegisterObject(object objectToRegister)
        {			
			ObjectCloner objectCloner = new ObjectCloner();
			object clonedObject = objectCloner.Clone(objectToRegister);

			Type objectType = clonedObject.GetType();
			PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
			object keyPropertyValue = keyProperty.GetValue(clonedObject, null);

			if (this.IsOkToRegister(objectToRegister, objectType, keyPropertyValue) == true)
			{
				this.m_RegisteredObjects.Add(keyPropertyValue, clonedObject);
			}			
		}		

        private bool IsOkToRegister(object objectToRegister, Type objectType, object keyPropertyValue)
        {
            bool result = false;            

            if (this.m_RegisteredRootInserts.ContainsKey(keyPropertyValue) == true)
            {
                throw new Exception("Cannot register the object because it is in the Root Inserts Dictionary.");
            }
            else if (this.m_RegisteredRootDeletes.ContainsKey(keyPropertyValue) == true)
            {
                throw new Exception("Cannot register the object because it is in the Root Deletes Dictionary.");
            }
            else if (this.m_RegisteredObjects.ContainsKey(keyPropertyValue) == false)
            {
                result = true;
            }
            else if (this.m_RegisteredObjects.ContainsKey(keyPropertyValue) == true)
            {
                object currentlyRegisteredObjectWithSameKey = this.m_RegisteredObjects[keyPropertyValue];
                Type currentlyRegisteredObjectWithSameKeyType = currentlyRegisteredObjectWithSameKey.GetType();
				if (objectType.Name != currentlyRegisteredObjectWithSameKeyType.Name)
				{
					throw new Exception("The object you are trying to register has the same key as another object but not the same type. Registered Object: " + currentlyRegisteredObjectWithSameKeyType.Name + " This Object: " + objectType.Name);
				}
				else
				{
					throw new Exception("Cannot register the object because it is already registered.");
				}
            }

            return result;
        }

        private bool IsRegistered(object objectToCheck)
        {
            bool result = false;

            Type objectType = objectToCheck.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToCheck, null);
            result = this.m_RegisteredObjects.ContainsKey(keyPropertyValue);            

            return result;
        }
        
        private void Deregister(object objectToDeRegister)
        {
            Type objectType = objectToDeRegister.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToDeRegister, null);

            this.m_RegisteredObjects.Remove(keyPropertyValue);
            this.m_RegisteredRootDeletes.Remove(keyPropertyValue);
            this.m_RegisteredRootInserts.Remove(keyPropertyValue);
        }        

        public void RegisterRootInsert(object rootObjectToInsert)
        {
            Type objectType = rootObjectToInsert.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(rootObjectToInsert, null);

            if (this.m_RegisteredRootInserts.ContainsKey(keyPropertyValue) == false)
            {
                this.m_RegisteredRootInserts.Add(keyPropertyValue, rootObjectToInsert);
            }
        }

        public void RegisterRootDelete(object rootObjectToDelete)
        {
            Type objectType = rootObjectToDelete.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(rootObjectToDelete, null);

			if (this.m_RegisteredObjects.ContainsKey(keyPropertyValue) == true)
			{
				this.m_RegisteredObjects.Remove(keyPropertyValue);
			}

            if (this.m_RegisteredRootDeletes.ContainsKey(keyPropertyValue) == false)
            {
                this.m_RegisteredRootDeletes.Add(keyPropertyValue, rootObjectToDelete);
            }
        }                		

        //public SubmissionResult SubmitChanges(object objectToSubmit)
        //{
        //    SubmissionResult result = new SubmissionResult();
            
        //    SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(objectToSubmit);                
        //    sqlCommandSubmitter.SubmitChanges();                

        //    return result;
        //}

        private void HandleRootDeleteSubmission(object objectToSubmit, object keyPropertyValue, SqlCommandSubmitter objectSubmitter)
        {
            DeleteCommandBuilder deleteCommandBuilder = new DeleteCommandBuilder();
            deleteCommandBuilder.Build(objectToSubmit, objectSubmitter.SqlDeleteFirstCommands, objectSubmitter.SqlDeleteCommands);
        }

        private void HandleRootInsertSubmission(object objectToSubmit, object originalValues, object keyPropertyValue, SqlCommandSubmitter objectSubmitter)
        {
            InsertCommandBuilder insertCommandBuilder = new InsertCommandBuilder();
            insertCommandBuilder.Build(objectToSubmit, objectSubmitter.SqlInsertCommands, objectSubmitter.SqlInsertLastCommands);
            this.m_RegisteredRootInserts.Remove(keyPropertyValue);
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

        public SqlCommandSubmitter GetSqlCommands(object objectToSubmit)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)objectToSubmit.GetType().GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            Type objectType = objectToSubmit.GetType();

            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToSubmit, null);

            object registeredObject = null;
            if (this.m_RegisteredObjects.TryGetValue(keyPropertyValue, out registeredObject) == true)
            {
                this.m_RegisteredObjects.Remove(keyPropertyValue);
                this.HandleUpdateSubmission(objectToSubmit, registeredObject, keyPropertyValue, objectSubmitter);
                this.RegisterObject(objectToSubmit);
            }
            else if (this.m_RegisteredRootDeletes.TryGetValue(keyPropertyValue, out registeredObject) == true)
            {
                this.m_RegisteredRootDeletes.Remove(keyPropertyValue);
                this.HandleRootDeleteSubmission(registeredObject, keyPropertyValue, objectSubmitter);
            }
            else if (this.m_RegisteredRootInserts.TryGetValue(keyPropertyValue, out registeredObject) == true)
            {
                this.m_RegisteredRootInserts.Remove(keyPropertyValue);
                this.HandleRootInsertSubmission(objectToSubmit, registeredObject, keyPropertyValue, objectSubmitter);
                this.RegisterObject(objectToSubmit);
            }
            else
            {
                throw new Exception("The object you request submission on is not registered.");
            }
            return objectSubmitter;
        }
	}
}
