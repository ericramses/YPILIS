using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileResult
	{
		private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_PanelSetOrderSurgical;

		private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC m_PanelSetOrderLynchSyndromeIHC;
		private YellowstonePathology.Business.Test.LynchSyndrome.IHCResult m_IHCResult;
		private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis m_PanelSetOrderMLH1MethylationAnalysis;				
        
        private YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder m_KRASStandardTestOrder;
        private YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest m_KRASStandardTest;

        private YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder m_BRAFV600EKTestOrder;
        private YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest m_BRAFV600EKTest;

        private YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest m_RASRAFPanelTest;
        private YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder m_RASRAFPanelTestOrder;

        private KRASExon23Mutation.KRASExon23MutationTest m_KRASExon23MutationTest;
        private KRASExon23Mutation.KRASExon23MutationTestOrder m_KRASExon23MutationTestOrder;

        private KRASExon4Mutation.KRASExon4MutationTest m_KRASExon4MutationTest;
        private KRASExon4Mutation.KRASExon4MutationTestOrder m_KRASExon4MutationTestOrder;

        private NRASMutationAnalysis.NRASMutationAnalysisTest m_NRASMutationAnalysisTest;
        private NRASMutationAnalysis.NRASMutationAnalysisTestOrder m_NRASMutationAnalysisTestOrder;

        private bool m_LSEIHCIsOrdered;
        private bool m_MLHIsOrdered;
        private bool m_BRAFV600EKIsOrdered;
        private bool m_KRASStandardIsOrdered;
        private bool m_KRASExon23MutationIsOrdered;
        private bool m_KRASExon4MutationIsOrdered;
        private bool m_NRASMutationAnalysisIsOrdered;
        private bool m_RASRAFIsOrdered;

        public ComprehensiveColonCancerProfileResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			ComprehensiveColonCancerProfile comprehensiveColonCancerProfile)
		{            
			this.m_SpecimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(comprehensiveColonCancerProfile.OrderedOnId);
			this.m_PanelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)accessionOrder.PanelSetOrderCollection.GetSurgical();
			this.m_SurgicalSpecimen = this.m_PanelSetOrderSurgical.SurgicalSpecimenCollection.GetBySpecimenOrderId(this.m_SpecimenOrder.SpecimenOrderId);

            bool restrictToOrderedOn = !comprehensiveColonCancerProfile.IncludeTestsPerformedOnOtherBlocks;
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
			{
                this.m_LSEIHCIsOrdered = true;
                this.m_PanelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
				this.m_IHCResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(this.m_PanelSetOrderLynchSyndromeIHC.ResultCode);
			}
            else
            {
                YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(accessionOrder.PatientId);
                if(patientHistory.PanelSetIdExists(102) == true)
                {
                    YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult = patientHistory.GetByPanelSetId(102);
                    YellowstonePathology.Business.Test.AccessionOrder lseIHCAccessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(patientHistoryResult.MasterAccessionNo);
                    this.m_LSEIHCIsOrdered = true;
                    this.m_PanelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)lseIHCAccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
                    this.m_IHCResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(this.m_PanelSetOrderLynchSyndromeIHC.ResultCode);
                }
            }

			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
			{
                this.m_MLHIsOrdered = true;
				this.m_PanelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
			}

            this.m_BRAFV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(this.m_BRAFV600EKTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
            {
                this.m_BRAFV600EKIsOrdered = true;
                this.m_BRAFV600EKTestOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_BRAFV600EKTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
            }

            this.m_KRASStandardTest = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(this.m_KRASStandardTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
            {
                this.m_KRASStandardIsOrdered = true;
                this.m_KRASStandardTestOrder = (YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_KRASStandardTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
            }

            this.m_KRASExon23MutationTest = new KRASExon23Mutation.KRASExon23MutationTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(this.m_KRASExon23MutationTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
            {
                this.m_KRASExon23MutationIsOrdered = true;
                this.m_KRASExon23MutationTestOrder = (KRASExon23Mutation.KRASExon23MutationTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_KRASExon23MutationTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
            }

            this.m_KRASExon4MutationTest = new KRASExon4Mutation.KRASExon4MutationTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(this.m_KRASExon4MutationTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
            {
                this.m_KRASExon4MutationIsOrdered = true;
                this.m_KRASExon4MutationTestOrder = (KRASExon4Mutation.KRASExon4MutationTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_KRASExon4MutationTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
            }

            this.m_NRASMutationAnalysisTest = new NRASMutationAnalysis.NRASMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(this.m_NRASMutationAnalysisTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
            {
                this.m_NRASMutationAnalysisIsOrdered = true;
                this.m_NRASMutationAnalysisTestOrder = (NRASMutationAnalysis.NRASMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_NRASMutationAnalysisTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
            }

            this.m_RASRAFPanelTest = new RASRAFPanel.RASRAFPanelTest();
            if(accessionOrder.PanelSetOrderCollection.Exists(this.m_RASRAFPanelTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn) == true)
            {
                this.m_RASRAFIsOrdered = true;
                this.m_RASRAFPanelTestOrder = (RASRAFPanel.RASRAFPanelTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_RASRAFPanelTest.PanelSetId, comprehensiveColonCancerProfile.OrderedOnId, restrictToOrderedOn);
            }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
		{
			get{ return this.m_SurgicalSpecimen; }
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return this.m_SpecimenOrder; }
		}

		public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder PanelSetOrderSurgical
		{
			get { return this.m_PanelSetOrderSurgical; }
		}

        public bool LSEIHCIsOrdered
        {
            get { return this.m_LSEIHCIsOrdered; }
        }

		public YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC PanelSetOrderLynchSyndromeIHC
		{
			get{ return this.m_PanelSetOrderLynchSyndromeIHC; }
		}

		public YellowstonePathology.Business.Test.LynchSyndrome.IHCResult IHCResult
		{
			get { return this.m_IHCResult; }
		}

        public bool MLHIsOrdered
        {
            get { return this.m_MLHIsOrdered; }
        }

		public YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis PanelSetOrderMLH1MethylationAnalysis
		{
			get { return this.m_PanelSetOrderMLH1MethylationAnalysis; }
		}

        public bool KRASStandardIsOrderd
        {
            get { return this.m_KRASStandardIsOrdered; }
        }

        public YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder KRASStandardTestOrder
        {
            get { return this.m_KRASStandardTestOrder; }
        }

        public bool BRAFV600EKIsOrdered
        {
            get { return this.m_BRAFV600EKIsOrdered; }
        }
        
        public bool RASRAFIsOrdered
        {
            get { return this.m_RASRAFIsOrdered; }
        }

        public YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder BRAFV600EKTestOrder
		{
			get { return this.m_BRAFV600EKTestOrder; }
		}
        
        public bool KRASExon23MutationIsOrdered
        {
            get { return this.m_KRASExon23MutationIsOrdered; }
        }

        public KRASExon23Mutation.KRASExon23MutationTestOrder KRASExon23MutationTestOrder
        {
            get { return this.m_KRASExon23MutationTestOrder; }
        }

        public bool KRASExon4MutationIsOrdered
        {
            get { return this.m_KRASExon4MutationIsOrdered; }
        }

        public KRASExon4Mutation.KRASExon4MutationTestOrder KRASExon4MutationTestOrder
        {
            get { return this.m_KRASExon4MutationTestOrder; }
        }


        public bool NRASMutationAnalysisIsOrdered
        {
            get { return this.m_NRASMutationAnalysisIsOrdered; }
        }
        
        public NRASMutationAnalysis.NRASMutationAnalysisTestOrder NRASMutationAnalysisTestOrder
        {
            get { return this.m_NRASMutationAnalysisTestOrder; }
        }

        public RASRAFPanel.RASRAFPanelTestOrder RASRAFTestOrder
        {
            get { return this.m_RASRAFPanelTestOrder; }
        }
    }
}
