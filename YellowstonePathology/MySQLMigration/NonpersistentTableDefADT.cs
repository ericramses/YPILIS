using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefADT : NonpersistentTableDef
    {
        public NonpersistentTableDefADT()
        {
            this.m_TableName = "tblADT";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MessageId", "varchar", "500", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("DateReceived", "datetime", "3", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Message", "text", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PFirstName", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PLastName", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PBirthdate", "datetime", "3", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("AccountNo", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MedicalRecordNo", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MessageType", "varchar", "50", "NULL", true));

            this.SetKeyField("MessageId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
