using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlide
    {
        string m_VantageSlideId;
        string m_MasterAccessionNo;
        string m_CurrentLocation;
        List<VantageSlideScan> m_SlideScans;

        public VantageSlide()
        {
            this.m_SlideScans = new List<VantageSlideScan>();
        }

        public List<VantageSlideScan> SlideScans
        {
            get { return this.m_SlideScans; }
        }

        public string VantageSlideId
        {
            get { return this.m_VantageSlideId; }
            set { this.m_VantageSlideId = value; }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        public string CurrentLocation
        {
            get { return this.m_CurrentLocation; }
            set { this.m_CurrentLocation = value; }
        }

        public string GetRedisKey()
        {
            return this.m_MasterAccessionNo + ":" + this.m_VantageSlideId;
        }

        public string ToJson()
        {            
            return JsonConvert.SerializeObject(this);            
        }

        public static VantageSlide FromJson(string json)
        {
            return JsonConvert.DeserializeObject<VantageSlide>(json);
        }
    }
}
