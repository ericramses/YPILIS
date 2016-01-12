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

        private RegisteredObjectCollection m_RegisteredObjects;
        private RegisteredObjectCollection m_RegisteredRootInserts;
        private RegisteredObjectCollection m_RegisteredRootDeletes;
        private RegisteredCollections m_RegisteredCollections;
        
		static ObjectTrackerV2()
		{
		}
		
        private ObjectTrackerV2() 
        {            
            this.m_RegisteredObjects = new RegisteredObjectCollection();
            this.m_RegisteredRootInserts = new RegisteredObjectCollection();
            this.m_RegisteredRootDeletes = new RegisteredObjectCollection();
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

        public void RegisterObject(object objectToRegister, object registeredBy)
        {
			if (objectToRegister.GetType().BaseType.Name == "ObservableCollection`1")
			{
				RegisterObjectCollection(objectToRegister, registeredBy);
			}
			else
			{
				ObjectCloner objectCloner = new ObjectCloner();
				object clonedObject = objectCloner.Clone(objectToRegister);
				this.m_RegisteredObjects.Register(clonedObject, registeredBy);
			}
		}

		private void RegisterObjectCollection(object objectCollectionToRegister, object registeredBy)
		{
            Collection<object> clonedCollection = new Collection<object>();
            int listCount = (int)objectCollectionToRegister.GetType().GetProperty("Count").GetValue(objectCollectionToRegister, null);

            for (int i = 0; i < listCount; i++)
            {                
                object[] index = { i };
                object listObject = objectCollectionToRegister.GetType().GetProperty("Item").GetValue(objectCollectionToRegister, index);
                this.RegisterObject(listObject, registeredBy);
                clonedCollection.Add(listObject);
            }

            RegisteredCollection registeredCollection = new RegisteredCollection(objectCollectionToRegister, clonedCollection);
            this.m_RegisteredCollections.Add(registeredCollection);            
		}

        public bool IsRegistered(object objectToCheck, object registeredBy)
        {
            bool result = false;

            if(this.m_RegisteredObjects.IsRegisteredBy(objectToCheck, registeredBy) == true)
            {
           		result = true;
            }

            return result;
        }

        public void Deregister(object objectToDeRegister, object registeredBy)
        {
            this.m_RegisteredObjects.Unregister(objectToDeRegister, registeredBy);
            this.m_RegisteredRootDeletes.Unregister(objectToDeRegister, registeredBy);
            this.m_RegisteredRootInserts.Unregister(objectToDeRegister, registeredBy);
        }        

        public void RegisterRootInsert(object rootObjectToInsert, object registeredBy)
        {
            this.m_RegisteredRootInserts.Register(rootObjectToInsert, registeredBy);
        }

        public void RegisterRootDelete(object rootObjectToDelete, object registeredBy)
        {
			this.m_RegisteredObjects.Unregister(rootObjectToDelete, registeredBy);

            this.m_RegisteredRootDeletes.Register(rootObjectToDelete, registeredBy);
        }                

		private void SubmitCollectionChanges(object objectCollectionToSubmit, object registeredBy)
		{
			int listCount = (int)objectCollectionToSubmit.GetType().GetProperty("Count").GetValue(objectCollectionToSubmit, null);
			for (int i = 0; i < listCount; i++)
			{
				object[] index = { i };
				object listObject = objectCollectionToSubmit.GetType().GetProperty("Item").GetValue(objectCollectionToSubmit, index);
				if (this.m_RegisteredObjects.IsRegisteredBy(listObject, registeredBy) == true)
				{
					this.SubmitChanges(listObject, registeredBy);
		            this.Deregister(listObject, registeredBy);
				}

			}
			
			RegisteredCollection registeredCollection = this.m_RegisteredCollections.GetRegisteredCollection(objectCollectionToSubmit);
            List<object> insertedObjects = registeredCollection.GetInsertedObjects();
            foreach (object insertedObject in insertedObjects)
            {
                this.RegisterRootInsert(insertedObject, registeredBy);
                this.SubmitChanges(insertedObject, registeredBy);
                this.Deregister(insertedObject, registeredBy);
            }            

            List<object> deletedObjects = registeredCollection.GetDeletedObjects();
            foreach (object deletedObject in deletedObjects)
            {
                this.RegisterRootDelete(deletedObject, registeredBy);
                this.SubmitChanges(deletedObject, registeredBy);
                this.Deregister(deletedObject, registeredBy);
            }            
            
            this.m_RegisteredCollections.Remove(registeredCollection);
            this.RegisterObjectCollection(objectCollectionToSubmit, registeredBy);
		}                           
              
        /*public void PrepareRemoteTransferAgent(object objectToSubmit,  RemoteObjectTransferAgent remoteTransferAgent)
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
        }*/

        /*public void SubmitChanges(RemoteObjectTransferAgent remoteTransferAgent)
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
        }*/

		/*private void ResetFromRemoteTransferAgent(RemoteObjectTransferAgent remoteTransferAgent)
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
		}*/

        public SubmissionResult SubmitChanges(object objectToSubmit, object registeredBy)
        {
            SubmissionResult result = new SubmissionResult();

            if (objectToSubmit.GetType().BaseType.Name == "ObservableCollection`1")
            {
                this.SubmitCollectionChanges(objectToSubmit, registeredBy);
            }
            else
            {
                SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(objectToSubmit, registeredBy);                
                sqlCommandSubmitter.SubmitChanges();                
            }

            return result;
        }

        private void HandleRootDeleteSubmission(object objectToSubmit, object keyPropertyValue, SqlCommandSubmitter objectSubmitter)
        {
            DeleteCommandBuilder deleteCommandBuilder = new DeleteCommandBuilder();
            deleteCommandBuilder.Build(objectToSubmit, objectSubmitter.SqlDeleteFirstCommands, objectSubmitter.SqlDeleteCommands);
        }

        private void HandleRootInsertSubmission(object objectToSubmit, object keyPropertyValue, SqlCommandSubmitter objectSubmitter, object registeredBy)
        {
            InsertCommandBuilder insertCommandBuilder = new InsertCommandBuilder();
            insertCommandBuilder.Build(objectToSubmit, objectSubmitter.SqlInsertCommands, objectSubmitter.SqlInsertLastCommands);
            this.m_RegisteredRootInserts.Unregister(objectToSubmit, registeredBy);
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

        public SqlCommandSubmitter GetSqlCommands(object objectToSubmit, object registeredBy)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)objectToSubmit.GetType().GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            Type objectType = objectToSubmit.GetType();

            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToSubmit, null);

            object registeredObject = null;
            if(this.m_RegisteredObjects.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredObjects.Get(objectToSubmit);
                registeredObject = registeredObjectToSubmit.Value;
                this.m_RegisteredObjects.Unregister(objectToSubmit, registeredBy);
	            this.HandleUpdateSubmission(objectToSubmit, registeredObject, keyPropertyValue, objectSubmitter);
	            this.RegisterObject(objectToSubmit, registeredBy);
            }
            else if(this.m_RegisteredRootDeletes.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredRootDeletes.Get(objectToSubmit);
                registeredObject = registeredObjectToSubmit.Value;
                this.m_RegisteredRootDeletes.Unregister(objectToSubmit, registeredBy);
	            this.HandleRootDeleteSubmission(registeredObject, keyPropertyValue, objectSubmitter);
            }
            else if(this.m_RegisteredRootInserts.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
	            this.m_RegisteredRootInserts.Unregister(objectToSubmit, registeredBy);
	            this.HandleRootInsertSubmission(objectToSubmit, keyPropertyValue, objectSubmitter, registeredBy);
	            this.RegisterObject(objectToSubmit, registeredBy);
            }
            else
            {
                throw new Exception("The object you request submission on is not registered.");
            }
            return objectSubmitter;
        }
	}
}
