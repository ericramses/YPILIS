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
using System.Windows;

namespace YellowstonePathology.Business.Persistence
{
	/// <summary>
	/// Description of ObjectTrackerV2.
	/// </summary>
	public sealed class ObjectTrackerV2
	{
        private static volatile ObjectTrackerV2 instance;
        private static object syncRoot = new Object();        

        private RegisteredObjectCollection m_RegisteredObjects;
        private RegisteredObjectCollection m_RegisteredRootInserts;
        private RegisteredObjectCollection m_RegisteredRootDeletes;        
        
		static ObjectTrackerV2()
		{

		}
		
        private ObjectTrackerV2() 
        {            
            this.m_RegisteredObjects = new RegisteredObjectCollection();
            this.m_RegisteredRootInserts = new RegisteredObjectCollection();
            this.m_RegisteredRootDeletes = new RegisteredObjectCollection();
        }

        public static ObjectTrackerV2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ObjectTrackerV2();
                    }
                }
                return instance;
            }
        }

        public void RegisterObject(object objectToRegister, object registeredBy)
        {
            if (objectToRegister == null)
            {
                MessageBox.Show("There is trouble in paradise! Please call Sid (6050).");
                return;
            }

			ObjectCloner objectCloner = new ObjectCloner();
			object clonedObject = objectCloner.Clone(objectToRegister);
			this.m_RegisteredObjects.Register(clonedObject, registeredBy);			
		}

        public void CleanUp(object registeredBy)
        {            
            this.m_RegisteredObjects.CleanUp(registeredBy);
            this.m_RegisteredRootDeletes.CleanUp(registeredBy);
            this.m_RegisteredRootInserts.CleanUp(registeredBy);            
        }

        public RegisteredObjectCollection RegisteredObjects
        {
            get { return this.m_RegisteredObjects; }
        }

        public void RegisterRootInsert(object rootObjectToInsert, object registeredBy)
        {
            this.m_RegisteredRootInserts.Register(rootObjectToInsert, registeredBy);
        }

        public void RegisterRootDelete(object rootObjectToDelete, object registeredBy)
        {
			if(this.m_RegisteredObjects.Exists(rootObjectToDelete) == true)
            {
                RegisteredObject registeredObject = this.m_RegisteredObjects.Get(rootObjectToDelete);
                this.m_RegisteredObjects.Remove(registeredObject);
            }
            this.m_RegisteredRootDeletes.Register(rootObjectToDelete, registeredBy);
        }

        public SubmissionResult SubmitChanges(object objectToSubmit, object registeredBy)
        {
            SubmissionResult result = new SubmissionResult();
            
            SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(objectToSubmit, registeredBy);                
            sqlCommandSubmitter.SubmitChanges();                

            return result;
        }        

        public SubmissionResult SubmitChanges(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, object registeredBy, bool releaseLock)
        {
            SubmissionResult result = new SubmissionResult();
            if(releaseLock == true)
            {
	    		accessionOrder.LockAquiredByHostName = null;
	    		accessionOrder.LockAquiredById = null;
	    		accessionOrder.LockAquiredByUserName = null;
	    		accessionOrder.TimeLockAquired = null;
            }
            
            SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(accessionOrder, registeredBy);                
            sqlCommandSubmitter.SubmitChanges();                

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
            this.m_RegisteredRootInserts.Deregister(objectToSubmit, registeredBy);
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

        public SqlCommandSubmitter GetSqlCommands(object objectToSubmit, object registeredBy)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)objectToSubmit.GetType().GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            Type objectType = objectToSubmit.GetType();

            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToSubmit, null);

            object originalValuesObject = null;
            if(this.m_RegisteredObjects.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredObjects.Get(objectToSubmit);
                originalValuesObject = registeredObjectToSubmit.Value;
	            this.HandleUpdateSubmission(objectToSubmit, originalValuesObject, keyPropertyValue, objectSubmitter);
                ObjectCloner objectCloner = new ObjectCloner();
                object clonedObject = objectCloner.Clone(objectToSubmit);
                registeredObjectToSubmit.Value = (clonedObject);
            }
            else if(this.m_RegisteredRootDeletes.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredRootDeletes.Get(objectToSubmit);
                originalValuesObject = registeredObjectToSubmit.Value;
                this.m_RegisteredRootDeletes.Remove(registeredObjectToSubmit);
	            this.HandleRootDeleteSubmission(originalValuesObject, keyPropertyValue, objectSubmitter);
            }
            else if(this.m_RegisteredRootInserts.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredObjects.Get(objectToSubmit);
	            this.HandleRootInsertSubmission(objectToSubmit, keyPropertyValue, objectSubmitter, registeredBy);
                this.m_RegisteredRootInserts.Remove(registeredObjectToSubmit);
            }
            else
            {
                throw new Exception("The object you requested submission on is not registered.");
            }
            return objectSubmitter;
        }
	}
}
