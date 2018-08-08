using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEResultCollection : ObservableCollection<LSEResult>
	{
		public LSEResultCollection()
		{
			
		}

        public LSEResultCollection OrderByMatched()
        {
            LSEResultCollection result = new LSEResultCollection();
            IOrderedEnumerable<LSEResult> orderedResult= this.OrderBy(i => i.IHCMatched);
            for(int i=orderedResult.Count() - 1; i>=0; i--)
            {
                result.Add(orderedResult.ElementAt(i));
            }
            return result;
        }

        public void ClearMatched()
        {
            foreach (LSEResult lseResult in this)
            {
                lseResult.IHCMatched = false;
            }
        }

        public bool HasIHCMatch(LSEResult lseResultToMatch)
        {
            bool result = false;
            foreach(LSEResult lseResult in this)
            {
                if(lseResult.IsIHCMatch(lseResultToMatch) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void SetIHCMatch(LSEResult lseResultToMatch)
        {            
            foreach (LSEResult lseResult in this)
            {
                if (lseResult.IsIHCMatch(lseResultToMatch) == true)
                {
                    lseResult.IHCMatched = true;                    
                }
            }            
        }

        public static LSEResultCollection GetAll()
        {
            LSEResultCollection result = new LSEResultCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonMSH2MSH6Loss());
            result.Add(new LSEColonMSH2Loss());
            result.Add(new LSEColonMSH6Loss());
            result.Add(new LSEColonPMS2Loss());

            result.Add(new LSEColorectalResult2());
            result.Add(new LSEColorectalResult12());
            result.Add(new LSEColorectalResult12B());
            result.Add(new LSEColorectalResult3());
            result.Add(new LSEColorectalResult32());
            result.Add(new LSEColorectalResult4());
            result.Add(new LSEColorectalResult42());            
            result.Add(new LSEColorectalResult6());            
            result.Add(new LSEColorectalResult8());                        
            result.Add(new LSEColorectalResult11());

            result.Add(new LSEGYNResult1());
            result.Add(new LSEGYNResult2());
            result.Add(new LSEGYNResult3());
            result.Add(new LSEGYNResult3a());

            result.Add(new LSEGeneralResult1());
            result.Add(new LSEGeneralResult2());

            return result;
        }

        public static LSEResultCollection GetColonResults()
        {
            LSEResultCollection result = new LSEResultCollection();

            result.Add(new LSEColonAllIntact());
            result.Add(new LSEColonMSH2MSH6Loss());
            result.Add(new LSEColonMSH2Loss());
            result.Add(new LSEColonMSH6Loss());
            result.Add(new LSEColonPMS2Loss());

            result.Add(new LSEColorectalResult2());
            result.Add(new LSEColorectalResult12());
            result.Add(new LSEColorectalResult12B());
            result.Add(new LSEColorectalResult3());
            result.Add(new LSEColorectalResult32());
            result.Add(new LSEColorectalResult4());
            result.Add(new LSEColorectalResult42());            
            result.Add(new LSEColorectalResult6());            
            result.Add(new LSEColorectalResult8());                        
            result.Add(new LSEColorectalResult11());

            return result;
        }

        public static LSEResultCollection GetGYNResults()
        {
            LSEResultCollection result = new LSEResultCollection();
            result.Add(new LSEGYNResult1());
            result.Add(new LSEGYNResult2());
            result.Add(new LSEGYNResult3());
            result.Add(new LSEGYNResult3a());
            return result;
        }

        public static LSEResultCollection GetProstateResults()
        {
            LSEResultCollection result = new LSEResultCollection();
            result.Add(new LSEGeneralResult1());
            result.Add(new LSEGeneralResult2());
            return result;
        }        

        public static LSEResult GetResult(LSEResult evalResult, string lseType)
		{
			LSEResult result = null;
            LSEResultCollection collection = null;

            if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.COLON)
            {
                collection = LSEResultCollection.GetColonResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GYN)
            {
                collection = LSEResultCollection.GetGYNResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GENERAL)
            {
                collection = LSEResultCollection.GetProstateResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET)
            {
                collection = new LSEResultCollection();
            }

            if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GENERAL)
            {
                if(evalResult.AreAllIntact() == true)
                {
                    result = new Business.Test.LynchSyndrome.LSEGeneralResult1();
                }
                else if(evalResult.AreAnyLoss() == true)
                {
                    result = new Business.Test.LynchSyndrome.LSEGeneralResult2();
                }

                result.MLH1Result = evalResult.MLH1Result;
                result.MSH2Result = evalResult.MSH2Result;
                result.MSH6Result = evalResult.MSH6Result;
                result.PMS2Result = evalResult.PMS2Result;
            }
            else
            {
                foreach (LSEResult lSEResult in collection)
                {
                    if (lSEResult.MLH1Result == evalResult.MLH1Result &&
                        lSEResult.MSH2Result == evalResult.MSH2Result &&
                        lSEResult.MSH6Result == evalResult.MSH6Result &&
                        lSEResult.PMS2Result == evalResult.PMS2Result)
                    {
                        bool brafResultIsEqual = false;
                        if (evalResult.BRAFIsIndicated == true)
                        {
                            brafResultIsEqual = (lSEResult.BrafResult == evalResult.BrafResult);
                        }
                        else
                        {
                            brafResultIsEqual = true;
                        }

                        bool methResultIsEqual = false;
                        if (lSEResult.MethResult == LSEResultEnum.NotPerformed)
                        {
                            methResultIsEqual = true;
                        }
                        else
                        {
                            methResultIsEqual = (lSEResult.MethResult == evalResult.MethResult);
                        }

                        if (brafResultIsEqual == true && methResultIsEqual == true)
                        {
                            result = lSEResult;
                            break;
                        }
                    }
                }
            }                          			            

			return result;
		}
	}
}
