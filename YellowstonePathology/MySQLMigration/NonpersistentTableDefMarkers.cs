using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefMarkers : NonpersistentTableDef
    {
        public NonpersistentTableDefMarkers()
        {
            this.m_TableName = "tblMarkers";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MarkerId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("MarkerName", "varchar", "50", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("CPTCode", "varchar", "20", "'88180'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("QTYBill", "int", "11", "0", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("OrderFlag", "int", "11", null, true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Type", "varchar", "30", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Predictive", "tinyint", "1", "'0'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("IsNormalMarker", "tinyint", "1", "'0'", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("IsMyelodysplastic", "tinyint", "1", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("MarkerId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
