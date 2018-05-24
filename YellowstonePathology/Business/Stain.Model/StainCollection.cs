using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YellowstonePathology.Business.Stain.Model
{
    public class StainCollection : ObservableCollection<Stain>
    {
        private static StainCollection instance;

        public StainCollection() { }

        public static StainCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = LoadFromRedis();
                }
                return instance;
            }
        }

        public bool Exists(string stainId)
        {
            bool result = false;
            foreach (Stain stain in this)
            {
                if (stain.StainId == stainId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Stain GetStain(string stainId)
        {
            Stain result = null;
            foreach (Stain stain in this)
            {
                if (stain.StainId == stainId)
                {
                    result = stain;
                    break;
                }
            }
            return result;
        }

        public bool ExistsByTestid(string testId)
        {
            bool result = false;
            foreach (Stain stain in this)
            {
                if (stain.TestId == testId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Stain GetStainByTestId(string testId)
        {
            Stain result = null;
            foreach (Stain stain in this)
            {
                if (stain.TestId == testId)
                {
                    result = stain;
                    break;
                }
            }
            return result;
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        private static StainCollection LoadFromRedis()
        {
            StainCollection result = new Model.StainCollection();
            Store.RedisDB stainDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.Stain);
            foreach (string jString in stainDb.GetAllJSONKeys())
            {
                Stain stain = JsonStainFactory.FromJson(jString);
                result.Add(stain);
            }
            return result;
        }
    }
}

