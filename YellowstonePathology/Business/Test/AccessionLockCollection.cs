﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Test
{
    public class AccessionLockCollection : ObservableCollection<AccessionLock>
    {
        public AccessionLockCollection()
        {
            this.Build();
        }

        public void ClearLocks()
        {            
            foreach (Business.Test.AccessionLock accessionLock in this)
            {                
                if (accessionLock.IsLockAquiredByMe == true)
                {
                    accessionLock.ReleaseLock();
                }
            }
        }

        public void Refresh()
        {
            this.ClearItems();
            this.Build();
        }

        private void Build()
        {
            List<RedisKey> keys = RedisLocksConnection.Instance.Server.Keys((int)RedisDatabaseEnum.Default, "AccessionLock:*", 10, CommandFlags.None).ToList<RedisKey>();

            List<AccessionLock> list = new List<AccessionLock>();
            foreach(RedisKey key in keys)
            {
                if(RedisLocksConnection.Instance.Db.KeyExists(key) == true)
                {
                    HashEntry[] hashEntries = RedisLocksConnection.Instance.Db.HashGetAll(key);
                    AccessionLock item = new AccessionLock(hashEntries);
                    list.Add(item);
                }                
            }

            list.Sort(delegate(AccessionLock x, AccessionLock y) 
            {
                if(x.TimeAquired > y.TimeAquired)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });

            foreach(AccessionLock item in list)
            {
                this.Add(item);
            }
        }
    }
}
