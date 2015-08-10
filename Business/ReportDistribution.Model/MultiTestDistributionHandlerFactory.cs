using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerFactory
    {
        public static MultiTestDistributionHandler GetHandler(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            MultiTestDistributionHandler result = null;
            if (accessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
            {
                WHPHoldList holdList = new WHPHoldList();
                if (holdList.Exists(accessionOrder.PhysicianId) == true)
                {
                    result = new MultiTestDistributionHandlerWHPHold(accessionOrder);
                }
                else
                {
                    YellowstonePathology.Business.Client.Model.ClientGroupStVincent clientGroupStVincent = new YellowstonePathology.Business.Client.Model.ClientGroupStVincent();
                    if (clientGroupStVincent.Exists(accessionOrder.ClientId) == true)
                    {
                        result = new MultiTestDistributionHandlerWHPSVH(accessionOrder);
                    }
                    else
                    {
                        result = new MultiTestDistributionHandlerWHPStandard(accessionOrder);
                    }
                }
            }
            else
            {
                result = new MultiTestDistributionHandler(accessionOrder);
            }
            return result;
        }
    }
}
