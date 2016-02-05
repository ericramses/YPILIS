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

        public Dictionary<object, object> m_ClonedObjects;
        public Dictionary<object, object> m_Objects;

        static ObjectGatway()
        {

        }
        private ObjectGatway() 
        {
            this.m_Objects = new Dictionary<object, object>();
            this.m_ClonedObjects = new Dictionary<object, object>();
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
            
            if (this.m_Objects.ContainsKey(masterAccessionNo) == true)
            {
                result = (YellowstonePathology.Business.Test.AccessionOrder)this.m_Objects[masterAccessionNo];
                if (result.LockedAquired == false)
                {
                    this.m_Objects.Remove(masterAccessionNo);
                    this.m_ClonedObjects.Remove(masterAccessionNo);
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
            YellowstonePathology.Business.Persistence.SubmissionResult result = sqlCommandSubmitter.SubmitChanges();            

            return result;
        }

        private void RegisterObject(object objectToRegister)
        {			
			ObjectCloner objectCloner = new ObjectCloner();
			object clonedObject = objectCloner.Clone(objectToRegister);

			Type objectType = clonedObject.GetType();
			PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
			object keyPropertyValue = keyProperty.GetValue(clonedObject, null);

			if (this.IsOkToRegister(objectToRegister, objectType, keyPropertyValue) == true)
			{
				this.m_ClonedObjects.Add(keyPropertyValue, clonedObject);
                this.m_Objects.Add(keyPropertyValue, objectToRegister);
			}            
        }		

        private bool IsOkToRegister(object objectToRegister, Type objectType, object keyPropertyValue)
        {
            bool result = false;            

            if (this.m_ClonedObjects.ContainsKey(keyPropertyValue) == false)
            {
                result = true;
            }
            else if (this.m_ClonedObjects.ContainsKey(keyPropertyValue) == true)
            {
                object currentlyRegisteredObjectWithSameKey = this.m_ClonedObjects[keyPropertyValue];
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
            result = this.m_ClonedObjects.ContainsKey(keyPropertyValue);            

            return result;
        }

        private bool IsRegistered(object objectKey, Type objectType)
        {
            bool result = false;            

            foreach(object o in this.m_Objects)
            {                
                if (o.GetType().Name == objectType.Name)
                {
                    PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                    object keyPropertyValue = keyProperty.GetValue(o, null);
                    if(keyPropertyValue == objectKey)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private void Deregister(object objectToDeRegister)
        {
            Type objectType = objectToDeRegister.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(objectToDeRegister, null);
            this.m_ClonedObjects.Remove(keyPropertyValue);
        }        

        //Not tested yet
        public SubmissionResult SubmitRootInsert(object rootObjectToInsert)
        {
            Type objectType = rootObjectToInsert.GetType();
            PersistentClass persistentClassAttribute = (PersistentClass)objectType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(rootObjectToInsert, null);
            SubmissionResult result = this.HandleRootInsertSubmission(rootObjectToInsert, keyPropertyValue, objectSubmitter);
            this.RegisterObject(rootObjectToInsert);
            return result;
        }

        //Not tested yet
        public SubmissionResult SubmitRootDelete(object rootObjectToDelete)
        {
            Type objectType = rootObjectToDelete.GetType();
            PersistentClass persistentClassAttribute = (PersistentClass)objectType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(rootObjectToDelete, null);
            SubmissionResult result = this.HandleRootDeleteSubmission(rootObjectToDelete, keyPropertyValue, objectSubmitter);
            return result;
        }

        private SubmissionResult HandleRootDeleteSubmission(object objectToSubmit, object keyPropertyValue, SqlCommandSubmitter objectSubmitter)
        {
            DeleteCommandBuilder deleteCommandBuilder = new DeleteCommandBuilder();
            deleteCommandBuilder.Build(objectToSubmit, objectSubmitter.SqlDeleteFirstCommands, objectSubmitter.SqlDeleteCommands);
            SubmissionResult result = objectSubmitter.SubmitChanges();
            return result;
        }

        private SubmissionResult HandleRootInsertSubmission(object objectToSubmit, object keyPropertyValue, SqlCommandSubmitter objectSubmitter)
        {
            InsertCommandBuilder insertCommandBuilder = new InsertCommandBuilder();
            insertCommandBuilder.Build(objectToSubmit, objectSubmitter.SqlInsertCommands, objectSubmitter.SqlInsertLastCommands);
            SubmissionResult result = objectSubmitter.SubmitChanges();
            return result;
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
            if (this.m_ClonedObjects.TryGetValue(keyPropertyValue, out registeredObject) == true)
            {
                this.m_ClonedObjects.Remove(keyPropertyValue);
                this.HandleUpdateSubmission(objectToSubmit, registeredObject, keyPropertyValue, objectSubmitter);

                ObjectCloner objectCloner = new ObjectCloner();
                object clonedObject = objectCloner.Clone(objectToSubmit);
                this.m_ClonedObjects.Add(keyPropertyValue, clonedObject);
            }            
            else
            {
                throw new Exception("The object you request submission on is not registered.");
            }
            return objectSubmitter;
        }

        public void RefreshTypingShortcut(YellowstonePathology.Business.Typing.TypingShortcut typingShortcut)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblTypingShortcut where ShortcutId = @ShortcutId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ShortcutId", SqlDbType.Int).Value = typingShortcut.ShortcutId;           

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {                        
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(typingShortcut, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }            

            if(this.IsRegistered(typingShortcut) == false)
            {
                this.RegisterObject(typingShortcut);
            }
        }

        public void RefreshClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblClient where ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = client.ClientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            if (this.IsRegistered(client) == false)
            {
                this.RegisterObject(client);
            }
        }

        public void RefreshPhysician(YellowstonePathology.Business.Domain.Physician physician)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblPhysician where PhysicianId = @PhysicianId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physician.PhysicianId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            if (this.IsRegistered(physician) == false)
            {
                this.RegisterObject(physician);
            }
        }

        public YellowstonePathology.Business.User.UserPreference GetUserPreference()
        {
            string hostName = Environment.MachineName;
            SqlCommand cmd = new SqlCommand("Select * from tblUserPreference where HostName = @HostName");
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@HostName", System.Data.SqlDbType.VarChar).Value = hostName;
            YellowstonePathology.Business.User.UserPreference userPreference = null;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        userPreference = new User.UserPreference();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(userPreference, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        this.RegisterObject(userPreference);
                    }
                }
            }

            return userPreference;
        }

        public YellowstonePathology.Business.ApplicationVersion GetApplicationVersion()
        {
            YellowstonePathology.Business.ApplicationVersion result = new ApplicationVersion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblApplicationVersion";
            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        this.RegisterObject(result);
                    }
                }
            }

            return result;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderByClientOrderId(string clientOrderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrderByClientOrderId";

            SqlParameter clientOrderIdParameter = new SqlParameter("@ClientOrderId", SqlDbType.VarChar, 100);
            clientOrderIdParameter.Value = clientOrderId;
            cmd.Parameters.Add(clientOrderIdParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.ClientOrder.Model.ClientOrder result = null;
            if (this.IsRegistered(clientOrderId, typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrder)))
            {
                result = (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)this.m_Objects[clientOrderId];
                YellowstonePathology.Business.Gateway.ClientOrderBuilder.Build(result, cmd);
            }
            else
            {
                result = new ClientOrder.Model.ClientOrder();
                YellowstonePathology.Business.Gateway.ClientOrderBuilder.Build(result, cmd);
                this.RegisterObject(result);
            }            

            return result;
        }        
    }
}
