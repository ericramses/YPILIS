using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{ 
    public class DocumentId
    {
        private object m_Key;
        private Type m_Type;
        private Object m_Writer;
        private bool m_LockAquired;        

        public DocumentId(object o, object writer)
        {
            this.m_Type = o.GetType();

            PropertyInfo keyProperty = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            this.m_Key = keyProperty.GetValue(o, null);

            this.m_Writer = writer;            

            if (o is YellowstonePathology.Business.Test.AccessionOrder)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)o;
                this.m_LockAquired = accessionOrder.LockAquired;
            }
        }

        public DocumentId(Type type, object writer, object key)
        {
            this.m_Type = type;
            this.m_Key = key;
            this.m_Writer = writer;            
        }

        public object Key
        {
            get { return this.m_Key; }            
        }
        
        public Type Type
        {
            get { return this.m_Type; }            
        }

        public object Writer
        {
            get { return this.m_Writer; }            
        }  
        
        public bool LockAquired
        {
            get { return this.m_LockAquired;  }
        }                  
    }    
}
