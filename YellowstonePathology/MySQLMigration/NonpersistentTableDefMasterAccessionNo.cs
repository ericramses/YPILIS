using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefMasterAccessionNo : NonpersistentTableDef
    {
        public NonpersistentTableDefMasterAccessionNo()
        {
            this.m_TableName = "tblMasterAccessionNo";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("NextMasterAccessionNo", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("NextMasterAccessionNo");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
