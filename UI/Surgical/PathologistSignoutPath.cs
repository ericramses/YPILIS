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

        private YellowstonePathology.Business.Audit.Model.PapCorrelationAudit m_PapCorrelationAudit;
        private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
        private YellowstonePathology.Business.Surgical.PQRSMeasure m_PqrsMeasure;
        private PathologistSignoutDialog m_PathologistSignoutDialog;

        private YellowstonePathology.Business.Audit.Model.AuditCollection m_AuditCollection;
        private bool m_PqrsRequired;

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
            this.m_AuditCollection.Add(new Business.Audit.Model.PapCorrelationAudit(this.m_AccessionOrder));

            this.m_PapCorrelationAudit = new Business.Audit.Model.PapCorrelationAudit(this.m_AccessionOrder);

            this.m_PathologistSignoutDialog = new PathologistSignoutDialog();
        }

        public void Start()
        {
            if (this.m_SurgicalTestOrder.Final == false)
            {
                this.PagesToShow();
                if (this.m_PapCorrelationAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    this.m_SurgicalTestOrder.PapCorrelationRequired = true;
                    this.ShowPapCorrelationPage();
                }
                else
                {
                    this.m_SurgicalTestOrder.PapCorrelationRequired = false;
                    this.m_SurgicalTestOrder.PapCorrelation = 0;

                    if(this.m_PqrsRequired == true)
                    {
                        this.ShowPQRSMeasurePage();
                    }
                }

                if (this.m_PapCorrelationAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure ||
                    this.m_PqrsRequired == true)
                {
                    this.m_PathologistSignoutDialog.ShowDialog();
                }
            }
        }

        private void PagesToShow()
        {
            this.m_PapCorrelationAudit.Run();
            SetPQRSRequired();
        }

        private void SetPQRSRequired()
        {
            this.m_PqrsRequired = false;
            YellowstonePathology.Business.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.Business.Surgical.PQRSMeasureCollection.GetAll();
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                {
                    int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
                    if (pqrsMeasure.DoesMeasureApply(this.m_SurgicalTestOrder, surgicalSpecimen, patientAge) == true)
                    {
                        this.m_SurgicalSpecimen = surgicalSpecimen;
                        this.m_PqrsMeasure = pqrsMeasure;
                        this.m_PqrsRequired = true;
                        break;
                    }
                }
                if (this.m_PqrsRequired == true) break;
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
            if (this.m_PqrsRequired == true)
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

        public void ShowPQRSMeasurePage()
        {
            PQRSMeasurePage pqrsMeasurePage = new PQRSMeasurePage(this.m_PqrsMeasure, this.m_SurgicalSpecimen);
            pqrsMeasurePage.Cancel += new PQRSMeasurePage.CancelEventHandler(PQRSMeasurePage_Cancel);
            pqrsMeasurePage.AddPQRSCode += new PQRSMeasurePage.AddPQRSCodeEventHandler(PQRSMeasurePage_AddPQRSCode);
            pqrsMeasurePage.PQRSCodeNotApplicable += new PQRSMeasurePage.PQRSCodeNotApplicableEventHandler(PQRSMeasurePage_PQRSCodeNotApplicable);
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(pqrsMeasurePage);
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
