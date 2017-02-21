using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefStainResultOption : NonpersistentTableDef
    {
        public NonpersistentTableDefStainResultOption()
        {
            this.m_TableName = "tblStainResultOption";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("StainResultOptionId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("StainResult", "varchar", "100", "'See Comment'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("StainResultOptionId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
