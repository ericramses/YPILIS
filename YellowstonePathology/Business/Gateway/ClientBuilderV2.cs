using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class ClientBuilderV2
    {
        YellowstonePathology.Business.Client.Model.Client m_Client;

        public ClientBuilderV2()
        {
        }

        public YellowstonePathology.Business.Client.Model.Client Client
        {
            get { return this.m_Client; }
        }

        public void Build(SqlCommand cmd)
        {
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        this.m_Client = new YellowstonePathology.Business.Client.Model.Client();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(this.m_Client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            this.m_Client.ClientLocationCollection.Add(clientLocation);
                        }
                    }
                }
            }
        }
    }
}
