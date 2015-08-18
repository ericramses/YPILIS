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

        private bool m_LSEIHCIsOrdered;
        private bool m_MLHIsOrdered;
        private bool m_BRAFV600EKIsOrdered;
        private bool m_KRASStandardIsOrdered;        
        

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

        public YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder BRAFV600EKTestOrder
		{
			get { return this.m_BRAFV600EKTestOrder; }
		}		
	}
}
