using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Cytology
{
	public partial class OtherCondition : DomainBase
	{
		#region Fields
		private int m_LineID;
		private string m_OtherConditionText;
		#endregion

		#region Properties
		public int LineID
		{
			get { return this.m_LineID; }
			set
			{
				if(this.m_LineID != value)
				{
					this.m_LineID = value;
					this.NotifyPropertyChanged("LineID");
				}
			}
		}

		public string OtherConditionText
		{
			get { return this.m_OtherConditionText; }
			set
			{
				if(this.m_OtherConditionText != value)
				{
					this.m_OtherConditionText = value;
					this.NotifyPropertyChanged("OtherConditionText");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_LineID = propertyWriter.WriteInt("LineID");
			this.m_OtherConditionText = propertyWriter.WriteString("OtherCondition");
		}
		#endregion
	}
}
