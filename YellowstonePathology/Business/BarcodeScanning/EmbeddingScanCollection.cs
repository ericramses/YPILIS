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
            /*Store.RedisDB embeddingDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan);
            if (embeddingDb.DataBase.KeyExists(scan.HashKey) == true)
            {
                HashEntry[] hashEntries = scan.GetHasEntries();
                embeddingDb.DataBase.HashSet(scan.HashKey, "Updated", scan.Updated.ToString());
            }*/

            bool exists = YellowstonePathology.Business.Gateway.AccessionOrderGateway.EmbeddingScanExists(scan.AliquotOrderId);
            if(exists == true)
            {
                YellowstonePathology.Business.Gateway.AccessionOrderGateway.UpdateEmbeddingScan(scan);
            }
        }

        public EmbeddingScan HandleScan(string aliquotOrderId, DateTime processorStartTime, TimeSpan processorFixationDuration)
        {            
            EmbeddingScan scan = new EmbeddingScan(aliquotOrderId, processorStartTime, processorFixationDuration);
            //Store.RedisDB embeddingDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan);
            //if (embeddingDb.DataBase.KeyExists("EmbeddingScan:" + aliquotOrderId) == true)
            bool exists = YellowstonePathology.Business.Gateway.AccessionOrderGateway.EmbeddingScanExists(aliquotOrderId);
            if (exists == true)
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
            /*else
            {
                embeddingDb.DataBase.SetAdd("EmbeddingScans:" + DateTime.Today.ToShortDateString(), scan.HashKey);
            }

            HashEntry[] hashEntries = scan.GetHasEntries();
            embeddingDb.DataBase.HashSet(scan.HashKey, hashEntries);*/
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetEmbeddingScan(scan, DateTime.Today);
            this.InsertItem(0, scan);

            return scan;
        }

        public static EmbeddingScanCollection GetByScanDateOld(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();
            Store.RedisDB embeddingDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan);
            RedisValue[] members = embeddingDb.DataBase.SetMembers("EmbeddingScans:" + scanDate.ToShortDateString());

            List<EmbeddingScan> list = new List<EmbeddingScan>();
            for (int i = 0; i < members.Length; i++)
            {

                HashEntry[] hashEntries = embeddingDb.DataBase.HashGetAll(members[i].ToString());
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

        public static EmbeddingScanCollection GetByScanDate(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();
            EmbeddingScanCollection temp = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingScanCollectionByScanDate(scanDate);

            List<EmbeddingScan> list = temp.ToList<EmbeddingScan>();

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
            /*EmbeddingScanCollection result = new EmbeddingScanCollection();
            Store.RedisDB embeddingDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.EmbeddingScan);
            foreach (var key in Store.AppDataStore.Instance.RedisStore.GetServer(Store.AppDBNameEnum.EmbeddingScan).Keys(pattern: "EmbeddingScans:*"))
            {
                RedisValue[] members = embeddingDb.DataBase.SetMembers(key);                
                for (int i = 0; i < members.Length; i++)
                {
                    HashEntry[] hashEntries = embeddingDb.DataBase.HashGetAll(members[i].ToString());
                    EmbeddingScan item = new EmbeddingScan(hashEntries);
                    result.Add(item);
                }
            }*/

            EmbeddingScanCollection result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllEmbeddingScans();
            return result;
        }
    }
}