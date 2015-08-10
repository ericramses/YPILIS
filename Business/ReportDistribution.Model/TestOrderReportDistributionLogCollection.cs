using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
	public class TestOrderReportDistributionLogCollection : ObservableCollection<TestOrderReportDistributionLog>
	{     
        public const string PREFIXID = "TORD";

        public TestOrderReportDistributionLogCollection()
        {

		}        
	}
}
