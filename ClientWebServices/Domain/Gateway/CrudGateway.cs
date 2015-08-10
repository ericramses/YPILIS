using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ClientWebServices.Domain.Gateway
{
    public class CrudGateway
    {
		public static void SubmitChanges<T>(YellowstonePathology.Business.Domain.Persistence.CollectionTrackingBase<T> objects) where T : YellowstonePathology.Business.Domain.Persistence.ITrackable, YellowstonePathology.Business.Domain.Persistence.INotifyDBPropertyChanged, YellowstonePathology.Business.Domain.Persistence.IPersistable
        {
            YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges<T>(objects, YellowstonePathology.Business.Domain.Persistence.DataLocationEnum.ProductionData);
        }

		public static void SubmitChanges<T>(List<T> objects) where T : YellowstonePathology.Business.Domain.Persistence.ITrackable, YellowstonePathology.Business.Domain.Persistence.INotifyDBPropertyChanged, YellowstonePathology.Business.Domain.Persistence.IPersistable
        {
            YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.SubmitChanges<T>(objects, YellowstonePathology.Business.Domain.Persistence.DataLocationEnum.ProductionData);
        }        
    }
}