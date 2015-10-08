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
            this.Add(new AncillaryStudiesAreHandledAudit(surgicalTestOrder));
            this.Add(new SurgicalCaseHasQuestionMarksAudit(accessionOrder, surgicalTestOrder));
            this.Add(new SigningUserIsAssignedUserAudit(surgicalTestOrder, systemIdentity));
            this.Add(new SvhCaseHasMRNAndAccountNoAudit(accessionOrder));
            this.Add(new CaseHasNotFoundClientAudit(accessionOrder));
            this.Add(new CaseHasNotFoundProviderAudit(accessionOrder));
            this.Add(new CaseHasUnfinaledPeerReviewAudit(accessionOrder));
            this.Add(new GradedStainsAreHandledAudit(surgicalTestOrder));
            this.Add(new IntraoperativeConsultationCorrelationAudit(surgicalTestOrder));
            this.Add(new PapCorrelationAudit(accessionOrder));
            this.Add(new PQRSIsHandledAudit(accessionOrder));
            this.Add(new NonASCIICharacterAudit(surgicalTestOrder));
        }
    }
}
