using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Stain.Model
{
    public class TestFactory
    {
        public static Test.Model.Test CreateTestFromStain(Stain stain)
        {
            Test.Model.Test test = null;
            switch (stain.StainType)
            {
                case "IHC":
                    {
                        test = new Test.Model.ImmunoHistochemistryTest();
                        LoadTest(stain, test);
                        break;
                    }
                case "CytochemicalStain":
                    {
                        test = new Test.Model.CytochemicalTest();
                        LoadTest(stain, test);
                        break;
                    }
                case "CytochemicalForMicroorganisms":
                    {
                        test = new Test.Model.CytochemicalForMicroorganisms();
                        LoadTest(stain, test);
                        break;
                    }
                case "GradedStain":
                    {
                        test = new Test.Model.GradedTest();
                        LoadTest(stain, test);
                        break;
                    }
                case "DualStain":
                    {
                        Test.Model.Test firstTest = CreateTestFromStain(stain.FirstStain);
                        Test.Model.Test secondTest = CreateTestFromStain(stain.SecondStain);
                        test = new Test.Model.DualStain(firstTest, secondTest, stain.DepricatedFirstStainId, stain.DepricatedSecondStainId);
                        LoadTest(stain, test);
                        break;
                    }
            }

            return test;
        }

        private static void LoadTest(Stain stain, Test.Model.Test test)
        {
            test.OrderComment = stain.OrderComment;
            test.IsBillable = stain.IsBillable;
            test.HasGCode = stain.HasGCode;
            test.HasCptCodeLevels = stain.HasCptCodeLevels;
            test.IsDualOrder = stain.IsDualOrder;
            test.TestId = stain.TestId;
            test.TestName = stain.StainName;
            test.TestAbbreviation = stain.StainAbbreviation;
            test.Active = stain.Active;
            test.StainResultGroupId = stain.StainResultGroupId;
            test.AliquotType = stain.AliquotType;
            test.NeedsAcknowledgement = stain.NeedsAcknowledgement;
            test.DefaultResult = stain.DefaultResult;
            test.RequestForAdditionalReport = stain.RequestForAdditionalReport;
            test.UseWetProtocol = stain.UseWetProtocol;
            test.PerformedByHand = stain.PerformedByHand;
        }
    }
}
