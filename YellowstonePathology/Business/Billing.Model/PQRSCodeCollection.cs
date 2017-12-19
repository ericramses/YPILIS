using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCodeCollection : ObservableCollection<PQRSCode>
    {
        public PQRSCodeCollection() { }

        public static PQRSCodeCollection GetAll(bool expandModifiers)
        {
            PQRSCodeCollection result = new PQRSCodeCollection();
            foreach(CptCode cptCode in CptCodeCollection.Instance)
            {
                if(cptCode.CodeType == CPTCodeTypeEnum.PQRS)  //"PQRS")
                {
                   result.Add((PQRSCode)cptCode);
                }
            }
            
            /*result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3125F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3125F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3125F", "8P"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3126F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3126F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3126F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G8797", null));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3250F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3260", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3260F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3260F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3260F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3267F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3267F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3267F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G8721", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G8722", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G8723", null));            
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G8798", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3394F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3394F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("3395F", null));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G9418", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G9419", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G9420", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G9421", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.Instance.Get("G9428", null));*/

            return result;
        }

        /*private static void ExpandModifiers(JObject jObject, PQRSCodeCollection pqrsCodeCollection)
        {
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                string modifierString = codeModifier["modifier"].ToString();
                PQRSCode code = PQRSCodeFactory.FromJson(jObject, modifierString);
                pqrsCodeCollection.Add(code);
            }
        }*/

        public static PQRSCode Get(string code, string modifier)
        {
            PQRSCode result = (PQRSCode)CptCodeCollection.Instance.Get(code, modifier);
            return result;
        }
    }
}
