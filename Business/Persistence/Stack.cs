﻿using System;
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
        
        public DocumentCollection Documents
        {
            get { return this.m_Documents; }
        }

        public void Push(object o, object writer)
        {
            DocumentId documentId = new DocumentId(o, writer);
            if (this.KeyTypeExists(documentId) == true)
            {
                Document document = this.Get(documentId);
                this.PushOne(document, writer);
            }
            else
            {
                throw new Exception("You are trying to push an object that is not on the stack.");
            }
        }        

        public void ReleaseLock(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, object writer)
        {            
            DocumentId documentId = new DocumentId(accessionOrder, writer);
            Document document = this.Get(documentId);

            if (document != null)
            {                
                if (accessionOrder.IsLockAquiredByMe == true)
                {                
                    accessionOrder.ReleaseLock();             
                    document.IsLockAquiredByMe = false;                    
                }
                document.Submit();
            }
            else
            {
                throw new Exception("You are trying to release the lock on a document that is not in the stack.");
            }         
        }        

        public void Flush()
        {
            for (int i = this.m_Documents.Count - 1; i > -1; i--)
            {
                Document document = this.m_Documents[i];
                document.ReleaseLock();
                document.Submit();
                this.m_Documents.Remove(document);
            }
        }        

        public void Clear(object writer)
        {
            for (int i = 0; i < this.m_Documents.Count; i++)
            {
                Document document = this.m_Documents[i];
                if (document.IsGlobal == false)
                {
                    document.RemoveWriter(writer);
                }

                if (document.Writers.Count == 0) this.m_Documents.Remove(document);
            }
        }

        public void Save()
        {
            foreach (Document document in this.m_Documents)
            {
                document.Submit();
            }
        }        

        public void Push(object writer)
        {            
            for (int i = this.m_Documents.Count - 1; i > -1;  i--)
            {
                this.PushOne(this.m_Documents[i], writer);
            }
        }

        private void PushOne(Document document, object writer)
        {                                             
            if (document.WriterExists(writer) == true)
            {                
                document.RemoveWriter(writer);

                if (document.Writers.Count == 0)
                {
                    if(document.IsLockAquiredByMe == true)
                    {
                        document.ReleaseLock();
                    }
                    
                    if (document.IsGlobal == false)
                    {
                        this.m_Documents.Remove(document);
                    }
                }                
            }
            document.Submit();
        }                

        public Document Pull(DocumentId documentId, DocumentBuilder documentBuilder)
        {
            Document document = null;                       

            if (this.KeyTypeExists(documentId) == true)
            {

                if(this.WriterTypeExistsOtherThanThis(documentId) == true)
                {
                    Document otherDocument = this.GetWriterTypeOtherThanThis(documentId);
                    this.PushOne(otherDocument, documentId.Writer);
                }

                document = this.Get(documentId);
                if (document.Writers.Exists(p => p == documentId.Writer) == false)
                {
                    document.Writers.Add(documentId.Writer);
                }                
                
                if(document.Value is YellowstonePathology.Business.Test.AccessionOrder)
                {                    
                    //Save even if the lock is not aquired.
                    if(document.IsDirty() == true)
                    {
                        document.Submit();
                    }
                    else
                    {
                        documentBuilder.Refresh(document.Value);
                        document.ResetClone();

                        Business.Test.AccessionOrder accessionOrder = (Business.Test.AccessionOrder)document.Value;                        
                        document.IsLockAquiredByMe = accessionOrder.IsLockAquiredByMe;
                    }
                }
                else
                {
                    if(documentId.ValueWasPassedIn == true)
                    {
                        Document existingDocument = this.Get(documentId);
                        if (existingDocument.IsDirty() == true) existingDocument.Submit();
                        this.m_Documents.Remove(existingDocument);
                        documentBuilder.Refresh(documentId.Value);
                        document = new DocumentUpdate(documentId);
                        this.m_Documents.Add(document);
                    }
                    else
                    {
                        if (document.IsDirty() == true)
                        {
                            document.Submit();
                        }
                        else
                        {
                            documentBuilder.Refresh(document.Value);
                            document.ResetClone();
                        }
                    }                    
                }
            }   
            else if(this.WriterTypeExists(documentId) == true)
            {
                Document outgoingDocument = this.WriterTypeGet(documentId);
                this.PushOne(outgoingDocument, documentId.Writer);

                if(documentId.ValueWasPassedIn == true)
                {
                    document = new DocumentUpdate(documentId);
                    this.m_Documents.Add(document);
                }
                else
                {
                    object value = documentBuilder.BuildNew();
                    documentId.Value = value;
                    document = new DocumentUpdate(documentId);
                    this.m_Documents.Add(document);
                }                
            }           
            else
            {
                if (documentId.ValueWasPassedIn == true)
                {
                    documentBuilder.Refresh(documentId.Value);                    
                }
                else
                {
                    object value = documentBuilder.BuildNew();
                    documentId.Value = value;
                }

                document = new DocumentUpdate(documentId);
                this.m_Documents.Add(document);

                if (document.Value is YellowstonePathology.Business.Test.AccessionOrder)
                {
                    Business.Test.AccessionOrder accessionOrder = (Business.Test.AccessionOrder)document.Value;
                    document.IsLockAquiredByMe = accessionOrder.IsLockAquiredByMe;
                }
            }


            //if (this.m_Documents.HasMultipleSameAO("16-9590") == true) System.Windows.MessageBox.Show("Stack has multple AO with same MA");
            return document;
        }               

        public void InsertDocument(object o, object writer)
        {
            DocumentId documentId = new DocumentId(o, writer);            

            DocumentInsert documentInsert = new DocumentInsert(documentId);
            documentInsert.SetLock();
            documentInsert.Submit();

            DocumentUpdate documentUpdate = new DocumentUpdate(documentId);
            this.m_Documents.Add(documentUpdate);
        }

        public void DeleteDocument(object o, object writer)
        {
            DocumentId documentId = new DocumentId(o, writer);
            this.m_Documents.Remove(documentId);

            DocumentDelete documentDelete = new DocumentDelete(documentId);
            documentDelete.Submit();
        }        

        public Document Get(DocumentId documentId)
        {
            Document result = null;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type.FullName == documentId.Type.FullName 
                    && document.Key.ToString() == documentId.Key.ToString())
                {
                    result = document;
                    break;
                }
            }

            return result;
        }

        public Document WriterTypeGet(DocumentId documentId)
        {
            Document result = null;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type.FullName == documentId.Type.FullName &&
                    document.Writers.Exists(p => p == documentId.Writer))
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
                if (document.Type.FullName == documentId.Type.FullName && 
                    document.Writers.Exists(p => p == documentId.Writer))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool WriterTypeExistsOtherThanThis(DocumentId documentId)
        {
            bool result = false;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type.FullName == documentId.Type.FullName &&
                    document.Writers.Exists(p => p == documentId.Writer))
                {
                    if(document.Key.ToString() != documentId.Key.ToString())
                    {
                        result = true;
                        break;
                    }                    
                }
            }

            return result;
        }

        private Document GetWriterTypeOtherThanThis(DocumentId documentId)
        {
            Document result = null;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type.FullName == documentId.Type.FullName &&
                    document.Writers.Exists(p => p == documentId.Writer))
                {
                    if (document.Key.ToString() != documentId.Key.ToString())
                    {
                        result = document;
                        break;
                    }
                }
            }

            return result;
        }

        private bool TypeExists(DocumentId documentId)
        {
            bool result = false;

            foreach (Document document in this.m_Documents)
            {
                if (document.Type.FullName == documentId.Type.FullName)
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
                if (document.Key.ToString() == documentId.Key.ToString() && 
                    document.Type.FullName == documentId.Type.FullName)
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
                if (document.Type.FullName == documentId.Type.FullName                     
                    && document.Key.ToString() == documentId.Key.ToString()
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
