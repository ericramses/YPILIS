﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace YellowstonePathology.Business.Test
{    
    public class PanelSetOrderCPTCodeBillCollection : ObservableCollection<PanelSetOrderCPTCodeBill>
    {
        public const string PREFIXID = "BLL";

        public PanelSetOrderCPTCodeBillCollection()
        {

        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string panelSetOrderCPTCodeBillId = element.Element("PanelSetOrderCPTCodeBillId").Value;
                    if (this[i].PanelSetOrderCPTCodeBillId == panelSetOrderCPTCodeBillId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public PanelSetOrderCPTCodeBill GetNextItem(string reportNo)
        {
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            string panelSetOrderCPTCodeBillId = YellowstonePathology.Business.OrderIdParser.GetNextPanelSetOrderCPTCodeBillId(this, reportNo);
			PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = new PanelSetOrderCPTCodeBill(reportNo, objectId, panelSetOrderCPTCodeBillId);
            return panelSetOrderCPTCodeBill;
        }        

        public bool CPTCodeExists(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill item in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode == item.CPTCode && panelSetOrderCPTCodeBill.Modifier == item.Modifier && panelSetOrderCPTCodeBill.BillTo == item.BillTo)
                {
                    result = true;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill GetExisting(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill result = null;
            foreach (PanelSetOrderCPTCodeBill item in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode == item.CPTCode && panelSetOrderCPTCodeBill.Modifier == item.Modifier && panelSetOrderCPTCodeBill.BillTo == item.BillTo)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }        

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill GetByCPTCode(string cptCode)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill result = null;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode == cptCode)
                {
                    result = panelSetOrderCPTCodeBill;
                    break;
                }
            }
            return result;
        }

        public int GetBilledCount(string cptCode, string modifier)
        {
            int result = 0;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode == cptCode && panelSetOrderCPTCodeBill.Modifier == modifier)
                {
                    result += panelSetOrderCPTCodeBill.Quantity;                    
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill Get(string panelSetOrderCPTCodeBillId)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill result = null;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.PanelSetOrderCPTCodeBillId == panelSetOrderCPTCodeBillId)
                {
                    result = panelSetOrderCPTCodeBill;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection GetMissing(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection shouldBeIncludedOnBillCollection)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection result = new PanelSetOrderCPTCodeBillCollection();
            
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection GetSummaryCollection()
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection result = new PanelSetOrderCPTCodeBillCollection();
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (result.CPTCodeExists(panelSetOrderCPTCodeBill) == false)
                {
                    result.Add(panelSetOrderCPTCodeBill);
                }
                else
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill existing = this.GetExisting(panelSetOrderCPTCodeBill);
                    existing.Quantity = existing.Quantity + panelSetOrderCPTCodeBill.Quantity;
                }
            }
            return result;
        }

        public void Add(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection summaryCollection)
        {
            foreach (PanelSetOrderCPTCodeBill item in summaryCollection)
            {
                PanelSetOrderCPTCodeBill newItem = this.GetNextItem(item.ReportNo);
                newItem.From(item);
                this.Add(newItem);
            }
        }

        public void ReplaceCode(string codeToReplace, string newCode)
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode.ToUpper() == codeToReplace.ToUpper())
                {
                    panelSetOrderCPTCodeBill.CPTCode = newCode;
                }
            }
        }

        public bool HasItemsToSendToPSA()
        {
            bool result = false;           
            if (this.HasBillToPatientByYPIItems() == true) result = true;
            if (this.HasPQRSCodes() == true) result = true;
            return result;
        }

        public bool HasPQRSCodes()
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(panelSetOrderCPTCodeBill.CPTCode, panelSetOrderCPTCodeBill.Modifier);    
                if (cptCode is YellowstonePathology.Business.Billing.Model.PQRSCode == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasClientBillItems()
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {                
                if (panelSetOrderCPTCodeBill.BillTo == "Client")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasBillToPatientByYPIItems()
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.BillTo == "Patient")
                {
                    if (panelSetOrderCPTCodeBill.BillBy != "CLNT")
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool HasMRNStartingWithA()
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (string.IsNullOrEmpty(panelSetOrderCPTCodeBill.MedicalRecord) == false && 
                    panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("A") == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void SetPostDate(Nullable<DateTime> postDate)
        {
            foreach (PanelSetOrderCPTCodeBill item in this)
            {
                if (item.PostDate.HasValue == false)
                {
                    item.PostDate = postDate;
                }
            }
        }

        public bool Exists(string cptCode, string modifier)
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode == cptCode && panelSetOrderCPTCodeBill.Modifier == modifier)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool CPTCodeExists(string cptCode)
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.CPTCode == cptCode)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string panelSetOrderCPTCodeBillId)
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
            {
                if (panelSetOrderCPTCodeBill.PanelSetOrderCPTCodeBillId == panelSetOrderCPTCodeBillId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void ReverseBillByClient(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseOriginal = this.GetNextItem(panelSetOrderCPTCodeBill.ReportNo);
            reverseOriginal.ClientId = panelSetOrderCPTCodeBill.ClientId;
            reverseOriginal.BillTo = panelSetOrderCPTCodeBill.BillTo;
            reverseOriginal.BillBy = panelSetOrderCPTCodeBill.BillBy;
            reverseOriginal.CPTCode = panelSetOrderCPTCodeBill.CPTCode;
            reverseOriginal.Modifier = panelSetOrderCPTCodeBill.Modifier;
            reverseOriginal.Quantity = panelSetOrderCPTCodeBill.Quantity * (-1);
            reverseOriginal.PostDate = DateTime.Today;
            reverseOriginal.MedicalRecord = panelSetOrderCPTCodeBill.MedicalRecord;
            reverseOriginal.Account = panelSetOrderCPTCodeBill.Account;
            this.Add(reverseOriginal);

            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseNew = this.GetNextItem(panelSetOrderCPTCodeBill.ReportNo);
            reverseNew.ClientId = panelSetOrderCPTCodeBill.ClientId;
            reverseNew.BillTo = panelSetOrderCPTCodeBill.BillTo;
            reverseNew.BillBy = "YPIIBLGS";
            reverseNew.CPTCode = panelSetOrderCPTCodeBill.CPTCode;
            reverseNew.Modifier = panelSetOrderCPTCodeBill.Modifier;
            reverseNew.Quantity = panelSetOrderCPTCodeBill.Quantity;
            reverseNew.PostDate = DateTime.Today;
            reverseNew.MedicalRecord = panelSetOrderCPTCodeBill.MedicalRecord;
            reverseNew.Account = panelSetOrderCPTCodeBill.Account;
            this.Add(reverseNew);
        }

        public void ReverseBillTo(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseOriginal = this.GetNextItem(panelSetOrderCPTCodeBill.ReportNo);
            reverseOriginal.ClientId = panelSetOrderCPTCodeBill.ClientId;
            reverseOriginal.BillTo = panelSetOrderCPTCodeBill.BillTo;
            reverseOriginal.BillBy = panelSetOrderCPTCodeBill.BillBy;
            reverseOriginal.CPTCode = panelSetOrderCPTCodeBill.CPTCode;
            reverseOriginal.Modifier = panelSetOrderCPTCodeBill.Modifier;
            reverseOriginal.Quantity = panelSetOrderCPTCodeBill.Quantity * (-1);
            reverseOriginal.PostDate = DateTime.Today;
            reverseOriginal.MedicalRecord = panelSetOrderCPTCodeBill.MedicalRecord;
            reverseOriginal.Account = panelSetOrderCPTCodeBill.Account;
            this.Add(reverseOriginal);

            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseNew = this.GetNextItem(panelSetOrderCPTCodeBill.ReportNo);
            reverseNew.ClientId = panelSetOrderCPTCodeBill.ClientId;
            reverseNew.BillTo = panelSetOrderCPTCodeBill.GetBillToReverse();
            reverseNew.BillBy = panelSetOrderCPTCodeBill.BillBy;
            reverseNew.CPTCode = panelSetOrderCPTCodeBill.CPTCode;
            reverseNew.Modifier = panelSetOrderCPTCodeBill.Modifier;
            reverseNew.Quantity = panelSetOrderCPTCodeBill.Quantity;
            reverseNew.PostDate = DateTime.Today;
            reverseNew.MedicalRecord = panelSetOrderCPTCodeBill.MedicalRecord;
            reverseNew.Account = panelSetOrderCPTCodeBill.Account;
            this.Add(reverseNew);            
        }

        public void PostAsGlobal(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item)
        {                        
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill globalItem = this.GetNextItem(item.ReportNo);
            globalItem.ClientId = item.ClientId;
            globalItem.BillTo = "Patient";
            globalItem.BillBy = item.BillBy;
            globalItem.CPTCode = item.CPTCode;
            globalItem.Modifier = null;
            globalItem.Quantity = item.Quantity;
            globalItem.PostDate = DateTime.Today;
            globalItem.MedicalRecord = item.MedicalRecord;
            globalItem.Account = item.Account;
            this.Add(globalItem);                
        }

        public void Reverse(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill globalItem = this.GetNextItem(item.ReportNo);
            globalItem.ClientId = item.ClientId;
            globalItem.BillTo = item.BillTo;
            globalItem.BillBy = item.BillBy;
            globalItem.CPTCode = item.CPTCode;
            globalItem.Modifier = item.Modifier;
            globalItem.Quantity = item.Quantity * (-1);
            globalItem.PostDate = DateTime.Today;
            globalItem.MedicalRecord = item.MedicalRecord;
            globalItem.Account = item.Account;
            this.Add(globalItem);
        }

        public void AddWithClientId(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item, int clientId)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.GetNextItem(item.ReportNo);
            panelSetOrderCPTCodeBill.ClientId = item.ClientId;
            panelSetOrderCPTCodeBill.BillTo = item.BillTo;
            panelSetOrderCPTCodeBill.BillBy = item.BillBy;
            panelSetOrderCPTCodeBill.CPTCode = item.CPTCode;
            panelSetOrderCPTCodeBill.Modifier = item.Modifier;
            panelSetOrderCPTCodeBill.Quantity = item.Quantity;
            panelSetOrderCPTCodeBill.PostDate = DateTime.Today;
            panelSetOrderCPTCodeBill.ClientId = clientId;
            panelSetOrderCPTCodeBill.MedicalRecord = item.MedicalRecord;
            panelSetOrderCPTCodeBill.Account = item.Account;
            this.Add(panelSetOrderCPTCodeBill);
        }

        public void Post(YellowstonePathology.Business.Test.PanelSetOrderCPTCode item)
        {
            throw new Exception("throw up.");

           /* item.PostDate = DateTime.Today;

            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseOriginal = this.GetNextItem(item.ReportNo);
            reverseOriginal.ClientId = item.ClientId;
            reverseOriginal.BillTo = "Patient";
            reverseOriginal.BillBy = "YPIBLGS";
            reverseOriginal.CPTCode = item.CPTCode;
            reverseOriginal.Modifier = item.Modifier;
            reverseOriginal.Quantity = item.Quantity;
            reverseOriginal.PostDate = DateTime.Today;
            this.Add(reverseOriginal);*/
        }

        public void ImportReverse(DateTime postDate)
        {
            List<PanelSetOrderCPTCodeBill> itemList = this.GetList();
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item in itemList)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseOriginal = this.GetNextItem(item.ReportNo);
                reverseOriginal.ClientId = item.ClientId;
                reverseOriginal.BillTo = item.BillTo;
                reverseOriginal.BillBy = item.BillBy;
                reverseOriginal.CPTCode = item.CPTCode;
                reverseOriginal.Modifier = item.Modifier;
                reverseOriginal.Quantity = item.Quantity * (-1);
                reverseOriginal.PostDate = postDate;
                reverseOriginal.MedicalRecord = item.MedicalRecord;
                reverseOriginal.Account = item.Account;

                this.Add(reverseOriginal);

                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill reverseNew = this.GetNextItem(item.ReportNo);
                reverseNew.ClientId = item.ClientId;
                reverseNew.BillTo = item.GetBillToReverse();
                reverseNew.BillBy = item.BillBy;
                reverseNew.CPTCode = item.CPTCode;
                reverseNew.Modifier = item.Modifier;
                reverseNew.Quantity = item.Quantity;
                reverseNew.PostDate = postDate;
                reverseNew.MedicalRecord = item.MedicalRecord;
                reverseNew.Account = item.Account;
                this.Add(reverseNew);
            }
        }        

        public List<PanelSetOrderCPTCodeBill> GetList()
        {
            List<PanelSetOrderCPTCodeBill> result = new List<PanelSetOrderCPTCodeBill>();
            foreach (PanelSetOrderCPTCodeBill item in this)
            {
                result.Add(item);
            }
            return result;
        }

        public void Unpost()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {                
                if (this[i].PostDate.HasValue == false || this[i].PostDate == DateTime.Today)
                {
                    this.Remove(this[i]);
                }                
            }
        }

        public bool PostedItemsExist()
        {
            bool result = false;
            foreach (PanelSetOrderCPTCodeBill item in this)
            {
                if (item.PostDate.HasValue == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public void Sync(DataTable dataTable, string reportNo)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string panelSetOrderCPTCodeBillId = dataTableReader["PanelSetOrderCPTCodeBillId"].ToString();
                string cptCodeReportBillNo = dataTableReader["ReportNo"].ToString();

                PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = null;

                if (this.Exists(panelSetOrderCPTCodeBillId) == true)
                {
                    panelSetOrderCPTCodeBill = this.Get(panelSetOrderCPTCodeBillId);
                }
                else if (reportNo == cptCodeReportBillNo)
                {
                    panelSetOrderCPTCodeBill = new PanelSetOrderCPTCodeBill();
                    this.Add(panelSetOrderCPTCodeBill);
                }

                if (panelSetOrderCPTCodeBill != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(panelSetOrderCPTCodeBill, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string panelSetOrderCPTCodeBillId = dataTable.Rows[idx]["PanelSetOrderCPTCodeBillId"].ToString();
                    if (this[i].PanelSetOrderCPTCodeBillId == panelSetOrderCPTCodeBillId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }
    }
}
