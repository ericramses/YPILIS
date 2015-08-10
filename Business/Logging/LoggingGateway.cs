using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace YellowstonePathology.Core.Logging
{
    public class LoggingGateway
    {        					
		public static void InsertBarcodeScan(YellowstonePathology.Core.BarcodeScanning.BarcodeScan barcodeScan)
		{
            YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader xmlPropertyReader = new YellowstonePathology.Business.Domain.Persistence.SqlXmlPropertyReader();
            YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges<YellowstonePathology.Core.BarcodeScanning.BarcodeScan>(barcodeScan, YellowstonePathology.Business.Domain.Persistence.DataLocationEnum.LocalData, xmlPropertyReader);			
		}
	}
}
