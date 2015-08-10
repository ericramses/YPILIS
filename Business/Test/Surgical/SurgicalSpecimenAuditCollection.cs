using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalSpecimenAuditCollection : ObservableCollection<SurgicalSpecimenAudit>
	{
		public const string PREFIXID = "SSRA";

		public SurgicalSpecimenAuditCollection()
        {
        }

		public SurgicalSpecimenAudit GetNextItem(string surgicalAuditId, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, string amendmentId)
		{
			string surgicalSpecimenAuditId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			SurgicalSpecimenAudit surgicalSpecimenAudit = new SurgicalSpecimenAudit(surgicalSpecimenAuditId, surgicalSpecimenAuditId, surgicalAuditId, surgicalSpecimen, amendmentId);

			return surgicalSpecimenAudit;
		}
	}
}
