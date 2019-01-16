using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSERuleCollection : ObservableCollection<LSERule>
	{
		public LSERuleCollection()
		{
			
		}

        public static LSERuleCollection GetMatchCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleResults result = new LynchSyndrome.LSERuleResults(LSERuleCollection.GetAll(), true);
            result = result.LSERuleCollection.GetIndicationCollection(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            if(result.AbleToContinue == true) result = result.LSERuleCollection.GetIHCMatchCollection(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            if (result.AbleToContinue == true) result = result.LSERuleCollection.GetBRAFMatchCollection(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            if (result.AbleToContinue == true) result = result.LSERuleCollection.GetMethMatchCollection(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            return result.LSERuleCollection;
        }

        private LSERuleResults GetIndicationCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection lseRuleCollection = new LynchSyndrome.LSERuleCollection();
            if (panelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType != LSEType.NOTSET)
            {
                foreach (LSERule lseRule in this)
                {
                    if (lseRule.Indication == panelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType)
                    {
                        lseRuleCollection.Add(lseRule);
                    }
                }
            }

            LSERuleResults result = SetResult(lseRuleCollection);
            return result;
        }

        private LSERuleResults GetIHCMatchCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection lseRuleCollection = new LynchSyndrome.LSERuleCollection();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderLynchSyndromeIHC.Final == true)
                {
                    IHCResult ihcResult = panelSetOrderLynchSyndromeIHC.GetSummaryResult();
                    foreach (LSERule lseRule in this)
                    {
                        if (lseRule.IsIHCMatch(ihcResult) == true)
                        {
                            lseRuleCollection.Add(lseRule);
                        }
                    }
                }
            }

            LSERuleResults result = SetResult(lseRuleCollection);
            return result;
        }

        private LSERuleResults GetBRAFMatchCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection lseRuleCollection = new LSERuleCollection();
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnalysisTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            string brafResult = TestResult.NotApplicable;
            if (accessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderBraf.Final == true) brafResult = panelSetOrderBraf.GetSummaryResult();
            }
            else if (accessionOrder.PanelSetOrderCollection.Exists(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (brafMutationAnalysisTestOrder.Final == true) brafResult = brafMutationAnalysisTestOrder.GetSummaryResult();
            }
            else if (accessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderRASRAF.Final == true) brafResult = panelSetOrderRASRAF.GetBrafSummaryResult();
            }

            foreach (LSERule lseRule in this)
            {
                if ((lseRule.BRAFRequired == true || lseRule.MethRequired == true) && lseRule.BRAFResult == brafResult)
                {
                    lseRuleCollection.Add(lseRule);
                }
            }

            LSERuleResults result = SetResult(lseRuleCollection);
            return result;
        }

        private LSERuleResults GetMethMatchCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection lseRuleCollection = new LSERuleCollection();
            string methResult = null;
            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderMLH1MethylationAnalysis.Final == true)
                {
                    methResult = panelSetOrderMLH1MethylationAnalysis.GetSummaryResult();
                    foreach (LSERule lseRule in this)
                    {
                        if (lseRule.MethRequired == true && lseRule.MethResult == methResult)
                        {
                            lseRuleCollection.Add(lseRule);
                        }
                    }
                }
            }

            LSERuleResults result = SetResult(lseRuleCollection);
            return result;
        }

        private LSERuleResults SetResult(LSERuleCollection lseRuleCollection)
        {
            LSERuleResults result = new LSERuleResults();
            if (lseRuleCollection.Count > 0)
            {
                result.LSERuleCollection = lseRuleCollection;
                result.AbleToContinue = true;
            }
            else
            {
                result.LSERuleCollection = this;
                result.AbleToContinue = false;
            }
            return result;
        }

        public static LSERuleCollection GetAll()
        {
            LSERuleCollection result = new LSERuleCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonMLH1Loss());
            result.Add(new LSEColonMLH1Loss1());
            result.Add(new LSEColonMLH1Loss2());
            result.Add(new LSEColonMLH1PMS2Loss());
            result.Add(new LSEColonMLH1PMS2Loss1());
            result.Add(new LSEColonMLH1PMS2Loss2());
            result.Add(new LSEColonMLH1PMS2Loss3());
            result.Add(new LSEColonMLH1PMS2Loss4());
            result.Add(new LSEColonMSH2Loss());
            result.Add(new LSEColonMSH2MSH6Loss());
            result.Add(new LSEColonMSH6Loss());
            result.Add(new LSEColonPMS2Loss());

            result.Add(new LSEGYNAllIntact());
            result.Add(new LSEGYNMLH1PMS2Loss());
            result.Add(new LSEGYNMSH2MSH6Loss());
            result.Add(new LSEGYNPMS2Loss());

            result.Add(new LSEGeneralAllIntact());
            result.Add(new LSEGeneralAnyLoss());

            return result;
        }

        public static LSERuleCollection GetColonResults()
        {
            LSERuleCollection result = new LSERuleCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonMLH1Loss());
            result.Add(new LSEColonMLH1Loss1());
            result.Add(new LSEColonMLH1Loss2());
            result.Add(new LSEColonMLH1PMS2Loss());
            result.Add(new LSEColonMLH1PMS2Loss1());
            result.Add(new LSEColonMLH1PMS2Loss2());
            result.Add(new LSEColonMLH1PMS2Loss3());
            result.Add(new LSEColonMLH1PMS2Loss4());
            result.Add(new LSEColonMSH2Loss());
            result.Add(new LSEColonMSH2MSH6Loss());
            result.Add(new LSEColonMSH6Loss());
            result.Add(new LSEColonPMS2Loss());

            return result;
        }

        public static LSERuleCollection GetGYNResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEGYNAllIntact());
            result.Add(new LSEGYNMLH1PMS2Loss());
            result.Add(new LSEGYNMSH2MSH6Loss());
            result.Add(new LSEGYNPMS2Loss());
            return result;
        }

        public static LSERuleCollection GetProstateResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEGeneralAllIntact());
            result.Add(new LSEGeneralAnyLoss());
            return result;
        }

        /*
        public static LSEResult GetResult(LSEResult evalResult, string lseType)
		{
			LSEResult result = null;
            LSEResultCollection collection = null;

            if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.COLON)
            {
                collection = LSEResultCollection.GetColonResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GYN)
            {
                collection = LSEResultCollection.GetGYNResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GENERAL)
            {
                collection = LSEResultCollection.GetProstateResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET)
            {
                collection = new LSEResultCollection();
            }

            if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GENERAL)
            {
                if(evalResult.AreAllIntact() == true)
                {
                    result = new Business.Test.LynchSyndrome.LSEGeneralResult1();
                }
                else if(evalResult.AreAnyLoss() == true)
                {
                    result = new Business.Test.LynchSyndrome.LSEGeneralResult2();
                }

                result.MLH1Result = evalResult.MLH1Result;
                result.MSH2Result = evalResult.MSH2Result;
                result.MSH6Result = evalResult.MSH6Result;
                result.PMS2Result = evalResult.PMS2Result;
            }
            else
            {
                foreach (LSEResult lSEResult in collection)
                {
                    if (lSEResult.MLH1Result == evalResult.MLH1Result &&
                        lSEResult.MSH2Result == evalResult.MSH2Result &&
                        lSEResult.MSH6Result == evalResult.MSH6Result &&
                        lSEResult.PMS2Result == evalResult.PMS2Result)
                    {
                        bool brafResultIsEqual = false;
                        if (evalResult.ReflexToBRAFMeth == true)
                        {
                            brafResultIsEqual = (lSEResult.BrafResult == evalResult.BrafResult);
                        }
                        else
                        {
                            brafResultIsEqual = true;
                        }

                        bool methResultIsEqual = false;
                        if (lSEResult.MethResult == LSEResultEnum.NotPerformed)
                        {
                            methResultIsEqual = true;
                        }
                        else
                        {
                            methResultIsEqual = (lSEResult.MethResult == evalResult.MethResult);
                        }

                        if (brafResultIsEqual == true && methResultIsEqual == true)
                        {
                            result = lSEResult;
                            break;
                        }
                    }
                }
            }                          			            

			return result;
		}
        */
    }
}
