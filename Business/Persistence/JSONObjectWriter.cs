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
            JSONIndenter.IndentDepth = 1;
            Type objectType = objectToWrite.GetType();
            this.m_OString.Append(this.WriteThisObject(objectToWrite));
            this.HandlePersistentChildCollections(objectToWrite, this.m_OString);
            this.HandlePersistentChildren(objectToWrite, this.m_OString);
            JSONIndenter.IndentDepth = 1;
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
                            this.InsertBeforeEndOfParent(parentStringBuilder, collectionStringBuilder.ToString());
                        }
                        else
                        {
                            this.SetSeperator(collectionStringBuilder);
                        }
                    }
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
            JSONIndenter.IndentDepth = JSONIndenter.IndentDepth - 2;
            JSONIndenter.Indent(source);
        }

        private void SetSeperator(StringBuilder source)
        {
            string stringToTrim = source.ToString();
            int initialLength = stringToTrim.Length;
            string trimedString = stringToTrim.TrimEnd();
            int trimedLength = trimedString.Length;
            string whiteSpaceString = stringToTrim.Substring(trimedLength, initialLength - trimedLength);
            if (whiteSpaceString.Contains("\n") == true)
            {
                source.Insert(trimedLength, ",");
            }
            else
            {
                source.Insert(trimedLength, ", \n");
            }
        }

        private void InsertBeforeEndOfParent(StringBuilder parent, string child)
        {
            string parentString = parent.ToString();
            int indexOfLastCurlyBrace = parentString.LastIndexOf("}");
            string stringToTrim = parentString.Substring(0, indexOfLastCurlyBrace);
            string trimedString = stringToTrim.TrimEnd();
            int trimedLength = trimedString.Length;
            parent.Insert(trimedLength, child);
            string result = parent.ToString();
        }
    }
}
