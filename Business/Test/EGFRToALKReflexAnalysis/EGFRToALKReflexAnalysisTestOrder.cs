﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	[PersistentClass("tblEGFRToALKReflexAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class EGFRToALKReflexAnalysisTestOrder : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{
		private string m_Method;
		private string m_Interpretation;
        private string m_References;
        private string m_TumorNucleiPercentage;
        private bool m_QNSForALK;
        private bool m_QNSForROS1;
        private bool m_QNSForPDL1;

		public EGFRToALKReflexAnalysisTestOrder() 
        {
            
        }

		public EGFRToALKReflexAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{
            
		}

        public override void OrderInitialTests(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {                        
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(egfrMutationAnalysisTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo, systemIdentity);
            accessionOrder.TakeATrip(orderTestOrderVisitor);            
        }		

        public void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            StringBuilder interpretation = new StringBuilder();
            StringBuilder method = new StringBuilder();
            StringBuilder references = new StringBuilder();

            if (accessionOrder.PanelSetOrderCollection.Exists(60) == true)
            {
                YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
                interpretation.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.Interpretation);                
                references.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.References);                
                method.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(131) == true)
            {                
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);

                references.AppendLine();
                references.AppendLine("ALK: " + alkForNSCLCByFISHTestOrder.References);

                interpretation.AppendLine();
                interpretation.AppendLine("ALK: " + alkForNSCLCByFISHTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine("ALK: " + alkForNSCLCByFISHTestOrder.Method);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(204) == true)
            {                
                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(204);
                //references.AppendLine();
                //references.AppendLine("ROS1: " + ros1ByFISHTestOrder.References);

                interpretation.AppendLine();
                interpretation.AppendLine("ROS1: " + ros1ByFISHTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine("ROS1: " + ros1ByFISHTestOrder.Method);
            }            

            char[] lineFeedCharacters = { '\r', '\n' };            
            this.Interpretation = interpretation.ToString().TrimEnd(lineFeedCharacters);
            this.Method = method.ToString().TrimEnd(lineFeedCharacters);
            this.References = references.ToString().TrimEnd(lineFeedCharacters);
        }		

		[PersistentProperty]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				this.m_Method = value;
				NotifyPropertyChanged("Method");
			}
		}

		[PersistentProperty]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				this.m_Interpretation = value;
				NotifyPropertyChanged("Interpretation");
			}
		}

        [PersistentProperty]
        public string References
        {
            get { return this.m_References; }
            set
            {
                this.m_References = value;
                NotifyPropertyChanged("References");
            }
        }

        [PersistentProperty()]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

        [PersistentProperty]
        public bool QNSForALK
        {
            get { return this.m_QNSForALK; }
            set
            {
                this.m_QNSForALK = value;
                NotifyPropertyChanged("QNSForALK");
            }
        }

        [PersistentProperty]
        public bool QNSForROS1
        {
            get { return this.m_QNSForROS1; }
            set
            {
                this.m_QNSForROS1 = value;
                NotifyPropertyChanged("QNSForROS1");
            }
        }

        [PersistentProperty]
        public bool QNSForPDL1
        {
            get { return this.m_QNSForPDL1; }
            set
            {
                this.m_QNSForPDL1 = value;
                NotifyPropertyChanged("QNSForPDL1");
            }
        }

		public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("EGFR Mutation Analysis");
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
				result.AppendLine(egfrMutationAnalysisTestOrder.ToResultString(accessionOrder));
			}
			result.AppendLine();

			result.AppendLine("ALK For NSCLC By FISH");
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkForNSCLCByFISHTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
			if(accessionOrder.PanelSetOrderCollection.Exists(alkForNSCLCByFISHTest.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
				result.AppendLine(alkForNSCLCByFISHTestOrder.ToResultString(accessionOrder));
			}
			result.AppendLine();

			result.AppendLine("Interpretation:");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}
	}
}
