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
                            indentCount += 1;

                            JSONIndenter.AddTabs(collectionStringBuilder, indentCount);
                            collectionStringBuilder.Append("\"" + childCollectionObject.GetType().Name + "\":");

                            collectionStringBuilder.AppendLine();
                            JSONIndenter.AddTabs(collectionStringBuilder, indentCount);
                            collectionStringBuilder.Append("[");

                            indentCount += 1;                            
                        }

                        StringBuilder childStringBuilder = new StringBuilder();
                        object collectionItem = childCollectionObject[i];

                        string json = JSONWriter.Write(collectionItem, indentCount); 
                        childStringBuilder.Append(json);                        
                        HandlePersistentChildCollections(collectionItem, childStringBuilder, indentCount);

                        collectionStringBuilder.AppendLine();                     
                        collectionStringBuilder.Append(childStringBuilder);

                        if (i == childCollectionObject.Count - 1)
                        {
                            indentCount -= 1;                            
                            JSONIndenter.AddTabs(collectionStringBuilder, indentCount);
                            collectionStringBuilder.Append("]");
                            parentStringBuilder.Insert(parentStringBuilder.Length - 1, collectionStringBuilder.ToString());
                            indentCount -= 1;
                        }                       
                    }                    
                }
            }
        }                                                
    }
}
