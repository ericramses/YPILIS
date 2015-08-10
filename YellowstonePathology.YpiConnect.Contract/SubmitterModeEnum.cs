using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{
	[DataContract]
	public enum SubmitterModeEnum
    {
		[EnumMember]
		Normal,
		[EnumMember]
		BeginEnd
    }
}
