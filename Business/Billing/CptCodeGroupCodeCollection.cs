using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Billing
{
    public class CptCodeGroupCodeCollection : ObservableCollection<CptCodeGroupCode>
	{
		public CptCodeGroupCodeCollection()
		{
		}

		public bool Exists(string cptCode)
		{
			bool result = false;
			foreach (CptCodeGroupCode cptCodeGroupCode in this)
			{
				if (cptCodeGroupCode.CptCode == cptCode)
				{
					result = true;
					break;
				}
			}
			
			return result;
		}
	}
}
