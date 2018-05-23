using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Stain.Model
{
    public class StainCollection : ObservableCollection<Stain>
    {
        private static StainCollection instance;

        StainCollection() { }

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

