/*
 * Created by SharpDevelop.
 * User: William.Copland
 * Date: 12/14/2015
 * Time: 13:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Test.PDL1
{
	/// <summary>
	/// Description of PDL1NoResult.
	/// </summary>
	public class PDL1NoResult : PDL1Result
	{
		public PDL1NoResult()
		{
            this.m_Result = null;
            this.m_ResultCode = null;
            this.m_ResultAbbreviation = null;
		}
	}
}
