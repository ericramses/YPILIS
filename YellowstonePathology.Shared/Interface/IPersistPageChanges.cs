using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Shared.Interface
{
	public interface IPersistPageChanges
	{
		void Save();
		bool OkToSaveOnNavigation(Type pageNavigatingTo);
		bool OkToSaveOnClose();        
		void UpdateBindingSources();
	}
}
