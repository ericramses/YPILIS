using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public partial class OrderIdParser
    {
        private static readonly string LegacyReportNoPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d-\d+";
        private static readonly string LegacyReportNoNumberPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d-(?<ReportNoNumber>\d+)";
        private static readonly string LegacyReportNoLetterPattern = @"(?<ReportNoLetter>[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*)\d\d-\d+";
        private static readonly string LegacyReportNoYearPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*(?<ReportNoYear>\d\d)-\d+";
        private static readonly string LegacyPSAReportNoPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d\d+";

        private static readonly string LegacyMasterAccessionNoPattern = @"^\d+";
        private static readonly string MasterAccessionNoPattern = @"^\d\d-\d+";

        private static readonly string ReportNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*";
        private static readonly string ReportNoLetterPattern = @"^\d\d-\d+.(?<ReportNoPrefix>[AaBbFfMmPpRrSsTtYyQq])\d*";
        private static readonly string ReportNoNumberPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYyQq](?<ReportNoNumber>\d*)";
        private static readonly string ReportNoYearPattern = @"^(?<ReportNoYear>\d\d)-\d+.[AaBbFfMmPpRrSsTtYyQq]{1}\d*";

        private static readonly string PanelOrderIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+";
        private static readonly string PanelOrderNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO(?<PanelOrderNo>\d+)";

        private static readonly string TestOrderIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+.TO\d+";
        private static readonly string TestOrderNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+.TO(?<TestOrderNo>\d+)";        

		private static readonly string SurgicalSpecimenIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+";
		private static readonly string SurgicalSpecimenNoPattern = @"^\d\d-\d+.[Ss]\.SSR+(?<SurgicalSpecimenNo>\d+)";

		private static readonly string StainResultIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.STR\d+";
		private static readonly string StainResultNoPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.STR(?<StainResultNo>\d+)";

		private static readonly string IntraoperativeConsultationResultIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.IC\d+";
		private static readonly string IntraoperativeConsultationResultNoPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.IC(?<IntraopResultNo>\d+)";

		private static readonly string SurgicalAuditIdPattern = @"^\d\d-\d+.[Ss]\.SRA\d+";
		private static readonly string SurgicalAuditNoPattern = @"^\d\d-\d+.[Ss]\.SRA+(?<SurgicalAuditNo>\d+)";

		private static readonly string SurgicalSpecimenAuditIdPattern = @"^\d\d-\d+.[Ss]\.SRA\d+\.SSRA\d+";
		private static readonly string SurgicalSpecimenAuditNoPattern = @"^\d\d-\d+.[Ss]\.SRA+\d+\.SSRA(?<SurgicalSpecimenAuditNo>\d+)";

        private static readonly string CptBillingCodeIdPattern = @"^\d\d-\d+.CPT\d+";
        private static readonly string CptBillingCodeNoPattern = @"^\d\d-\d+.CPT(?<CptBillingCodeNo>\d+)";

        private static readonly string Icd9BillingCodeIdPattern = @"^\d\d-\d+.ICD\d+";
        private static readonly string Icd9BillingCodeNoPattern = @"^\d\d-\d+.ICD(?<IcdBillingCodeNo>\d+)";

        private static readonly string PanelSetOrderCommentIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*\.PSOC\d+";
        private static readonly string PanelSetOrderCommentNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*\.PSOC(?<PanelSetOrderCommentNo>\d+)";

        private static readonly string TaskOrderIdPattern = @"^\d\d-\d+.TSKO\d+";
        private static readonly string TaskOrderNoPattern = @"^\d\d-\d+.TSKO(?<TaskOrderNo>\d+)";

        private static readonly string TaskOrderDetailIdPattern = @"^\d\d-\d+.TSKO\d+\.TSKOD\d+";
        private static readonly string TaskOrderDetailNoPattern = @"^\d\d-\d+.TSKO\d+\.TSKOD(?<TaskOrderDetailNo>\d+)";        

        private static readonly string AmendmentIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.AM\d+";
        private static readonly string AmendmentNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.AM(?<AmendmentNo>\d+)";

        private static readonly string FlowMarkerIdPattern = @"^\d\d-\d+.[Ff]{1}\d*.FM\d+";
        private static readonly string FlowMarkerNoPattern = @"^\d\d-\d+.[Ff]{1}\d*.FM(?<FlowMarkerNo>\d+)";

        private static readonly string PanelSetOrderBillableIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PSOB\d+";
        private static readonly string PanelSetOrderBillableNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PSOB(?<PanelSetOrderBillableNo>\d+)";

        private string m_IdToParse;

        public OrderIdParser(string idToParse)
        {
            this.m_IdToParse = idToParse;
        }

        private static int GetIdNumber(string idString, string prefix)
        {
            int result = 0;
            if (idString.Contains(prefix) == true)
            {
                int startIndex = idString.IndexOf(prefix) + prefix.Length;
                result = Convert.ToInt32(idString.Substring(startIndex));
            }
            return result;
        }

        public string MasterAccessionNo
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(MasterAccessionNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? MasterAccessionNoNumber
        {
            get
            {
                int? result = null;
                string masterAccessionNo = this.MasterAccessionNo;
                if (string.IsNullOrEmpty(masterAccessionNo) == false) result = Convert.ToInt32(masterAccessionNo.Substring(3));
                return result;
            }
        }

        public int? MasterAccessionNoYear
        {
            get
            {
                int? result = null;
                string masterAccessionNo = this.MasterAccessionNo;
                if (string.IsNullOrEmpty(masterAccessionNo) == false) result = Convert.ToInt32(masterAccessionNo.Substring(0, 2));
                return result;
            }
        }

        public bool IsLegacyMasterAccessionNo
        {
            get
            {
                bool result = false;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyMasterAccessionNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = true;
                return result;
            }
        }

        #region ReportNo
        public bool IsLegacyReportNo
        {
            get
            {
                bool result = false;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = true;
                return result;
            }
        }

        public bool IsLegacyPSAReportNo
        {
            get
            {
                bool result = false;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyPSAReportNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = true;
                return result;
            }
        }

        public bool IsValidMasterAccessionNo
        {
            get
            {
                bool result = false;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(MasterAccessionNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = true;
                return result;
            }
        }

        public string CreateCyotlogyReportNoFromMasterAccessionNo()
        {
            return this.MasterAccessionNo + ".P";
        }

        public string CreateSurgicalReportNoFromMasterAccessionNo()
        {
            return this.MasterAccessionNo + ".S";
        }

        public bool IsValidReportNo
        {
            get
            {
                bool result = false;
                if (string.IsNullOrEmpty(this.ReportNo) == false)
                {
                    if (string.IsNullOrEmpty(this.ReportNoLetter) == false)
                    {
                        result = true;
                    }
                }
                return result;
            }
        }

        public bool IsValidCytologyReportNo
        {
            get
            {
                bool result = false;
                if (this.ReportNo != null)
                {
                    if (this.ReportNoLetter == "P") result = true;
                }
                return result;
            }
        }

        public bool IsValidSurgicalReportNo
        {
            get
            {
                bool result = false;
                if (this.ReportNo != null)
                {
                    if (this.ReportNoLetter == "S") result = true;
                }
                return result;
            }
        }

        public string LegacyReportNo
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public string ReportNo
        {
            get
            {
                string result = null;
                if (this.IsLegacyReportNo == true) result = LegacyReportNo;
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (match.Captures.Count != 0) result = match.Value;
                }
                return result;
            }
        }

        public string ReportNoLetter
        {
            get
            {
                string result = null;
                if (this.IsLegacyReportNo == false)
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoLetterPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (match.Captures.Count != 0) result = match.Groups["ReportNoPrefix"].Captures[0].Value;
                }
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoLetterPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (match.Captures.Count != 0) result = match.Groups["ReportNoLetter"].Captures[0].Value;
                }
                return result;
            }
        }

        public int? ReportNoNumber
        {
            get
            {
                int? result = null;
                if (this.IsLegacyReportNo == true)
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoNumberPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (string.IsNullOrEmpty(match.Groups["ReportNoNumber"].Captures[0].Value) == false)
                        result = Convert.ToInt32(match.Groups["ReportNoNumber"].Captures[0].Value);
                }
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoNumberPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (string.IsNullOrEmpty(match.Groups["ReportNoNumber"].Captures[0].Value) == false)
                        result = Convert.ToInt32(match.Groups["ReportNoNumber"].Captures[0].Value);
                }
                return result;
            }
        }

        public int? ReportNoYear
        {
            get
            {
                int? result = null;
                if (this.IsLegacyReportNo == true)
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoYearPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (string.IsNullOrEmpty(match.Groups["ReportNoYear"].Captures[0].Value) == false)
                        result = Convert.ToInt32(match.Groups["ReportNoYear"].Captures[0].Value);
                }
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoYearPattern);
                    System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                    if (string.IsNullOrEmpty(match.Groups["ReportNoYear"].Captures[0].Value) == false)
                        result = Convert.ToInt32(match.Groups["ReportNoYear"].Captures[0].Value);
                }
                return result;
            }
        }

        public string SurgicalReportNoFromMasterAccessionNo
        {
            get
            {
                string result = null;
                if (this.MasterAccessionNo != null)
                {
                    result = this.MasterAccessionNo + ".S";
                }
                return result;
            }
        }

        public static string SurgicalReportNoFromNumber(string number)
        {
            string result = null;
            int num;
            if (Int32.TryParse(number, out num) == true)
            {
                if (DateTime.Today.Year > 2013) result = DateTime.Today.Year.ToString().Substring(2) + "-" + number + ".S";
                else result = "S" + DateTime.Today.Year.ToString().Substring(2) + "-" + number;
            }
            return result;
        }

        public static string IncrementReportNo(string reportNo, int increment)
        {
            string result = null;
            int number;
            OrderIdParser orderIdParser = new OrderIdParser(reportNo);
            if (orderIdParser.ReportNo != null)
            {
                if (orderIdParser.IsLegacyReportNo == true)
                {
                    number = orderIdParser.ReportNoNumber.Value + increment;
                    result = orderIdParser.ReportNoLetter + orderIdParser.ReportNoYear.Value.ToString() + "-" + number.ToString();
                }
                else
                {
                    number = orderIdParser.MasterAccessionNoNumber.Value + increment;
                    result = orderIdParser.ReportNoYear.Value.ToString() + "-" + number.ToString() + "." + orderIdParser.ReportNoLetter;
                    int? reportNumber = orderIdParser.ReportNoNumber;
                    if (reportNumber != null) result += reportNumber.Value.ToString();
                }
            }
            return result;
        }

        public string LegacyReportNoFromLegacyPSAReportNo
        {
            get
            {
                string result = null;
                if (this.IsLegacyPSAReportNo == true)
                {
                    result = this.m_IdToParse.Substring(0, 3) + "-" + this.m_IdToParse.Substring(3);
                }
                return result;
            }
        }
        #endregion

        public string PanelOrderId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelOrderIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? PanelOrderNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelOrderNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["PanelOrderNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["PanelOrderNo"].Captures[0].Value);
                return result;
            }
        }               
        
        public string SurgicalSpecimenId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? SurgicalSpecimenResultNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["SurgicalSpecimenNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["SurgicalSpecimenNo"].Captures[0].Value);
                return result;
            }
        }

        public string StainResultId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(StainResultIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? StainResultNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(StainResultNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["StainResultNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["StainResultNo"].Captures[0].Value);
                return result;
            }
        }

        public string IntraperativeConsultationResultId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(IntraoperativeConsultationResultIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? IntraperativeConsultationResultNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(IntraoperativeConsultationResultNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["IntraopResultNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["IntraopResultNo"].Captures[0].Value);
                return result;
            }
        }

        public string SurgicalAuditId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalAuditIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? SurgicalAuditNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalAuditNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["SurgicalAuditNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["SurgicalAuditNo"].Captures[0].Value);
                return result;
            }
        }

        public string SurgicalSpecimenResultAuditId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenAuditIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? SurgicalSpecimenResultAuditNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenAuditNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["SurgicalSpecimenAuditNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["SurgicalSpecimenAuditNo"].Captures[0].Value);
                return result;
            }
        }

        public string CptBillingCodeId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(CptBillingCodeIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? CptBillingCodeNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(CptBillingCodeNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["CptBillingCodeNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["CptBillingCodeNo"].Captures[0].Value);
                return result;
            }
        }

        public string Icd9BillingCodeId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(Icd9BillingCodeIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? Icd9BillingCodeNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(Icd9BillingCodeNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["IcdBillingCodeNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["IcdBillingCodeNo"].Captures[0].Value);
                return result;
            }
        }

        public string PanelSetOrderCommentId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderCommentIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? PanelSetOrderCommentNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderCommentNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["PanelSetOrderCommentNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["PanelSetOrderCommentNo"].Captures[0].Value);
                return result;
            }
        }

        #region TaskOrder
        public string TaskOrderId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? TaskOrderNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["TaskOrderNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["TaskOrderNo"].Captures[0].Value);
                return result;
            }
        }

        public static string GetNextTaskOrderId(YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection, string masterAccessionNo)
        {
            string result = string.Empty;
            int largestId = 0;
			foreach (YellowstonePathology.Business.Task.Model.TaskOrder taskOrder in taskOrderCollection)
            {
                OrderIdParser orderIdParser = new OrderIdParser(taskOrder.TaskOrderId);
                int? taskOrderNo = orderIdParser.TaskOrderNo;
                if (taskOrderNo == null)
                {
					int currentId = GetIdNumber(taskOrder.TaskOrderId, YellowstonePathology.Business.Task.Model.TaskOrderCollection.PREFIXID);
                    if (currentId > largestId) largestId = currentId;
                }
                else
                {
                    if (taskOrderNo.Value > largestId) largestId = taskOrderNo.Value;
                }
            }
			return masterAccessionNo + "." + YellowstonePathology.Business.Task.Model.TaskOrderCollection.PREFIXID + (largestId + 1).ToString();
        }
        #endregion

        #region TaskOrderDetail
        public string TaskOrderDetailId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderDetailIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? TaskOrderDetailNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderDetailNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["TaskOrderDetailNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["TaskOrderDetailNo"].Captures[0].Value);
                return result;
            }
        }

		public static string GetNextTaskOrderDetailId(YellowstonePathology.Business.Task.Model.TaskOrderDetailCollection taskOrderDetailCollection, string taskOrderId)
        {
            string result = string.Empty;
            int largestId = 0;
			foreach (YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail in taskOrderDetailCollection)
            {
                OrderIdParser orderIdParser = new OrderIdParser(taskOrderDetail.TaskOrderDetailId);
                int? taskOrderDetailNo = orderIdParser.TaskOrderDetailNo;
                if (taskOrderDetailNo == null)
                {
					int currentId = GetIdNumber(taskOrderDetail.TaskOrderDetailId, YellowstonePathology.Business.Task.Model.TaskOrderDetailCollection.PREFIXID);
                    if (currentId > largestId) largestId = currentId;
                }
                else
                {
                    if (taskOrderDetailNo.Value > largestId) largestId = taskOrderDetailNo.Value;
                }
            }
			return taskOrderId + "." + YellowstonePathology.Business.Task.Model.TaskOrderDetailCollection.PREFIXID + (largestId + 1).ToString();
        }
        #endregion

        #region Amendment
        public string AmendmentId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AmendmentIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? AmendmentNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AmendmentNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["AmendmentNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["AmendmentNo"].Captures[0].Value);
                return result;
            }
        }

        public static string GetNextAmendmentId(YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection, string reportNo)
        {
            string result = string.Empty;
            int largestId = 0;
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                OrderIdParser orderIdParser = new OrderIdParser(amendment.AmendmentId);
                int? amendmentNo = orderIdParser.AmendmentNo;
                if (amendmentNo == null)
                {
                    int currentId = GetIdNumber(amendment.AmendmentId, YellowstonePathology.Business.Amendment.Model.AmendmentCollection.PREFIXID);
                    if (currentId > largestId) largestId = currentId;
                }
                else
                {
                    if (amendmentNo.Value > largestId) largestId = amendmentNo.Value;
                }
            }
            return reportNo + "." + YellowstonePathology.Business.Amendment.Model.AmendmentCollection.PREFIXID + (largestId + 1).ToString();
        }
        #endregion

        public string FlowMarkerId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(FlowMarkerIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? FlowMarkerNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(FlowMarkerNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["FlowMarkerNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["FlowMarkerNo"].Captures[0].Value);
                return result;
            }
        }

        public string PanelSetOrderBillableId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderBillableIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? PanelSetOrderBillableNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderBillableNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["PanelSetOrderBillableNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["PanelSetOrderBillableNo"].Captures[0].Value);
                return result;
            }
        }
    }
}
