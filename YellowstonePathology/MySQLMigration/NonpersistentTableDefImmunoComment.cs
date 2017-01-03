using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefImmunoComment : NonpersistentTableDef
    {
        public NonpersistentTableDefImmunoComment()
        {
            this.m_TableName = "tblImmunoComment";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("immunocommentid", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Comment", "varchar", "1000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("immunocommentid");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
