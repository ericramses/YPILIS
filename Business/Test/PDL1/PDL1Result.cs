using System;

namespace YellowstonePathology.Business.Test.PDL1
{
	/// <summary>
	/// Description of PDL1Result.
	/// </summary>
	public class PDL1Result : TestResult
	{
		protected string m_ResultAbbreviation;
		
		public PDL1Result()
		{
		}

        public string ResultAbbreviation
        {
            get { return this.m_ResultAbbreviation; }
        }
	}
}
