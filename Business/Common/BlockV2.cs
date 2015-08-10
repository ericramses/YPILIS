using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class BlockV2 : Block
	{
		public BlockV2()
		{
		}

		public override string ToString()
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);

			StringBuilder result = new StringBuilder(this.m_Prefix + this.m_Delimeter);
			result.Append(this.FormattedCassetteColumn + this.m_Delimeter);
			result.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
			result.Append(this.BlockTitle + this.m_Delimeter);
			result.Append(this.PatientInitials + this.m_Delimeter);
			result.Append(this.CompanyId + this.m_Delimeter);
			result.Append(this.ScanningId + this.m_Delimeter);
			result.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
			result.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
			return result.ToString();
		}
	}
}
