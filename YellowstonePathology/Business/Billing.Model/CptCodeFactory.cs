using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeFactory
    {
        public CptCodeFactory() { }

        public static CptCode FromJson(JObject jObject)
        {
            CptCode result = new Model.CptCode();
            string cptType = jObject["CptType"].ToString();
            switch(cptType)
            {
                case "Normal":
                    {
                        result = new Model.CptCode();
                        break;
                    }
                case "PQRS":
                    {
                        result = new PQRSCode();
                        ((PQRSCode)result).ReportingDefinition = jObject["ReportingDefinition"].ToString();
                        ((PQRSCode)result).FormattedReportingDefinition = jObject["FormattedReportingDefinition"].ToString();
                        ((PQRSCode)result).Header = jObject["Header"].ToString(); ;
                        break;
                    }
                case "GCode":
                    {
                        result = new Model.CptCode();
                        break;
                    }

            }


            result.Code = jObject["ReportingDefinition"].ToString(); ;
            result.Description = jObject["ReportingDefinition"].ToString();
            result.FeeSchedule = (FeeScheduleEnum)Enum.Parse(typeof(FeeScheduleEnum), jObject.Value<int>("FeeSchedule").ToString());
            result.HasTechnicalComponent = jObject.Value<bool>("HasTechnicalComponent");
            result.HasProfessionalComponent = jObject.Value<bool>("HasProfessionalComponent");
            result.Modifier = jObject["Modifier"].ToString();;
            result.IsBillable = jObject.Value<bool>("IsBillable");
            result.GCode = jObject["GCode"].ToString();;
            result.HasMedicareQuantityLimit = jObject.Value<bool>("HasMedicareQuantityLimit");
            result.MedicareQuantityLimit = jObject.Value<int>("MedicareQuantityLimit");
            result.CodeType = (CPTCodeTypeEnum)Enum.Parse(typeof(CPTCodeTypeEnum), jObject.Value<int>("CodeType").ToString());
            result.SVHCDMCode = jObject["SVHCDMCode"].ToString();;
            result.SVHCDMDescription = jObject["SVHCDMDescription"].ToString();;

            return result;
        }
    }
}
