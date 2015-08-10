using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;

namespace YellowstonePathology.Business.Billing
{
    public class CptCodeList : ObservableCollection<CptCodeListItem>
    {
        private SqlCommand m_Cmd;

        public CptCodeList()
        {
            this.m_Cmd = new SqlCommand();
        }

        public void SetFillByAll()
        {
            this.m_Cmd.Parameters.Clear();
            string sql = "Select * from tblCptCode";
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
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
                        CptCodeListItem item = new CptCodeListItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }

    }
}
