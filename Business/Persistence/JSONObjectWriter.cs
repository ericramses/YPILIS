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
        private JSONIndenter m_JSONIndenter;

        public JSONObjectWriter()
        {
            this.m_OString = new StringBuilder();
            this.m_JSONIndenter = new JSONIndenter();
        }

        public string Write(object objectToClone)
        {
            Type objectType = objectToClone.GetType();
            object clonedObject = this.CloneThisObject(objectToClone);
            this.m_JSONIndenter.Indent();
            this.HandlePersistentChildCollections(objectToClone, clonedObject);
            this.HandlePersistentChildren(objectToClone, clonedObject);
            this.m_JSONIndenter.Exdent();
            JSONWriter.SetCloseBrace(this.m_OString, this.m_JSONIndenter);
            //return clonedObject;
            return this.m_OString.ToString();
        }

        /*public string JSONString
        {
            get { return this.m_OString.ToString(); }
        }*/

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
                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {
                        if (i == 0)
                        {
                            JSONWriter.SetSeperator(this.m_OString);
                            JSONWriter.SetObjectName(this.m_OString, childCollectionObject, this.m_JSONIndenter);
                            JSONWriter.SetOpenBracket(this.m_OString, this.m_JSONIndenter);
                        }
                        object clonedCollectionItem = this.CloneThisObject(childCollectionObject[i]);

                        clonedCollection.Add(clonedCollectionItem);
                        object collectionItem = childCollectionObject[i];

                        this.m_JSONIndenter.Indent();
                        this.HandlePersistentChildCollections(collectionItem, clonedCollectionItem);
                        this.HandlePersistentChildren(collectionItem, clonedCollectionItem);
                        this.m_JSONIndenter.Exdent();
                        JSONWriter.SetCloseBrace(this.m_OString, this.m_JSONIndenter);
                        if(i == childCollectionObject.Count - 1)
                        {
                            JSONWriter.SetCloseBracket(this.m_OString, this.m_JSONIndenter);
                        }
                        else
                        {
                            JSONWriter.SetSeperator(this.m_OString);
                        }
                    }
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
                    JSONWriter.SetSeperator(this.m_OString);
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    JSONWriter.SetObjectName(this.m_OString, childObject, this.m_JSONIndenter);
                    object clonedObject = this.CloneThisObject(childObject);

                    childPropertyInfo.SetValue(parentObjectClone, clonedObject, null);
                    this.HandlePersistentChildCollections(childObject, clonedObject);
                    this.HandlePersistentChildren(childObject, clonedObject);
                    this.m_JSONIndenter.Exdent();
                    JSONWriter.SetCloseBrace(this.m_OString, this.m_JSONIndenter);
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

            this.m_OString.Append(JSONWriter.Write(clone, this.m_JSONIndenter));
            return clone;
        }
    }
}
