using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain.Billing
{	
	public class BillingSpecimenCollection : ObservableCollection<BillingSpecimen>
	{
        public void RemoveAllReportNoExcept(string reportNoToKeep)
        {
			List<string> specimenOrderIdsToDelete = new List<string>();

            foreach (BillingSpecimen billingSpecimen in this)
            {
                foreach (BillingReport billingReport in billingSpecimen.BillingReportCollection)
                {
                    if (billingReport.ReportNo != reportNoToKeep)
                    {
                        specimenOrderIdsToDelete.Add(billingSpecimen.SpecimenOrderId);
                    }
                }
            }

			foreach (string specimenOrderId in specimenOrderIdsToDelete)
            {
                this.Delete(specimenOrderId);
            }
        }

		public void Delete(string specimenOrderId)
        {
            for (int i=0; i<this.Count; i++)
            {
                if (this[i].SpecimenOrderId == specimenOrderId)
                {
                    this.RemoveItem(i);
                    break;
                }
            }
        }
	}
}
