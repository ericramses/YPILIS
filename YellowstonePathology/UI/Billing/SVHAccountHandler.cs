using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Billing
{
    public class SVHAccountHandler
    {                
        public void MatchAccessionOrdersToADT(DateTime postDate)
        {
            List<Business.Billing.Model.AccessionListItem> accessionList = Business.Gateway.AccessionOrderGateway.GetSVHNotPosted();
            foreach (Business.Billing.Model.AccessionListItem accessionListItem in accessionList)
            {
                List<Business.Billing.Model.ADTListItem> adtList = Business.Gateway.AccessionOrderGateway.GetADTList(accessionListItem.PFirstName, accessionListItem.PLastName, accessionListItem.PBirthdate.Value);

                foreach (Business.Billing.Model.ADTListItem adtItem in adtList)
                {
                    DateTime received = DateTime.Parse(adtItem.DateReceived.ToShortDateString());
                    int daysDiff = (int)(postDate - received).TotalDays;
                    if (daysDiff <= 3)
                    {
                        if (adtItem.MedicalRecord.StartsWith("V") == true)
                        {
                            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(accessionListItem.MasterAccessionNo, this);
                            Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(accessionListItem.ReportNo);

                            foreach (Business.Test.PanelSetOrderCPTCodeBill psocb in pso.PanelSetOrderCPTCodeBillCollection)
                            {
                                if (psocb.BillTo == "Client")
                                {
                                    psocb.MedicalRecord = adtItem.MedicalRecord;
                                    psocb.Account = adtItem.Account;
                                }

                                if (psocb.PostDate.HasValue == false)
                                    psocb.PostDate = postDate;
                            }

                            Business.Persistence.DocumentGateway.Instance.Push(ao, this);
                            break;
                        }
                    }
                }
            }
        }
    }
}
