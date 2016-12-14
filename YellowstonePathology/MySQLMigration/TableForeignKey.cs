using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class TableForeignKey
    {
        private string m_SqlServerForeignKeyName;
        private string m_MySqlForeignKeyName;
        private string m_TableName;
        private string m_ColumnName;
        private string m_ReferenceTableName;
        private string m_ReferenceColumnName;

        public TableForeignKey(string sqlServerForeignKeyName,
            string tableName,
            string columnName,
            string referenceTableName,
            string referenceColumnName)
        {
            this.m_SqlServerForeignKeyName = sqlServerForeignKeyName;
            this.m_TableName = tableName;
            this.m_ColumnName = columnName;
            this.m_ReferenceTableName = referenceTableName;
            this.m_ReferenceColumnName = referenceColumnName;
            this.m_MySqlForeignKeyName = "fk_" + this.m_TableName + "_" + this.m_ReferenceTableName;
        }

        public string SqlServerForeignKeyName
        {
            get { return this.m_SqlServerForeignKeyName; }
        }

        public string MySqlForeignKeyName
        {
            get { return this.m_MySqlForeignKeyName; }
        }

        public string TableName
        {
            get { return this.m_TableName; }
        }

        public string ColumnName
        {
            get { return this.m_ColumnName; }
        }

        public string ReferenceTableName
        {
            get { return this.m_ReferenceTableName; }
        }

        public string ReferenceColumnName
        {
            get { return this.m_ReferenceColumnName; }
        }

        public string GetCreateStatement(string indexName)
        {
            StringBuilder result = new StringBuilder();
            result.Append("alter table ");
            result.Append(this.m_TableName);
            result.Append(" add constraint ");
            result.Append(this.m_MySqlForeignKeyName);
            result.Append(" foreign key ");
            result.Append(indexName);
            result.Append(" (");
            result.Append(this.m_ColumnName);
            result.Append(") references ");
            result.Append(this.m_ReferenceTableName);
            result.Append("(");
            result.Append(this.m_ReferenceColumnName);
            result.Append(");");

            return result.ToString();
        }
    }
}
