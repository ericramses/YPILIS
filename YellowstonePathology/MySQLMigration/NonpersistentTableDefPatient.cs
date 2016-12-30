using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefPatient : NonpersistentTableDef
    {
        public NonpersistentTableDefPatient()
        {
            this.m_TableName = "tblPatient";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PatientId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("PatientId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
