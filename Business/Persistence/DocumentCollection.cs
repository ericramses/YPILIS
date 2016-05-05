﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentCollection : ObservableCollection<Document>
    {
        public void Remove(DocumentId documentId)
        {
            for(int i=0; i< this.Count; i++)
            {
                Document document = this[i];
                if(documentId.Type.FullName == document.Type.FullName &&
                    documentId.Key.ToString() == document.Key.ToString())
                {
                    this.Remove(document);
                    break;
                }
            }
        }

        public bool HasMultipleSameAO(string masterAccessionNo)
        {
            bool result = false;
            int count = 0;
            foreach(Document document in this)
            {
                if(document.Key.ToString() == masterAccessionNo)
                {
                    count += 1;
                }
            }
            if (count > 1) result = true;
            return result;
        }
    }
}
