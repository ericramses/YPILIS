using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
	// Summary:
	//     Notifies clients that a DB property value has changed.
	public interface INotifyDBPropertyChanged
	{
		// Summary:
		//     Occurs when a DBproperty value changes.
		event DBPropertyChangedEventHandler DBPropertyChanged;
	}
}
