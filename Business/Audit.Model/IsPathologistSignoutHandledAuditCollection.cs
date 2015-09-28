using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class IsPathologistSignoutHandledAuditCollection : AuditCollection
    {
        public IsPathologistSignoutHandledAuditCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = accessionOrder.PanelSetOrderCollection.GetSurgical();
            this.Add(new Business.Audit.Model.AncillaryStudiesAreHandledAudit(surgicalTestOrder));
            this.Add(new Business.Audit.Model.SurgicalCaseHasQuestionMarksAudit(surgicalTestOrder));
            this.Add(new Business.Audit.Model.SigningUserIsAssignedUserAudit(surgicalTestOrder, systemIdentity));
            this.Add(new Business.Audit.Model.SvhCaseHasMRNAndAccountNoAudit(accessionOrder));
            this.Add(new Business.Audit.Model.CaseHasNotFoundClientAudit(accessionOrder));
            this.Add(new Business.Audit.Model.CaseHasNotFoundProviderAudit(accessionOrder));
            this.Add(new Business.Audit.Model.CaseHasUnfinaledPeerReviewAudit(accessionOrder));
            this.Add(new Business.Audit.Model.GradedStainsAreHandledAudit(surgicalTestOrder));
            this.Add(new Business.Audit.Model.IntraoperativeConsultationCorrelationAudit(surgicalTestOrder));
            this.Add(new Business.Audit.Model.PapCorrelationAudit(accessionOrder));
            this.Add(new Business.Audit.Model.PQRSIsHandledAudit(accessionOrder));
        }
    }
}
