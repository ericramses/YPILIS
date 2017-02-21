using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefICD9Code : NonpersistentTableDef
    {

        public NonpersistentTableDefICD9Code()
        {
            this.m_TableName = "tblICD9Code";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Icd9CodeId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Icd9Code", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ICD10Code", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Category", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("Icd9CodeId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
