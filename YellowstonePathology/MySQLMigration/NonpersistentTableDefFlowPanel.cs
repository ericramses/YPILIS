using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefFlowPanel : NonpersistentTableDef
    {
        public NonpersistentTableDefFlowPanel()
        {
            this.m_TableName = "tblFlowPanel";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelName", "varchar", "100", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "5000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Active", "tinyint", "1", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("PanelId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
