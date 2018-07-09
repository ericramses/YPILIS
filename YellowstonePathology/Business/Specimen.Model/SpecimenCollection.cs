using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class SpecimenCollection : ObservableCollection<Specimen>
    {
        private static SpecimenCollection instance;

        public SpecimenCollection() { }

        public static SpecimenCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = BuildFromRedis();
                }
                return instance;
            }
        }

        public bool Exists(string specimenId)
        {
            bool result = false;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Specimen GetSpecimen(string specimenId)
        {
            Specimen result = null;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = specimen;
                    break;
                }
            }
            return result;
        }

        private static SpecimenCollection Sort(SpecimenCollection specimenCollection)
        {
            SpecimenCollection result = new SpecimenCollection();
            IOrderedEnumerable<Specimen> orderedResult = specimenCollection.OrderBy(i => i.SpecimenName);
            foreach (Specimen specimen in orderedResult)
            {
                result.Add(specimen);
            }
            return result;
        }

        public static SpecimenCollection GetSkins()
        {
            SpecimenCollection result = new SpecimenCollection();
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXOSPCMN")); // new SpecimenDefinition.SkinExcisionOrientedBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXUOSPCMN")); // new SpecimenDefinition.SkinExcisionUnorientedBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXOCSPCMN")); // new SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXUOCSPCMN")); // new SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKSHPHMSSPCMN")); // new SpecimenDefinition.SkinShavePunchMiscBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKSHCSPCMN")); // new SpecimenDefinition.SkinShavewithCurettingsBiopsy());            
            return Sort(result);
        }

        /*public static SpecimenCollection GetAll()
        {
            SpecimenCollection result = new SpecimenCollection();
            result.Add(new SpecimenDefinition.NullSpecimen());
            result.Add(new SpecimenDefinition.AdenoidExcision());
            result.Add(new SpecimenDefinition.AorticValve());
            result.Add(new SpecimenDefinition.AppendixExcision());
            result.Add(new SpecimenDefinition.BreastReduction());
            result.Add(new SpecimenDefinition.CervicalBiopsy());
            result.Add(new SpecimenDefinition.ECC());
            result.Add(new SpecimenDefinition.EMB());
            result.Add(new SpecimenDefinition.EMC());
            result.Add(new SpecimenDefinition.FemoralHead());
            result.Add(new SpecimenDefinition.FallopianTube());
            result.Add(new SpecimenDefinition.FallopianTubeAndOvaries());
            result.Add(new SpecimenDefinition.Peripheral());
            result.Add(new SpecimenDefinition.GallbladderExcision());
            result.Add(new SpecimenDefinition.Biopsy());
            result.Add(new SpecimenDefinition.BladderTUR());
            result.Add(new SpecimenDefinition.GIBiopsy());
            result.Add(new SpecimenDefinition.KneeTissue());
            result.Add(new SpecimenDefinition.LEEP());
            result.Add(new SpecimenDefinition.CervicalCone());            
            result.Add(new SpecimenDefinition.LEEPPieces());
            result.Add(new SpecimenDefinition.MitralValve());
            result.Add(new SpecimenDefinition.NeedleCoreBiopsy());
            result.Add(new SpecimenDefinition.POC());
            result.Add(new SpecimenDefinition.ProstateExceptRadicalResection());            
            result.Add(new SpecimenDefinition.ProstateNeedleBiopsy());
            result.Add(new SpecimenDefinition.ProstateRadicalResection());
            result.Add(new SpecimenDefinition.ProstateTUR());
            result.Add(new SpecimenDefinition.SinusContent());
            result.Add(new SpecimenDefinition.SinglePlacenta());
            result.Add(new SpecimenDefinition.TwinPlacenta());
            result.Add(new SpecimenDefinition.SkinExcisionOrientedBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionUnorientedBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy());            
            result.Add(new SpecimenDefinition.SkinShavePunchMiscBiopsy());
            result.Add(new SpecimenDefinition.SkinShavewithCurettingsBiopsy());
            result.Add(new SpecimenDefinition.ThinPrepFluid());
            result.Add(new SpecimenDefinition.TonsilAdenoidExcision());
            result.Add(new SpecimenDefinition.TonsilExcision());
            result.Add(new SpecimenDefinition.Uterus());            
            result.Add(new SpecimenDefinition.UterusAdnexa());
            result.Add(new SpecimenDefinition.GenericSpecimenGrossOnly());
            result.Add(new SpecimenDefinition.GenericSpecimenGrossRequiredWithBlocks());
            result.Add(new SpecimenDefinition.AutopsySpecimen());
            result.Add(new SpecimenDefinition.Fluid());
            result.Add(new SpecimenDefinition.Urine());
            result.Add(new SpecimenDefinition.Consult());
            result.Add(new SpecimenDefinition.InitialReading());
            result.Add(new SpecimenDefinition.BoneBiopsy());
            result.Add(new SpecimenDefinition.ExplantedDevices());
            result.Add(new SpecimenDefinition.FNA());
            return Sort(result);
        }*/

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public static SpecimenCollection BuildFromRedis()
        {
            SpecimenCollection result = new SpecimenCollection();
            Store.RedisDB specimenDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.Specimen);
            foreach (string jString in specimenDb.GetAllJSONKeys())
            {
                Specimen specimen = JsonConvert.DeserializeObject<Specimen>(jString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });

                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                string code = jObject["cptCodeId"].ToString();
                if (string.IsNullOrEmpty(code) == false)
                {
                    specimen.CPTCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(code, null);
                }
                    result.Add(specimen);
            }
            return result;
        }
    }
}
