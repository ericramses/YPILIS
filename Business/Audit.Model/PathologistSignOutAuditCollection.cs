using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PathologistSignoutAuditCollection : AuditCollection
    {
        public PathologistSignoutAuditCollection()
        { }

        public PathologistSignoutAuditCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
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
            this.Add(new PQRSIsRequiredAudit(accessionOrder));
            this.Add(new NonASCIICharacterAudit(surgicalTestOrder));
            this.Add(new LynchSyndromeAudit(accessionOrder));
            this.Add(new CCCPAudit(accessionOrder));
            this.Add(new BRAFMetastaticMelanomaAudit(accessionOrder));
            this.Add(new HighRiskHPVForSiteAudit(accessionOrder));
            this.Add(new KRASForMetastaticColorectalCancerAudit(accessionOrder));
            this.Add(new PNHOnBoneMarrowSpecimenAudit(accessionOrder));
        }

        public AuditCollection GetAuditMessageCollection()
        {
            AuditCollection result = new AuditCollection();
            foreach(Audit audit in this)
            {
                if(audit.GetType() == typeof(AncillaryStudiesAreHandledAudit) ||
                    audit.GetType() == typeof(SurgicalCaseHasQuestionMarksAudit) ||
                    audit.GetType() == typeof(SigningUserIsAssignedUserAudit) ||
                    audit.GetType() == typeof(SvhCaseHasMRNAndAccountNoAudit) ||
                    audit.GetType() == typeof(CaseHasNotFoundClientAudit) ||
                    audit.GetType() == typeof(CaseHasNotFoundProviderAudit) ||
                    audit.GetType() == typeof(CaseHasUnfinaledPeerReviewAudit) ||
                    audit.GetType() == typeof(GradedStainsAreHandledAudit) ||
                    audit.GetType() == typeof(IntraoperativeConsultationCorrelationAudit))
                {
                    result.Add(audit);
                }
            }
            return result;
        }

        public AuditCollection GetSuggestedTestAuditCollection()
        {
            AuditCollection result = new AuditCollection();
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(LynchSyndromeAudit) ||
                    audit.GetType() == typeof(CCCPAudit) ||
                    audit.GetType() == typeof(BRAFMetastaticMelanomaAudit) ||
                    audit.GetType() == typeof(HighRiskHPVForSiteAudit) ||
                    audit.GetType() == typeof(KRASForMetastaticColorectalCancerAudit) ||
                    audit.GetType() == typeof(PNHOnBoneMarrowSpecimenAudit))
                {
                    result.Add(audit);
                }
            }
            return result;
        }

        public PapCorrelationAudit GetPapCorrelationAudit()
        {
            PapCorrelationAudit result = null;
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(PapCorrelationAudit))
                {
                    result = audit as PapCorrelationAudit;
                }
            }
            return result;
        }

        public PQRSIsRequiredAudit GetPQRSIsRequiredAudit()
        {
            PQRSIsRequiredAudit result = null;
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(PQRSIsRequiredAudit))
                {
                    result = audit as PQRSIsRequiredAudit;
                }
            }
            return result;
        }

        public NonASCIICharacterAudit GetNonASCIICharacterAudit()
        {
            NonASCIICharacterAudit result = null;
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(NonASCIICharacterAudit))
                {
                    result = audit as NonASCIICharacterAudit;
                }
            }
            return result;
        }
    }
}