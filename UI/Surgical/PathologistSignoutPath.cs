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

        private PathologistSignoutDialog m_PathologistSignoutDialog;
        private YellowstonePathology.Business.Rules.Surgical.WordSearchList m_PapCorrelationWordList;

        private bool m_GoingBack;


        public PathologistSignoutPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_SystemIdentity = systemIdentity;

            this.m_PathologistSignoutDialog = new PathologistSignoutDialog();

            this.m_PapCorrelationWordList = new Business.Rules.Surgical.WordSearchList();
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("ECC", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("CERVIX", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("CERVICAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("VAGINAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("ENDOCERVICAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("BLADDER", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("THYROID", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("BREAST", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("GALLBLADDER", false, string.Empty));
        }

        public void Start()
        {
            if (this.ShowPapCorrelationPage() == false)
            {
                this.ShowPathologistSignoutPage1();
            }
            this.m_PathologistSignoutDialog.ShowDialog();
        }

        private bool ShowPapCorrelationPage()
        {
            bool result = false;
            if (this.m_SurgicalTestOrder.Final == false)
            {
                if (this.m_AccessionOrder.SpecimenOrderCollection.FindWordsInDescription(this.m_PapCorrelationWordList) == true)
                {
                    this.m_SurgicalTestOrder.PapCorrelationRequired = true;
                    PapCorrelationPage papCorrelationPage = new PapCorrelationPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
                    papCorrelationPage.Next += PapCorrelationPage_Next;
                    papCorrelationPage.Back += PapCorrelationPage_Back;
                    this.m_PathologistSignoutDialog.PageNavigator.Navigate(papCorrelationPage);
                    result = true;
                }
                else
                {
                    this.m_SurgicalTestOrder.PapCorrelationRequired = false;
                    this.m_SurgicalTestOrder.PapCorrelation = 0;
                }
            }
            return result;
        }

        private void PapCorrelationPage_Next(object sender, EventArgs e)
        {
            this.m_GoingBack = false;
            if (this.HandlePQRS() == false)
            {
                this.ShowPathologistSignoutPage1();
            }
        }

        private void PapCorrelationPage_Back(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        public bool HandlePQRS()
        {
            bool pqrsFound = false;
            YellowstonePathology.UI.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.UI.Surgical.PQRSMeasureCollection.GetAll();
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.UI.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                {
                    pqrsFound = false;
                    int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
                    if (pqrsMeasure.DoesMeasureApply(this.m_SurgicalTestOrder, surgicalSpecimen, patientAge) == true)
                    {
                        PQRSMeasurePage pqrsMeasurePage = new PQRSMeasurePage(pqrsMeasure, surgicalSpecimen);
                        pqrsMeasurePage.Cancel += new PQRSMeasurePage.CancelEventHandler(PQRSMeasurePage_Cancel);
                        pqrsMeasurePage.AddPQRSCode += new PQRSMeasurePage.AddPQRSCodeEventHandler(PQRSMeasurePage_AddPQRSCode);
                        pqrsMeasurePage.PQRSCodeNotApplicable += new PQRSMeasurePage.PQRSCodeNotApplicableEventHandler(PQRSMeasurePage_PQRSCodeNotApplicable);
                        this.m_PathologistSignoutDialog.PageNavigator.Navigate(pqrsMeasurePage);
                        pqrsFound = true;
                        break;
                    }
                }
                if (pqrsFound) break;
            }
            return pqrsFound;
        }

        private void PQRSMeasurePage_PQRSCodeNotApplicable(object sender, EventArgs e)
        {
            this.MoveFromPQRSMeasurePage();
        }

        private void PQRSMeasurePage_AddPQRSCode(object sender, CustomEventArgs.AddPQRSReturnEventArgs e)
        {
            this.AddPQRSCode(e.PQRSCode, e.SurgicalSpecimen);
            this.MoveFromPQRSMeasurePage();
        }

        private void PQRSMeasurePage_Cancel(object sender, EventArgs e)
        {
            this.MoveFromPQRSMeasurePage();
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

        private void MoveFromPQRSMeasurePage()
        {
            if (this.m_GoingBack == true)
            {
                this.ShowPapCorrelationPage();
            }
            else
            {
                this.ShowPathologistSignoutPage1();
            }
        }

        private void ShowPathologistSignoutPage1()
        {
            PathologistSignoutPage1 pathologistSignoutPage1 = new PathologistSignoutPage1(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_ObjectTracker);
            pathologistSignoutPage1.Next += PathologistSignoutPage1_Next;
            pathologistSignoutPage1.Back += PathologistSignoutPage1_Back;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(pathologistSignoutPage1);
        }

        private void PathologistSignoutPage1_Next(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        private void PathologistSignoutPage1_Back(object sender, EventArgs e)
        {
            this.m_GoingBack = true;
            if (this.HandlePQRS() == false)
            {
                if (this.ShowPapCorrelationPage() == false)
                {
                    this.m_PathologistSignoutDialog.Close();
                }
            }
        }
    }
}
