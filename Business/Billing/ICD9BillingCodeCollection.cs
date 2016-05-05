﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Billing
{
	[XmlType("ICD9BillingCodeCollection")]
    public class ICD9BillingCodeCollection : ObservableCollection<ICD9BillingCode>
	{
		public const string PREFIXID = "ICD";

		public ICD9BillingCodeCollection()
        {

		}

		//WHC remove reportNo when changing to new Billing Accession
		public void UpdateMasterAccessionNo(string masterAccessionNo, string reportNo)
        {
			foreach (ICD9BillingCode icd9BillingCode in this)
            {
                icd9BillingCode.MasterAccessionNo = masterAccessionNo;
				icd9BillingCode.ReportNo = reportNo;
			}
        }

        public ICD9BillingCode GetNextItem(string reportNo, string masterAccessionNo, string specimenOrderId, string icd9Code, string icd10Code, int quantity)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			ICD9BillingCode icd9BillingCode = new ICD9BillingCode(objectId, reportNo, masterAccessionNo, specimenOrderId, icd9Code, icd10Code, objectId, quantity);
			return icd9BillingCode;
		}

        public bool CodeExists(string icd10Code)
        {
            bool result = false;
			foreach (ICD9BillingCode item in this)
            {                
                if (item.ICD10Code == icd10Code)
                {
                    result = true;
                    break;
                }                
            }
            return result;
        }

        public bool PapMedicareCodesExist()
        {
            bool result = false;
            foreach (ICD9BillingCode item in this)
            {
                if (item.ICD9Code == "V76.2" || 
                    item.ICD9Code == "V76.47" || 
                    item.ICD9Code == "V76.49" ||
                    item.ICD9Code == "V72.31" || 
                    item.ICD9Code == "V15.89")
                {
                    result = true;
                    break;                    
                }
            }
            return result;
        }

        public bool DoesCollectionHaveCodes(List<string> icd9CodeList)
        {
            bool result = false;
			foreach (ICD9BillingCode item in this)
            {
                foreach (string icd9Code in icd9CodeList)
                {
                    if (item.ICD9Code == icd9Code)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

		public ICD9BillingCode GetCurrent()
		{
			return this.Count > 0 ? (ICD9BillingCode)this[0] : null;
		}

		public ICD9BillingCode GetCurrent(string icd9BillingCodeId)
		{
			foreach (ICD9BillingCode item in this)
			{
				if (item.Icd9BillingId == icd9BillingCodeId)
				{
					return item;
				}
			}
			return null;
		}

		public ICD9BillingCodeCollection GetSurgicalSpecimenCollection(string surgicalSpecimenId)
		{
			ICD9BillingCodeCollection result = new ICD9BillingCodeCollection();
			foreach (ICD9BillingCode icd9BillingCode in this)
			{
				if (icd9BillingCode.SurgicalSpecimenId == surgicalSpecimenId) result.Add(icd9BillingCode);
			}

			return result;
		}

		public ICD9BillingCodeCollection GetReportCollection(string reportNo)
		{
			ICD9BillingCodeCollection result = new ICD9BillingCodeCollection();
			foreach (ICD9BillingCode icd9BillingCode in this)
			{
				if (icd9BillingCode.ReportNo == reportNo) result.Add(icd9BillingCode);
			}

			return result;
		}
	}
}
