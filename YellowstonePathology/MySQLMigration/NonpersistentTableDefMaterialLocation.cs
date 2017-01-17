using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefMaterialLocation : NonpersistentTableDef
    {
        public NonpersistentTableDefMaterialLocation()
        {
            this.m_TableName = "tblMaterialLocation";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MaterialLocationId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Name", "varchar", "200", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Address", "varchar", "200", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("City", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("State", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Zip", "varchar", "20", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("MaterialLocationId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
