using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Test.Model
{
    public class StainCollection : ObservableCollection<Stain>
    {
        StainCollection() { }

        public static StainCollection GetAll()
        {
            StainCollection result = new Model.StainCollection();
            Store.RedisDB stainDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.Stain);
            foreach (string jString in (string[])stainDb.GetAllJSONKeys())
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                Stain stain = JsonStainFactory.FromJson(jObject);
                result.Add(stain);
            }
            return result;
        }
    }
}
