using System;
using System.ComponentModel;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Logging
{
    public class ScanLog : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_materialId;
        protected string m_materialType;
        protected string m_facilityId;
        protected string m_facilityName;
        protected string m_locationId;
        protected string m_locationName;
        protected int m_scannedById;
        protected string m_scannedBy;
        protected DateTime m_scanDate;
        protected bool m_updated;

        public ScanLog()
        { }

        public ScanLog(string materialId, string materialType, string facilityId, string facilityName, string locationId,
            string locationName, bool updated)
        {
            this.m_materialId = materialId;
            this.m_materialType = materialType;
            this.m_facilityId = facilityId;
            this.m_facilityName = facilityName;
            this.m_locationId = locationId;
            this.m_locationName = locationName;
            this.m_updated = updated;
            this.m_scannedById = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
            this.m_scannedBy = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserName;
            this.m_scanDate = DateTime.Now;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string materialId
        {
            get { return this.m_materialId; }
            set
            {
                if (this.m_materialId != value)
                {
                    this.m_materialId = value;
                    this.NotifyPropertyChanged("materialId");
                }
            }
        }

        public string materialType
        {
            get { return this.m_materialType; }
            set
            {
                if (this.m_materialType != value)
                {
                    this.m_materialType = value;
                    this.NotifyPropertyChanged("materialType");
                }
            }
        }

        public string facilityId
        {
            get { return this.m_facilityId; }
            set
            {
                if (this.m_facilityId != value)
                {
                    this.m_facilityId = value;
                    this.NotifyPropertyChanged("facilityId");
                }
            }
        }

        public string facilityName
        {
            get { return this.m_facilityName; }
            set
            {
                if (this.m_facilityName != value)
                {
                    this.m_facilityName = value;
                    this.NotifyPropertyChanged("facilityName");
                }
            }
        }

        public string locationId
        {
            get { return this.m_locationId; }
            set
            {
                if (this.m_locationId != value)
                {
                    this.m_locationId = value;
                    this.NotifyPropertyChanged("locationId");
                }
            }
        }

        public string LocationName
        {
            get { return this.m_locationName; }
            set
            {
                if (this.m_locationName != value)
                {
                    this.m_locationName = value;
                    this.NotifyPropertyChanged("locationName");
                }
            }
        }

        public int scannedById
        {
            get { return this.m_scannedById; }
            set
            {
                if (this.m_scannedById != value)
                {
                    this.m_scannedById = value;
                    this.NotifyPropertyChanged("scannedById");
                }
            }
        }

        public string scannedBy
        {
            get { return this.m_scannedBy; }
            set
            {
                if (this.m_scannedBy != value)
                {
                    this.m_scannedBy = value;
                    this.NotifyPropertyChanged("scannedBy");
                }
            }
        }

        public DateTime scanDate
        {
            get { return this.m_scanDate; }
            set
            {
                if (this.m_scanDate != value)
                {
                    this.m_scanDate = value;
                    this.NotifyPropertyChanged("scanDate");
                }
            }
        }

        public bool updated
        {
            get { return this.m_updated; }
            set
            {
                if (this.m_updated != value)
                {
                    this.m_updated = value;
                    this.NotifyPropertyChanged("updated");
                }
            }
        }

        public virtual string ToJSON()
        {
            string result = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return result;
        }

        public virtual ScanLog DeserializeJSON(RedisValue json)
        {
            ScanLog result = null;
            result = JsonConvert.DeserializeObject<Business.Logging.ScanLog>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });
            return result;
        }

        public void WriteToRedis()
        {
            Business.RedisLocksConnection redis = new RedisLocksConnection("default");            
            string result = this.ToJSON();
            redis.Db.ListRightPush("scanLogs", result);
        }

        public ScanLog BuildFromRedis()
        {
            ScanLog result = null;
            Business.RedisLocksConnection redis = new RedisLocksConnection("default");            
            RedisValue item = redis.Db.ListLeftPop("scanLogs");
            if(item != RedisValue.Null)
            {
                RedisValue json = item.ToString();
                result = this.DeserializeJSON(json);
            }

            return result;
        }
    }
}
