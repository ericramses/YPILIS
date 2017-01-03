using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.MySQLMigration
{
    public class TableIndexCollection : ObservableCollection<TableIndex>
    {
        public TableIndexCollection() { }

        public bool HasSingleColumnIndex(string columnName)
        {
            bool result = false;
            foreach(TableIndex tableIndex in this)
            {
                if(tableIndex.IndexedColumnCollection.Count == 1)
                {
                    if(tableIndex.IndexedColumnCollection[0].ColumnName == columnName)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public TableIndex GetSingleColumnIndex(string columnName)
        {
            TableIndex result = null;
            foreach (TableIndex tableIndex in this)
            {
                if (tableIndex.IndexedColumnCollection.Count == 1)
                {
                    if (tableIndex.IndexedColumnCollection[0].ColumnName == columnName)
                    {
                        result = tableIndex;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
