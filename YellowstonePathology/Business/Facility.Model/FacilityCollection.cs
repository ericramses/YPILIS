using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Facility.Model
{	
	public class FacilityCollection : ObservableCollection<Facility>
	{
        private static FacilityCollection instance;


        public static FacilityCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetAllFacilities();
                    instance.Insert(0, new Facility());
                }
                return instance;
            }
        }

        public static FacilityCollection Refresh()
        {
            instance = null;
            return Instance;
        }

        public FacilityCollection()
		{
            
		}

        protected override void InsertItem(int index, Facility item)
        {
            base.InsertItem(index, item);
        }

        private static FacilityCollection Sort(FacilityCollection facilityCollection)
        {
            FacilityCollection result = new FacilityCollection();
            IOrderedEnumerable<Facility> orderedResult = facilityCollection.OrderBy(i => i.FacilityName);
            foreach (Facility facility in orderedResult)
            {
                result.Add(facility);
            }
            return result;
        }

        public static FacilityCollection GetPathGroupFacilities()
        {
            FacilityCollection result = new FacilityCollection();
<<<<<<< HEAD
            result.Add(new NullFacility());
            result.Add(new YellowstonePathologistBillings());
            result.Add(new YellowstonePathologistBozeman());
            result.Add(new ButtePathology());
            result.Add(new PathologyAssociatesOfIdahoFalls());
            result.Add(new PathologyConsultantsOfWesternMontana());
            result.Add(new ProfessionalPathologyOfWyoming());
            result.Add(new SheridanPathologyAssociates());            
            return result;
        }

        public static FacilityCollection GetAllFacilities()
        {
            FacilityCollection result = new FacilityCollection();
            result.Add(new NullFacility());
            result.Add(new YellowstonePathologyInstituteBillings());
            result.Add(new YellowstonePathologistBillings());
            result.Add(new YellowstonePathologyInstituteCody());
            result.Add(new YellowstonePathologistCody());
            result.Add(new YellowstonePathologistBozeman());            
            result.Add(new NeogenomicsIrvine());
            result.Add(new ARUP());
            result.Add(new BillingsClinic());
            result.Add(new Showdair());
            result.Add(new Therapath());
            result.Add(new UniversityOfWashington());
            result.Add(new MontanaDermatology());
            result.Add(new GenomicHealth());            
            result.Add(new UCSanFrancisco());
            result.Add(new UCSFDermPathService());
            result.Add(new WesternMontanaClinic());
            result.Add(new JohnHopkins());
            result.Add(new EmoryUniversity());
            result.Add(new UniPath());
            result.Add(new NationalInstituteOfHealth());
            result.Add(new MayoClinic());
            result.Add(new TXChildrensHospital());
            result.Add(new MUSC());
            result.Add(new CockerellDermatology());
            result.Add(new CarisLifeSciences());
            result.Add(new BigSkyDermatology());
            result.Add(new HuntsmanCancerInstitute());
            result.Add(new MDAndersonCancerCenter());
            result.Add(new ChildrensHospitalColorado());
            result.Add(new FoundationMedicine());
            result.Add(new BreastPathologyConsultants());
            result.Add(new ButtePathology());
            result.Add(new ColoradoGeneticsLaboratory());
            result.Add(new PhenoPath());
            result.Add(new StVincentHealthcare());
            result.Add(new UniversityOfArkansasMedicalSciences());
            result.Add(new SeattleCancerCenterAlliance());
            result.Add(new CancerTreatmentCentersOfAmerica());
            result.Add(new PathologyAssociatesOfIdahoFalls());
            result.Add(new LoweLawGroup());
            result.Add(new ProfessionalPathologyOfWyoming());
            result.Add(new TallmanDermatology());
            result.Add(new BozemanDeaconess());
            result.Add(new CMMC());
            result.Add(new StJamesHealth());
            result.Add(new MontanaDepartmentofJustice());
            result.Add(new UniversityOfMichigan());
            result.Add(new UniversityOfColoradoHospital());
            result.Add(new AnschutzPathologyUCH());
            result.Add(new UniversityOfMiami());
            result.Add(new ClevelandClinicFoundation());
            result.Add(new SheridanPathologyAssociates());
            result.Add(new MontanaSkinCancerandDermatologyCenter());
            result.Add(new BigHornCountyMemorialHospital());
            result.Add(new MountainViewHospital());
            result.Add(new EasternIdahoRegionalMedicalCenter());
            result.Add(new SheridanMemorialHospital());
            result.Add(new ChristianaCare());
            result.Add(new NeogenomicsNashville());
            result.Add(new CBCI());
            result.Add(new AssociatedDermatology());
            result.Add(new VAEasternColorado());
            result.Add(new StLukesMagicValley());
            result.Add(new StanfordUniversityMedicalCenter());
            result.Add(new MadisonMemorialHospital());
            result.Add(new OregonHealthScienceUniversity());
            result.Add(new CasperDermatologyClinic());
            result.Add(new FortHarrisonVA());
            result.Add(new NorthernMontanaHealthcare());
            result.Add(new UniversityOfMississippi());
            result.Add(new CastleBiosciences());
            result.Add(new BeneifsHealthSystem());
            result.Add(new UniversityOfArizonaCancerCenter());
            result.Add(new PathologyConsultantsOfWesternMontana());
            result.Add(new WilliamsPorterDayLaw());
            result.Add(new EasternColoradoHealthcare());
            result.Add(new MyriadGeneticsLab());
            result.Add(new UCDavisMedicalCenter());
            result.Add(new VirginiaMason());
            result.Add(new UCHLoneTreeBreastCenter());
            result.Add(new Cellnetix());
            result.Add(new UniversityOfUtahHealthCare());
            result.Add(new NathionalJewishHealth());
            result.Add(new CommunityHospitalAnaconda());
            result.Add(new MarshfieldClinic());
            return Sort(result);
        }

        public Facility GetByFacilityId(string facilityId)
        {
            Facility result = null;
            foreach (Facility facility in this)
=======
            foreach (Facility facility in FacilityCollection.Instance)
>>>>>>> facility
            {
                if( facility.FacilityId == null ||
                    facility.FacilityId == "YPBLGS" ||       //YellowstonePathologistBillings
                    facility.FacilityId == "YPBZMN" ||      //YellowstonePathologistBozeman
                    facility.FacilityId == "BTTPTHLGY" ||   //ButtePathology
                    facility.FacilityId == "PAOIF" ||       //PathologyAssociatesOfIdahoFalls
                    facility.FacilityId == "PCOWM" ||       //PathologyConsultantsOfWesternMontana
                    facility.FacilityId == "PPWY" ||        //ProfessionalPathologyOfWyoming
                    facility.FacilityId == "SHPTHASS")      //SheridanPathologyAssociates
                {
                    result.Add(facility);
                }
            }
            return result;
        }

        public Facility GetByFacilityId(string facilityId)
        {
            Facility result = null;
            foreach (Facility facility in this)
            {
                if (facility.FacilityId == facilityId)
                {
                    result = facility;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string facilityId)
        {
            bool result = false;
            foreach (Facility facility in this)
            {
                if (facility.FacilityId == facilityId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static FacilityCollection GetAllYPFacilities()
        {
            FacilityCollection result = new FacilityCollection();
            foreach(Facility facility in FacilityCollection.Instance)
            {
                if(facility.FacilityId == "YPBLGS" ||   //YellowstonePathologistBillings
                    facility.FacilityId == "YPIBLGS" || //YellowstonePathologyInstituteBillings
                    facility.FacilityId == "YPCDY" ||   //YellowstonePathologistCody
                    facility.FacilityId == "YPICDY" ||  //YellowstonePathologyInstituteCody
                    facility.FacilityId == "YPBZMN" )   //YellowstonePathologistBozeman
                {
                    result.Add(facility);
                }
            }

            return result;
        }

        public static bool IsAYellowstonePathologyFacility(Facility facility)
        {
            bool result = false;

            if (facility.FacilityId == "YPBLGS" ||   //YellowstonePathologistBillings
                facility.FacilityId == "YPIBLGS" || //YellowstonePathologyInstituteBillings
                facility.FacilityId == "YPCDY" ||   //YellowstonePathologistCody
                facility.FacilityId == "YPICDY" ||  //YellowstonePathologyInstituteCody
                facility.FacilityId == "YPBZMN")   //YellowstonePathologistBozeman
            {
                result = true;
            }

            return result;
        }        

        public static string GetBillBy(string professionalComponentBillingFacilityId, string technicalComponentBillingFacilityId, string billingComponent, string billTo)
        {
            string result = null;

            Facility technicalFacility = FacilityCollection.Instance.GetByFacilityId(technicalComponentBillingFacilityId);
            Facility professionalFacility = FacilityCollection.Instance.GetByFacilityId(professionalComponentBillingFacilityId);
            Facility ypiFacility = FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            Facility ypFacility = FacilityCollection.Instance.GetByFacilityId("YPBLGS");

            if (FacilityCollection.IsAYellowstonePathologyFacility(technicalFacility) == true && FacilityCollection.IsAYellowstonePathologyFacility(professionalFacility) == true)
            {
                if (billingComponent == "Global" && billTo == "Client")
                {
                    result = ypFacility.FacilityId;
                }
                else
                {
                    result = ypiFacility.FacilityId;
                }
            }
            else
            {
                if (billingComponent == "Professional")
                {
                    result = professionalComponentBillingFacilityId;
                }
                else if (billingComponent == "Technical")
                {
                    result = technicalComponentBillingFacilityId;
                }
                else if (billingComponent == "Global")
                {
                    result = technicalComponentBillingFacilityId;
                }
            }

            return result;            
        }        
	}
}
