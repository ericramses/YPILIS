using System;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

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
            Stain stain = this.FirstOrDefault(s => s.StainId == stainId);
            return stain != null ? true : false;
        }

        public Stain GetStain(string stainId)
        {
            Stain result = this.FirstOrDefault(s => s.StainId == stainId);
            return result;
        }

        public bool ExistsByTestid(string testId)
        {
            Stain stain = this.FirstOrDefault(s => s.TestId == testId);
            return stain != null ? true : false;
        }

        public Stain GetStainByTestId(string testId)
        {
            Stain result = this.FirstOrDefault(s => s.TestId == testId);
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

        public static void SetInRedis(Stain stain)
        {
            Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.Stain).DataBase.Execute("json.set", new string[] { stain.StainId, ".", stain.ToJSON() });

        }

        public void DeleteStain(Stain stain)
        {

        }

        public static void Reload()
        {
            instance = null;
        }

        public static string GetQuotedTestIds()
        {
            StringBuilder result = new StringBuilder();
            foreach(Stain stain in Instance)
            {
                result.Append("'" + stain.TestId + "',");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}

