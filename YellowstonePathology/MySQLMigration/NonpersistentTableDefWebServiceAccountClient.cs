using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefWebServiceAccountClient : NonpersistentTableDef
    {
        public NonpersistentTableDefWebServiceAccountClient()
        {
            this.m_TableName = "tblWebServiceAccountClient";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("WebServiceAccountClientId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("WebServiceAccountId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ClientId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "null", true));

            this.SetKeyField("WebServiceAccountClientId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
