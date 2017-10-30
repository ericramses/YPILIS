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
            string cptType = jObject["CPTType"].ToString();
            switch(cptType)
            {
                case CptCode.CptTypeNormal:
                    {
                        result = new Model.CptCode();
                        break;
                    }
                case CptCode.CptTypePQRS:
                    {
                        result = new PQRSCode();
                        ((PQRSCode)result).ReportingDefinition = GetEmptyString(jObject["ReportingDefinition"].ToString());
                        ((PQRSCode)result).FormattedReportingDefinition = GetEmptyString(jObject["FormattedReportingDefinition"].ToString());
                        ((PQRSCode)result).Header = GetNullString(jObject["Header"].ToString());
                        break;
                    }
                case CptCode.CptTypeGCode:
                    {
                        result = new Model.CptCode();
                        break;
                    }
            }

            result.Code = jObject["Code"].ToString();
            result.CPTCodeId = jObject["CPTCodeId"].ToString();
            result.Description = GetNullString(jObject["Description"].ToString());
            result.FeeSchedule = (FeeScheduleEnum)Enum.Parse(typeof(FeeScheduleEnum), jObject.Value<int>("FeeSchedule").ToString());
            result.HasTechnicalComponent = jObject.Value<bool>("HasTechnicalComponent");
            result.HasProfessionalComponent = jObject.Value<bool>("HasProfessionalComponent");
            result.Modifier = GetNullString(jObject["Modifier"].ToString());
            result.IsBillable = jObject.Value<bool>("IsBillable");
            result.GCode = GetNullString(jObject["GCode"].ToString());
            result.HasMedicareQuantityLimit = jObject.Value<bool>("HasMedicareQuantityLimit");
            result.MedicareQuantityLimit = jObject.Value<int>("MedicareQuantityLimit");
            result.CodeType = (CPTCodeTypeEnum)Enum.Parse(typeof(CPTCodeTypeEnum), jObject.Value<int>("CodeType").ToString());
            result.SVHCDMCode = GetNullString(jObject["SVHCDMCode"].ToString());
            result.SVHCDMDescription = GetNullString(jObject["SVHCDMDescription"].ToString());

            return result;
        }

        private static string GetNullString(string value)
        {
            string result = null;
            if (string.IsNullOrEmpty(value) == false) result = value;
            return result;
        }

        private static string GetEmptyString(string value)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(value) == false) result = value;
            return result;
        }
    }
}
