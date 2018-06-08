using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Test.Model
{
	public class TestCollection : ObservableCollection<Test>
    {
        private static TestCollection instance;
        private static object syncRoot = new Object();

        public TestCollection()
        {

        }

        public static TestCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetAllTests(true);
                }
                return instance;
            }
        }

        public bool HasTestRequiringAcknowledgement()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.Model.Test test in this)
            {
                if (test.NeedsAcknowledgement == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public Test GetTest(string testId)
        {
			Test result = null;
			foreach (Test test in this)
            {
                if (test.TestId == testId)
                {
                    result = test;
                    break;
                }
            }
            return result;
        }

        public Test GetTestByTestNameId(string testNameId)
        {
            Test result = null;
            foreach (Test test in this)
            {
                if (test.TestNameId == testNameId)
                {
                    result = test;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string testId)
        {
            bool result = false;
            foreach (Test test in this)
            {
                if (test.TestId == testId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public TestCollection GetTestsStartingWith(string firstLetter)
		{
			TestCollection result = new TestCollection();
			List<Test> tests = new List<Test>();
			foreach (Test test in TestCollection.Instance)
			{
				if (test.TestName.Substring(0, 1) == firstLetter) tests.Add(test);
			}

			tests.Sort(Test.CompareByTestName);
			foreach (Test test in tests)
			{
				result.Add(test);
			}
			return result;
		}

        public ObservableCollection<object> GetTestsStartingWithToObjectCollection(string firstLetter, bool includeWetProtocols)
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            List<Test> tests = new List<Test>();
            //allTests.Add(Business.Test.Model.TestCollection.GetWetIron());

            foreach (Test test in TestCollection.Instance)
            {
                if (test.TestName.ToUpper().Substring(0, 1) == firstLetter.ToUpper()) tests.Add(test);
            }

            tests.Sort(Test.CompareByTestName);
            foreach (Test test in tests)
            {
                result.Add(test);
            }
            return result;
        }               

        public static Iron GetWetIron()
        {
            Iron wetIron = new Iron();
            wetIron.UseWetProtocol = true;
            return wetIron;
        }

        private static TestCollection GetAllTests(bool includeWetProtocols)
        {
            TestCollection result = new TestCollection();
            
            //No CptCode Tests
            result.Add(new HandE());
            result.Add(new TouchPrep());
            result.Add(new Recut());
            result.Add(new NonGynCytologyStain());
            result.Add(new UnstainedRecut());
            result.Add(new BRAF());
            result.Add(new DiffQuik());
            result.Add(new CellBlock());
            result.Add(new GrossOnly());
            result.Add(new KRASBRAFReflex());
            result.Add(new Negative());
            result.Add(new HandEAfterSlide());
            result.Add(new WrightsStain());
            result.Add(new FineNeedleAspirate());
            result.Add(new IntraoperativeConsultationwithFrozen());
            result.Add(new NonGynCytology());
            result.Add(new TissueSmear());
            result.Add(new KRASMutationAnalysis());
            result.Add(new BCellClonality());
            result.Add(new IntraoperativeConsultation());
            result.Add(new HER2Amplification());

			//Cytochemical For Microorganisms
            result.Add(new Giemsa());
            result.Add(new Gout()); //x
            result.Add(new HuckerTwort()); //x

            //Cytochemical
            result.Add(new Elastic());
            result.Add(new Melanin()); //x
            result.Add(new Mucin()); //x
            result.Add(new OilRedO()); //x
            result.Add(new AlphaNaphthylAcetateEsterase()); //x
            result.Add(new NaphtholASDChloroacetateEsterase()); //x
            result.Add(new CopperRhodanine()); //x
            result.Add(new Fites()); //x
            result.Add(new Alk()); //x

            //IHC
            result.Add(new ALK1()); //x
            result.Add(new AmyloidAProtein()); //x
            result.Add(new AmyloidPProtein());  //x          
            result.Add(new CD138Quantitative()); //x
            result.Add(new CD25()); //x
            result.Add(new CD52()); //x
            result.Add(new CD79a()); //x
            result.Add(new Cytokeratin17()); //x
            result.Add(new Cytokeratin818()); //x
            result.Add(new EGFR()); //x
            result.Add(new FactorVIII()); //x
            result.Add(new GCDFP15()); //x
            result.Add(new HER2Neu()); //
            result.Add(new Lysozyme()); //x
            result.Add(new MuscleSpecificActin()); //x
            result.Add(new NSE()); //x
            result.Add(new TRAP()); //x
            result.Add(new Tyrosinase()); //x
            result.Add(new Villin()); //

            result.Add(new BartonellaHenselae()); //x
            result.Add(new Uroplakin()); //x


			result.Add(new Surfactant()); //x
            result.Add(new PAX2()); //x
            result.Add(new CD235a()); //x
            result.Add(new MITF()); //x
            result.Add(new CD38()); //x
            result.Add(new PanMelanomaCocktail()); //x
            result.Add(new BerEP4()); //x
            result.Add(new NotListed1());
            result.Add(new NotListed2());
            result.Add(new NotListed3());

			result.Add(new Ki67SemiQuantitative()); //x

            //NG CT
            result.Add(new NeisseriaGonorrhoeae()); //x
            result.Add(new ChlamydiaTrachomatis()); //x
            result.Add(new ERV3()); //x
            result.Add(new ERV3NG()); //x
            result.Add(new ERV3CT()); //x

            result.Add(new ParoxysmalNocturnalHemoglobinuria()); //x

            result.Add(new CDX2VillinDualStain()); //x

            /*
            result.Add(new AcidFast());
            result.Add(new GMS());
            result.Add(new PASforFungus());
            result.Add(new SteinerandSteiner());
            result.Add(new AlcianBlue());
            result.Add(new CongoRed());
            result.Add(new Iron());
            result.Add(new PAS());
            result.Add(new PASAlcianBlue());
            result.Add(new PASWithDiastase());
            result.Add(new Reticulin());
            result.Add(new Trichrome());
            result.Add(new Bcl2());
            result.Add(new Bcl6());
            result.Add(new CA125());
            result.Add(new CA199());
            result.Add(new Calretinin());
            result.Add(new CD10());
            result.Add(new CD117());
            result.Add(new CD138());
            result.Add(new CD15());
            result.Add(new CD20());
            result.Add(new CD23());
            result.Add(new CD3());
            result.Add(new CD30());
            result.Add(new CD34());
            result.Add(new CD4());            
            result.Add(new CD45());
            result.Add(new CD5());
            result.Add(new CD56());
            result.Add(new CD68());
            result.Add(new CD8());
            result.Add(new CDX2());
            result.Add(new CEA());
            result.Add(new Chromogranin());
            result.Add(new CyclinD1());
            result.Add(new Cytokeratin20());
            result.Add(new Cytokeratin34());
            result.Add(new Cytokeratin56());
            result.Add(new Cytokeratin7());
            result.Add(new CytomegaloVirus());
            result.Add(new Desmin());
            result.Add(new Ecadherin());
            result.Add(new EMA());
            result.Add(new EstrogenReceptor());
            result.Add(new FactorXIIIa());
            result.Add(new Fascin());
            result.Add(new GFAP());
            result.Add(new HelicobacterPylori());
            result.Add(new HMB45());
            result.Add(new KappaByISH());
            result.Add(new LambdaByISH());
            result.Add(new Ki67());            
            result.Add(new Mammaglobin());
            result.Add(new MelanA());
            result.Add(new MLH1());
            result.Add(new MSH2());
            result.Add(new MUM1());
            result.Add(new Myeloperoxidase());
            result.Add(new NKX31());
            result.Add(new P16());
            result.Add(new P504sRacemase());
            result.Add(new P53());            
            result.Add(new Pancytokeratin());
            result.Add(new PAX5());
            result.Add(new PlacentalAlkalinePhosphatase());
            result.Add(new ProgesteroneReceptor());
            result.Add(new ProstateSpecificAntigen());
            result.Add(new ProstaticAcidPhosphatase());
            result.Add(new RCC());
            result.Add(new S100());
            result.Add(new SmoothMuscleActin());
            result.Add(new SmoothMuscleMyosin());
            result.Add(new Synaptophysin());
            result.Add(new TdT());            
            result.Add(new TTF1());
            result.Add(new U6());
            result.Add(new Vimentin());
            result.Add(new OSCAR());
            result.Add(new CD19());
            result.Add(new WT1());
            result.Add(new CD31());
            result.Add(new CD99());
            result.Add(new CD7());
            result.Add(new HepatocyteSpecificAntigen());
            result.Add(new MOC31());
            result.Add(new PMS2());
            result.Add(new MSH6());
            result.Add(new D240());            
            result.Add(new PAX8());
            result.Add(new NapsinA());
            result.Add(new Glypican3());
            result.Add(new DOG1());
            result.Add(new BetaCatenin());
            result.Add(new GATA3());
            result.Add(new EstrogenReceptorSemiquant());
            result.Add(new ProgesteroneReceptorSemiquant());
            result.Add(new SOX10());
            result.Add(new SATB2());
            result.Add(new P40());
            result.Add(new Tryptase());
            result.Add(new CMyc());
            result.Add(new HER2DISH());
            result.Add(new IgKappa());
            result.Add(new IgLambda());
            result.Add(new CD3CD20DualStain());
            result.Add(new Cytokeratin34P504sRacemaseDualStain());            
            result.Add(new Ki67MelanADualStain());
            result.Add(new OSCARSmoothMuscleMyosinDualStain());
            result.Add(new TTF1NapsinADualStain());
            result.Add(new PAX5CD5DualStain());
            */

            foreach (Stain.Model.Stain stain in Stain.Model.StainCollection.Instance)
            {
                Test test = Stain.Model.TestFactory.CreateTestFromStain(stain);
                result.Add(test);
            }
            return result;
        }

        public static TestCollection GetIHCTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests(false);
            foreach (Test test in allTests)
            {
                if (test is ImmunoHistochemistryTest == true)
                {
                    result.Add(test);
                }
            }
            return result;
        }

        public static TestCollection GetGradedTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests(false);
            foreach (Test test in allTests)
            {
                if (test is GradedTest == true)
                {
                    result.Add(test);
                }
            }
            return result;
        }

        public static TestCollection GetCytochemicalTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests(false);
            foreach (Test test in allTests)
            {
                if (test is CytochemicalTest == true)
                {
                    result.Add(test);
                }
            }
            return result;
        }

        public static TestCollection GetCytochemicalForMicroorganismsTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests(false);
            foreach (Test test in allTests)
            {
                if (test is CytochemicalForMicroorganisms)
                {
                    result.Add(test);
                }
            }
            return result;
        }       
    }
}
