using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.ECW
{
	public class OruTechnicalGross : MirthXmlBase
	{
		#region Public constants
		/// <summary>
		/// name of the custom data elememt
		/// </summary>
		public static readonly string TechnicalGrossDescriptionLabel = "Technical Gross Description:";
		#endregion Public constants

		#region Private data
		/// <summary>
		/// the XML data block
		/// </summary>
		private readonly TechnicalGrossData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="data">XML data</param>
		public OruTechnicalGross(TechnicalGrossData data)
			: base(TechnicalGrossData.ReportName, data)
		{
			this.m_Data = data;
			AddCustomObxSegments(OruTechnicalGross.TechnicalGrossDescriptionLabel, m_Data.TechnicalGrossDescriptionText);
		}
		#endregion Constructors

		#region Private methods
		/// <summary>
		/// Adds the custom data for this data type
		/// </summary>
		/// <param name="header"></param>
		/// <param name="body"></param>
		private void AddCustomObxSegments(string header, string body)
		{
			AddObxSegmentsForHeaderSection(header, body);
		}
		#endregion Private methods
	}
}
