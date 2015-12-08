using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONObjectWriter
    {
        private StringBuilder m_OString;

        public JSONObjectWriter()
        {
            this.m_OString = new StringBuilder();
        }

        public object Write(object objectToWrite)
        {
            Type objectType = objectToWrite.GetType();
            this.m_OString.Append(this.WriteThisObject(objectToWrite));
            this.HandlePersistentChildCollections(objectToWrite, this.m_OString);
            this.HandlePersistentChildren(objectToWrite, this.m_OString);
            return this.m_OString.ToString();
        }

        public string JSONString
        {
            get { return this.m_OString.ToString(); }
        }

        private void HandlePersistentChildCollections(object parentObject, StringBuilder parentStringBuilder)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();

                List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
                foreach (PropertyInfo propertyInfo in childCollectionProperties)
                {
                    StringBuilder collectionStringBuilder = new StringBuilder();
                    IList childCollectionObject = (IList)propertyInfo.GetValue(parentObject, null);                    
                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {
                        if (i == 0)
                        {
                            this.SetSeperator(collectionStringBuilder);
                            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth + 1;
                            this.SetObjectName(childCollectionObject, collectionStringBuilder);
                            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth + 1;
                            this.SetOpenCollectionBracket(collectionStringBuilder);
                            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth + 1;
                        }

                        StringBuilder childStringBuilder = new StringBuilder();
                        object collectionItem = childCollectionObject[i];
                        childStringBuilder.Append(this.WriteThisObject(collectionItem));

                        this.HandlePersistentChildCollections(collectionItem, childStringBuilder);
                        this.HandlePersistentChildren(collectionItem, childStringBuilder);
                        collectionStringBuilder.Append(childStringBuilder);

                        if (i == childCollectionObject.Count - 1)
                        {
                            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth - 1;
                            this.SetCloseCollectionBracket(collectionStringBuilder);
                            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth - 2;
                        }
                        else
                        {
                            this.SetSeperator(collectionStringBuilder);
                        }
                    }
                    this.InsertBeforeEndOfParent(parentStringBuilder, collectionStringBuilder.ToString());
                }
            }
        }

        private void HandlePersistentChildren(object parentObject, StringBuilder parentStringBuilder)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();
                List<PropertyInfo> childProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
                JSONIndenter.IndentDepth = JSONIndenter.IndentDepth + 1;
                foreach (PropertyInfo childPropertyInfo in childProperties)
                {
                    StringBuilder childStringBuilder = new StringBuilder();
                    this.SetSeperator(childStringBuilder);
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    this.SetObjectName(childObject, childStringBuilder);
                    childStringBuilder.Append(this.WriteThisObject(childObject));

                    this.HandlePersistentChildCollections(childObject, childStringBuilder);
                    this.HandlePersistentChildren(childObject, childStringBuilder);
                    this.InsertBeforeEndOfParent(parentStringBuilder, childStringBuilder.ToString());
                }
                JSONIndenter.IndentDepth = JSONIndenter.IndentDepth - 1;
            }
        }
        
        private string WriteThisObject(object objectToWrite)
        {
            return JSONWriter.Write(objectToWrite);
        }

        private void SetObjectName(object o, StringBuilder source)
        {
            JSONIndenter.Indent(source);
            source.Append("\"" + o.GetType().Name + "\":");
        }

        private void SetOpenCollectionBracket(StringBuilder source)
        {
            source.Append(" \n");
            JSONIndenter.Indent(source);
            source.Append("[ \n");
        }

        private void SetCloseCollectionBracket(StringBuilder source)
        {
            JSONIndenter.Indent(source);
            source.Append("] \n");
        }

        private void SetSeperator(StringBuilder source)
        {
            source.Append(", \n");
        }

        private void InsertBeforeEndOfParent(StringBuilder parent, string child)
        {
            string result = parent.ToString();
            int indexOfLastClosingCurlyBrace = result.LastIndexOf("}");
            parent.Insert(indexOfLastClosingCurlyBrace, child);
        }
    }
}
