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
            JSONIndenter.Indent(this.m_OString);
            this.HandlePersistentChildCollections(objectToClone, clonedObject);
            this.HandlePersistentChildren(objectToClone, clonedObject);
            JSONIndenter.Exdent(this.m_OString);
            SetCloseBrace(this.m_OString);
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
                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {
                        if (i == 0)
                        {
                            SetSeperator(this.m_OString);
                            SetObjectName(this.m_OString, childCollectionObject);
                            SetOpenBracket(this.m_OString);
                        }
                        object clonedCollectionItem = this.CloneThisObject(childCollectionObject[i]);

                        clonedCollection.Add(clonedCollectionItem);
                        object collectionItem = childCollectionObject[i];

                        JSONIndenter.Indent(this.m_OString);
                        this.HandlePersistentChildCollections(collectionItem, clonedCollectionItem);
                        this.HandlePersistentChildren(collectionItem, clonedCollectionItem);
                        JSONIndenter.Exdent(this.m_OString);
                        SetCloseBrace(this.m_OString);
                        if(i == childCollectionObject.Count - 1)
                        {
                            SetCloseBracket(this.m_OString);
                        }
                        else
                        {
                            SetSeperator(this.m_OString);
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
                    SetSeperator(this.m_OString);
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    SetObjectName(this.m_OString, childObject);
                    object clonedObject = this.CloneThisObject(childObject);

                    childPropertyInfo.SetValue(parentObjectClone, clonedObject, null);
                    this.HandlePersistentChildCollections(childObject, clonedObject);
                    this.HandlePersistentChildren(childObject, clonedObject);
                    JSONIndenter.Exdent(this.m_OString);
                    SetCloseBrace(this.m_OString);
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

            this.m_OString.Append(JSONWriter.Write(clone));
            return clone;
        }       

        private static void SetCloseBrace(StringBuilder oString)
        {
            oString.Append(" \n\t}");
        }

        private static void SetOpenBracket(StringBuilder oString)
        {
            oString.Append(" \n\t[ \n");
        }

        private static void SetCloseBracket(StringBuilder oString)
        {
            oString.Append(" \n\t]");
        }

        private static void SetObjectName(StringBuilder oString, object o)
        {
            oString.Append("\t\"" + o.GetType().Name + "\":");
        }

        private static void SetSeperator(StringBuilder oString)
        {
            oString.Append(", \n");
        }              
    }      
}
