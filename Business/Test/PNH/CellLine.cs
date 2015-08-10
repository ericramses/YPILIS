using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHCellLine
	{
		string m_Name;
		decimal m_Threshold;
		decimal m_LargeLimit;
		decimal m_Type1;
		decimal m_Type2;
		decimal m_Type3;

		public PNHCellLine(string name, decimal threshold, decimal largeLimit)
		{
			m_Name = name;
			m_Threshold = threshold;
			m_LargeLimit = largeLimit;
			m_Type1 = 0.0m;
			m_Type2 = 0.0m;
			m_Type3 = 0.0m;
		}

		public string Name
		{
			get { return this.m_Name; }
			set { this.m_Name = value; }
		}

		public decimal Threshold
		{
			get { return this.m_Threshold; }
			set { this.m_Threshold = value; }
		}

		public decimal LargeLimit
		{
			get { return this.m_LargeLimit; }
			set { this.m_LargeLimit = value; }
		}

		public decimal CellLineValue
		{
			get { return this.Type2 + this.Type3; }
		}

		public decimal Type1
		{
			get { return this.m_Type1; }
			set { this.m_Type1 = value; }
		}

		public decimal Type2
		{
			get { return this.m_Type2; }
			set { this.m_Type2 = value; }
		}

		public decimal Type3
		{
			get { return this.m_Type3; }
			set { this.m_Type3 = value; }
		}

		public bool IsAboveThreshold
		{
			get { return this.CellLineValue >= this.Threshold; }
		}

		public bool IsAboveLargeLimit
		{
			get { return this.CellLineValue >= this.LargeLimit; }
		}
	}
}
