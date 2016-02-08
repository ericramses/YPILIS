using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class Stack
    {
        private DocumentCollection m_Documents;

        public Stack()
        {
            this.m_Documents = new DocumentCollection();
        }

        public void Push(object o, object writer)
        {
            DocumentId documentId = new DocumentId(o, writer);
            if (this.KeyTypeExists(documentId) == true)
            {
                Document document = this.Get(documentId);
                this.Push(document, writer);
            }
            else
            {
                throw new Exception("You are trying to push an object that is not on the stack.");
            }
        }

        public void Push(object writer)
        {
            foreach(Document document in this.m_Documents)
            {
                this.Push(document, writer);
            }
        }

        private void Push(Document document, object writer)
        {                                    
            document.RemoveWriter(writer);

            if (document.Writers.Count == 0)
            {
                document.ReleaseLock();
                document.Submit();
                this.m_Documents.Remove(document);
            }
            else
            {
                document.Submit();
            }            
        }      

        public Document Pull(DocumentId documentId)
        {
            Document result = null;
            
            if (this.KeyTypeExists(documentId) == true)
            {
                result = this.Get(documentId);
                if (result.Writers.Exists(p => p == documentId.Writer) == false)
                {
                    result.Writers.Add(documentId.Writer);
                }
                this.Push(result, documentId.Writer);
            }              
            else
            {
                result = new Document(documentId);
                this.m_Documents.Add(result);     
            }

            return result;
        }        

        public void InsertDocument(object o, object party, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            DocumentId documentId = new DocumentId(o, party);            

            DocumentInsert documentInsert = new DocumentInsert(documentId);
            documentInsert.SetLock(systemIdentity);
            documentInsert.Submit();

            DocumentUpdate documentUpdate = new DocumentUpdate(documentId);
            this.m_Documents.Add(documentUpdate);
        }

        public void DeleteDocument(object o, object party)
        {
            DocumentId documentId = new DocumentId(o, party);

            //Remove it from the stack.

            DocumentDelete documentDelete = new DocumentDelete(documentId);
            documentDelete.Submit();
        }        

        public Document Get(DocumentId documentId)
        {
            Document result = null;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type == documentId.Type && document.Key == documentId.Key)
                {
                    result = document;
                    break;
                }
            }

            return result;
        }        

        private bool WriterTypeExists(DocumentId documentId)
        {
            bool result = false;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type == documentId.Type && document.Writers.Exists(p => p == documentId.Writer))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool TypeExists(DocumentId documentId)
        {
            bool result = false;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type == documentId.Type)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool KeyTypeExists(DocumentId documentId)
        {
            bool result = false;

            foreach (Document document in this.m_Documents)
            {
                if (document.Key == documentId.Key && document.Type == documentId.Type)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }        

        public bool TypeKeyLockExists(DocumentId documentId)
        {
            bool result = false;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type == documentId.Type                     
                    && document.Key == documentId.Key
                    && documentId.LockAquired == true)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }        
    }
}
