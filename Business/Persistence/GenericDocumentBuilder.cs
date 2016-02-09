using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class GenericDocumentBuilder : DocumentBuilder
    {
        private SqlCommand m_SQLCommand;

        public GenericDocumentBuilder(SqlCommand sqlCommand)
        {
            this.m_SQLCommand = sqlCommand;            
        }

        public override void Build(object o)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (SqlDataReader dr = m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(o, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}
