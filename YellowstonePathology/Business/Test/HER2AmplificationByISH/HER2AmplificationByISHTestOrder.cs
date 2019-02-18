using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	[PersistentClass("tblHER2AmplificationByISHTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class HER2AmplificationByISHTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		public static string PositiveResult = "POSITIVE (amplified)";
		public static string NegativeResult = "NEGATIVE (not amplified)";
		public static string IndeterminateResult = "INDETERMINATE";
		public static string EquivocalResult = "EQUIVOCAL";

		private string m_Result;
		private int m_CellsCounted;
		private int m_TotalChr17SignalsCounted;
		private int m_TotalHer2SignalsCounted;
		private int m_Her2byIHCOrder;
		private int m_NumberOfObservers;
		private bool m_IncludeImmunoRecommendedComment;
		private bool m_IncludeResultComment;
		private bool m_IncludePolysomyComment;
		private string m_PolysomyPercent;
		private string m_Chr17SignalRangeLow;
		private string m_Chr17SignalRangeHigh;
		private string m_Her2SignalRangeLow;
		private string m_Her2SignalRangeHigh;
		private string m_CommentLabel;
		private string m_SampleAdequacy;
		private string m_ProbeSignalIntensity;
		private string m_TechComment;
		private string m_ResultComment;
		private string m_InterpretiveComment;
		private string m_ResultDescription;
		private string m_SourceBlock;
		private string m_GeneticHeterogeneity;
		private string m_Her2Chr17ClusterRatio;
		private string m_ReportReference;
		private string m_Indicator;
		private string m_Method;
		private bool m_NotInterpretable;
		private string m_ASRComment;
        private string m_FixationComment;
        private bool m_HER2ByIHCRequired;
        private bool m_RecountRequired;

        public HER2AmplificationByISHTestOrder()
		{
        }

        public HER2AmplificationByISHTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Method = "This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved " +
                "Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on " +
                "paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 " +
                "and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).";
            this.m_ASRComment = "This test was performed using a US FDA approved DNA probe kit, modified to report results according to " +
                "ASCO/CAP guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the " +
                "responsibility for test performance";
		}
        
		public Nullable<double> Her2Chr17Ratio
		{
			get
			{
				Nullable<double> ratio = null;
				if (TotalHer2SignalsCounted > 0 && TotalChr17SignalsCounted > 0)
				{
					double dratio = (double)TotalHer2SignalsCounted / (double)TotalChr17SignalsCounted;
					ratio = Convert.ToDouble(Math.Round((dratio), 2));
				}
				return ratio;
			}
			set { }
		}

		public Nullable<double> AverageHer2NeuSignal
		{
			get
			{
				Nullable<double> result = null;
				if (TotalHer2SignalsCounted > 0 && CellsCounted > 0)
				{
					double dratio = (double)TotalHer2SignalsCounted / (double)CellsCounted;
					result = Convert.ToDouble(Math.Round((dratio), 2));
				}
				return result;
			}
			set { }
		}

		public string AverageChr17Signal
		{
			get
			{
				string ratio = "Unable to calculate";
				if (TotalChr17SignalsCounted > 0 && CellsCounted > 0)
				{
					double dratio = (double)TotalChr17SignalsCounted / (double)CellsCounted;
					ratio = Convert.ToString(Math.Round((dratio), 2));
				}
				return ratio;
			}
			set { }
		}

		public string AverageHer2Chr17Signal
		{
			get
			{
				string ratio = "Unable to calculate";
				Nullable<double> dratio = this.AverageHer2Chr17SignalAsDouble;
				if (dratio.HasValue)
				{
					ratio = Convert.ToString(Math.Round((dratio.Value), 2));
				}
				return ratio;
			}
			set { }
		}

		public Nullable<double> AverageHer2Chr17SignalAsDouble
		{
			get
			{
				Nullable<double> dratio = null;
				if (TotalChr17SignalsCounted > 0 && TotalHer2SignalsCounted > 0 && CellsCounted > 0)
				{
					dratio = ((double)TotalHer2SignalsCounted / (double)CellsCounted) / ((double)TotalChr17SignalsCounted / (double)CellsCounted);
				}
				return dratio;
			}
		}

		public int TotalChr17SignalsCountedUI
		{
			get { return this.m_TotalChr17SignalsCounted; }
			set
			{
				if (this.m_TotalChr17SignalsCounted != value)
				{
					this.TotalChr17SignalsCounted = value;
					NotifyPropertyChanged("Her2Chr17Ratio");
					NotifyPropertyChanged("TotalChr17SignalsCountedUI");
					NotifyPropertyChanged("AverageChr17Signal");
					NotifyPropertyChanged("AverageHer2Chr17Signal");
				}
			}
		}

		public int TotalHer2SignalsCountedUI
		{
			get { return this.m_TotalHer2SignalsCounted; }
			set
			{
				if (this.m_TotalHer2SignalsCounted != value)
				{
					this.TotalHer2SignalsCounted = value;
					NotifyPropertyChanged("Her2Chr17Ratio");
					NotifyPropertyChanged("TotalHer2SignalsCountedUI");
					NotifyPropertyChanged("AverageHer2NeuSignal");
					NotifyPropertyChanged("AverageHer2Chr17Signal");
				}
			}
		}

		public int CellsCountedUI
		{
			get { return this.m_CellsCounted; }
			set
			{
				if (this.m_CellsCounted != value)
				{
					this.CellsCounted = value;
					NotifyPropertyChanged("CellsCountedUI");
					NotifyPropertyChanged("AverageHer2NeuSignal");
					NotifyPropertyChanged("AverageChr17Signal");
					NotifyPropertyChanged("AverageHer2Chr17Signal");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool IncludeImmunoRecommendedComment
		{
			get { return this.m_IncludeImmunoRecommendedComment; }
			set
			{
				if(this.m_IncludeImmunoRecommendedComment != value)
				{
					this.m_IncludeImmunoRecommendedComment = value;
					this.NotifyPropertyChanged("IncludeImmunoRecommendedComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool IncludeResultComment
		{
			get { return this.m_IncludeResultComment; }
			set
			{
				if(this.m_IncludeResultComment != value)
				{
					this.m_IncludeResultComment = value;
					this.NotifyPropertyChanged("IncludeResultComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool IncludePolysomyComment
		{
			get { return this.m_IncludePolysomyComment; }
			set
			{
				if(this.m_IncludePolysomyComment != value)
				{
					this.m_IncludePolysomyComment = value;
					this.NotifyPropertyChanged("IncludePolysomyComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int CellsCounted
		{
			get { return this.m_CellsCounted; }
			set
			{
				if(this.m_CellsCounted != value)
				{
					this.m_CellsCounted = value;
					this.NotifyPropertyChanged("CellsCounted");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int TotalChr17SignalsCounted
		{
			get { return this.m_TotalChr17SignalsCounted; }
			set
			{
				if(this.m_TotalChr17SignalsCounted != value)
				{
					this.m_TotalChr17SignalsCounted = value;
					this.NotifyPropertyChanged("TotalChr17SignalsCounted");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int TotalHer2SignalsCounted
		{
			get { return this.m_TotalHer2SignalsCounted; }
			set
			{
				if(this.m_TotalHer2SignalsCounted != value)
				{
					this.m_TotalHer2SignalsCounted = value;
					this.NotifyPropertyChanged("TotalHer2SignalsCounted");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int Her2byIHCOrder
		{
			get { return this.m_Her2byIHCOrder; }
			set
			{
				if(this.m_Her2byIHCOrder != value)
				{
					this.m_Her2byIHCOrder = value;
					this.NotifyPropertyChanged("Her2byIHCOrder");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "2", "int")]
		public int NumberOfObservers
		{
			get { return this.m_NumberOfObservers; }
			set
			{
				if(this.m_NumberOfObservers != value)
				{
					this.m_NumberOfObservers = value;
					this.NotifyPropertyChanged("NumberOfObservers");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if(this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string PolysomyPercent
		{
			get { return this.m_PolysomyPercent; }
			set
			{
				if(this.m_PolysomyPercent != value)
				{
					this.m_PolysomyPercent = value;
					this.NotifyPropertyChanged("PolysomyPercent");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string Chr17SignalRangeLow
		{
			get { return this.m_Chr17SignalRangeLow; }
			set
			{
				if(this.m_Chr17SignalRangeLow != value)
				{
					this.m_Chr17SignalRangeLow = value;
					this.NotifyPropertyChanged("Chr17SignalRangeLow");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Chr17SignalRangeHigh
		{
			get { return this.m_Chr17SignalRangeHigh; }
			set
			{
				if(this.m_Chr17SignalRangeHigh != value)
				{
					this.m_Chr17SignalRangeHigh = value;
					this.NotifyPropertyChanged("Chr17SignalRangeHigh");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string Her2SignalRangeLow
		{
			get { return this.m_Her2SignalRangeLow; }
			set
			{
				if(this.m_Her2SignalRangeLow != value)
				{
					this.m_Her2SignalRangeLow = value;
					this.NotifyPropertyChanged("Her2SignalRangeLow");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string Her2SignalRangeHigh
		{
			get { return this.m_Her2SignalRangeHigh; }
			set
			{
				if(this.m_Her2SignalRangeHigh != value)
				{
					this.m_Her2SignalRangeHigh = value;
					this.NotifyPropertyChanged("Her2SignalRangeHigh");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CommentLabel
		{
			get { return this.m_CommentLabel; }
			set
			{
				if(this.m_CommentLabel != value)
				{
					this.m_CommentLabel = value;
					this.NotifyPropertyChanged("CommentLabel");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string ResultDescription
		{
			get { return this.m_ResultDescription; }
			set
			{
				if(this.m_ResultDescription != value)
				{
					this.m_ResultDescription = value;
					this.NotifyPropertyChanged("ResultDescription");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string SampleAdequacy
		{
			get { return this.m_SampleAdequacy; }
			set
			{
				if(this.m_SampleAdequacy != value)
				{
					this.m_SampleAdequacy = value;
					this.NotifyPropertyChanged("SampleAdequacy");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "30", "null", "varchar")]
		public string ProbeSignalIntensity
		{
			get { return this.m_ProbeSignalIntensity; }
			set
			{
				if(this.m_ProbeSignalIntensity != value)
				{
					this.m_ProbeSignalIntensity = value;
					this.NotifyPropertyChanged("ProbeSignalIntensity");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string TechComment
		{
			get { return this.m_TechComment; }
			set
			{
				if(this.m_TechComment != value)
				{
					this.m_TechComment = value;
					this.NotifyPropertyChanged("TechComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string ResultComment
		{
			get { return this.m_ResultComment; }
			set
			{
				if(this.m_ResultComment != value)
				{
					this.m_ResultComment = value;
					this.NotifyPropertyChanged("ResultComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string InterpretiveComment
		{
			get { return this.m_InterpretiveComment; }
			set
			{
				if(this.m_InterpretiveComment != value)
				{
					this.m_InterpretiveComment = value;
					this.NotifyPropertyChanged("InterpretiveComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string GeneticHeterogeneity
		{
			get { return this.m_GeneticHeterogeneity; }
			set
			{
				if(this.m_GeneticHeterogeneity != value)
				{
					this.m_GeneticHeterogeneity = value;
					this.NotifyPropertyChanged("GeneticHeterogeneity");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string Her2Chr17ClusterRatio
		{
			get { return this.m_Her2Chr17ClusterRatio; }
			set
			{
				if(this.m_Her2Chr17ClusterRatio != value)
				{
					this.m_Her2Chr17ClusterRatio = value;
					this.NotifyPropertyChanged("Her2Chr17ClusterRatio");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ReportReference
		{
			get { return this.m_ReportReference; }
			set
			{
				if(this.m_ReportReference != value)
				{
					this.m_ReportReference = value;
					this.NotifyPropertyChanged("ReportReference");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "350", "null", "varchar")]
		public string SourceBlock
		{
			get { return this.m_SourceBlock; }
			set
			{
				if(this.m_SourceBlock != value)
				{
					this.m_SourceBlock = value;
					this.NotifyPropertyChanged("SourceBlock");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Indicator
		{
			get { return this.m_Indicator; }
			set
			{
				if(this.m_Indicator != value)
				{
					this.m_Indicator = value;
					this.NotifyPropertyChanged("Indicator");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public bool NotInterpretable
		{
			get { return this.m_NotInterpretable; }
			set
			{
				if (this.m_NotInterpretable != value)
				{
					this.m_NotInterpretable = value;
					this.NotifyPropertyChanged("NotInterpretable");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ASRComment
		{
			get { return this.m_ASRComment; }
			set
			{
				if (this.m_ASRComment != value)
				{
					this.m_ASRComment = value;
					this.NotifyPropertyChanged("ASRComment");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string FixationComment
        {
            get { return this.m_FixationComment; }
            set
            {
                if (this.m_FixationComment != value)
                {
                    this.m_FixationComment = value;
                    this.NotifyPropertyChanged("FixationComment");
                }
            }
        }

        [PersistentProperty()]
        public bool HER2ByIHCRequired
        {
            get { return this.m_HER2ByIHCRequired; }
            set
            {
                if (this.m_HER2ByIHCRequired != value)
                {
                    this.m_HER2ByIHCRequired = value;
                    this.NotifyPropertyChanged("HER2ByIHCRequired");
                }
            }
        }

        [PersistentProperty()]
        public bool RecountRequired
        {
            get { return this.m_RecountRequired; }
            set
            {
                if (this.m_RecountRequired != value)
                {
                    this.m_RecountRequired = value;
                    this.NotifyPropertyChanged("RecountRequired");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			string result = "HER2 Amplification by D-ISH: " + this.Result;
			return result;
		}

		public AuditResult IsOkToSetResults(AccessionOrder accessionOrder)
		{
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;
            if (this.m_Accepted == true)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = "The results may not be set because they have already been accepted." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(this.m_Indicator) == true)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message += "The indication must be set before results can be set." + Environment.NewLine;
            }

            if (this.m_NotInterpretable == false)
            {
                if (this.m_TotalHer2SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "The Total Her2 Signals Counted must be set before results can be set." + Environment.NewLine;
                }
                if (this.m_TotalChr17SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "The Total Chr17 Signals Counted must be set before results can be set." + Environment.NewLine;
                }
                if (this.m_CellsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "The Cells Counted must be set before results can be set." + Environment.NewLine;
                }
                if (this.m_GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInClusters)
                {
                    if (string.IsNullOrEmpty(this.m_Her2Chr17ClusterRatio) == true)
                    {
                        result.Status = AuditStatusEnum.Failure;
                        result.Message += "The Her2Chr/17 Cluster Ratio must be set before results can be set." + Environment.NewLine;
                    }
                }
            }
            
            if(result.Status == AuditStatusEnum.OK)
            {
                this.SetHER2ByIHCRequired();
                HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationResultCollection(accessionOrder.PanelSetOrderCollection, this.m_ReportNo);
                HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindMatch();
                if (this.HER2ByIHCRequired == true)
                {
                    YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
                    if (this.CheckTestExists(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.IsOkToOrderIHC(accessionOrder);
                        if (auditResult.Status == AuditStatusEnum.Failure)
                        {
                            result.Status = auditResult.Status;
                            result.Message = auditResult.Message;
                        }
                        else
                        {
                            result.Status = AuditStatusEnum.Warning;
                            result.Message = "The results may not be set as a " + her2AmplificationByIHCTest.PanelSetName + " must be ordered." + Environment.NewLine + "Order the test now?";
                        }
                    }
                    else
                    {
                        if (this.CheckTestIsAccepted(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == false)
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message = "The results may not be set until the " + her2AmplificationByIHCTest.PanelSetName + " is accepted.";
                        }
                        else
                        {
                            if (her2AmplificationResult.IsRecountNeeded() == true)
                            {
                                HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();
                                if (this.CheckTestExists(accessionOrder, her2AmplificationRecountTest.PanelSetId) == false)
                                {
                                    result.Status = AuditStatusEnum.Failure;
                                    result.Message = "The results may not be set until a " + her2AmplificationRecountTest.PanelSetName + " is ordered.";
                                }
                                else
                                {
                                    if (this.CheckTestIsAccepted(accessionOrder, her2AmplificationRecountTest.PanelSetId) == false)
                                    {
                                        result.Status = AuditStatusEnum.Failure;
                                        result.Message = "The results may not be set until the " + her2AmplificationRecountTest.PanelSetName + " is accepted.";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
		}

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result =  base.IsOkToAccept(accessionOrder);
            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Indicator) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the indication is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the result is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationResultCollection(accessionOrder.PanelSetOrderCollection, this.m_ReportNo);
                HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindMatch();
                if (this.HER2ByIHCRequired == true)
                {
                    YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
                    if (this.CheckTestExists(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.IsOkToOrderIHC(accessionOrder);
                        if (auditResult.Status == AuditStatusEnum.Failure)
                        {
                            result.Status = auditResult.Status;
                            result.Message = auditResult.Message;
                        }
                        else
                        {
                            result.Status = AuditStatusEnum.Warning;
                            result.Message = "The results may not be accepted as a " + her2AmplificationByIHCTest.PanelSetName + " must be ordered." + Environment.NewLine + "Order the test now?";
                        }
                    }
                    else
                    {
                        if (this.CheckTestIsAccepted(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == false)
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message = "The results may not be accepted until the " + her2AmplificationByIHCTest.PanelSetName + " is accepted.";
                        }
                        else
                        {
                            if (her2AmplificationResult.IsRecountNeeded() == true)
                            {
                                HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();
                                if (this.CheckTestExists(accessionOrder, her2AmplificationRecountTest.PanelSetId) == false)
                                {
                                    result.Status = AuditStatusEnum.Failure;
                                    result.Message = "The results may not be accepted as a " + her2AmplificationRecountTest.PanelSetName + " must be ordered.";
                                }
                                else
                                {
                                    if (this.CheckTestIsAccepted(accessionOrder, her2AmplificationRecountTest.PanelSetId) == false)
                                    {
                                        result.Status = AuditStatusEnum.Failure;
                                        result.Message = "The results may not be accepted until the " + her2AmplificationRecountTest.PanelSetName + " is accepted.";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;
            Rules.MethodResult methodResult = base.IsOkToFinalize();
            if (methodResult.Success == false)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = methodResult.Message;
            }

            if(result.Status == AuditStatusEnum.OK)
            {
                HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationByISH.HER2AmplificationResultCollection(accessionOrder.PanelSetOrderCollection, this.m_ReportNo);
                HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindMatch();
                if (this.m_HER2ByIHCRequired == true)
                {
                    Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
                    if (this.CheckTestExists(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.IsOkToOrderIHC(accessionOrder);
                        if (auditResult.Status == AuditStatusEnum.Failure)
                        {
                            result.Status = auditResult.Status;
                            result.Message = auditResult.Message;
                        }
                        else
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message = "Unable to finalize as a " + her2AmplificationByIHCTest.PanelSetName + "is required";
                        }
                    }
                    else
                    {
                        if (this.CheckTestIsAccepted(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == false)
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message = "Unable to finalize as the " + her2AmplificationByIHCTest.PanelSetName + "has not been accepted";
                        }
                    }
                }

                if(result.Status == AuditStatusEnum.OK && this.m_RecountRequired == true)
                {
                    Test.HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();
                    if(this.CheckTestExists(accessionOrder, her2AmplificationRecountTest.PanelSetId) == false)
                    {
                        result.Status = AuditStatusEnum.Failure;
                        result.Message = "Unable to finalize as a " + her2AmplificationRecountTest.PanelSetName + "is required";
                    }
                    else
                    {
                        if (this.CheckTestIsAccepted(accessionOrder, her2AmplificationRecountTest.PanelSetId) == false)
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message = "Unable to finalize as  " + her2AmplificationRecountTest.PanelSetName + "has not been accepted";
                        }
                    }
                }
            }

            return result;
        }

        public AuditResult IsOkToOrderIHC(AccessionOrder accessionOrder)
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;

            Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            if (this.CheckTestExists(accessionOrder, her2AmplificationByIHCTest.PanelSetId) == true)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = "A Her2 Amplification By IHC has already been ordered.";
            }
            else if(accessionOrder.ClientId == 1201 || accessionOrder.ClientId == 1469 || accessionOrder.ClientId == 1476) // Pathology Associates of Idaho Falls, Teton Radiology Idaho Falls, Steele Memorial Medical Center Lab
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = "Normally a Her2 Amplification By IHC would need to be ordered.  However, this order is from a client that may have already performed the IHC.  A course of action must be determined.";
            }
            return result;
        }

        private bool CheckTestExists(AccessionOrder accessionOrder, int panelSetId)
        {
            bool result = accessionOrder.PanelSetOrderCollection.Exists(panelSetId, this.OrderedOnId, true);
            return result;
        }

        private bool CheckTestIsAccepted(AccessionOrder accessionOrder, int panelSetId)
        {
            bool result = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetId, this.OrderedOnId, true).Accepted;
            return result;
        }

        public void SetHER2ByIHCRequired()
        {
            bool result = false;
            if (this.m_Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication)
            {
                if (this.AverageHer2Chr17SignalAsDouble.HasValue && this.AverageHer2NeuSignal.HasValue)
                {
                    if (this.AverageHer2Chr17SignalAsDouble >= 2.0 && this.AverageHer2NeuSignal < 4.0) result = true;
                    else if (this.AverageHer2Chr17SignalAsDouble < 2.0 && this.AverageHer2NeuSignal >= 4.0) result = true;
                }
            }
            this.HER2ByIHCRequired = result;
            if (result == false) this.m_RecountRequired = false;
        }
    }
}