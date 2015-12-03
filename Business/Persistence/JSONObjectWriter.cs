using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONObjectWriter
    {
        private StringBuilder m_OString;

        public JSONObjectWriter()
        {
            this.m_OString = new StringBuilder();
        }

        public object Write(object objectToClone)
        {
            Type objectType = objectToClone.GetType();
            JSONWriter.SetOpenBrace(this.m_OString);
            object clonedObject = this.CloneThisObject(objectToClone);
            this.HandlePersistentChildCollections(objectToClone, clonedObject);
            this.HandlePersistentChildren(objectToClone, clonedObject);
            //JSONWriter.RemoveLastSeperator(this.m_OString);
            JSONWriter.SetCloseBrace(this.m_OString);
            return clonedObject;
        }

        public string JSONString
        {
            get { return this.m_OString.ToString(); }
        }

        private void HandlePersistentChildCollections(object parentObject, object clonedParent)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();

                List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
                foreach (PropertyInfo propertyInfo in childCollectionProperties)
                {
                    IList childCollectionObject = (IList)propertyInfo.GetValue(parentObject, null);                    
                    IList clonedCollection = (IList)Activator.CreateInstance(childCollectionObject.GetType());
                    JSONWriter.SetSeperator(this.m_OString);
                    JSONWriter.SetObjectName(this.m_OString, childCollectionObject);
                    JSONWriter.SetOpenBracket(this.m_OString);
                    JSONWriter.SetOpenBrace(this.m_OString);
                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {
                        object clonedCollectionItem = this.CloneThisObject(childCollectionObject[i]);
                        clonedCollection.Add(clonedCollectionItem);
                        object collectionItem = childCollectionObject[i];

                        this.HandlePersistentChildCollections(collectionItem, clonedCollectionItem);
                        this.HandlePersistentChildren(collectionItem, clonedCollectionItem);
                        if(i < childCollectionObject.Count - 1)
                        {
                            JSONWriter.SetSeperator(this.m_OString);
                        }
                    }
                    propertyInfo.SetValue(clonedParent, clonedCollection, null);
                    JSONWriter.SetCloseBrace(this.m_OString);
                    JSONWriter.SetCloseBracket(this.m_OString);
                }
            }
        }

        private void HandlePersistentChildren(object parentObject, object parentObjectClone)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();
                List<PropertyInfo> childProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
                foreach (PropertyInfo childPropertyInfo in childProperties)
                {
                    JSONWriter.SetSeperator(this.m_OString);
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    object clonedObject = this.CloneThisObject(childObject);

                    childPropertyInfo.SetValue(parentObjectClone, clonedObject, null);
                    this.HandlePersistentChildCollections(childObject, clonedObject);
                    this.HandlePersistentChildren(childObject, clonedObject);
                }
            }
        }

        private object CloneThisObject(object objectToClone)
        {
            if (objectToClone == null) return null;            

            object clone = Activator.CreateInstance(objectToClone.GetType());

            PropertyInfo keyPersistentProperty = objectToClone.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPersistentPropertyValue = keyPersistentProperty.GetValue(objectToClone, null);
            keyPersistentProperty.SetValue(clone, keyPersistentPropertyValue, null);                

            PropertyInfo[] persistentProperties = objectToClone.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToArray();
            foreach (PropertyInfo persistentProperty in persistentProperties)
            {
                object persistentPropertyValue = persistentProperty.GetValue(objectToClone, null);
                persistentProperty.SetValue(clone, persistentPropertyValue, null);                
            }
            JSONWriter.SetObjectName(this.m_OString, clone);
            this.m_OString.Append(JSONWriter.Write(clone));
            return clone;
        }
    }
}
