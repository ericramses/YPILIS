using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCytologyScreeningImpression : NonpersistentTableDef
    {
        public NonpersistentTableDefCytologyScreeningImpression()
        {
            this.m_TableName = "tblCytologyScreeningImpression";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("CytologyScreeningImpressionId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ResultCode", "varchar", "2", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Abbreviation", "varchar", "25", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Description", "varchar", "500", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "null", true));

            this.SetKeyField("CytologyScreeningImpressionId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
