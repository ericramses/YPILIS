﻿using System;
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

        public static LSEResultCollection GetAll()
        {
            LSEResultCollection result = new LSEResultCollection();

            result.Add(new LSEColorectalResult1());
            result.Add(new LSEColorectalResult1A());
            result.Add(new LSEColorectalResult2());
            result.Add(new LSEColorectalResult12());
            result.Add(new LSEColorectalResult12B());
            result.Add(new LSEColorectalResult3());
            result.Add(new LSEColorectalResult32());
            result.Add(new LSEColorectalResult4());
            result.Add(new LSEColorectalResult42());
            result.Add(new LSEColorectalResult5());
            result.Add(new LSEColorectalResult6());
            result.Add(new LSEColorectalResult7());
            result.Add(new LSEColorectalResult8());            
            result.Add(new LSEColorectalResult10());
            result.Add(new LSEColorectalResult11());

            result.Add(new LSEGYNResult1());
            result.Add(new LSEGYNResult2());
            result.Add(new LSEGYNResult3());
            result.Add(new LSEGYNResult3a());

            result.Add(new LSEProstateResult1());
            result.Add(new LSEProstateResult2());

            return result;
        }

        public static LSEResultCollection GetColonResults()
        {
            LSEResultCollection result = new LSEResultCollection();

            result.Add(new LSEColorectalResult1());
            result.Add(new LSEColorectalResult1A());
            result.Add(new LSEColorectalResult2());
            result.Add(new LSEColorectalResult12());
            result.Add(new LSEColorectalResult12B());
            result.Add(new LSEColorectalResult3());
            result.Add(new LSEColorectalResult32());
            result.Add(new LSEColorectalResult4());
            result.Add(new LSEColorectalResult42());
            result.Add(new LSEColorectalResult5());
            result.Add(new LSEColorectalResult6());
            result.Add(new LSEColorectalResult7());
            result.Add(new LSEColorectalResult8());            
            result.Add(new LSEColorectalResult10());
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
            result.Add(new LSEProstateResult1());
            result.Add(new LSEProstateResult2());
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
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.PROSTATE)
            {
                collection = LSEResultCollection.GetProstateResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET)
            {
                collection = new LSEResultCollection();
            }

            if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.PROSTATE)
            {
                if(evalResult.AreAllIntact() == true)
                {
                    result = new Business.Test.LynchSyndrome.LSEProstateResult1();
                }
                else if(evalResult.AreAnyLoss() == true)
                {
                    result = new Business.Test.LynchSyndrome.LSEProstateResult2();
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
                        if (lSEResult.MethResult == LSEResultEnum.NotApplicable)
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
