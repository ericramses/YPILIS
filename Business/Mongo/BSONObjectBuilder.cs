using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using YellowstonePathology.Business.Persistence;
using System.Reflection;

namespace YellowstonePathology.Business.Mongo
{
    public class BSONObjectBuilder
    {        
        public static object Build(BsonDocument bsonDocument, Type objectType)
        {                        
            object result = Activator.CreateInstance(objectType);
            YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, result);
            BuildChildCollections(bsonDocument, result);
            return result;
        }   
     
        private static void BuildChildCollections(BsonDocument bsonParent, object objectParent)
        {
            Type parentObjectType = objectParent.GetType();
            List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {                
                Type collectionType = propertyInfo.PropertyType;
                Type itemType = collectionType.BaseType.GetGenericArguments()[0];

                object collectionObject = Activator.CreateInstance(collectionType);
                IList castedResult = (IList)collectionObject;  

                BsonArray childCollectionArray = bsonParent[propertyInfo.Name].AsBsonArray;
                foreach (BsonDocument childBsonDocument in childCollectionArray)
                {
                    object itemObject = Activator.CreateInstance(itemType);
                    BSONPropertyWriter.Write(childBsonDocument, itemObject);
                    castedResult.Add(itemObject);
                }

                propertyInfo.SetValue(objectParent, collectionObject, null);
            }            
        }
    }
}


/*
            for(int i=0; i< bsonParent.ElementCount; i++)
            {
                if(bsonParent[i].IsBsonArray == true)                
                {
                    //object collectionObject = Activator.CreateInstance("sdf");
                    //IList castedResult = (IList)result;  

                    BsonArray childCollection = bsonParent[i].AsBsonArray;
                    foreach (BsonDocument childBsonDocument in childCollection)
                    {
                        string assemblyQualifiedName = childBsonDocument.GetValue("AssemblyQualifiedName").ToString();
                        Type childType = Type.GetType(assemblyQualifiedName);
                        object childObject = Activator.CreateInstance(childType);
                        BSONPropertyWriter.Write(childBsonDocument, childObject);
                    }
                }                
            }
            */
