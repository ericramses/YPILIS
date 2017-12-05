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
            
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3125f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3125f1p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3125f8p"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3126f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3126f1p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3126f8p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8797"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3250f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260f1p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260f8p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3267f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3267f1p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3267f8p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8721"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8722"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8723"));            
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8798"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3394f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3394f8p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3395f"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g9418"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g9419"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g9420"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g9421"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g9428"));

            return result;
        }

        public static PQRSCodeCollection GetBreastPQRSCodes()
        {            
            PQRSCodeCollection result = new PQRSCodeCollection();
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260f"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260f1p"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3260f8p"));
            return result;
        }

		public static PQRSCodeCollection GetBarrettsEsophagusPQRSCodes()
		{
			PQRSCodeCollection result = new PQRSCodeCollection();
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3126f"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3126f1p"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3126f8p"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8797"));
			return result;
		}

		public static PQRSCodeCollection GetColorectalPQRSCodes()
		{
			PQRSCodeCollection result = new PQRSCodeCollection();
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8721"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8722"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8723"));
			return result;
		}

		public static PQRSCodeCollection GetRadicalProstatectomyPQRSCodes()
		{
			PQRSCodeCollection result = new PQRSCodeCollection();
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3267f"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3267f1p"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:3267f8p"));
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("pqrs:g8798"));
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
