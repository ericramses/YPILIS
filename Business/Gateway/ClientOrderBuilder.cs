using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class ClientOrderBuilder
    {
        private static string ServerSqlConnectionString = "Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";
        public static void Build(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, SqlCommand cmd)
        {
            using (SqlConnection cn = new SqlConnection(ServerSqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    BuildClientOrder(dr, clientOrder);
                    dr.NextResult();
                    clientOrder.ClientOrderDetailCollection.Clear();
                    BuildClientOrderDetailCollection(clientOrder.ClientOrderDetailCollection, dr);
                }
            }
        }

        public static void BuildClientOrder(SqlDataReader dr, YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {            
            Nullable<int> panelSetId = null;
            while (dr.Read())
            {
                if (dr["PanelSetId"] != DBNull.Value)
                {
                    panelSetId = Convert.ToInt32(dr["PanelSetId"].ToString());
                }
            }

            clientOrder = YellowstonePathology.Business.ClientOrder.Model.ClientOrderFactory.GetClientOrder(panelSetId);
            dr.NextResult();

            while (dr.Read())
            {
                YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrder, dr);
                propertyWriter.WriteProperties();
            }

            dr.NextResult();
            while (dr.Read())
            {
                YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
                propertyWriter.WriteProperties();
                clientOrder.ClientLocation = clientLocation;
            }            
        }

        public static void BuildClientOrderDetailCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection, SqlDataReader dr)
        {
            int clientOrderDetailCount = 0;
            while (dr.Read())
            {
                clientOrderDetailCount = Convert.ToInt32(dr["ClientOrderDetailCount"].ToString());
            }

            if (clientOrderDetailCount > 0)
            {
                for (int i = 0; i < clientOrderDetailCount; i++)
                {
                    dr.NextResult();

                    string orderTypeCode = null;
                    while (dr.Read())
                    {
                        orderTypeCode = dr["OrderTypeCode"].ToString();
                    }

                    dr.NextResult();

                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, Persistence.PersistenceModeEnum.UpdateChangedProperties);
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrderDetail, dr);
                        propertyWriter.WriteProperties();
                        clientOrderDetailCollection.Add(clientOrderDetail);
                    }
                }
            }
            else
            {
                dr.NextResult();
                dr.NextResult();
            }
        }
    }
}
