/*
 * Created by SharpDevelop.
 * User: William.Copland
 * Date: 12/14/2015
 * Time: 12:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
