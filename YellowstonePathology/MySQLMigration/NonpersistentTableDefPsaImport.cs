using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefPsaImport : NonpersistentTableDef
    {
        public NonpersistentTableDefPsaImport()
        {
            this.m_TableName = "tblPsaImport";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PsaImportId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ReportNo", "varchar", "20", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PostDate", "datetime", "3", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("PsaImportId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
