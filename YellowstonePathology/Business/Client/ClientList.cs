using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;

namespace YellowstonePathology.Business.Client
{
    public class ClientList : ObservableCollection<ClientListItem>
    {
        private SqlCommand m_Cmd;

        public ClientList()
        {
            this.m_Cmd = new SqlCommand();
        }

        public void Fill()
        {
            this.Clear();
            using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
            {
                cn.Open();
                this.m_Cmd.Connection = cn;
                using (SqlDataReader dr = this.m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ClientListItem item = new ClientListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }

        public void SetFillCommandByAll()
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblClient Order By ClientName";            
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void SetFillCommandByName(string clientName)
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblClient Where ClientName like @ClientName Order By ClientName";
            this.m_Cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 200).Value = clientName + '%';
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }
    }    
}
