using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefOrderComment : NonpersistentTableDef
    {
        public NonpersistentTableDefOrderComment()
        {
            this.m_TableName = "tblOrderComment";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("OrderCommentId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Category", "varchar", "100", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Action", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("RequiresResponse", "tinyint", "1", "'0'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("RequiresNotification", "tinyint", "1", "'0'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Response", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("NotificationAddress", "varchar", "250", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("OrderCommentId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
