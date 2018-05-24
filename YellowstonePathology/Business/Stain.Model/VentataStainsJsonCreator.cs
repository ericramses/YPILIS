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
            stain.VentanaBenchMarkWetId = benchmark.VentanaBenchMarkId;
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
            stain.VentanaBenchMarkId = benchmark.VentanaBenchMarkId;
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

            return stain;
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
                                    Business.Test.Model.Test secondTest = this.m_AllTests.GetTest(((Business.Test.Model.DualStain)test).SecondTest.TestId);
                                    stain.SecondStain = this.CreateStain(secondTest, benchmark);
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
                        if (stain.VentanaBenchMarkId != benchmark.VentanaBenchMarkId)
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

/*
Business.Test.Model.TestCollection tests = Business.Test.Model.TestCollection.GetAllTests(true);
Business.Test.Model.TestCollection testsToUse = new Business.Test.Model.TestCollection();

Business.Surgical.VentanaBenchMarkCollection ventanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();
            foreach (Business.Surgical.VentanaBenchMark benchmark in ventanaBenchMarkCollection)
            {
                string[] ids = new string[2];
ids[0] = benchmark.YPITestId;
                if (ids[0].Contains('/'))
                {
                    string id = ids[0];
ids = id.Split('/');
                }

                for (int idx = 0; idx< 2; idx++)
                {
                    string testid = ids[idx];
                    if (string.IsNullOrEmpty(testid) == false)
                    {
                        Business.Test.Model.Test test = tests.GetTest(testid);
                        if (test != null)
                        {
                            if (testsToUse.Exists(test.TestId) == false) testsToUse.Add(test);
                        }
                        else
                        {
                            //MessageBox.Show("Missing testid " + testid);
                        }
                    }
                }
            }

            int cnt = testsToUse.Count;
            for (int idx = 0; idx<cnt; idx++)
            {
                Business.Test.Model.Test dualTest = testsToUse[idx];
                if (dualTest is Business.Test.Model.DualStain)
                {
                    string id = ((Business.Test.Model.DualStain)dualTest).FirstTest.TestId;
                    if (testsToUse.Exists(id) == false)
                    {
                        Business.Test.Model.Test test = tests.GetTest(id);
                        if (test != null) testsToUse.Add(test);
                        else
                        {
                            //MessageBox.Show("Missing " + test.TestName + " - " + test.TestId);
                            return;
                        }
                    }
                    id = ((Business.Test.Model.DualStain)dualTest).SecondTest.TestId;
                    if (testsToUse.Exists(id) == false)
                    {
                        Business.Test.Model.Test test = tests.GetTest(id);
                        if (test != null) testsToUse.Add(test);
                        else
                        {
                            //MessageBox.Show("Missing " + test.TestName + " - " + test.TestId);
                            return;
                        }
                    }
                }
            }
///////////
            int num = 0;
int numb = 0;
StringBuilder names = new StringBuilder();
            using (StreamWriter sw = new StreamWriter(@"C:\ProgramData\ypi\lisdata\StainCollection2.json", false))
            {
                sw.WriteLine("{\"root\": [");
                foreach (Business.Test.Model.Test test in testsToUse)
                {
                    names.Append(test.TestName);
                    numb++;
                    foreach (Business.Surgical.VentanaBenchMark benchmark in ventanaBenchMarkCollection)
                    {
                        bool found = false;
string[] ids = new string[2];
ids[0] = benchmark.YPITestId;
                        if (ids[0].Contains('/'))
                        {
                            string id = ids[0];
ids = id.Split('/');
                        }
                        for (int idx = 0; idx< 2; idx++)
                        {
                            string testid = ids[idx];
                            if (string.IsNullOrEmpty(testid) == false)
                            {
                                if (test.TestId == testid)
                                {
                                    sw.Write("{  \"stainType\": \"");
                                    if (test is Business.Test.Model.DualStain == true) sw.Write("DualStain");
                                    else if (test is Business.Test.Model.CytochemicalTest == true) sw.Write("CytochemicalStain");
                                    else if (test is Business.Test.Model.GradedTest == true) sw.Write("GradedStain");
                                    else if (test is Business.Test.Model.CytochemicalForMicroorganisms == true) sw.Write("CytochemicalForMicroorganisms");
                                    else if (test is Business.Test.Model.ImmunoHistochemistryTest == true) sw.Write("IHC");
                                    else if (test is Business.Test.Model.NoCptCodeTest == true) sw.Write("Unknown");
                                    else sw.Write("Stain");
                                    sw.WriteLine("\",");
                                    if (benchmark.StainerType == "BenchMark Special Stains")
                                    {
                                        sw.WriteLine("  \"isSpecialStain\": true,");
                                    }
                                    else sw.WriteLine("  \"isSpecialStain\": false,");

                                    if (test is Business.Test.Model.DualStain)
                                    {
                                        Business.Test.Model.Test ftest = tests.GetTest(((Business.Test.Model.DualStain)test).FirstTest.TestId);
sw.WriteLine("  \"FirstTestName\": \"" + ((Business.Test.Model.DualStain)test).FirstTest.TestName + "\",");
                                        sw.Write("  \"stainType\": \"");
                                        if (ftest is Business.Test.Model.DualStain == true) sw.Write("DualStain");
                                        else if (ftest is Business.Test.Model.CytochemicalTest == true) sw.Write("CytochemicalStain");
                                        else if (ftest is Business.Test.Model.GradedTest == true) sw.Write("GradedStain");
                                        else if (ftest is Business.Test.Model.CytochemicalForMicroorganisms == true) sw.Write("CytochemicalForMicroorganisms");
                                        else if (ftest is Business.Test.Model.ImmunoHistochemistryTest == true) sw.Write("IHC");
                                        else if (ftest is Business.Test.Model.NoCptCodeTest == true) sw.Write("Unknown");
                                        else sw.Write("Stain");
                                        sw.WriteLine("\",");
                                        sw.WriteLine("  \"isSpecialStain\": \"find out\",");
                                        Business.Test.Model.Test stest = tests.GetTest(((Business.Test.Model.DualStain)test).SecondTest.TestId);
sw.WriteLine("  \"SecondTestName\": \"" + ((Business.Test.Model.DualStain)test).SecondTest.TestName + "\",");
                                        sw.Write("  \"stainType\": \"");
                                        if (stest is Business.Test.Model.DualStain == true) sw.Write("DualStain");
                                        else if (stest is Business.Test.Model.CytochemicalTest == true) sw.Write("CytochemicalStain");
                                        else if (stest is Business.Test.Model.GradedTest == true) sw.Write("GradedStain");
                                        else if (stest is Business.Test.Model.CytochemicalForMicroorganisms == true) sw.Write("CytochemicalForMicroorganisms");
                                        else if (stest is Business.Test.Model.ImmunoHistochemistryTest == true) sw.Write("IHC");
                                        else if (stest is Business.Test.Model.NoCptCodeTest == true) sw.Write("Unknown");
                                        else sw.Write("Stain");
                                        sw.WriteLine("\",");
                                        sw.WriteLine("  \"isSpecialStain\": \"find out\",");
                                    }
                                    sw.Write(test.ToJSON());
                                    sw.WriteLine(",");
                                    found = true;
                                    names.AppendLine(" wrote " + test.TestName);
                                    num++;
                                }
                                else if (test.TestId == "116")
                                {
                                    sw.WriteLine("{  \"stainType\": \"GradedStain\",");
                                    sw.WriteLine("  \"isSpecialStain\": false,");
                                    sw.Write(test.ToJSON());
                                    found = true;
                                    names.AppendLine(" wrote " + test.TestName);
                                    num++;
                                }
                            }
                        }
                        if (found) break;
                    }
                }
                sw.WriteLine("]}");
            }

            using (StreamWriter nsw = new StreamWriter(@"C:\ProgramData\ypi\lisdata\StainsWritten.txt", false))
            {
                nsw.WriteLine(names.ToString());
            }
*/