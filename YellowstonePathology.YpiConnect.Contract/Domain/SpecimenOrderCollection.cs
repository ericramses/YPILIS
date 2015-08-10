using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
	[CollectionDataContract]
	public class SpecimenOrderCollection : ObservableCollection<SpecimenOrder>
	{
		public SpecimenOrderCollection()
        {
        }
	}
}
