﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class HPV1618OneOrBothPositiveResult : HPV1618Result
    {                        
        public HPV1618OneOrBothPositiveResult(string pantherHPV16Result, string pantherHPV18Result) 
        {
            if(pantherHPV16Result == HPV1618Result.PantherHPV16PositiveResult)
            {
                this.m_HPV16Result = "Positive";
                this.m_HPV16ResultCode = "HPV1618G16PSTV";
            }
            else
            {
                this.m_HPV16Result = "Negative";
                this.m_HPV16ResultCode = "HPV1618G16NGTV";
            }

            if(pantherHPV18Result == HPV1618Result.PantherHPV1845PositiveResult)
            {
                this.m_HPV18Result = "Positive";
                this.m_HPV18ResultCode = "HPV1618G18PSTV";
            }
            else
            {
                this.m_HPV18Result = "Negative";
                this.m_HPV18ResultCode = "HPV1618G18PSTV";
            }                       
        }

        public override string GetSqlStatement(string aliquotOrderId)
        {
            string sql = @"Update tblPanelSetOrderHPV1618 set HPV16Result = '" + this.m_HPV16Result + "',  "
                        + "HPV16ResultCode = '" + this.m_HPV16ResultCode + "', "
                        + "HPV18Result = '" + this.m_HPV18Result + "', "
                        + "HPV18ResultCode = '" + this.m_HPV18ResultCode + "' "
                        + "from tblPanelSetOrderHPV1618 hpv, tblPanelSetOrder pso "
                        + "where hpv.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

            sql += @"Update tblPanelSetOrder set [HoldDistribution] = 1, "
            + "[Accepted] = 1, "
            + "[AcceptedBy] = 'Optimus Prime', "
            + "[AcceptedById] = 5134, "
            + "[AcceptedDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
            + "[AcceptedTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "' "            
            + "where PanelSetId = 62 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";

            return sql;
        }
    }
}
