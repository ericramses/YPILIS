using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace YellowstonePathology.Business.Stain.Model
{
    public class VentataStainsJsonCreator
    {
        private Business.Test.Model.TestCollection m_AllTests;
        private Business.Test.Model.TestCollection m_TestsToUse;
        private StainCollection m_Stains;
        private Business.Surgical.VentanaBenchMarkCollection m_VentanaBenchMarkCollection;
        private Business.Surgical.VentanaBenchMarkCollection m_WetStains;
        private Business.Rules.MethodResult m_MethodResult;

        public VentataStainsJsonCreator()
        {
            this.m_AllTests = Business.Test.Model.TestCollection.GetAllTests(true);
            this.m_TestsToUse = new Test.Model.TestCollection();
            this.m_Stains = new StainCollection();
            this.m_VentanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();

            this.m_WetStains = new Surgical.VentanaBenchMarkCollection();
            foreach (Surgical.VentanaBenchMark benchmark in this.m_VentanaBenchMarkCollection)
            {
                if (benchmark.IsWetProtocol == true)
                {
                    this.m_WetStains.Add(benchmark);
                }
            }

            this.m_MethodResult = new Rules.MethodResult();
        }

        public void CreateStains()
        {
            this.BuildTestsToUse();
            this.AddTestsForDualStains();
            this.BuildStains();
            this.SetWetStains();
            this.SetDualWetStains();
            this.WriteJson();
        }

        private void BuildTestsToUse()
        {
            foreach (Business.Surgical.VentanaBenchMark benchmark in this.m_VentanaBenchMarkCollection)
            {
                string[] ids = GetYPIId(benchmark.YPITestId);
                foreach (string testid in ids)
                {
                    if (string.IsNullOrEmpty(testid) == false)
                    {
                        Business.Test.Model.Test test = this.m_AllTests.GetTest(testid);
                        if (test != null)
                        {
                            if (this.m_TestsToUse.Exists(test.TestId) == false) this.m_TestsToUse.Add(test);
                        }
                        else
                        {
                            this.m_MethodResult.Success = false;
                            this.m_MethodResult.Message += "Missing testid " + testid;
                        }
                    }
                }
            }
        }

        private string[] GetYPIId(string benchmarkId)
        {
            string[] result = benchmarkId.Split(new char[] { '/' });
            return result;
        }

        private void AddTestsForDualStains()
        {
            int cnt = this.m_TestsToUse.Count;
            for (int idx = 0; idx < cnt; idx++)
            {
                Business.Test.Model.Test dualTest = this.m_TestsToUse[idx];
                if (dualTest is Business.Test.Model.DualStain)
                {
                    string id = ((Business.Test.Model.DualStain)dualTest).FirstTest.TestId;
                    if (this.m_TestsToUse.Exists(id) == false)
                    {
                        Business.Test.Model.Test test = this.m_AllTests.GetTest(id);
                        if (test != null) this.m_TestsToUse.Add(test);
                        else
                        {
                            this.m_MethodResult.Success = false;
                            this.m_MethodResult.Message += "All tests missing " + test.TestName + " - " + test.TestId;
                            return;
                        }
                    }
                    id = ((Business.Test.Model.DualStain)dualTest).SecondTest.TestId;
                    if (this.m_TestsToUse.Exists(id) == false)
                    {
                        Business.Test.Model.Test test = this.m_AllTests.GetTest(id);
                        if (test != null) this.m_TestsToUse.Add(test);
                        else
                        {
                            this.m_MethodResult.Success = false;
                            this.m_MethodResult.Message += "All tests missing " + test.TestName + " - " + test.TestId;
                            return;
                        }
                    }
                }
            }
        }

        private string GetStainType(Business.Test.Model.Test test)
        {
            string result = null;
            if (test is Business.Test.Model.DualStain == true) result = "DualStain";
            else if (test is Business.Test.Model.CytochemicalTest == true) result = "CytochemicalStain";
            else if (test is Business.Test.Model.GradedTest == true) result = "GradedStain";
            else if (test is Business.Test.Model.CytochemicalForMicroorganisms == true) result = "CytochemicalForMicroorganisms";
            else if (test is Business.Test.Model.ImmunoHistochemistryTest == true) result = "IHC";
            else if (test is Business.Test.Model.NoCptCodeTest == true) result = "Unknown";
            else result = "Stain";
            return result;
        }

        private void SetWetProtocol(Stain stain, Surgical.VentanaBenchMark benchmark)
        {
            stain.VentanaBenchMarkWetId = benchmark.BarcodeNumber;
            stain.VentanaBenchMarkWetProtocolName = benchmark.ProtocolName;
            stain.HasWetProtocol = true;
        }

        private Stain CreateStain(Business.Test.Model.Test test, Business.Surgical.VentanaBenchMark benchmark)
        {
            Stain stain = new Model.Stain();
            stain.StainType = this.GetStainType(test);
            stain.OrderComment = test.OrderComment;
            stain.TestId = test.TestId;
            stain.StainName = test.TestName;
            stain.StainAbbreviation = test.TestAbbreviation;
            stain.AliquotType = test.AliquotType;
            stain.DefaultResult = test.DefaultResult;
            stain.HistologyDisplayString = test.HistologyDisplayString;
            stain.StainerType = benchmark.StainerType;
            stain.VentanaBenchMarkId = benchmark.BarcodeNumber;
            stain.VentanaBenchMarkProtocolName = benchmark.ProtocolName;
            stain.StainResultGroupId = test.StainResultGroupId;
            stain.IsBillable = test.IsBillable;
            stain.HasGCode = test.HasGCode;
            stain.IsDualOrder = test.IsDualOrder;
            stain.HasCptCodeLevels = test.HasCptCodeLevels;
            stain.Active = test.Active;
            stain.NeedsAcknowledgement = test.NeedsAcknowledgement;
            stain.UseWetProtocol = benchmark.IsWetProtocol;
            stain.PerformedByHand = test.PerformedByHand;
            stain.RequestForAdditionalReport = test.RequestForAdditionalReport;

            this.SetCodes(stain, test);
            return stain;
        }

        private void SetCodes(Stain stain, Business.Test.Model.Test test)
        {
            Business.Billing.Model.CptCode cptCode = null;
            if (test is Business.Test.Model.DualStain == true)
            {
                cptCode = ((Test.Model.DualStain)test).GetCptCode(Test.Model.CptCodeLevelEnum.Initial, false);
                stain.CPTCode = cptCode.Code;
                cptCode = ((Test.Model.DualStain)test).GetCptCode(Test.Model.CptCodeLevelEnum.Subsequent, false);
                stain.SubsequentCPTCode = cptCode.Code;
                if (test.HasGCode == true)
                {
                    cptCode = ((Test.Model.ImmunoHistochemistryTest)test).GetGCode(Test.Model.CptCodeLevelEnum.Initial, false);
                    stain.GCode = cptCode.Code;
                }
            }
            else if (test is Business.Test.Model.ImmunoHistochemistryTest == true)
            {
                cptCode = ((Test.Model.ImmunoHistochemistryTest)test).GetCptCode(false);
                stain.CPTCode = cptCode.Code;
                cptCode = ((Test.Model.ImmunoHistochemistryTest)test).GetCptCode(Test.Model.CptCodeLevelEnum.Subsequent, false);
                stain.SubsequentCPTCode = cptCode.Code;
                if (test.HasGCode == true)
                {
                    cptCode = ((Test.Model.ImmunoHistochemistryTest)test).GetGCode(Test.Model.CptCodeLevelEnum.Initial, false);
                    stain.GCode = cptCode.Code;
                }
            }
            else if (test is Business.Test.Model.CytochemicalTest == true)
            {
                cptCode = ((Test.Model.CytochemicalTest)test).GetCptCode(false);
                stain.CPTCode = cptCode.Code;
                if (test.HasGCode == true)
                {
                    cptCode = ((Test.Model.CytochemicalTest)test).GetGCode(false);
                    stain.GCode = cptCode.Code;
                }
            }
            else if (test is Business.Test.Model.GradedTest == true)
            {
                cptCode = ((Test.Model.GradedTest)test).GetCptCode(false);
                stain.CPTCode = cptCode.Code;
                if (test.HasGCode == true)
                {
                    cptCode = ((Test.Model.GradedTest)test).GetGCode(false);
                    stain.GCode = cptCode.Code;
                }
            }
            else if (test is Business.Test.Model.CytochemicalForMicroorganisms == true)
            {
                cptCode = ((Test.Model.CytochemicalForMicroorganisms)test).GetCptCode(false);
                stain.CPTCode = cptCode.Code;
                if (test.HasGCode == true)
                {
                    cptCode = ((Test.Model.CytochemicalForMicroorganisms)test).GetGCode(false);
                    stain.GCode = cptCode.Code;
                }
            }
            else if (test is Business.Test.Model.NoCptCodeTest == true)
            {
                if (test.HasGCode == true)
                {
                    cptCode = ((Test.Model.NoCptCodeTest)test).GetGCode(false);
                    stain.GCode = cptCode.Code;
                }
            }
        }

        private void BuildStains()
        {
            foreach (Business.Test.Model.Test test in this.m_TestsToUse)
            {
                foreach (Business.Surgical.VentanaBenchMark benchmark in this.m_VentanaBenchMarkCollection)
                {
                    bool found = false;
                    string[] ids = this.GetYPIId(benchmark.YPITestId);
                    foreach (string testid in ids)
                    {
                        if (string.IsNullOrEmpty(testid) == false)
                        {
                            if (test.TestId == testid)
                            {
                                found = true;
                                Stain stain = this.CreateStain(test, benchmark);

                                if (test is Business.Test.Model.DualStain)
                                {
                                    Business.Test.Model.Test firstTest = this.m_AllTests.GetTest(((Business.Test.Model.DualStain)test).FirstTest.TestId);
                                    stain.FirstStain = this.CreateStain(firstTest, benchmark);
                                    stain.FirstStain.CPTCode = stain.CPTCode;
                                    stain.FirstStain.SubsequentCPTCode = stain.SubsequentCPTCode;
                                    stain.FirstStain.GCode = stain.GCode;

                                    Business.Test.Model.Test secondTest = this.m_AllTests.GetTest(((Business.Test.Model.DualStain)test).SecondTest.TestId);
                                    stain.SecondStain = this.CreateStain(secondTest, benchmark);
                                    stain.SecondStain.CPTCode = stain.CPTCode;
                                    stain.SecondStain.GCode = stain.GCode;
                                    stain.SecondStain.SubsequentCPTCode = stain.SubsequentCPTCode;

                                    stain.DepricatedFirstTestId = ((Business.Test.Model.DualStain)test).DepricatedFirstTestId;
                                    stain.DepricatedSecondTestId = ((Business.Test.Model.DualStain)test).DepricatedSecondTestId;
                                    stain.CPTCode = null;
                                    stain.SubsequentCPTCode = null;
                                    stain.GCode = null;
                                }
                                if (this.m_Stains.ExistsByTestid(stain.TestId) == false) this.m_Stains.Add(stain);
                            }
                            else if (test.TestId == "116")
                            {
                                Stain stain = this.CreateStain(test, benchmark);
                                this.m_Stains.Add(stain);
                                found = true;
                            }
                        }
                    }
                    if (found) break;
                }
            }
        }

        private void SetWetStains()
        {
            foreach (Stain stain in this.m_Stains)
            {
                this.SetWetForStain(stain);
            }
            Stain NKX31 = this.m_Stains.GetStainByTestId("355");
            Surgical.VentanaBenchMark benchmark = this.m_WetStains.GetByVentanaTestId("999");
            this.SetWetProtocol(NKX31, benchmark);
        }

        private void SetWetForStain(Stain stain)
        {
            foreach (Surgical.VentanaBenchMark benchmark in this.m_WetStains)
            {
                string[] ids = this.GetYPIId(benchmark.YPITestId);
                foreach (string testid in ids)
                {
                    if (testid == stain.TestId)
                    {
                        if (stain.VentanaBenchMarkId != benchmark.BarcodeNumber)
                            this.SetWetProtocol(stain, benchmark);
                    }
                }
            }
        }

        private void SetDualWetStains()
        {
            foreach (Stain stain in this.m_Stains)
            {
                if (stain.IsDualOrder == true)
                {
                    this.SetWetForStain(stain.FirstStain);
                    this.SetWetForStain(stain.SecondStain);
                }
            }
        }

        private void WriteJson()
        {
            string jString = this.m_Stains.ToJSON();
            using (StreamWriter sw = new StreamWriter(@"C:\ProgramData\ypi\lisdata\StainCollection3.json", false))
            {
                sw.Write(jString);
            }
        }
    }
}