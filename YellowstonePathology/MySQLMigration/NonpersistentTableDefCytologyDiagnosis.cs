using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCytologyDiagnosis : NonpersistentTableDef
    {
        public NonpersistentTableDefCytologyDiagnosis()
        {
            this.m_TableName = "tblCytologyDiagnosis";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ID", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "150", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Abbreviation", "char", "15", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Bethesda2001", "tinyint", "1", "'0'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Bethesda2001ID", "int", "11", "'0'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("GradeLevel", "int", "11", "'0'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("ID");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
