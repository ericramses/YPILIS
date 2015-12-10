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
        public static String Write(object objectToWrite)
        {
            StringBuilder oString = new StringBuilder();
            int indentCount = 0;
            Type objectType = objectToWrite.GetType();
            oString.Append(JSONWriter.Write(objectToWrite, indentCount));
            HandlePersistentChildCollections(objectToWrite, oString, indentCount);
            HandlePersistentChildren(objectToWrite, oString, indentCount);
            indentCount = 0;
            return oString.ToString();
        }        

        private static void HandlePersistentChildCollections(object parentObject, StringBuilder parentStringBuilder, int indentCount)
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
                            collectionStringBuilder.AppendLine();
                            JSONIndenter.AddTabs(collectionStringBuilder, indentCount);
                            collectionStringBuilder.Append("\"" + childCollectionObject.GetType().Name + "\":");

                            collectionStringBuilder.AppendLine();
                            JSONIndenter.AddTabs(collectionStringBuilder, indentCount);
                            collectionStringBuilder.Append("]");

                            indentCount += 1;                            
                        }

                        StringBuilder childStringBuilder = new StringBuilder();
                        object collectionItem = childCollectionObject[i];

                        string json = JSONWriter.Write(collectionItem, indentCount); 
                        childStringBuilder.Append(json);

                        HandlePersistentChildCollections(collectionItem, childStringBuilder, indentCount);
                        HandlePersistentChildren(collectionItem, childStringBuilder, indentCount);
                        collectionStringBuilder.Append(childStringBuilder);

                        if (i == childCollectionObject.Count - 1)
                        {
                            indentCount -= 1;
                            collectionStringBuilder.AppendLine();
                            JSONIndenter.AddTabs(collectionStringBuilder, indentCount);
                            collectionStringBuilder.Append("]");
                            parentStringBuilder.Insert(parentStringBuilder.Length - 1, collectionStringBuilder.ToString());
                        }                       
                    }                    
                }
            }
        }

        private static void HandlePersistentChildren(object parentObject, StringBuilder parentStringBuilder, int indentCount)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();
                List<PropertyInfo> childProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
                indentCount += 1;
                foreach (PropertyInfo childPropertyInfo in childProperties)
                {
                    StringBuilder childStringBuilder = new StringBuilder();
                    SetSeperator(childStringBuilder);
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    SetObjectName(childObject, childStringBuilder, indentCount);
                    string json = JSONWriter.Write(childObject, indentCount);                    
                    childStringBuilder.Append(json);

                    HandlePersistentChildCollections(childObject, childStringBuilder, indentCount);
                    HandlePersistentChildren(childObject, childStringBuilder, indentCount);
                    InsertBeforeEndOfParent(parentStringBuilder, childStringBuilder.ToString());
                }
                indentCount -= 1;
            }
        }                             

        private static void SetObjectName(object o, StringBuilder source, int indentCount)
        {
            JSONIndenter.AddTabs(source, indentCount);
            source.Append("\"" + o.GetType().Name + "\":");
        }

        private static void SetOpenCollectionBracket(StringBuilder source, int indentCount)
        {
            source.Append(" \n");
            JSONIndenter.AddTabs(source, indentCount);
            source.Append("[ \n");
        }

        private static void SetCloseCollectionBracket(StringBuilder source, int indentCount)
        {                        
            source.AppendLine();
            JSONIndenter.AddTabs(source, indentCount);
            source.Append("]");
        }       

        private static void SetSeperator(StringBuilder source)
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
                source.Insert(trimedLength, ", \r\n");
            }
        }

        private static void InsertBeforeEndOfParent(StringBuilder parent, string child)
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
