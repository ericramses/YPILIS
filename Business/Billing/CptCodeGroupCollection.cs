using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Billing
{
    public class CptCodeGroupCollection : ObservableCollection<CptCodeGroup>
	{
		public CptCodeGroupCollection()
		{
		}

		public bool Exists(string cptCodeGroupId)
		{
			bool result = false;
			foreach (CptCodeGroup cptCodeGroup in this)
			{
				if (cptCodeGroup.CptCodeGroupId == cptCodeGroupId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public bool GroupNameExists(string groupName)
		{
			bool result = false;
			foreach (CptCodeGroup cptCodeGroup in this)
			{
				if (cptCodeGroup.GroupName == groupName)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public CptCodeGroup GroupById(string cptCodeGroupId)
		{
			CptCodeGroup result = null;
			foreach (CptCodeGroup cptCodeGroup in this)
			{
				if (cptCodeGroup.CptCodeGroupId == cptCodeGroupId)
				{
					result = cptCodeGroup;
					break;
				}
			}
			return result;
		}
	}
}
