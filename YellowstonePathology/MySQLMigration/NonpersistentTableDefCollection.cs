using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCollection : ObservableCollection<NonpersistentTableDef>
    {
        public NonpersistentTableDefCollection()
        { }

        public static NonpersistentTableDefCollection GetAll()
        {
            NonpersistentTableDefCollection result = new NonpersistentTableDefCollection();
            result.Add(new NonpersistentTableDefPanelSet());

            return result;
        }
    }
}
