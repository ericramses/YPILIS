using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
		// Summary:
		//     Represents the method that will handle the System.ComponentModel.INotifyDBPropertyChanged.PropertyChanged
		//     event raised when a DBproperty is changed on a component.
		//
		// Parameters:
		//   sender:
		//     The source of the event.
		//
		//   e:
		//     A System.ComponentModel.PropertyChangedEventArgs that contains the event
		//     data.
		public delegate void DBPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
