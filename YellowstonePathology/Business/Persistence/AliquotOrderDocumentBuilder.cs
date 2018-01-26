﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class AliquotOrderDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public AliquotOrderDocumentBuilder(string aliquotOrderId)
        {
            this.m_SQLCommand = new MySqlCommand("select * from tblAliquotOrder where AliquotOrderId = @AliquotOrderId;");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.AddWithValue("@AliquotOrderId", aliquotOrderId);
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
            this.Build(aliquotOrder);
            return aliquotOrder;
        }

        private void Build(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {            
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {                        
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }        
    }
}
