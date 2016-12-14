using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class IndexedColumn
    {
        private string m_ColumnName;
        private int m_PositionInIndex;
        private bool m_IsUnique;

        public IndexedColumn(string columnName, int positionInIndex, bool isUnique)
        {
            this.m_ColumnName = columnName;
            this.m_PositionInIndex = positionInIndex;
            this.m_IsUnique = isUnique;
        }

        public string ColumnName
        {
            get { return this.m_ColumnName; }
        }

        public int PositionInIndex
        {
            get { return this.m_PositionInIndex; }
        }

        public bool IsUnique
        {
            get { return this.m_IsUnique; }
        }
    }
}
