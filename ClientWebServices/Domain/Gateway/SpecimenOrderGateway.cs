using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ClientWebServices.Domain.Gateway
{
    public class SpecimenOrderGateway
    {
        /*
        public static List<YellowstonePathology..SpecimenOrder> GetAllSpecimenOrders()
        {                                        
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select YpiConnectSpecimenOrderId, " +
	                    "(select * From tblYpiConnectSpecimenOrder where YpiConnectSpecimenOrderId = so.YpiConnectSpecimenOrderId " +
		                "for xml path('SpecimenOrder'), type) [XmlDocument] " +
	                    "From tblYpiConnectSpecimenOrder so";
            cmd.CommandType = System.Data.CommandType.Text;
            return YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteListCommand<YellowstonePathology..Contract.SpecimenOrder>(cmd);
        } 
        */
    }
}
