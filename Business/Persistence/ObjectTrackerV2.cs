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
        private static ObjectTrackerV2 instance;

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
            //Application.Current.Exit += App_Exit;          
        }

        /*private void App_Exit(object sender, ExitEventArgs e)
        {
            for (int idx = this.m_RegisteredObjects.Count - 1; idx > -1; idx--)
            {
                RegisteredObject registeredObject = this.m_RegisteredObjects[idx];
                object registeredBy = registeredObject.RegisteredBy[0];
                Test.AccessionOrder accessonOrder = (Test.AccessionOrder)registeredObject.Value;
                if (accessonOrder.LockedAquired == true)
                {
                    this.SubmitChanges(accessonOrder, registeredBy, true);
                }
            }
        }*/

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

        public void RegisterRootInsert(object rootObjectToInsert, object registeredBy)
        {
            this.m_RegisteredRootInserts.Register(rootObjectToInsert, registeredBy);
        }

        public void RegisterRootDelete(object rootObjectToDelete, object registeredBy)
        {
			this.m_RegisteredObjects.Deregister(rootObjectToDelete, registeredBy);
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

            object registeredObject = null;
            if(this.m_RegisteredObjects.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredObjects.Get(objectToSubmit);
                registeredObject = registeredObjectToSubmit.Value;
                this.m_RegisteredObjects.Deregister(objectToSubmit, registeredBy);
	            this.HandleUpdateSubmission(objectToSubmit, registeredObject, keyPropertyValue, objectSubmitter);
	            this.RegisterObject(objectToSubmit, registeredBy);
            }
            else if(this.m_RegisteredRootDeletes.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
                RegisteredObject registeredObjectToSubmit = this.m_RegisteredRootDeletes.Get(objectToSubmit);
                registeredObject = registeredObjectToSubmit.Value;
                this.m_RegisteredRootDeletes.Deregister(objectToSubmit, registeredBy);
	            this.HandleRootDeleteSubmission(registeredObject, keyPropertyValue, objectSubmitter);
            }
            else if(this.m_RegisteredRootInserts.IsRegisteredBy(objectToSubmit, registeredBy) == true)
            {
	            this.m_RegisteredRootInserts.Deregister(objectToSubmit, registeredBy);
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
