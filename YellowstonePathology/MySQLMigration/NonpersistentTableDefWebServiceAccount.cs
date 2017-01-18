using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefWebServiceAccount : NonpersistentTableDef
    {
        public NonpersistentTableDefWebServiceAccount()
        {
            this.m_TableName = "tblWebServiceAccount";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("WebServiceAccountId", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("UserName", "varchar", "50", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Password", "varchar", "50", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("DisplayName", "varchar", "100", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("PrimaryClientId", "int", "11", "'0'", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("DownloadFileType", "varchar", "100", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("InitialPage", "varchar", "100", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ApplicationTimeoutMinutes", "int", "11", "15", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("RemoteFileDownloadDirectory", "varchar", "200", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("RemoteFileUploadDirectory", "varchar", "200", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("AlertEmailAddress", "varchar", "200", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("SaveUserNameLocal", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("SavePasswordLocal", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableApplicationTimeout", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableSaveSettings", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableFileUpload", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableFileDownload", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableOrderEntry", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableReportBrowser", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableBillingBrowser", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("EnableEmailAlert", "tinyint", "1", "0", false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("VersionCurrentlyUsing", "varchar", "50", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("SystemUserId", "int", "11", "0", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Signature", "varchar", "500", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("FacilityId", "varchar", "50", "null", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "null", true));


            this.SetKeyField("WebServiceAccountId");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
