using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCytologyOtherConditions : NonpersistentTableDef
    {
        public NonpersistentTableDefCytologyOtherConditions()
        {
            this.m_TableName = "tblCytologyOtherConditions";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("LineID", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("OtherCondition", "varchar", "2000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("LineID");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }

    }
}
