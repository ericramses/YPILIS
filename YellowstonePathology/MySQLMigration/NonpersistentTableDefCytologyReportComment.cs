using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCytologyReportComment : NonpersistentTableDef
    {
        public NonpersistentTableDefCytologyReportComment()
        {
            this.m_TableName = "tblCytologyReportComment";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("CommentID", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("RowOrder", "int", "11", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Comment", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("AbbreviatedComment", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("CommentID");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
