/*
 * Created by SharpDevelop.
 * User: William.Copland
 * Date: 12/22/2015
 * Time: 12:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
	/// <summary>
	/// Description of ObjectTrackerV2.
	/// </summary>
	public sealed class ObjectTrackerV2
	{
        private static ObjectTrackerV2 instance;

        private Dictionary<object, object> m_RegisteredObjects;
        private Dictionary<object, object> m_RegisteredRootInserts;
        private Dictionary<object, object> m_RegisteredRootDeletes;
        private RegisteredCollections m_RegisteredCollections;
        
		static ObjectTrackerV2()
		{
		}
		
        private ObjectTrackerV2() 
        {            
            this.m_RegisteredObjects = new Dictionary<object, object>();
            this.m_RegisteredRootInserts = new Dictionary<object, object>();
            this.m_RegisteredRootDeletes = new Dictionary<object, object>();
            this.m_RegisteredCollections = new RegisteredCollections();
        }

        public static ObjectTrackerV2 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectTrackerV2();
                }
                return instance;
            }
        }

        public void RegisterObject(object objectToRegister)
        {
			if (objectToRegister.GetType().BaseType.Name == "ObservableCollection`1")
			{
				RegisterObjectCollection(objectToRegister);
			}
			else
			{
				ObjectCloner objectCloner = new ObjectCloner();
				object clonedObject = objectCloner.Clone(objectToRegister);

				Type objectType = clonedObject.GetType();
				PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
				object keyPropertyValue = keyProperty.GetValue(clonedObject, null);

                this.Deregister(objectToRegister);
				this.m_RegisteredObjects.Add(keyPropertyValue, clonedObject);
			}
		}

		private void RegisterObjectCollection(object objectCollectionToRegister)
		{
            Collection<object> clonedCollection = new Collection<object>();
            int listCount = (int)objectCollectionToRegister.GetType().GetProperty("Count").GetValue(objectCollectionToRegister, null);

            for (int i = 0; i < listCount; i++)
            {                
                object[] index = { i };
                object listObject = objectCollectionToRegister.GetType().GetProperty("Item").GetValue(objectCollectionToRegister, index);
                this.RegisterObject(listObject);
                clonedCollection.Add(listObject);
            }

            RegisteredCollection registeredCollection = new RegisteredCollection(objectCollectionToRegister, clonedCollection);
            this.m_RegisteredCollections.Add(registeredCollection);            
		}

        public bool IsRegistered(object objectToCheck)
        {
            bool result = false;

            Type objectType = objectToCheck.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToCheck, null);
            result = this.m_RegisteredObjects.ContainsKey(keyPropertyValue);            

            return result;
        }

        public void Deregister(object objectToDeRegister)
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

		private void SubmitCollectionChanges(object objectCollectionToSubmit)
		{
			int listCount = (int)objectCollectionToSubmit.GetType().GetProperty("Count").GetValue(objectCollectionToSubmit, null);
			for (int i = 0; i < listCount; i++)
			{
				object[] index = { i };
				object listObject = objectCollectionToSubmit.GetType().GetProperty("Item").GetValue(objectCollectionToSubmit, index);
				PropertyInfo keyProperty = listObject.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
				object keyPropertyValue = keyProperty.GetValue(listObject, null);
				if (this.m_RegisteredObjects.ContainsKey(keyPropertyValue) == true)
				{
					this.SubmitChanges(listObject);
				}

                this.Deregister(listObject);
			}
			
			RegisteredCollection registeredCollection = this.m_RegisteredCollections.GetRegisteredCollection(objectCollectionToSubmit);
            List<object> insertedObjects = registeredCollection.GetInsertedObjects();
            foreach (object insertedObject in insertedObjects)
            {
                this.RegisterRootInsert(insertedObject);
                this.SubmitChanges(insertedObject);
                this.Deregister(insertedObject);
            }            

            List<object> deletedObjects = registeredCollection.GetDeletedObjects();
            foreach (object deletedObject in deletedObjects)
            {
                this.RegisterRootDelete(deletedObject);
                this.SubmitChanges(deletedObject);
                this.Deregister(deletedObject);
            }            
            
            this.m_RegisteredCollections.Remove(registeredCollection);
            this.RegisterObjectCollection(objectCollectionToSubmit);
		}                           
              
        public void PrepareRemoteTransferAgent(object objectToSubmit,  RemoteObjectTransferAgent remoteTransferAgent)
        {
            remoteTransferAgent.ObjectToSubmit = objectToSubmit;
            foreach (KeyValuePair<object, object> pair in this.m_RegisteredObjects)
            {
                TransferObject transferObject = new TransferObject(pair.Key, pair.Value);
                remoteTransferAgent.RegisteredObjects.Add(transferObject);
            }
            foreach (KeyValuePair<object, object> pair in this.m_RegisteredRootDeletes)
            {
                TransferObject transferObject = new TransferObject(pair.Key, pair.Value);
                remoteTransferAgent.RegisteredRootDeletes.Add(transferObject);
            }
            foreach (KeyValuePair<object, object> pair in this.m_RegisteredRootInserts)
            {
                TransferObject transferObject = new TransferObject(pair.Key, pair.Value);
                remoteTransferAgent.RegisteredRootInserts.Add(transferObject);
            }
			this.ResetFromRemoteTransferAgent(remoteTransferAgent);
        }

        public void SubmitChanges(RemoteObjectTransferAgent remoteTransferAgent)
        {            
            foreach (TransferObject transferObject in remoteTransferAgent.RegisteredObjects)
            {
                this.m_RegisteredObjects.Add(transferObject.Key, transferObject.Value);
            }

            foreach (TransferObject transferObject in remoteTransferAgent.RegisteredRootDeletes)
            {
                this.m_RegisteredRootDeletes.Add(transferObject.Key, transferObject.Value);
            }            

            foreach (TransferObject transferObject in remoteTransferAgent.RegisteredRootInserts)
            {
                this.m_RegisteredRootInserts.Add(transferObject.Key, transferObject.Value);
            }

            this.SubmitChanges(remoteTransferAgent.ObjectToSubmit);            
        }

		private void ResetFromRemoteTransferAgent(RemoteObjectTransferAgent remoteTransferAgent)
		{
			object objectToSubmit = remoteTransferAgent.ObjectToSubmit;
			Type objectType = objectToSubmit.GetType();
			PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
			object keyPropertyValue = keyProperty.GetValue(objectToSubmit, null);

			object registeredObject = null;
			if (this.m_RegisteredObjects.TryGetValue(keyPropertyValue, out registeredObject) == true)
			{
				this.m_RegisteredObjects.Remove(keyPropertyValue);
				this.RegisterObject(objectToSubmit);
			}
			else if (this.m_RegisteredRootDeletes.TryGetValue(keyPropertyValue, out registeredObject) == true)
			{
				this.m_RegisteredRootDeletes.Remove(keyPropertyValue);
			}
			else if (this.m_RegisteredRootInserts.TryGetValue(keyPropertyValue, out registeredObject) == true)
			{
				this.m_RegisteredRootInserts.Remove(keyPropertyValue);
				this.RegisterObject(objectToSubmit);
			}
			else
			{
				throw new Exception("The object you request submission on is not registered.");
			}
		}

        public SubmissionResult SubmitChanges(object objectToSubmit)
        {
            SubmissionResult result = new SubmissionResult();

            if (objectToSubmit.GetType().BaseType.Name == "ObservableCollection`1")
            {
                this.SubmitCollectionChanges(objectToSubmit);
            }
            else
            {
                SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(objectToSubmit);                
                sqlCommandSubmitter.SubmitChanges();                
            }

            return result;
        }

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

        public void InsertSubclassOnly(object subclassedObject)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)subclassedObject.GetType().GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter sqlCommandSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            InsertCommandBuilder insertCommandBuilder = new InsertCommandBuilder();
            insertCommandBuilder.BuildSubclassOnly(subclassedObject, sqlCommandSubmitter.SqlInsertCommands, sqlCommandSubmitter.SqlInsertLastCommands);
            sqlCommandSubmitter.SubmitChanges();
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
