using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[CollectionDataContract]
	public class CptIcd9BillingCodeCollection : ObservableCollection<CptIcd9BillingCode>
    {
        public CptIcd9BillingCodeCollection()
        {

        }

		public void Add(string masterAccessionNo, string icd9BillingCodeId, string cptBillingCodeId)
        {
            CptIcd9BillingCode cptIcd9BillingCode = new CptIcd9BillingCode();
            cptIcd9BillingCode.Icd9BillingCodeId = icd9BillingCodeId;
            cptIcd9BillingCode.CptBillingCodeId = cptBillingCodeId;
            cptIcd9BillingCode.MasterAccessionNo = masterAccessionNo;
            this.Add(cptIcd9BillingCode);
        }
    }
}
