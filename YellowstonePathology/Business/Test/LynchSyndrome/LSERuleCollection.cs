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

        public LSERuleCollection OrderByMatched()
        {
            LSERuleCollection result = new LSERuleCollection();
            IOrderedEnumerable<LSERule> orderedResult= this.OrderBy(i => i.IHCMatched);
            for(int i=orderedResult.Count() - 1; i>=0; i--)
            {
                result.Add(orderedResult.ElementAt(i));
            }
            return result;
        }

        public void ClearMatched()
        {
            foreach (LSERule lseResult in this)
            {
                lseResult.IHCMatched = false;
            }
        }

        public bool HasIHCMatch(LSERule lseResultToMatch)
        {
            bool result = false;
            foreach(LSERule lseResult in this)
            {
                if(lseResult.IsIHCMatch(lseResultToMatch) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static LSERuleCollection GetMatchCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleResults result = new LynchSyndrome.LSERuleResults(accessionOrder, panelSetOrderLynchSyndromeEvaluation, LSERuleCollection.GetAll());
            result.LSERuleCollection.GetIndicationCollection(result);
            if(result.AbleToContinue == true) result.LSERuleCollection.GetIHCMatchCollection(result);
            if (result.AbleToContinue == true) result.LSERuleCollection.GetBRAFMatchCollection(result);
            if (result.AbleToContinue == true) result.LSERuleCollection.GetMethMatchCollection(result);
            return result.LSERuleCollection;
        }

        public void GetIndicationCollection(LSERuleResults lseRuleResults)
        {
            lseRuleResults.AbleToContinue = false;
            if (lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType != LSEType.NOTSET)
            {
                LSERuleCollection lseRuleCollection = new LynchSyndrome.LSERuleCollection();
                foreach (LSERule lseRule in this)
                {
                    if (lseRule.Indication == lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType)
                    {
                        lseRuleCollection.Add(lseRule);
                        lseRuleResults.AbleToContinue = true;
                    }
                }

                if(lseRuleResults.AbleToContinue == true) lseRuleResults.LSERuleCollection = lseRuleCollection;
            }
        }

        public void GetIHCMatchCollection(LSERuleResults lseRuleResults)
        {
            lseRuleResults.AbleToContinue = false;
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            if (lseRuleResults.AccessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)lseRuleResults.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderLynchSyndromeIHC.Final == true)
                {
                    IHCResult ihcResult = panelSetOrderLynchSyndromeIHC.GetSummaryResult();
                    LSERuleCollection lseRuleCollection = new LynchSyndrome.LSERuleCollection();
                    foreach (LSERule lseRule in this)
                    {
                        if (lseRule.MLH1Result == ihcResult.MLH1Result.LSEResult &&
                            lseRule.MSH2Result == ihcResult.MSH2Result.LSEResult &&
                            lseRule.MSH6Result == ihcResult.MSH6Result.LSEResult &&
                            lseRule.PMS2Result == ihcResult.PMS2Result.LSEResult)
                        {
                            lseRuleCollection.Add(lseRule);
                            lseRuleResults.AbleToContinue = true;
                        }
                    }
                    if (lseRuleResults.AbleToContinue == true) lseRuleResults.LSERuleCollection = lseRuleCollection;
                }
            }
        }

        public void GetBRAFMatchCollection(LSERuleResults lseRuleResults)
        {
            lseRuleResults.AbleToContinue = false;
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnalysisTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            LSEResultEnum brafResult = LSEResultEnum.NotApplicable;
            if (lseRuleResults.AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)lseRuleResults.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderBraf.Final == true) brafResult = panelSetOrderBraf.GetSummaryResult();
            }
            else if (lseRuleResults.AccessionOrder.PanelSetOrderCollection.Exists(brafMutationAnalysisTest.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)lseRuleResults.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnalysisTest.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (brafMutationAnalysisTestOrder.Final == true) brafResult = brafMutationAnalysisTestOrder.GetSummaryResult();
            }
            else if (lseRuleResults.AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)lseRuleResults.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderRASRAF.Final == true) brafResult = panelSetOrderRASRAF.GetBrafSummaryResult();
            }

            if (brafResult == LSEResultEnum.Detected || brafResult == LSEResultEnum.NotDetected)
            {
                LSERuleCollection lseRuleCollection = new LSERuleCollection();
                foreach (LSERule lseRule in this)
                {
                    if (lseRule.BRAFResult == brafResult)
                    {
                        lseRuleCollection.Add(lseRule);
                        lseRuleResults.AbleToContinue = true;
                    }
                    if (lseRuleResults.AbleToContinue == true) lseRuleResults.LSERuleCollection = lseRuleCollection;
                }
            }
        }

        private void GetMethMatchCollection(LSERuleResults lseRuleResults)
        {
            lseRuleResults.AbleToContinue = false;
            LSEResultEnum methResult = LSEResultEnum.NotApplicable;
            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (lseRuleResults.AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)lseRuleResults.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, lseRuleResults.PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderMLH1MethylationAnalysis.Final == true)
                {
                    methResult = panelSetOrderMLH1MethylationAnalysis.GetSummaryResult();
                    LSERuleCollection lseRuleCollection = new LSERuleCollection();
                    foreach (LSERule lseRule in this)
                    {
                        if (lseRule.MethResult == methResult)
                        {
                            lseRuleCollection.Add(lseRule);
                            lseRuleResults.AbleToContinue = true;
                        }
                    }
                    if (lseRuleResults.AbleToContinue == true) lseRuleResults.LSERuleCollection = lseRuleCollection;
                }
            }
        }

        public static LSERuleCollection GetAll()
        {
            LSERuleCollection result = new LSERuleCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonMSH2MSH6Loss());
            result.Add(new LSEColonMLH1Loss());
            result.Add(new LSEColonMLH1Loss1());
            result.Add(new LSEColonMLH1Loss2());
            result.Add(new LSEColonMSH2Loss());
            result.Add(new LSEColonMSH6Loss());
            result.Add(new LSEColonPMS2Loss());            

            result.Add(new LSEGYNResult1());
            result.Add(new LSEGYNResult2());
            result.Add(new LSEGYNResult3());
            result.Add(new LSEGYNResult3a());

            result.Add(new LSEGeneralResult1());
            result.Add(new LSEGeneralResult2());

            return result;
        }

        public static LSERuleCollection GetColonResults()
        {
            LSERuleCollection result = new LSERuleCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonMLH1Loss());
            result.Add(new LSEColonMLH1Loss1());
            result.Add(new LSEColonMLH1Loss2());
            result.Add(new LSEColonMSH2MSH6Loss());
            result.Add(new LSEColonMSH2Loss());
            result.Add(new LSEColonMSH6Loss());
            result.Add(new LSEColonPMS2Loss());            

            return result;
        }

        public static LSERuleCollection GetGYNResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEGYNResult1());
            result.Add(new LSEGYNResult2());
            result.Add(new LSEGYNResult3());
            result.Add(new LSEGYNResult3a());
            return result;
        }

        public static LSERuleCollection GetProstateResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEGeneralResult1());
            result.Add(new LSEGeneralResult2());
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
