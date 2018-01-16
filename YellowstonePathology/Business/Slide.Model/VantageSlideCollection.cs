using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Slide.Model
{
    public class VantageSlideCollection : ObservableCollection<VantageSlide>
    {


        private string m_MasterAccessionNo;

        public VantageSlideCollection(string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.Load();
        }

        public void HandleSlideScan(string vantageSlideId)
        {
            VantageSlide slide = null;

            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
            YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            if (this.Exists(vantageSlideId) == false)
            {
                slide = new VantageSlide();
                slide.MasterAccessionNo = this.m_MasterAccessionNo;
                slide.VantageSlideId = vantageSlideId;
                slide.CurrentLocation = thisFacility.FacilityId;
                this.Add(slide);
            }
            else
            {
                slide = this.Get(vantageSlideId);
            }

            VantageSlideScan slideScan = new VantageSlideScan();
            slideScan.Location = thisLocation.LocationId;
            slideScan.ScanDate = DateTime.Now;
            slideScan.SlideId = vantageSlideId;
            slideScan.ScannedBy = Business.User.SystemIdentity.Instance.User.UserName;
            slide.SlideScans.Add(slideScan);

            string jsonSlide = slide.ToJson();

            Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).DataBase.Execute("json.set", new string[] { slide.GetRedisKey(), ".", jsonSlide });
            
        }

        public VantageSlide Get(string vantageSlideId)
        {
            VantageSlide result = null; ;
            foreach (VantageSlide slide in this)
            {
                if (slide.VantageSlideId == vantageSlideId)
                {
                    result = slide;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string vantageSlideId)
        {
            bool result = false;
            foreach(VantageSlide slide in this)
            {
                if(slide.VantageSlideId == vantageSlideId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void Load()
        {
            string[] results = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).GetAllJSONKeys(this.m_MasterAccessionNo);
            foreach(string result in results)
            {
                VantageSlide vantageSlide = VantageSlide.FromJson(result);
                this.Add(vantageSlide);
            }
        }

        public void SetLocation(string location)
        {
            foreach(VantageSlide slide in this)
            {
                slide.CurrentLocation = location;
                string jsonSlide = slide.ToJson();
                Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.VantageSlide).DataBase.Execute("json.set", new string[] { slide.GetRedisKey(), ".", jsonSlide });
            }
        }
    }
}
