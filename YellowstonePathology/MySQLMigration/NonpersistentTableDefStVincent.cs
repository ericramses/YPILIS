using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefStVincent : NonpersistentTableDef
    {
        public NonpersistentTableDefStVincent()
        {
            this.m_TableName = "tblStVincent";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ClientID", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("closerule", "tinyint", "1", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("clinic", "tinyint", "1", "0", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("ClientID");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
