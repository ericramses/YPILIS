using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class KeyLockFactory
	{
		public static string KeyTypeDescription(KeyLock keyLock)
		{
			string result = string.Empty;
			switch (keyLock.KeyLockTypeEnum)
			{
				case KeyLockTypeEnum.AccessionOrder:
					result = "Accession Order";
					break;
				case KeyLockTypeEnum.SpecimenLog:
					result = "Specimen Log";
					break;
			}
			return result;
		}
	}
}
