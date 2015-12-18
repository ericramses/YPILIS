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
            Type objectType = objectToWrite.GetType();
            oString.Append(JSONWriter.Write(objectToWrite));
            HandlePersistentChildCollections(objectToWrite, oString);            
            return oString.ToString();
        }        

        private static void HandlePersistentChildCollections(object parentObject, StringBuilder parentStringBuilder)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();

                List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
                for (int j=0; j<childCollectionProperties.Count; j++)
                {
                    IList childCollectionObject = (IList)childCollectionProperties[j].GetValue(parentObject, null);
                    StringBuilder collectionStringBuilder = new StringBuilder(", \"" + childCollectionObject.GetType().Name + "\":[");                     

                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {                        
                        object collectionItem = childCollectionObject[i];
                        StringBuilder childStringBuilder = new StringBuilder(JSONWriter.Write(collectionItem));                        
                        HandlePersistentChildCollections(collectionItem, childStringBuilder);
                        collectionStringBuilder.Append(childStringBuilder);

                        if (i < childCollectionObject.Count - 1)
                        {
                            collectionStringBuilder.Append(", ");
                        }                         
                    }

                    if(j == childCollectionObject.Count - 1)
                    {
                        collectionStringBuilder.Append("]");                        
                    }
                    else
                    {
                        collectionStringBuilder.Append("], ");
                    }
                    parentStringBuilder.Insert(parentStringBuilder.Length - 1, collectionStringBuilder.ToString());
                }                
            }
        }                                                
    }
}
