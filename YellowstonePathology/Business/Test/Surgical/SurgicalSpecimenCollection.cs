﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalSpecimenCollection : ObservableCollection<SurgicalSpecimen>
	{
		public const string PREFIXID = "SSR";
		
		public SurgicalSpecimenCollection()
        {

		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string surgicalSpecimenId = element.Element("SurgicalSpecimenId").Value;
                    if (this[i].SurgicalSpecimenId == surgicalSpecimenId)
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

        public bool HasIC()
        {
            bool result = false;
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                if(surgicalSpecimen.IntraoperativeConsultationResultCollection.Count != 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasStainResult(string testOrderId)
        {
            bool result = false;
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
                {
                    if (stainResult.TestOrderId == testOrderId)
                    {
                        result = true;
                        break;
                    }
                }
            }            
            return result;
        }

        public YellowstonePathology.Business.SpecialStain.StainResultItem GetStainResult(string testOrderId)
        {
            YellowstonePathology.Business.SpecialStain.StainResultItem result = null;
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
                {
                    if (stainResult.TestOrderId == testOrderId)
                    {
                        result = stainResult;
                        break;
                    }
                }
            }
            return result;
        }

		public SurgicalSpecimen GetBySurgicalSpecimenId(string surgicalSpecimenId)
		{
			foreach(SurgicalSpecimen surgicalSpecimen in this)
			{
				if (surgicalSpecimen.SurgicalSpecimenId == surgicalSpecimenId)
				{
					return surgicalSpecimen;
				}
			}
			return null;
		}

        public SurgicalSpecimen GetBySpecimenNumber(string specimenNumber)
        {
            SurgicalSpecimen result = null;
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(surgicalSpecimen.SpecimenOrderId, @"([0-9]{2})-([0-9]+).([0-9]+)");
                if(matches.Count > 0)
                {
                    if(matches[0].Groups[3].Value == specimenNumber)
                    {
                        result = surgicalSpecimen;
                        break;
                    }
                }
            }
            return result;
        }

        public bool SpecimenOrderExists(string specimenOrderId)
        {
            bool result = false;
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                if (surgicalSpecimen.SpecimenOrderId == specimenOrderId)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool Exists(string surgicalSpecimenId)
        {
            bool result = false;
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                if (surgicalSpecimen.SurgicalSpecimenId == surgicalSpecimenId)
                {
                    result = true;
                }
            }
            return result;
        }

        public SurgicalSpecimen GetBySpecimenOrderId(string specimenOrderId)
		{
			foreach (SurgicalSpecimen surgicalSpecimen in this)
			{
				if (surgicalSpecimen.SpecimenOrderId == specimenOrderId)
				{
					return surgicalSpecimen;
				}
			}
			return null;
		}

        public SurgicalSpecimen Get(string surgicalSpecimenId)
        {
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                if (surgicalSpecimen.SurgicalSpecimenId == surgicalSpecimenId)
                {
                    return surgicalSpecimen;
                }
            }
            return null;
        }

        public void SetDiagnosisIds()
		{
			List<SurgicalSpecimen> surgicalSpecimenResultItems = (from ssr in this
																			orderby ssr.SurgicalSpecimenId
																			select (SurgicalSpecimen)ssr).ToList<SurgicalSpecimen>();
			int index = 1;
			foreach (SurgicalSpecimen surgicalSpecimen in surgicalSpecimenResultItems)
			{
				surgicalSpecimen.DiagnosisId = index;
				index++;
			}
		}

		public SurgicalSpecimen GetNextItem(string reportNo)
		{
			string surgicalSpecimenId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			SurgicalSpecimen result = new SurgicalSpecimen(reportNo, surgicalSpecimenId);
			return result;
		}

		public SurgicalSpecimen Add(string reportNo)
		{
			SurgicalSpecimen surgicalSpecimen = this.GetNextItem(reportNo);
			this.Add(surgicalSpecimen);
			return surgicalSpecimen;
		}

		public bool ContainsString(string text)
		{
			bool result = false;
			foreach (SurgicalSpecimen surgicalSpecimen in this)
			{
				if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false)
				{
					if (surgicalSpecimen.Diagnosis.ToUpper().Contains(text.ToUpper()) == true)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public void Remove(object obj)
		{
			SurgicalSpecimen item = obj as SurgicalSpecimen;
			if (item != null)
			{
				base.Remove(item);
			}
		}

        public bool HasDuplicateStains()
        {            
            bool result = false;            
            foreach (SurgicalSpecimen surgicalSpecimen in this)
            {
                if (surgicalSpecimen.StainResultItemCollection.HasDuplicates() == true)
                {
                    result = true;
                    break;
                }
            }            
            return result;
        }

		public Collection<YellowstonePathology.Business.SpecialStain.StainResultItem> GetNonMedicareBillableStainResults()
		{
			Collection<YellowstonePathology.Business.SpecialStain.StainResultItem> result = new Collection<YellowstonePathology.Business.SpecialStain.StainResultItem>();
			foreach (SurgicalSpecimen surgicalSpecimen in this)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
				{
					string stainType = stainResult.StainType;
					if (stainType == "Immunohistochemical" || stainType == "Cytolochemical") result.Add(stainResult);
				}
			}
			return result;
		}

		public Collection<YellowstonePathology.Business.SpecialStain.StainResultItem> GetMedicareBillableStainResults()
		{
			Collection<YellowstonePathology.Business.SpecialStain.StainResultItem> result = new Collection<YellowstonePathology.Business.SpecialStain.StainResultItem>();
			List<string> procedureNames = new List<string>();
			foreach (SurgicalSpecimen surgicalSpecimen in this)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
				{
					if (stainResult.StainType == "Immunohistochemical" ||
						stainResult.StainType == "Cytolochemical")
					{
						string procedureName = stainResult.ProcedureName;
						if (procedureNames.Contains(procedureName) == false)
						{
							procedureNames.Add(procedureName);
							result.Add(stainResult);
						}
					}
				}
			}
			return result;
		}        

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }

        public void Sync(DataTable dataTable, string reportNo)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string surgicalSpecimenId = dataTableReader["SurgicalSpecimenId"].ToString();
                string specimenReportNo = dataTableReader["ReportNo"].ToString();

                SurgicalSpecimen surgicalSpecimen = null;

                if (this.Exists(surgicalSpecimenId) == true)
                {
                    surgicalSpecimen = this.Get(surgicalSpecimenId);
                }
                else if (reportNo == specimenReportNo)
                {
                    surgicalSpecimen = new SurgicalSpecimen();
                    this.Add(surgicalSpecimen);
                }

                if (surgicalSpecimen != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(surgicalSpecimen, dataTableReader);
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
                    string surgicalSpecimenId = dataTable.Rows[idx]["SurgicalSpecimenId"].ToString();
                    if (this[i].SurgicalSpecimenId == surgicalSpecimenId)
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
