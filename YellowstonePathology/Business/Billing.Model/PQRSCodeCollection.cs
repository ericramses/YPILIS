using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCodeCollection : ObservableCollection<PQRSCode>
    {
        public static PQRSCodeCollection GetAll()
        {
            PQRSCodeCollection result = new PQRSCodeCollection();
            
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3125F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3125F1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3125F8P"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3126F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3126F1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3126F8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8797"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3250F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260F1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260F8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3267F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3267F1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3267F8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8721"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8722"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8723"));            
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8798"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3394F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3394F8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3395F"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG9418"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG9419"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG9420"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG9421"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG9428"));

            return result;
        }

        public static PQRSCodeCollection GetBreastPQRSCodes()
        {            
            PQRSCodeCollection result = new PQRSCodeCollection();
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260F"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260F1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3260F8P"));
            return result;
        }

		public static PQRSCodeCollection GetBarrettsEsophagusPQRSCodes()
		{
			PQRSCodeCollection result = new PQRSCodeCollection();
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3126F"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3126F1P"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3126F8P"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8797"));
			return result;
		}

		public static PQRSCodeCollection GetColorectalPQRSCodes()
		{
			PQRSCodeCollection result = new PQRSCodeCollection();
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8721"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8722"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8723"));
			return result;
		}

		public static PQRSCodeCollection GetRadicalProstatectomyPQRSCodes()
		{
			PQRSCodeCollection result = new PQRSCodeCollection();
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3267F"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3267F1P"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRS3267F8P"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("PQRSG8798"));
			return result;
		}
		
		public PQRSCode GetPQRSCode(string pqrsCode)
        {
            PQRSCode result = null;
			string[] splitString = pqrsCode.Split(new char[] { '-' });
            foreach (PQRSCode item in this)
            {
				if (splitString.Length > 1)
				{
					if (item.Code == splitString[0] && item.Modifier == splitString[1])
					{
						result = item;
						break;
					}
				}
				else if (item.Code == splitString[0])
                {
					result = item;
					break;
				}
            }
            return result;
        }
    }
}
