using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefFlowMarkerPanel : NonpersistentTableDef
    {
        public NonpersistentTableDefFlowMarkerPanel()
        {
            this.m_TableName = "tblFlowMarkerPanel";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MarkerPanelId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelId", "int", "11", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MarkerName", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Intensity", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Interpretation", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PanelName", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Reference", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Timestamp", "Timestamp", "3", null, false));

            this.SetKeyField("MarkerPanelId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
