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

        private YellowstonePathology.Business.Audit.Model.AuditCollection m_AuditCollection;
        private YellowstonePathology.Business.Audit.Model.AuditResult m_AuditResult;
        private PathologistSignoutDialog m_PathologistSignoutDialog;

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

            this.m_AuditCollection = new Business.Audit.Model.AuditCollection();
            this.m_AuditCollection.Add(new Business.Audit.Model.PapCorrelationAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.PqrsAudit(this.m_AccessionOrder));
            this.m_AuditCollection.Add(new Business.Audit.Model.AncillaryStudiesAreHandledAudit(this.m_SurgicalTestOrder));

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
            foreach(YellowstonePathology.Business.Audit.Model.Audit audit in this.m_AuditCollection)
            {
                audit.Run();
                if(audit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    Type auditType = audit.GetType();
                    if(auditType == typeof(YellowstonePathology.Business.Audit.Model.PapCorrelationAudit))
                    {
                        this.m_ActionList.Add(HandlePapCorrelation);
                    }
                    else if (auditType == typeof(YellowstonePathology.Business.Audit.Model.PqrsAudit))
                    {
                        this.m_ActionList.Add(HandlePqrs);
                    }
                    else if(auditType == typeof(YellowstonePathology.Business.Audit.Model.AncillaryStudiesAreHandledAudit))
                    {
                        this.m_ActionList.Add(HandleAncillaryStudies);
                    }
                }
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
            YellowstonePathology.Business.Audit.Model.PapCorrelationAudit papCorrelationAudit = new Business.Audit.Model.PapCorrelationAudit(this.m_AccessionOrder);
            papCorrelationAudit.Run();
            if (papCorrelationAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_SurgicalTestOrder.PapCorrelationRequired = true;
                PapCorrelationPage papCorrelationPage = new PapCorrelationPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
                papCorrelationPage.Next += PapCorrelationPage_Next;
                papCorrelationPage.Back += PapCorrelationPage_Back;
                this.m_PathologistSignoutDialog.PageNavigator.Navigate(papCorrelationPage);
            }
            else
            {
                //this.m_SurgicalTestOrder.PapCorrelationRequired = false;
                //this.m_SurgicalTestOrder.PapCorrelation = 0;
                this.IncrementActionIndex();
                this.InvokeAction(this.m_ActionIndex);
            }
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
            YellowstonePathology.Business.Audit.Model.PqrsAudit pqrsAudit = new Business.Audit.Model.PqrsAudit(this.m_AccessionOrder);
            pqrsAudit.Run();
            if (pqrsAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
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
                            PQRSMeasurePage pqrsMeasurePage = new PQRSMeasurePage(pqrsMeasure, surgicalSpecimen, false);
                            pqrsMeasurePage.Back += new PQRSMeasurePage.BackEventHandler(PQRSMeasurePage_Back);
                            pqrsMeasurePage.AddPQRSCode += new PQRSMeasurePage.AddPQRSCodeEventHandler(PQRSMeasurePage_AddPQRSCode);
                            pqrsMeasurePage.PQRSCodeNotApplicable += new PQRSMeasurePage.PQRSCodeNotApplicableEventHandler(PQRSMeasurePage_PQRSCodeNotApplicable);
                            this.m_PathologistSignoutDialog.PageNavigator.Navigate(pqrsMeasurePage);
                            result = true;
                            break;
                        }
                    }
                    if (result == true) break;
                }
            }
            else
            {
                this.IncrementActionIndex();
                this.InvokeAction(this.m_ActionIndex);
            }
        }

        private void PQRSMeasurePage_PQRSCodeNotApplicable(object sender, EventArgs e)
        {
            //this.m_GoingForward = true;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void PQRSMeasurePage_AddPQRSCode(object sender, CustomEventArgs.AddPQRSReturnEventArgs e)
        {
            this.AddPQRSCode(e.PQRSCode, e.SurgicalSpecimen);
            //this.m_GoingForward = true;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        private void PQRSMeasurePage_Back(object sender, EventArgs e)
        {
            this.m_GoingForward = false;
            this.IncrementActionIndex();
            this.InvokeAction(this.m_ActionIndex);
        }

        protected void AddPQRSCode(YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            if (this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Exists(pqrsCode.Code, 1) == false)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_SurgicalTestOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = 1;
                panelSetOrderCPTCode.CPTCode = pqrsCode.Code;
                panelSetOrderCPTCode.Modifier = pqrsCode.Modifier;
                panelSetOrderCPTCode.CodeableDescription = "PQRS Code";
                panelSetOrderCPTCode.CodeableType = "PQRS";
                panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
                panelSetOrderCPTCode.SpecimenOrderId = surgicalSpecimen.SpecimenOrderId;
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }
        }

        private void HandleAncillaryStudies()
        {
            YellowstonePathology.Business.Audit.Model.AncillaryStudiesAreHandledAudit ancillaryStudiesAreHandledAudit = new Business.Audit.Model.AncillaryStudiesAreHandledAudit(this.m_SurgicalTestOrder);
            ancillaryStudiesAreHandledAudit.Run();
            if (ancillaryStudiesAreHandledAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                YellowstonePathology.Business.Audit.Model.AuditResult auditResult = new Business.Audit.Model.AuditResult();
                auditResult.Message = ancillaryStudiesAreHandledAudit.Message.ToString();
                auditResult.Status = ancillaryStudiesAreHandledAudit.Status;
                PathologistSignoutAuditMessagePage pathologistSignoutAuditMessagePage = new PathologistSignoutAuditMessagePage(auditResult);
                pathologistSignoutAuditMessagePage.Next += PathologistSignoutAuditMessagePage_Next;
                pathologistSignoutAuditMessagePage.Back += PathologistSignoutAuditMessagePage_Back;
                this.m_PathologistSignoutDialog.PageNavigator.Navigate(pathologistSignoutAuditMessagePage);
            }
            else
            {
                this.IncrementActionIndex();
                this.InvokeAction(this.m_ActionIndex);
            }
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
