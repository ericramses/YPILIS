using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    class NonpersistentTableDefPanel : NonpersistentTableDef
    {
        public NonpersistentTableDefPanel()
        {
            this.m_TableName = "tblPanel";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelName", "varchar", "75", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Active", "tinyint", "1", "'1'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("DisplayOnly", "tinyint", "1", "'0'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("LabOrderSeq", "int", "11", "'0'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("HasResultTable", "tinyint", "1", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ResultTableName", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ClassName", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("AssemblyQualifiedClassName", "varchar", "500", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("PanelId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
