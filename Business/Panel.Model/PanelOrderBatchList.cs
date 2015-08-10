using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.Business.Panel.Model
{
	public class PanelOrderBatchList : ObservableCollection<PanelOrderBatch>
	{
		public PanelOrderBatchList()
		{
		}
	}
}
