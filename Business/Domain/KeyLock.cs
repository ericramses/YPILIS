using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class KeyLock
	{
		string m_Key;
		KeyLockTypeEnum m_KeyLockTypeEnum;

		public KeyLock()
		{
		}

		public string Key
		{
			get { return this.m_Key; }
			set { this.m_Key = value; }
		}

		public KeyLockTypeEnum KeyLockTypeEnum
		{
			get { return this.m_KeyLockTypeEnum; }
			set { this.m_KeyLockTypeEnum = value; }
		}
	}
}
