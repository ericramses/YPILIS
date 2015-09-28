using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PathologistSignoutAuditCollection : AuditCollection
    {
        public PathologistSignoutAuditCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = accessionOrder.PanelSetOrderCollection.GetSurgical();
            this.Add(new AncillaryStudiesAreHandledAudit(surgicalTestOrder));
            this.Add(new SurgicalCaseHasQuestionMarksAudit(surgicalTestOrder));
            this.Add(new SigningUserIsAssignedUserAudit(surgicalTestOrder, systemIdentity));
            this.Add(new SvhCaseHasMRNAndAccountNoAudit(accessionOrder));
            this.Add(new CaseHasNotFoundClientAudit(accessionOrder));
            this.Add(new CaseHasNotFoundProviderAudit(accessionOrder));
            this.Add(new CaseHasUnfinaledPeerReviewAudit(accessionOrder));
            this.Add(new GradedStainsAreHandledAudit(surgicalTestOrder));
            this.Add(new IntraoperativeConsultationCorrelationAudit(surgicalTestOrder));
            this.Add(new PapCorrelationAudit(accessionOrder));
            this.Add(new PQRSIsRequiredAudit(accessionOrder));
            this.Add(new LynchSyndromeAudit(accessionOrder));
            this.Add(new CCCPAudit(accessionOrder));
            this.Add(new BRAFMetastaticMelanomaAudit(accessionOrder));
        }
    }
}
