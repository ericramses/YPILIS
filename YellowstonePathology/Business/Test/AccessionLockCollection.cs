using System;
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
            RedisValue[] members = RedisLocksConnection.Instance.Db.SetMembers("AccessionLocks");

            List<AccessionLock> list = new List<AccessionLock>();
            for (int i = 0; i < members.Length; i++)
            {
                if(RedisLocksConnection.Instance.Db.KeyExists(members[i].ToString()) == true)
                {
                    HashEntry[] hashEntries = RedisLocksConnection.Instance.Db.HashGetAll(members[i].ToString());
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
