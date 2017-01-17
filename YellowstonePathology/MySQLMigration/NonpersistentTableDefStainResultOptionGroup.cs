using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefStainResultOptionGroup : NonpersistentTableDef
    {
        public NonpersistentTableDefStainResultOptionGroup()
        {
            this.m_TableName = "tblStainResultOptionGroup";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("StainResultOptionGroupId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("StainResultGroupId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("StainResultOptionId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("StainResultOptionGroupId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
