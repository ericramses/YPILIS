﻿using System;
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

        private YellowstonePathology.Business.Audit.Model.AuditCollection m_AuditCollection;
        private YellowstonePathology.Business.Audit.Model.AuditResult m_AuditResult;
        private YellowstonePathology.Business.Audit.Model.PapCorrelationAudit m_PapCorrelationAudit;
        private YellowstonePathology.Business.Audit.Model.PqrsAudit m_PqrsAudit;
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

            this.m_AuditCollection = new Business.Audit.Model.AuditCollection();
            this.m_PapCorrelationAudit = new Business.Audit.Model.PapCorrelationAudit(this.m_AccessionOrder);
            this.m_AuditCollection.Add(this.m_PapCorrelationAudit);
            this.m_PqrsAudit = new Business.Audit.Model.PqrsAudit(this.m_AccessionOrder);
            this.m_AuditCollection.Add(this.m_PqrsAudit);

            this.m_PathologistSignoutDialog = new PathologistSignoutDialog();
        }

        public void Start()
        {
            this.CaseCanBeSignedOut();
            if (this.m_AuditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.HandlePapCorrelation();
                if (this.m_PapCorrelationAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    this.ShowPapCorrelationPage();
                }
                else if (this.m_PqrsAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    this.ShowPQRSMeasurePage();
                }

                if (this.m_AuditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    this.m_PathologistSignoutDialog.ShowDialog();
                }
            }
        }

        private YellowstonePathology.Business.Audit.Model.AuditResult CaseCanBeSignedOut()
        {
            this.m_AuditResult = new Business.Audit.Model.AuditResult();
            this.m_AuditResult.Status = Business.Audit.Model.AuditStatusEnum.OK;

            if (this.m_SurgicalTestOrder.Final == false)
            {
                this.m_AuditResult = this.m_AuditCollection.Run2();
            }
            return this.m_AuditResult;
        }

        private void HandlePapCorrelation()
        {
            if (this.m_PapCorrelationAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_SurgicalTestOrder.PapCorrelationRequired = true;
            }
            else
            {
                this.m_SurgicalTestOrder.PapCorrelationRequired = false;
                this.m_SurgicalTestOrder.PapCorrelation = 0;
            }
        }

        private void ShowPapCorrelationPage()
        {
            PapCorrelationPage papCorrelationPage = new PapCorrelationPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
            papCorrelationPage.Next += PapCorrelationPage_Next;
            papCorrelationPage.Back += PapCorrelationPage_Back;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(papCorrelationPage);
        }

        private void PapCorrelationPage_Next(object sender, EventArgs e)
        {
            if (this.m_PqrsAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.ShowPQRSMeasurePage();
            }
            else
            {
                this.m_PathologistSignoutDialog.Close();
            }
        }

        private void PapCorrelationPage_Back(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        private bool ShowPQRSMeasurePage()
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
                        PQRSMeasurePage pqrsMeasurePage = new PQRSMeasurePage(pqrsMeasure, surgicalSpecimen);
                        pqrsMeasurePage.Cancel += new PQRSMeasurePage.CancelEventHandler(PQRSMeasurePage_Cancel);
                        pqrsMeasurePage.AddPQRSCode += new PQRSMeasurePage.AddPQRSCodeEventHandler(PQRSMeasurePage_AddPQRSCode);
                        pqrsMeasurePage.PQRSCodeNotApplicable += new PQRSMeasurePage.PQRSCodeNotApplicableEventHandler(PQRSMeasurePage_PQRSCodeNotApplicable);
                        this.m_PathologistSignoutDialog.PageNavigator.Navigate(pqrsMeasurePage);
                        result = true;
                        break;
                    }
                }
                if (result == true) break;
            }
            return result;
        }

        private void PQRSMeasurePage_PQRSCodeNotApplicable(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        private void PQRSMeasurePage_AddPQRSCode(object sender, CustomEventArgs.AddPQRSReturnEventArgs e)
        {
            this.AddPQRSCode(e.PQRSCode, e.SurgicalSpecimen);
            this.m_PathologistSignoutDialog.Close();
        }

        private void PQRSMeasurePage_Cancel(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
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
    }
}
