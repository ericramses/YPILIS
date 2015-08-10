using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EpicObxViewFactory
    {
        public static EpicObxView GetObxView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            EpicObxView view = null;
            switch (panelSetId)
            {
                case 1:
					view = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FEpicObxView(accessionOrder, reportNo, obxCount);
                    break;                
                case 3:
                    view = new YellowstonePathology.Business.Test.NGCT.NGCTEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 46:
					view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 13:
                case 128:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 14:
					view = new YellowstonePathology.Business.Test.HPVTWI.HPVTWIEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 15:
					view = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 18:
					view = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 19:
					view = new YellowstonePathology.Business.Test.PNH.PNHEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 27:
					view = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 20:
					view = new YellowstonePathology.Business.Test.LLP.LLPEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 21:
					view = new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 22:
					view = new YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PAAEpicObxView(accessionOrder, reportNo, obxCount);
                    break;                
                case 23:
					view = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.RPAEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 24:
					view = new YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				//case 28:
				//	view = new YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinEpicObxView(accessionOrder, reportNo, obxCount);
				//	break;
				case 30:
					view = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 32:
					view = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 33:
					view = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 34:
                    view = new EpicMthfrObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 35:
					view = new YellowstonePathology.Business.Test.Autopsy.AutopsyEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 36:
					view = new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCREpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 50:
					view = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 54:
                    view = new EpicCytogeneticsObxView(accessionOrder, reportNo, obxCount);
                    break;                
                case 60:
					view = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 61:
					view = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 62:
					view = new YellowstonePathology.Business.Test.HPV1618.HPV1618EpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 66:
					view = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 100:
					view = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 102:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 106:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationEpicObxView(accessionOrder, reportNo, obxCount);
                    break;  
				case 112:
					view = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileEpicObxView(accessionOrder, reportNo, obxCount);
					break;
                case 116:
					view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 124:
					view = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisEPICOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 131:
                    view = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 132:
					view = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 135:
					view = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationEpicObxView(accessionOrder, reportNo, obxCount);
					break;
                case 136:
					view = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 137:
					view = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 140:
					view = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 141:
					view = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 143:
					view = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 144:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 145:
					view = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 147:
					view = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 148:
					view = new EpicCCNDIBCLIGHObxView(accessionOrder, reportNo, obxCount);
					break;
				case 149:
					view = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 150:
					view = new EpicCEBPAObxView(accessionOrder, reportNo, obxCount);
					break;
				case 151:
					view = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 152:
					view = new EpicTCellClonalityByPCRObxView(accessionOrder, reportNo, obxCount);
					break;
				case 153:
					view = new EpicFLT3ObxView(accessionOrder, reportNo, obxCount);
					break;
				case 155:
					view = new EpicNPM1ObxView(accessionOrder, reportNo, obxCount);
					break;
				case 156:
					view = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 157:
					view = new EpicMPNFishObxView(accessionOrder, reportNo, obxCount);
					break;
				case 158:
					view = new EpicMDSByFishObxView(accessionOrder, reportNo, obxCount);
					break;
				case 159:
					view = new EpicMPLObxView(accessionOrder, reportNo, obxCount);
					break;
				case 160:
					view = new EpicMultipleFISHProbePanelObxView(accessionOrder, reportNo, obxCount);
					break;
				case 161:
					view = new EpicMultipleMyelomaIgHByFishObxView(accessionOrder, reportNo, obxCount);
					break;
				case 162:
					view = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCREpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 163:
					view = new EpicHer2AmplificationByFishObxView(accessionOrder, reportNo, obxCount);
					break;
				case 164:
					view = new EpicMDSExtendedPanelByFishObxView(accessionOrder, reportNo, obxCount);
					break;
				case 168:
					view = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 169:
					view = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 170:
					view = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 171:
					view = new EpicHer2AmplificationByIHCObxView(accessionOrder, reportNo, obxCount);
					break;
				case 172:
					view = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 173:
					view = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 174:
					view = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 175:
					view = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 177:
					view = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 178:
					view = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 179:
					view = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 181:
					view = new YellowstonePathology.Business.Test.CKIT.CKITEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 183:
					view = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 184:
					view = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 185:
					view = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 186:
					view = new YellowstonePathology.Business.Test.API2MALT1.API2MALT1EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 192:
					view = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHEpicObxView(accessionOrder, reportNo, obxCount);
					break;
			}
            return view;
        }
    }
}
