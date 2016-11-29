using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefTest : NonpersistentTableDef
    {
        public NonpersistentTableDefTest()
        {
            this.m_TableName = "tblTest";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("TestId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("TestName", "varchar", "250", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Abbreviation", "varchar", "250", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Active", "tinyint", "1", "1", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("StainResultGroupId", "int", "11", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("AliquotType", "varchar", "25", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("NeedsAcknowledgement", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("AcknowledgementQueueName", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("DefaultResult", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("RequestForAdditionalReport", "tinyint", "1", "0", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("TestId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
        }
    }
}
