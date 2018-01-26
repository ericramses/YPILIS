using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlide : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string m_VantageSlideId;
        string m_MasterAccessionNo;
        string m_CurrentLocation;
        VantageSlideScanCollection m_SlideScans;

        public VantageSlide()
        {
            this.m_SlideScans = new VantageSlideScanCollection();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public VantageSlideScanCollection SlideScans
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
            set
            {
                this.m_CurrentLocation = value;
                this.NotifyPropertyChanged("CurrentLocation");
            }
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
