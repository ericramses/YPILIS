using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business
{
	public class BatchTypeList : ObservableCollection<BatchTypeListItem>
	{
		public BatchTypeList()
        {
		}
	}

}
