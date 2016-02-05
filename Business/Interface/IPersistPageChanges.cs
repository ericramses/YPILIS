using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IPersistPageChanges
	{
		void Save(bool releaseLock);
		bool OkToSaveOnNavigation(Type pageNavigatingTo);
		bool OkToSaveOnClose();        
		void UpdateBindingSources();
	}
}
