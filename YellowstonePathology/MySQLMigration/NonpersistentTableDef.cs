using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDef : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_TableName;
        protected string m_SelectStatement;
        protected string m_InsertColumnsStatement;
        private string m_KeyField;
        private bool m_HasTable;
        private bool m_HasAllColumns;
        private int m_SqlServerRowCount;
        private int m_MySqlRowCount;
        protected bool m_IsAutoIncrement;
        protected List<NonpersistentColumnDef> m_ColumnDefinitions;
        protected TableIndexCollection m_TableIndexCollection;
        protected TableForeignKeyCollection m_TableForeignKeyCollection;

        public NonpersistentTableDef()
        {
            this.m_ColumnDefinitions = new List<NonpersistentColumnDef>();
            this.m_TableIndexCollection = new TableIndexCollection();
            m_TableForeignKeyCollection = new TableForeignKeyCollection();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string TableName
        {
            get { return this.m_TableName; }
        }

        public string SelectStatement
        {
            get { return this.m_SelectStatement; }
        }

        public string InsertColumnsStatement
        {
            get { return this.m_InsertColumnsStatement; }
        }

        public string KeyField
        {
            get { return this.m_KeyField; }
        }

        public List<NonpersistentColumnDef> ColumnDefinitions
        {
            get { return this.m_ColumnDefinitions; }
        }

        public bool HasTable
        {
            get { return this.m_HasTable; }
            set
            {
                this.m_HasTable = value;
                NotifyPropertyChanged("HasTable");
            }
        }

        public bool HasAllColumns
        {
            get { return this.m_HasAllColumns; }
            set
            {
                this.m_HasAllColumns = value;
                NotifyPropertyChanged("HasAllColumns");
            }
        }

        public int SqlServerRowCount
        {
            get { return this.m_SqlServerRowCount; }
            set
            {
                this.m_SqlServerRowCount = value;
                NotifyPropertyChanged("SqlServerRowCount");
            }
        }

        public int MySqlRowCount
        {
            get { return this.m_MySqlRowCount; }
            set
            {
                this.m_MySqlRowCount = value;
                NotifyPropertyChanged("MySqlRowCount");
            }
        }

        public bool IsAutoIncrement
        {
            get { return this.m_IsAutoIncrement; }
            set
            {
                this.m_IsAutoIncrement = value;
                NotifyPropertyChanged("IsAutoIncrement");
            }
        }

        public TableIndexCollection TableIndexCollection
        {
            get { return this.m_TableIndexCollection; }
            set
            {
                this.m_TableIndexCollection = value;
                NotifyPropertyChanged("TableIndexCollection");
            }
        }

        public TableForeignKeyCollection TableForeignKeyCollection
        {
            get { return this.m_TableForeignKeyCollection; }
            set
            {
                this.m_TableForeignKeyCollection = value;
                NotifyPropertyChanged("TableForeignKeyCollection");
            }
        }

        protected void SetKeyField(string keyField)
        {
            this.m_KeyField = keyField;
        }

        protected void SetSelectStatement()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Select ");
            foreach(NonpersistentColumnDef columnDef in this.m_ColumnDefinitions)
            {
                result.Append("[");
                result.Append(columnDef.ColumnName);
                result.Append("], ");
            }
            result.Remove(result.Length - 2, 2);

            result.Append(" from ");
            result.Append(this.m_TableName);

            this.m_SelectStatement = result.ToString();
        }

        protected void SetInsertColumnsStatement()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Insert into ");
            result.Append(this.m_TableName);
            result.Append("(");
            foreach (NonpersistentColumnDef columnDef in this.m_ColumnDefinitions)
            {
                result.Append(columnDef.ColumnName);
                result.Append(", ");
            }
            result.Remove(result.Length - 2, 2);
            result.Append(") Values (");

            this.m_InsertColumnsStatement = result.ToString();
        }

        public string GetCreateTableCommand()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Create Table If Not Exists ");
            result.Append(this.TableName);
            result.Append("(");
            foreach(NonpersistentColumnDef columnDef in this.m_ColumnDefinitions)
            {
                result.Append(columnDef.ColumnDefinition);
                result.Append(", ");
            }
            result.Remove(result.Length - 2, 2);
            result.Append(");");

            return result.ToString();
        }

        public string GetCreateAutoIncrementOnKeyFieldStatement()
        {
            StringBuilder result = new StringBuilder();
            if (this.m_IsAutoIncrement == true)
            {
                result.Append("ALTER TABLE ");
                result.Append(this.m_TableName);
                result.Append(" MODIFY `");
                result.Append(this.m_KeyField);
                result.Append(" int(11) NOT NULL AUTO_INCREMENT; ");
            }

            return result.ToString();
        }
    }
}
