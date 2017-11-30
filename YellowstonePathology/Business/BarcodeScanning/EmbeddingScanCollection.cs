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
            Business.RedisLocksConnection redis = new RedisLocksConnection(RedisDatabaseEnum.Default);            
            if (redis.Db.KeyExists(scan.HashKey) == true)
            {
                HashEntry[] hashEntries = scan.GetHasEntries();
                redis.Db.HashSet(scan.HashKey, "Updated", scan.Updated.ToString());
            }
        }

        public EmbeddingScan HandleScan(string aliquotOrderId, DateTime processorStartTime, TimeSpan processorFixationDuration)
        {
            Business.RedisLocksConnection redis = new RedisLocksConnection(RedisDatabaseEnum.Default);            
            EmbeddingScan scan = new EmbeddingScan(aliquotOrderId, processorStartTime, processorFixationDuration);

            if (redis.Db.KeyExists("EmbeddingScan:" + aliquotOrderId) == true)
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
                redis.Db.SetAdd("EmbeddingScans:" + DateTime.Today.ToShortDateString(), scan.HashKey);
            }

            HashEntry[] hashEntries = scan.GetHasEntries();
            redis.Db.HashSet(scan.HashKey, hashEntries);
            this.InsertItem(0, scan);

            return scan;
        }

        public static EmbeddingScanCollection GetByScanDate(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();

            Business.RedisLocksConnection redis = new RedisLocksConnection(RedisDatabaseEnum.Default);            
            RedisValue[] members = redis.Db.SetMembers("EmbeddingScans:" + scanDate.ToShortDateString());

            List<EmbeddingScan> list = new List<EmbeddingScan>();
            for (int i = 0; i < members.Length; i++)
            {
                HashEntry[] hashEntries = redis.Db.HashGetAll(members[i].ToString());
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
            Business.RedisLocksConnection redis = new RedisLocksConnection(Business.RedisDatabaseEnum.Default);            

            foreach (var key in redis.Server.Keys(pattern: "EmbeddingScans:*"))
            {
                RedisValue[] members = redis.Db.SetMembers(key);                
                for (int i = 0; i < members.Length; i++)
                {
                    HashEntry[] hashEntries = redis.Db.HashGetAll(members[i].ToString());
                    EmbeddingScan item = new EmbeddingScan(hashEntries);
                    result.Add(item);
                }
            }

            return result;
        }
    }
}