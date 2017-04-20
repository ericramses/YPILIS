using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefSpecimenAdequacy : NonpersistentTableDef
    {
        public NonpersistentTableDefSpecimenAdequacy()
        {
            this.m_TableName = "tblSpecimenAdequacy";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ResultCode", "varchar", "50", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "500", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("ResultCode");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
