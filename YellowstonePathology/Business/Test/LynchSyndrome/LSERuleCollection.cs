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
            LSERuleCollection indicationCollection = GetIndicationCollection(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            LSERuleCollection ihcCollection = GetIHCCollection(indicationCollection, accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            return ihcCollection;
        }

        private static LSERuleCollection GetIndicationCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection allLSERuleCollection = LSERuleCollection.GetAll();
            LSERuleCollection result = new LynchSyndrome.LSERuleCollection();
            foreach (LSERule lseRule in allLSERuleCollection)
            {
                if (lseRule.IncludeInIndicationCollection(panelSetOrderLynchSyndromeEvaluation) == true)
                {
                    result.Add(lseRule);
                }
            }            
            return result;
        }

        private static LSERuleCollection GetIHCCollection(LSERuleCollection lseRuleCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            LSERuleCollection result = new LSERuleCollection();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            foreach (LSERule lseRule in lseRuleCollection)
            {
                if (accessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                    if (panelSetOrderLynchSyndromeIHC.Final == true)
                    {
                        if (lseRule.IncludeInIHCCollection(panelSetOrderLynchSyndromeIHC) == true)
                        {
                            result.Add(lseRule);
                        }
                    }
                    else
                    {
                        result.Add(lseRule);
                    }
                }
                else
                {
                    result.Add(lseRule);
                }
            }
            return result;
        }

        /*private LSERuleResults GetIndicationCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
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
                        if (lseRule.IsAMatch(ihcResult) == true)
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
                if ((lseRule.BRAFRequired == true || lseRule.MethRequired == true)) // && lseRule.BRAFResult == brafResult)
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
                        if (lseRule.MethRequired == true) // && lseRule.MethResult == methResult)
                        {
                            lseRuleCollection.Add(lseRule);
                        }
                    }
                }
            }

            LSERuleResults result = SetResult(lseRuleCollection);
            return result;
        }*/        

        public static LSERuleCollection GetAll()
        {
            LSERuleCollection result = new LSERuleCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonBRAFMeth());
            result.Add(new LSEColonSendOut());

            result.Add(new LSEGYNAllIntact());
            result.Add(new LSEGYNBRAFMeth());
            result.Add(new LSEGYNSendOut());

            result.Add(new LSEGeneralAllIntact());
            result.Add(new LSEGeneralSendOut());

            return result;
        }

        /*public static LSERuleCollection GetColonResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonBRAFMeth());
            result.Add(new LSEColonSendOut());
            return result;
        }

        public static LSERuleCollection GetGYNResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEGYNAllIntact());
            result.Add(new LSEGYNBRAFMeth());
            result.Add(new LSEGYNSendOut());
            return result;
        }

        public static LSERuleCollection GetProstateResults()
        {
            LSERuleCollection result = new LSERuleCollection();
            result.Add(new LSEGeneralAllIntact());
            result.Add(new LSEGeneralSendOut());
            return result;
        }*/
    }
}
