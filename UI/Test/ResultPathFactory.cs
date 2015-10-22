using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class ResultPathFactory
    {
        public delegate void FinishedEventHandler(object sender, EventArgs e);
        public event FinishedEventHandler Finished;     

        public static ResultPath GetResultPath(int panelSetId,
            string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Visibility backButtonVisibility,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            ResultPath result = null;
            switch(panelSetId)
            {
				case 1:
					result = new JAK2V617FResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
					break;				
				case 3:
					result = new NGCTResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 14:
                    result = new HPVTWIResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 18:
					result = new BRAFV600EKResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, backButtonVisibility, systemIdentity);
                    break;
				case 19:
					result = new PNHResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 27:
					result = new KRASStandardResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 30:
                    result = new KRASStandardReflexResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, backButtonVisibility, systemIdentity);
                    break;
				case 32:
					result = new FactorVLeidenResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 33:
					result = new ProthrombinResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 36:
					result = new BCellClonalityByPCRResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 46:
					result = new HER2AmplificationByISHResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 50:
					result = new ErPrSemiQuantitativeResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 60:
                    result = new EGFRResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 61:
					result = new TrichomonasResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 62:
					result = new HPV1618ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 213:
                    result = new HPV1618ByPCRResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 66:
					result = new TestCancelledResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;				
				case 81:
                case 82:
                    result = new FNAResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 100:
					result = new BCL1t1114ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 102:
					result = new LynchSyndromeIHCPanelResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 106:
					result = new LynchSyndromeEvaluationResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, backButtonVisibility, systemIdentity);
                    break;
				case 112:
					result = new ComprehensiveColonCancerProfilePath(reportNo, accessionOrder, objectTracker, pageNavigator, backButtonVisibility, systemIdentity);
                    break;
				case 131:
                    result = new ALKForNSCLCByFISHResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 132:
					result = new MicrosatelliteInstabilityAnalysisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 135:
					result = new ABL1KinaseDomainMutationResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 136:
					result = new MPNStandardReflexPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 137:
					result = new MPNExtendedReflexPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 140:
					result = new CalreticulinMutationAnalysisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 141:
					result = new JAK2Exon1214ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 143:
					result = new ZAP70LymphoidPanelResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 144:
					result = new MLH1MethalationAnalysisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 145:
					result = new ChromosomeAnalysisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 147:
					result = new MultipleMyelomaMGUSByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 148:
					result = new CCNDIBCLIGHResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 149:
					result = new HighGradeLargeBCellLymphomaResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 150:
					result = new CEBPAResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 151:
					result = new CLLByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 152:
					result = new TCellClonalityByPCRResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 153:
					result = new FLT3ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 155:
					result = new NPM1ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 156:
					result = new BCRABLByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 157:
					result = new MPNFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 158:
					result = new MDSByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 159:
					result = new MPLResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 160:
					result = new MultipleFISHProbePanelResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 161:
					result = new MultipleMyelomaIgHByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 162:
					result = new BCRABLByPCRResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 163:
					result = new Her2AmplificationByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 164:
					result = new MDSExtendedPanelByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 168:
					result = new AMLStandardByFishResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 169:
					result = new ChromosomeAnalysisForFetalAnomalyResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 170:
					result = new NonHodgkinsLymphomaFISHPanelResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 171:
					result = new Her2AmplificationByIHCResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 172:
					result = new EosinophiliaByFISHResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 173:
					result = new PlasmaCellMyelomaRiskStratificationResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 174:
					result = new NeoARRAYSNPCytogeneticProfileResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 175:
					result = new KRASExon4MutationResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 177:
					result = new BCellGeneRearrangementResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 178:
					result = new MYD88MutationAnalysisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 179:
					result = new NRASMutationAnalysisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 181:
					result = new CKITResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 183:
					result = new CysticFibrosisResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 184:
					result = new DeletionsForGlioma1p19qResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 185:
					result = new BladderCancerFISHUrovysionResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				//case 186:
				//	result = new API2MALT1ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                //    break;
				case 192:
					result = new ALLAdultByFISHResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 201:
                    result = new IHCQCResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
				case 203:
					result = new ReviewForAdditionalTestingResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 204:
                    result = new ROS1ResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 212:
                    result = new MissingInformationResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
                case 214:
                    result = new TechInitiatedPeripheralSmearResultPath(reportNo, accessionOrder, objectTracker, pageNavigator, systemIdentity);
                    break;
            }
            return result;
        }


        public bool Start(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,                        
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            System.Windows.Visibility backButtonVisibility)
        {
			bool result = false;            
            YellowstonePathology.UI.Test.ResultPath resultPath = YellowstonePathology.UI.Test.ResultPathFactory.GetResultPath(panelSetOrder.PanelSetId, panelSetOrder.ReportNo, accessionOrder, objectTracker, pageNavigator, System.Windows.Visibility.Collapsed, systemIdentity);

            if (resultPath != null)
            {
				result = true;
                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Start();                
            }
            else
            {
                if (panelSetOrder is YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)
                {
                    result = true;

                    YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(accessionOrder.MasterAccessionNo);

                    if (clientOrders.Count > 0)
                    {
                        clientOrder = clientOrders[0];
                    }

                    YellowstonePathology.UI.Login.WomensHealthProfilePath womensHealthProfilePath = new YellowstonePathology.UI.Login.WomensHealthProfilePath(accessionOrder, objectTracker, clientOrder, pageNavigator, systemIdentity);
                    womensHealthProfilePath.Finished += new Login.WomensHealthProfilePath.FinishedEventHandler(WomensHealthProfilePath_Finished);
                    womensHealthProfilePath.Start();
                }
                else if (panelSetOrder is YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)
                {
                    result = true;
                    Test.EGFRToALKReflexPath eGFRToALKReflexPath = new Test.EGFRToALKReflexPath(panelSetOrder.ReportNo, accessionOrder, objectTracker, systemIdentity, pageNavigator, System.Windows.Visibility.Collapsed);
                    eGFRToALKReflexPath.Finish += new Test.EGFRToALKReflexPath.FinishEventHandler(EGFRToALKReflexPath_Finish);
                    eGFRToALKReflexPath.Start();
                }
                else if (panelSetOrder is YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel)
                {
                    result = true;
                    YellowstonePathology.UI.Test.InvasiveBreastPanelPath invasiveBreastPanelPath = new Test.InvasiveBreastPanelPath(panelSetOrder.ReportNo, accessionOrder, objectTracker, systemIdentity, pageNavigator);
                    invasiveBreastPanelPath.Finish += new Test.InvasiveBreastPanelPath.FinishEventHandler(InvasiveBreastPanelPath_Finish);
                    invasiveBreastPanelPath.Start();
                }

                else if (panelSetOrder is YellowstonePathology.Business.Test.API2MALT1.API2MALT1TestOrder)
                {
                    result = true;
                    ResultPath resultPathGenerated = YellowstonePathology.UI.Test.ResultPathFactory.BuildResultPath(panelSetOrder, accessionOrder, objectTracker, pageNavigator, System.Windows.Visibility.Collapsed, systemIdentity);
                    resultPathGenerated.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                    resultPathGenerated.Start();
                }
                else
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
                    if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
                    {
                        result = true;
                        PublishedDocumentResultPath publishedDocumentResultPath = new PublishedDocumentResultPath(panelSetOrder.ReportNo, accessionOrder, pageNavigator, systemIdentity);
                        publishedDocumentResultPath.Finish += new ResultPath.FinishEventHandler(ResultPath_Finish);
                        publishedDocumentResultPath.Start();
                    }
                }
            }

			return result;
        }

        private void WomensHealthProfilePath_Finished(object sender, EventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new EventArgs());
        }
       
        private void ResultPath_Finish(object sender, EventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new EventArgs());
        }        

        private void EGFRToALKReflexPath_Finish(object sender, EventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new EventArgs());
        }        

        private void InvasiveBreastPanelPath_Finish(object sender, EventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new EventArgs());
        }


        public static ResultPath BuildResultPath(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Visibility backButtonVisibility,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            ResultPath resultPath = new API2MALT1ResultPath((YellowstonePathology.Business.Test.API2MALT1.API2MALT1TestOrder)panelSetOrder, accessionOrder, objectTracker, pageNavigator, backButtonVisibility, systemIdentity );
            return resultPath;
        }
    }
}
