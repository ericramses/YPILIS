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
        public TestCollection()
        {

        }

        /*public bool HasTestRequiringAcknowledgement()
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
        }*/

		public Test GetTest(string testId)
        {
            Test result = this.FirstOrDefault(t => t.TestId == testId);
            return result;
        }

        public bool Exists(string testId)
        {
            Test test = this.FirstOrDefault(t => t.TestId == testId);
            return test == null ? false : true;
        }
    }
}
