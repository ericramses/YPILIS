using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefPanelSet :  NonpersistentTableDef
    {
        public NonpersistentTableDefPanelSet()
        {
            this.m_TableName = "tblPanelSet";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelSetId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelSetName", "varchar", "75", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Active", "tinyint", "1", "'1'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("CaseType", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ResultTableName", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("PanelSetId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
