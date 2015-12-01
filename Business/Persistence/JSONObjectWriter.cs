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
            object clonedObject = this.CloneThisObject(objectToClone);
            this.HandlePersistentChildCollections(objectToClone, clonedObject);
            this.HandlePersistentChildren(objectToClone, clonedObject);
            this.SetEnding();
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
                    this.SetBegining(childCollectionObject.GetType().Name);
                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {
                        object clonedCollectionItem = this.CloneThisObject(childCollectionObject[i]);
                        this.SetEnding();
                        clonedCollection.Add(clonedCollectionItem);
                        object collectionItem = childCollectionObject[i];

                        this.HandlePersistentChildCollections(collectionItem, clonedCollectionItem);
                        this.HandlePersistentChildren(collectionItem, clonedCollectionItem);
                    }
                    this.SetEnding();
                    propertyInfo.SetValue(clonedParent, clonedCollection, null);
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
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    object clonedObject = this.CloneThisObject(childObject);

                    childPropertyInfo.SetValue(parentObjectClone, clonedObject, null);
                    this.HandlePersistentChildCollections(childObject, clonedObject);
                    this.HandlePersistentChildren(childObject, clonedObject);
                    this.SetEnding();
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
            this.SetBegining(objectToClone.GetType().Name);
            this.m_OString.Append(JSONWriter.Write(clone));
            return clone;
        }

        private void SetBegining(string name)
        {
            this.m_OString.AppendLine("{\"" + name + "\": ");
            this.m_OString.Append("\t");
        }

        private void SetEnding()
        {
            this.m_OString.AppendLine("}");
        }
    }
}
