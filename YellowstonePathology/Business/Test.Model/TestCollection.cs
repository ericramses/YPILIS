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

        public static Test GetWetIron()
        {
            Test wetIron = Instance.GetTest("115");
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
            result.Add(new Gout());
            result.Add(new HuckerTwort());

            //Cytochemical
            result.Add(new Melanin());
            result.Add(new Mucin());
            result.Add(new OilRedO());
            result.Add(new AlphaNaphthylAcetateEsterase());
            result.Add(new NaphtholASDChloroacetateEsterase());
            result.Add(new CopperRhodanine());
            result.Add(new Fites());
            result.Add(new Alk());

            //IHC
            result.Add(new ALK1());
            result.Add(new AmyloidAProtein());
            result.Add(new AmyloidPProtein());           
            result.Add(new CD138Quantitative());
            result.Add(new CD25());
            result.Add(new CD52());
            result.Add(new CD79a());
            result.Add(new Cytokeratin17());
            result.Add(new Cytokeratin818());
            result.Add(new EGFR());
            result.Add(new FactorVIII());
            result.Add(new GCDFP15());
            result.Add(new HER2Neu());
            result.Add(new Lysozyme());
            result.Add(new MuscleSpecificActin());
            result.Add(new NSE());
            result.Add(new TRAP());
            result.Add(new Tyrosinase());
            result.Add(new Villin());

            result.Add(new BartonellaHenselae());
            result.Add(new Uroplakin());


			result.Add(new Surfactant());
            result.Add(new PAX2());
            result.Add(new CD235a());
            result.Add(new MITF());
            result.Add(new CD38());
            result.Add(new PanMelanomaCocktail());
            result.Add(new BerEP4());
            result.Add(new NotListed1());
            result.Add(new NotListed2());
            result.Add(new NotListed3());

            //NG CT
            result.Add(new NeisseriaGonorrhoeae());
            result.Add(new ChlamydiaTrachomatis());
            result.Add(new ERV3());
            result.Add(new ERV3NG());
            result.Add(new ERV3CT());

            result.Add(new ParoxysmalNocturnalHemoglobinuria());


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
