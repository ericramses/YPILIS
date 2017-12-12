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
            if (Business.RedisLocksConnection.Instance.DefaultDb.KeyExists(scan.HashKey) == true)
            {
                HashEntry[] hashEntries = scan.GetHasEntries();
                Business.RedisLocksConnection.Instance.DefaultDb.HashSet(scan.HashKey, "Updated", scan.Updated.ToString());
            }            
        }

        public EmbeddingScan HandleScan(string aliquotOrderId, DateTime processorStartTime, TimeSpan processorFixationDuration)
        {            
            EmbeddingScan scan = new EmbeddingScan(aliquotOrderId, processorStartTime, processorFixationDuration);

            if (Business.RedisLocksConnection.Instance.DefaultDb.KeyExists("EmbeddingScan:" + aliquotOrderId) == true)
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
                Business.RedisLocksConnection.Instance.DefaultDb.SetAdd("EmbeddingScans:" + DateTime.Today.ToShortDateString(), scan.HashKey);
            }

            HashEntry[] hashEntries = scan.GetHasEntries();
            Business.RedisLocksConnection.Instance.DefaultDb.HashSet(scan.HashKey, hashEntries);
            this.InsertItem(0, scan);            
            return scan;
        }

        public static EmbeddingScanCollection GetByScanDate(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();            
            RedisValue[] members = Business.RedisLocksConnection.Instance.DefaultDb.SetMembers("EmbeddingScans:" + scanDate.ToShortDateString());

            List<EmbeddingScan> list = new List<EmbeddingScan>();
            for (int i = 0; i < members.Length; i++)
            {
                HashEntry[] hashEntries = Business.RedisLocksConnection.Instance.DefaultDb.HashGetAll(members[i].ToString());
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

            foreach (var key in Business.RedisLocksConnection.Instance.Server.Keys(pattern: "EmbeddingScans:*"))
            {
                RedisValue[] members = Business.RedisLocksConnection.Instance.DefaultDb.SetMembers(key);                
                for (int i = 0; i < members.Length; i++)
                {
                    HashEntry[] hashEntries = Business.RedisLocksConnection.Instance.DefaultDb.HashGetAll(members[i].ToString());
                    EmbeddingScan item = new EmbeddingScan(hashEntries);
                    result.Add(item);
                }
            }
            
            return result;
        }
    }
}