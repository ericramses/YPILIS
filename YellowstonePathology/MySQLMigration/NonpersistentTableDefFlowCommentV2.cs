using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefFlowCommentV2 : NonpersistentTableDef
    {
        public NonpersistentTableDefFlowCommentV2()
        {
            this.m_TableName = "tblFlowCommentV2";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("CommentId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Category", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Comment", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Impression", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("CommentId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
