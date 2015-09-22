using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Surgical
{
    public class PathologistSignoutPath
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private List<Action> m_ActionList;
        private int m_ActionIndex;
        private bool m_GoingForward;
        private List<string> m_AuditMessages;

        private YellowstonePathology.Business.Audit.Model.AuditCollection m_AuditCollection;
        private YellowstonePathology.Business.Audit.Model.AuditResult m_AuditResult;
        private YellowstonePathology.Business.Audit.Model.AuditResult m_MessageAuditResult;
        private PathologistSignoutDialog m_PathologistSignoutDialog;
        private PqrsSignoutPage m_PqrsSignoutPage;

        public PathologistSignoutPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_SystemIdentity = systemIdentity;

            this.m_ActionList = new List<Action>();
            this.m_ActionIndex = 0;
            this.m_GoingForward = true;
            this.m_AuditMessages = new List<string>();

            this.m_AuditCollection = new Business.Audit.Model.AuditCollection();
            this.m_AuditCollection.Add(new Business.Audit.Model.PapCorrelationAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.PqrsAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.AncillaryStudiesAreHandledAudit(this.m_SurgicalTestOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.IntraoperativeConsultationCorrelationAudit(this.m_SurgicalTestOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.SurgicalCaseHasQuestionMarksAudit(this.m_SurgicalTestOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.SigningUserIsAssignedUserAudit(this.m_SurgicalTestOrder, this.m_SystemIdentity));
            this.m_AuditCollection.Add(new Business.Audit.Model.SvhCaseHasMRNAndAccountNoAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.CaseHasNotFoundClientAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.CaseHasNotFoundProviderAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.CaseHasUnfinaledPeerReviewAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.GradedStainsAreHandledAudit(this.m_SurgicalTestOrder));

            this.m_PathologistSignoutDialog = new PathologistSignoutDialog();
        }

        public void Start()
        {
            if (this.m_AuditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.SetActionList();
                this.m_ActionList[0].Invoke();
                this.m_PathologistSignoutDialog.ShowDialog();
            }
        }

        public YellowstonePathology.Business.Audit.Model.AuditResult CaseCanBeSignedOut()
        {
            this.m_AuditResult = this.m_AuditCollection.Run2();
            return this.m_AuditResult;
        }

        private void SetActionList()
        {
            StringBuilder msg = new StringBuilder();
            this.m_MessageAuditResult = new Business.Audit.Model.AuditResult();
            this.m_MessageAuditResult.Message = string.Empty;

            foreach(YellowstonePathology.Business.Audit.Model.Audit audit in this.m_AuditCollection)
            {
                audit.Run();
                if (audit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    Type auditType = audit.GetType();
                    switch (auditType.FullName)
                    {
                        case "YellowstonePathology.Business.Audit.Model.PapCorrelationAudit":
                            {
                                this.m_ActionList.Add(HandlePapCorrelation);
                                break;
                            }
                        case "YellowstonePathology.Business.Audit.Model.PqrsAudit":
                            {
                                this.m_ActionList.Add(HandlePqrs);
                                break;
                            }
                        default:
                            {
                                this.m_AuditMessages.Add(audit.Message.ToString());
                                this.m_MessageAuditResult.Status = audit.Status;
                                break;
                            }
                    }
                }
            }

            if (this.m_MessageAuditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_MessageAuditResult.Message = msg.ToString();
                this.m_ActionList.Add(HandleAuditMessages);
            }
        }

        private void InvokeAction(int idx)
        {
            if (idx < 0 || idx >= this.m_ActionList.Count)
            {
                this.m_PathologistSignoutDialog.Close();
            }
            else
            {
                this.m_ActionList[idx].Invoke();
            }
        }

        private void IncrementActionIndex()
        {
            if (this.m_GoingForward == true)
            {
                this.m_ActionIndex++;
            }
            else
            {
                this.m_ActionIndex--;
            }
        }

        private void HandlePapCorrelation()
        {
            //this.m_SurgicalTestOrder.PapCorrelationRequired = true;
            PapCorrelationPage papCorrelationPage = new PapCorrelationPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
            papCorrelationPage.Next += PapCorrelationPage_Next;
            papCorrelationPage.Back += PapCorrelationPage_Back;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(papCorrelationPage);
            //this.m_SurgicalTestOrder.PapCorrelationRequired = false;
            //this.m_SurgicalTestOrder.PapCorrelation = 0;
        }

        private void PapCorrelationPage_Next(object sender, EventArgs e)
        {
            this.m_GoingForward = true;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void PapCorrelationPage_Back(object sender, EventArgs e)
        {
            this.m_GoingForward = false;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void HandlePqrs()
        {
            if (this.m_PqrsSignoutPage == null)
            {
                bool result = false;
                YellowstonePathology.Business.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.Business.Surgical.PQRSMeasureCollection.GetAll();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
                {
                    foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                    {
                        int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
                        if (pqrsMeasure.DoesMeasureApply(this.m_SurgicalTestOrder, surgicalSpecimen, patientAge) == true)
                        {
                            this.m_PqrsSignoutPage = new PqrsSignoutPage(pqrsMeasure, surgicalSpecimen, this.m_SurgicalTestOrder, this.m_AccessionOrder, this.m_ObjectTracker);
                            this.m_PqrsSignoutPage.Next += new PqrsSignoutPage.NextEventHandler(PqrsSignoutPage_Next);
                            this.m_PqrsSignoutPage.Back += new PqrsSignoutPage.BackEventHandler(PqrsSignoutPage_Back);
                            this.m_PathologistSignoutDialog.PageNavigator.Navigate(this.m_PqrsSignoutPage);
                            result = true;
                            break;
                        }
                    }
                    if (result == true) break;
                }
            }
            else
            {
                this.m_PathologistSignoutDialog.PageNavigator.Navigate(this.m_PqrsSignoutPage);
            }
        }

        private void PqrsSignoutPage_Next(object sender, EventArgs e)
        {
            this.m_GoingForward = true;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void PqrsSignoutPage_Back(object sender, EventArgs e)
        {
            this.m_GoingForward = false;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void HandleAuditMessages()
        {
            PathologistSignoutAuditMessagePage pathologistSignoutAuditMessagePage = new PathologistSignoutAuditMessagePage(this.m_AuditMessages);
            pathologistSignoutAuditMessagePage.Next += PathologistSignoutAuditMessagePage_Next;
            pathologistSignoutAuditMessagePage.Back += PathologistSignoutAuditMessagePage_Back;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(pathologistSignoutAuditMessagePage);
        }

        private void PathologistSignoutAuditMessagePage_Next(object sender, EventArgs e)
        {
            this.m_GoingForward = true;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void PathologistSignoutAuditMessagePage_Back(object sender, EventArgs e)
        {
            this.m_GoingForward = false;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }
    }
}
