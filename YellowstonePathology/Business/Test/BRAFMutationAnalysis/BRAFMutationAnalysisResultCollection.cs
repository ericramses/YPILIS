using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
    {
        public BRAFMutationAnalysisResult GetResult(string resultCode)
        {
            BRAFMutationAnalysisResult result = new BRAFMutationAnalysisResult();
            foreach (BRAFMutationAnalysisResult brafResult in this)
            {
                if (brafResult.ResultCode == resultCode)
                {
                    result = brafResult;
                    break;
                }
            }
            return result;
        }

        public BRAFMutationAnalysisResult GetResult(string resultCode, string indication)
        {
            BRAFMutationAnalysisResult result = new BRAFMutationAnalysisResult();
            foreach (BRAFMutationAnalysisResult brafResult in this)
            {
                if (brafResult is BRAFMutationAnalysisIndeterminateResult && brafResult.ResultCode == resultCode)
                {
                    result = brafResult;
                    break;
                }
                if (brafResult is BRAFMutationAnalysisInsufficientResult && brafResult.ResultCode == resultCode)
                {
                    result = brafResult;
                    break;
                }
                if (brafResult.ResultCode == resultCode && brafResult.Indication == indication)
                {
                    result = brafResult;
                    break;
                }
            }
            return result;
        }

        public static BRAFMutationAnalysisResultCollection GetUniqueResultChoices()
        {
            BRAFMutationAnalysisResultCollection result = new BRAFMutationAnalysisResultCollection();
            //result.Add(new BRAFMutationAnalysisDetectedResult());
            result.Add(new BRAFMutationAnalysisNotDetectedResult());
            //result.Add(new BRAFMutationAnalysisIndeterminateResult());
            //result.Add(new BRAFMutationAnalysisInsufficientResult());
            return result;
        }

        public static BRAFMutationAnalysisResultCollection GetDetectedResults()
        {
            BRAFMutationAnalysisResultCollection result = new BRAFMutationAnalysisResultCollection();
            //result.Add(new BRAFMutationAnalysisDetectedCRCResult());
            //result.Add(new BRAFMutationAnalysisDetectedLynchSyndromeResult());
            //result.Add(new BRAFMutationAnalysisDetectedMetastaticMelanomaResult());
            //result.Add(new BRAFMutationAnalysisDetectedPapillaryThyroidResult());
            return result;
        }

        public static BRAFMutationAnalysisResultCollection GetNotDetectedResults()
        {
            BRAFMutationAnalysisResultCollection result = new BRAFMutationAnalysisResultCollection();
            result.Add(new BRAFMutationAnalysisNotDetectedCRCResult());
            result.Add(new BRAFMutationAnalysisNotDetectedLynchSyndromeResult());
            result.Add(new BRAFMutationAnalysisNotDetectedMetastaticMelanomaResult());
            result.Add(new BRAFMutationAnalysisNotDetectedPapillaryThyroidResult());
            return result;
        }

        public static BRAFMutationAnalysisResultCollection GetAll()
        {
            BRAFMutationAnalysisResultCollection result = new BRAFMutationAnalysisResultCollection();
            result.Add(new BRAFMutationAnalysisNotDetectedCRCResult());
            result.Add(new BRAFMutationAnalysisNotDetectedLynchSyndromeResult());
            result.Add(new BRAFMutationAnalysisNotDetectedMetastaticMelanomaResult());
            result.Add(new BRAFMutationAnalysisNotDetectedPapillaryThyroidResult());
            //result.Add(new BRAFMutationAnalysisDetectedCRCResult());
            //result.Add(new BRAFMutationAnalysisDetectedLynchSyndromeResult());
            //result.Add(new BRAFMutationAnalysisDetectedMetastaticMelanomaResult());
            //result.Add(new BRAFMutationAnalysisDetectedPapillaryThyroidResult());
            //result.Add(new BRAFMutationAnalysisIndeterminateResult());
            //result.Add(new BRAFMutationAnalysisInsufficientResult());
            return result;
        }
    }
}
