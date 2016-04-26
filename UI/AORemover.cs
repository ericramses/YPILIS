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

        public YellowstonePathology.Business.Rules.MethodResult Remove(string masterAccessionNo)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            if (string.IsNullOrEmpty(accessionOrder.MasterAccessionNo) == true)
            {
                methodResult.Success = false;
                methodResult.Message = "The Master Accession No does not exist.";
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Clear(this);
            }
            else
            {
                if(accessionOrder.PLastName == "Mouse" && accessionOrder.PFirstName == "Mickey")
                {
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrderByClientOrderId(accessionOrder.ClientOrderId, this);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(clientOrder, this);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(accessionOrder, this);
                }
                else
                {
                    methodResult.Success = false;
                    methodResult.Message = "The Master Accession No is not a Mickey Mouse case.";
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this);
                }
            }
            return methodResult;
        }
    }
}
