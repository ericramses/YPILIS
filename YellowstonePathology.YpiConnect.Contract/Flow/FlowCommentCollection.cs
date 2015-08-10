using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[CollectionDataContract]
	public partial class FlowCommentCollection : ObservableCollection<FlowComment>
    {
        public FlowCommentCollection()
        {
        }

        /*public void SetFillCommandAll()
        {
            string sql = "select * from tblFlowCommentV2";
            this.m_ParameterList.Clear();
            this.m_Cmd.CommandText = sql;
            this.m_Cmd.CommandType = CommandType.Text;
        }

        public void SetFillCommandByStainId(int commentId)
        {
            string sql = "select * from tblFlowCommentV2 where CommentId = " + commentId;            
            this.m_ParameterList.Clear();
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
                using (SqlDataReader dr = m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FlowCommentItem item = new FlowCommentItem();
                        item.Fill(dr);
                        this.Add(item);
                    }
                }
            }
        }*/
    }    
}
				
