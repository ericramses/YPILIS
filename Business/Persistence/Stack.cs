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
        private ObservableCollection<Document> m_Documents;

        public Stack()
        {
            this.m_Documents = new ObservableCollection<Document>();
        }

        public void Push(object documentToPush, object party)
        {
            
        }

        public Document Pull(DocumentId documentId)
        {
            Document result = null;

            if (this.KeyTypeExists(documentId) == true)
            {
                result = this.Get(documentId);
                if(result.Writers.Exists(p => p == documentId.Writer) == false)
                {
                    result.Writers.Add(documentId.Writer);
                }
            }
            else
            {
                result = new Document(documentId);
                this.m_Documents.Add(result);     
            }

            return result;
        }

        public void InsertDocument(object o, object party)
        {
            DocumentId documentId = new DocumentId(o, party);

            //Need to get the lock if it is an accessionorder            

            DocumentInsert documentInsert = new DocumentInsert(documentId);
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
