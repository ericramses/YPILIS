using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class TableIndex
    {
        private string m_SqlServerIndexName;
        private IndexedColumnCollection m_IndexedColumnCollection;

        public TableIndex(string sqlServerIndexName)
        {
            this.m_SqlServerIndexName = sqlServerIndexName;
            this.m_IndexedColumnCollection = new IndexedColumnCollection();
        }

        public string SqlServerIndexName
        {
            get { return this.m_SqlServerIndexName; }
        }

        public IndexedColumnCollection IndexedColumnCollection
        {
            get { return this.m_IndexedColumnCollection; }
        }

        public string GetIndexName(string tableName)
        {
            StringBuilder result = new StringBuilder();
            result.Append("idx_");
            result.Append(tableName);
            result.Append("_");
            result.Append(this.m_IndexedColumnCollection[0].ColumnName);
            if(this.m_IndexedColumnCollection.Count > 1)
            {
                result.Append("_With_");
                result.Append((this.m_IndexedColumnCollection.Count - 1).ToString());
            }

            return result.ToString();
        }

        public string GetMySqlFormatedColumnNames()
        {
            StringBuilder result = new StringBuilder();
            result.Append("(");
            foreach (IndexedColumn indexedColumn in this.IndexedColumnCollection)
            {
                result.Append(indexedColumn.ColumnName);
                result.Append(", ");
            }
            result.Remove(result.Length - 2, 2);
            result.Append(")");
            return result.ToString();
        }
    }
}
