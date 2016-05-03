using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class AORemover
    {
        public AORemover()
        { }

        public void Remove(Business.Test.AccessionOrder accessionOrder, object writer)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(accessionOrder.ClientOrderId, writer);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(clientOrder, writer);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(accessionOrder, writer);
        }
    }
}
