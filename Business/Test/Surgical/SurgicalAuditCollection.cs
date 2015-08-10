using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalAuditCollection : ObservableCollection<SurgicalAudit>
	{
		public const string PREFIXID = "SRA";

		public SurgicalAuditCollection()
        {
		}

		public SurgicalAudit GetNextItem(string amendmentId, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical, int pathologistId, int assignedToId)
		{
			string surgicalAuditId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			SurgicalAudit surgicalAudit = new Surgical.SurgicalAudit(surgicalAuditId, surgicalAuditId, amendmentId, panelSetOrderSurgical, pathologistId, assignedToId);
			return surgicalAudit;
		}

        public void SetAmendmentReference(YellowstonePathology.Business.Amendment.Model.Amendment amendment)
        {
            foreach (SurgicalAudit surgicalAudit in this)
            {
                if (surgicalAudit.AmendmentId == amendment.AmendmentId)
                {
                    surgicalAudit.Amendment = amendment;
                }
            }
        }
	}
}
