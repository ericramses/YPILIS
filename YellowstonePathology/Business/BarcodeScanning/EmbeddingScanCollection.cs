using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class EmbeddingScanCollection : ObservableCollection<EmbeddingScan>
    {
        public EmbeddingScanCollection()
        {            
        }        

        public void UpdateStatus(EmbeddingScan scan)
        {                        
            if (Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).KeyExists(scan.HashKey) == true)
            {
                HashEntry[] hashEntries = scan.GetHasEntries();
                Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).HashSet(scan.HashKey, "Updated", scan.Updated.ToString());
            }            
        }

        public EmbeddingScan HandleScan(string aliquotOrderId, DateTime processorStartTime, TimeSpan processorFixationDuration)
        {            
            EmbeddingScan scan = new EmbeddingScan(aliquotOrderId, processorStartTime, processorFixationDuration);
            
            if (Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).KeyExists("EmbeddingScan:" + aliquotOrderId) == true)
            {
                foreach (EmbeddingScan item in this)
                {
                    if (item.AliquotOrderId == aliquotOrderId)
                    {
                        this.Remove(item);
                        break;
                    }
                }
            }
            else
            {
                Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).SetAdd("EmbeddingScans:" + DateTime.Today.ToShortDateString(), scan.HashKey);
            }

            HashEntry[] hashEntries = scan.GetHasEntries();
            Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).HashSet(scan.HashKey, hashEntries);
            this.InsertItem(0, scan);            
            return scan;
        }

        public static EmbeddingScanCollection GetByScanDate(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();            
            RedisValue[] members = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).SetMembers("EmbeddingScans:" + scanDate.ToShortDateString());

            List<EmbeddingScan> list = new List<EmbeddingScan>();
            for (int i = 0; i < members.Length; i++)
            {
                HashEntry[] hashEntries = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).HashGetAll(members[i].ToString());
                EmbeddingScan item = new EmbeddingScan(hashEntries);
                list.Add(item);
            }

            list.Sort(delegate (EmbeddingScan x, EmbeddingScan y)
            {
                if (x.DateScanned == y.DateScanned) return 0;
                if (x.DateScanned > y.DateScanned)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });


            foreach (EmbeddingScan item in list)
            {
                result.InsertItem(0, item);
            }
            
            return result;
        }

        public static EmbeddingScanCollection GetAll()
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();            

            foreach (var key in Store.AppDataStore.Instance.RedisStore.GetServer(Store.AppDBNameEnum.EmbeddingScan).Keys(pattern: "EmbeddingScans:*"))
            {
                RedisValue[] members = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).SetMembers(key);                
                for (int i = 0; i < members.Length; i++)
                {
                    HashEntry[] hashEntries = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan).HashGetAll(members[i].ToString());
                    EmbeddingScan item = new EmbeddingScan(hashEntries);
                    result.Add(item);
                }
            }
            
            return result;
        }
    }
}