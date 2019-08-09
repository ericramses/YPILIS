using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
    class WomensHealthProfileEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public WomensHealthProfileEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            WomensHealthProfileTestOrder womensHealthProfileTestOrder = (WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            WomensHealthProfileResult womensHealthProfileResult = new WomensHealthProfileResult(this.m_AccessionOrder);

            ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new ThinPrepPap.ThinPrepPapTest();
            bool hasPap = this.m_AccessionOrder.PanelSetOrderCollection.Exists(thinPrepPapTest.PanelSetId);

            this.AddHeader(document, womensHealthProfileTestOrder, "Women's Health Profile");

            if (hasPap == true)
            {
                this.AddNextObxElementBeaker("PAPTESTRESULT", "PAP TEST RESULT: ", document, "F");

                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
                if (string.IsNullOrEmpty(panelSetOrderCytology.ScreeningImpression) == false)
                {
                    this.AddNextObxElementBeaker("EPITHELIALCELLDESCRIPTION", "Epithelial Cell Description: " + panelSetOrderCytology.ScreeningImpression, document, "F");
                }

                this.AddNextObxElementBeaker("SPECIMENADEQUACY", "Specimen Adequacy: " + panelSetOrderCytology.SpecimenAdequacy, document, "F");

                if (string.IsNullOrEmpty(panelSetOrderCytology.OtherConditions) == false)
                {
                    this.AddNextObxElementBeaker("OTHERCONDITIONS", "Other Conditions: " + panelSetOrderCytology.OtherConditions, document, "F");
                }

                if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
                {
                    this.AddNextObxElementBeaker("COMMENT", "Comment: " + panelSetOrderCytology.ReportComment, document, "F");
                }

                YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology screeningPanelOrder = null;
                YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology reviewPanelOrder = null;

                foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in panelSetOrderCytology.PanelOrders)
                {
                    Type objectType = panelOrder.GetType();
                    if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                    {
                        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                        if (cytologyPanelOrder.PanelId == 38)
                        {
                            if (cytologyPanelOrder.ScreeningType == "Primary Screening")
                            {
                                screeningPanelOrder = cytologyPanelOrder;
                            }
                            else if (cytologyPanelOrder.ScreeningType == "Pathologist Review")
                            {
                                reviewPanelOrder = cytologyPanelOrder;
                            }
                            else if (cytologyPanelOrder.ScreeningType == "Cytotech Review")
                            {
                                if (reviewPanelOrder == null || reviewPanelOrder.ScreeningType != "Pathologist Review")
                                {
                                    reviewPanelOrder = cytologyPanelOrder;
                                }
                            }
                        }
                    }
                }

                YellowstonePathology.Business.User.SystemUser systemUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(screeningPanelOrder.ScreenedById);
                if (string.IsNullOrEmpty(systemUser.Signature) == false)
                {
                    this.AddNextObxElementBeaker("SCREENEDBY", "Screened By: " + systemUser.Signature, document, "F");
                }

                string cytoTechFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(screeningPanelOrder.AcceptedDate);
                this.AddNextObxElementBeaker("FINALDATE", "E-Signed " + cytoTechFinal, document, "F");

                if (reviewPanelOrder != null)
                {
                    string reviewedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
                    string reviewedByFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

                    if (reviewedBy.IndexOf("MD") >= 0)
                    {
                        this.AddNextObxElementBeaker("INTERPRETEDBY", "Interpreted By: " + reviewedBy + " " + reviewedByFinal, document, "F");
                    }
                    else
                    {
                        this.AddNextObxElementBeaker("REVIEWEDBY", "Reviewed By: " + reviewedBy + " " + reviewedByFinal, document, "F");
                    }
                }
            }

            this.AddAmendments(document);

            this.AddNextObxElementBeaker("CURRENTMOLECULARTESTSUMMARY", "CURRENT MOLECULAR TEST SUMMARY", document, "F");

            YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
            YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
            {
                this.AddNextObxElementBeaker("NOTESTSPERFORMED", "No tests performed ", document, "F");
            }
            else
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
                    this.AddNextObxElementBeaker("HPVRESULT", "High Risk HPV: " + hpvTestOrder.Result, document, "F");
                    this.AddNextObxElementBeaker("HPVRESULTREFERENCE", "Reference: Negative", document, "F");
                    string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(hpvTestOrder.FinalDate);
                    this.AddNextObxElementBeaker("HPVFINALDATE", "Date Finalized: " + hpvFinal, document, "F");
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
                    this.AddNextObxElementBeaker("HPV16RESULT", "HPV type 16: " + panelSetOrderHPV1618.HPV16Result, document, "F");
                    this.AddNextObxElementBeaker("HPV16RESULTREFERENCE", "Reference: Negative", document, "F");

                    this.AddNextObxElementBeaker("HPV18RESULT", "HPV type 18: " + panelSetOrderHPV1618.HPV18Result, document, "F");
                    this.AddNextObxElementBeaker("HPV18RESULTREFERENCE", "Reference: Negative", document, "F");
                    string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderHPV1618.FinalDate);
                    this.AddNextObxElementBeaker("HPV1618FINALDATE", "Date Finalized: " + hpvFinal, document, "F");
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
                {
                    //this.AddNextObxElementBeaker("Chlamydia Gonorrhea Screening ", document, "F");
                    YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
                    this.AddNextObxElementBeaker("CHLAMYDIATRACHOMATISRESULT", "Chlamydia trachomatis: " + panelSetOrderNGCT.ChlamydiaTrachomatisResult, document, "F");
                    this.AddNextObxElementBeaker("CHLAMYDIATRACHOMATISRESULTREFERENCE", "Reference: Negative", document, "F");

                    this.AddNextObxElementBeaker("NEISSERIAGONORROEAERESULT", "Neisseria gonorrhoeae: " + panelSetOrderNGCT.NeisseriaGonorrhoeaeResult, document, "F");
                    this.AddNextObxElementBeaker("NEISSERIAGONORROEARESULTREFERENCE", "Reference: Negative", document, "F");
                    string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderNGCT.FinalDate);
                    this.AddNextObxElementBeaker("NGCTFINALDATE", "Date Finalized: " + hpvFinal, document, "F");
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
                {
                    //this.AddNextObxElementBeaker("Trichomonas Screening ", document, "F");
                    YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
                    this.AddNextObxElementBeaker("TRICHOMONASVAGINALISRESULT", "Trichomonas vaginalis: " + reportOrderTrichomonas.Result, document, "F");
                    this.AddNextObxElementBeaker("TRICHOMONASVAGINALISRESULTReference", "Reference: Negative", document, "F");
                    string hpvFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reportOrderTrichomonas.FinalDate);
                    this.AddNextObxElementBeaker("TRICHOMONASVAGINALISFINALDATE", "Date Finalized: " + hpvFinal, document, "F");
                }
            }

            this.AddNextObxElementBeaker("SPECIMENDESCRIPTION", "Specimen Description: Thin Prep Fluid", document, "F");
            this.AddNextObxElementBeaker("SPECIMENSOURCE", "Specimen Source: " + this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenSource, document, "F");
            string collectionDateTimeString = this.m_AccessionOrder.SpecimenOrderCollection[0].GetCollectionDateTimeString();
            this.AddNextObxElementBeaker("COLLECTIONDATETIME", "Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElementBeaker("CLINICALHISTORY", "Clinical History: ", document, "F");
            this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");

            this.AddNextObxElementBeaker("METHOD", "Method: ", document, "F");
            this.HandleLongString(womensHealthProfileResult.Method, document, "F");

            this.AddNextObxElementBeaker("REFERENCES", "References: ", document, "F");
            this.HandleLongString(womensHealthProfileResult.References, document, "F");

            string locationPerformed = womensHealthProfileTestOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", "Location Performed: " + locationPerformed, document, "F");

            this.AddNextObxElementBeaker("PRIORPAPANDMOLECULARTESTS", "PRIOR PAP AND GYN MOLECULAR TESTS", document, "F");
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            DateTime cutoffDate = this.m_AccessionOrder.AccessionDate.Value.AddYears(-5);

            YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
            YellowstonePathology.Business.Domain.PatientHistory priorPapRelatedHistory = patientHistory.GetPriorPapRelatedHistory(this.m_AccessionOrder.MasterAccessionNo, cutoffDate);

            if (priorPapRelatedHistory.Count == 0)
            {
                this.AddNextObxElementBeaker("NOPRIORPAPANDMOLECULARTESTSPERFORMED", "No prior tests performed ", document, "F");
            }
            else
            {
                int testCount = 0;
                foreach (YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult in priorPapRelatedHistory)
                {
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(patientHistoryResult.MasterAccessionNo);
                    foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                    {
                        string reportNo = null;
                        string result = null;

                        if (panelSetOrder.PanelSetId == panelSetThinPrepPap.PanelSetId ||
                            panelSetOrder.PanelSetId == panelSetHPV.PanelSetId ||
                            panelSetOrder.PanelSetId == panelSetHPV1618.PanelSetId ||
                            panelSetOrder.PanelSetId == panelSetNGCT.PanelSetId ||
                            panelSetOrder.PanelSetId == panelSetTrichomonas.PanelSetId)
                        {
                            testCount++;
                            reportNo = panelSetOrder.ReportNo;
                            string finaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrder.FinalDate);
                            result = panelSetOrder.GetResultWithTestName();
                            this.AddNextObxElementBeaker("PRIORTEST" + testCount, "Test: " + panelSetOrder.PanelSetName + " Report No: " + reportNo + " Result: " + result + " Final Date: " + finaldate, document, "F");
                        }
                    }
                }
            }
        }
    }
}
