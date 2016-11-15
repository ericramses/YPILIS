﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEResultStatusCompleteAfterMSI : LSEResultStatus
    {        
        public LSEResultStatusCompleteAfterMSI(LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string orderedOnId) 
            : base(lseResult, accessionOrder, orderedOnId)
        {
            LSEResult result1 = new LSEResult();
            result1.MLH1Result = LSEResultEnum.Intact;
            result1.MSH2Result = LSEResultEnum.Intact;
            result1.MSH6Result = LSEResultEnum.Intact;
            result1.PMS2Result = LSEResultEnum.Intact;
            m_LSEResultList.Add(result1);

            LSEResult result2 = new LSEResult();
            result2.MLH1Result = LSEResultEnum.Intact;
            result2.MSH2Result = LSEResultEnum.Loss;
            result2.MSH6Result = LSEResultEnum.Loss;
            result2.PMS2Result = LSEResultEnum.Intact;
            m_LSEResultList.Add(result2);

            LSEResult result3 = new LSEResult();
            result3.MLH1Result = LSEResultEnum.Intact;
            result3.MSH2Result = LSEResultEnum.Loss;
            result3.MSH6Result = LSEResultEnum.Intact;
            result3.PMS2Result = LSEResultEnum.Intact;
            m_LSEResultList.Add(result3);

            LSEResult result4 = new LSEResult();
            result4.MLH1Result = LSEResultEnum.Intact;
            result4.MSH2Result = LSEResultEnum.Intact;
            result4.MSH6Result = LSEResultEnum.Loss;
            result4.PMS2Result = LSEResultEnum.Intact;
            m_LSEResultList.Add(result4);

            LSEResult result5 = new LSEResult();
            result5.MLH1Result = LSEResultEnum.Intact;
            result5.MSH2Result = LSEResultEnum.Intact;
            result5.MSH6Result = LSEResultEnum.Intact;
            result5.PMS2Result = LSEResultEnum.Loss;
            m_LSEResultList.Add(result5);            

            if (this.IsMatch() == true)
            {
                this.m_IsMatch = true;
                this.m_IsOrdered = true;
                this.m_Status = "No further testing is required.";
            }
        }


        public override bool IsMatch()
        {
            bool result = false;
            foreach (LSEResult item in this.m_LSEResultList)
            {
                if (item.MLH1Result == this.m_LSEResult.MLH1Result &&
                    item.MSH2Result == this.m_LSEResult.MSH2Result &&
                    item.MSH6Result == this.m_LSEResult.MSH6Result &&
                    item.PMS2Result == this.m_LSEResult.PMS2Result)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        
    }
}
