using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace YellowstonePathology.MySQLMigration
{
    public class MySQLDatabaseBuilder
    {
        private string MySqlConnectionString;
        private string m_DBName;
        List<string> m_ForbiddenWords;
        List<string> m_ReservedWords;
        List<string> m_KeyWords;

        public MySQLDatabaseBuilder(string dbIndicator)
        {
            MySqlConnectionString = YellowstonePathology.Properties.Settings.Default.MySqlConnectionString;
            this.m_DBName = "lis";
            if (dbIndicator == "temp")
            {
                MySqlConnectionString = MySqlConnectionString.Replace("lis", "temp");
                this.m_DBName = "temp";
            }
        }

        public string CreateIndex(string tableName, string columnName)
        {
            string result = "Index already exists";
            string indexName = "idx_" + tableName + "_" + columnName;
            string command = "Create INDEX " + indexName + " on " + tableName + " (" + columnName + ");";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = command;
            cmd.Connection = new MySqlConnection(MySqlConnectionString);
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
                result = "Index created";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = "Index already exists";
            }

            return result;
        }

        public string CreatePrimaryKey(string tableName, string columnName, string keyType)
        {
            string result = "Primary Key created.";
            string command = "alter table " + tableName + " add constraint pk_" + tableName + " primary key(" + columnName;
            if (keyType == "TEXT")
            {
                command += "(50)";
            }
            command += ")";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = command;
            cmd.Connection = new MySqlConnection(MySqlConnectionString);
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = "Primary Key already exists";
            }

            return result;
        }

        public string DropColumn(string tableName, string columnToBeDropped)
        {
            string result = "Column " + columnToBeDropped + " has been dropped from " + tableName;
            string command = "alter table " + tableName + " drop column " + columnToBeDropped;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = command;
            cmd.Connection = new MySqlConnection(MySqlConnectionString);
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = "Column " + columnToBeDropped + " does not exist in " + tableName;
            }

            return result;
        }

        private void CreatePrimaryKeyCommand(string tableName, string columnName, string keyType, StringBuilder result)
        {
            string command = "alter table " + tableName + " add constraint pk_" + tableName + " primary key(" + columnName;
            if (keyType == "TEXT")
            {
                command += "(50)";
            }
            command += ")";
            result.AppendLine(command);
        }

        public Business.Rules.MethodResult AddTransferColumn(string tableName)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "alter table " + tableName + " add Transferred bit NULL";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                try
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    methodResult.Message = cmd.CommandText;
                    methodResult.Success = false;
                }
            }

            return methodResult;
        }

        public Business.Rules.MethodResult AddDBTS(string tableName)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            bool hasDBTS = MySQLDatabaseBuilder.HasTransferDBTSAttribute(tableName);
            bool hasTSA = MySQLDatabaseBuilder.HasTransferTransferStraightAcrossAttribute(tableName);

            if (hasDBTS == false) this.AddTransferDBTSAttribute(tableName);
            if (hasTSA == false) this.AddTransferStraightAcrossAttribute(tableName, false);
            return methodResult;
        }

        private string GetCreateTableCommand(string tableName, List<PropertyInfo> tableProperties)
        {
            string sqlCommand = "Create Table If Not Exists " + tableName + "(";

            for (int i = 0; i < tableProperties.Count; i++)
            {
                PropertyInfo columnProperty = tableProperties[i];
                sqlCommand += this.GetDataColumnDefinition(tableName, columnProperty);
            }

            sqlCommand = sqlCommand.Remove(sqlCommand.Length - 2, 2);
            sqlCommand += ");";
            return sqlCommand;
        }

        private string GetCreatePrimaryKeyCommand(string tableName, string columnName)
        {
            string result = "alter table " + tableName + " add constraint pk_" + tableName + " primary key(" + columnName + ")";
            return result;
        }

        private string GetAddColumnCommand(string tableName, string columnName, string columnDataType)
        {
            string result = "ALTER TABLE " + tableName + " ADD COLUMN " + columnName + " " + columnDataType + ";";
            return result;
        }

        private Business.Rules.MethodResult RunMySqlCommand(string command)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = command;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    methodResult.Message += "MySQL error executing " + command;
                    methodResult.Success = false;
                }
            }
            return methodResult;
        }

        private string KeyStringFromList(MigrationStatus migrationStatus, List<string> keys)
        {
            bool needsTic = migrationStatus.KeyFieldProperty.PropertyType == typeof(string) ? true : false;
            string result = string.Empty;

            foreach (string key in keys)
            {
                if (needsTic == true) result += "'" + key + "', ";
                else result += key + ", ";
            }
            result = result.Remove(result.Length - 2, 2);
            return result;
        }

        public Business.Rules.MethodResult BulkLoadData(MigrationStatus migrationStatus, int numberOfObjectsToMove, int whenToStop)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            if (this.TablesAreOkToUse(migrationStatus) == true)
            {
                if (migrationStatus.UnLoadedDataCount > 0)
                {

                    string countToMove = numberOfObjectsToMove.ToString();

                    int repetitions = this.GetTableRepeatCount(migrationStatus.TableName, countToMove);
                    if (repetitions > 0)
                    {
                        if (whenToStop <= repetitions)
                        {
                            repetitions = whenToStop;
                        }
                    }

                    for (int idx = 0; idx < repetitions; idx++)
                    {
                        List<string> keys = this.GetBulkLoadDataKeys(migrationStatus, countToMove);
                        if (keys.Count == 0)
                        {
                            break;
                        }
                        string keyString = this.KeyStringFromList(migrationStatus, keys);
                        methodResult = this.LoadData(migrationStatus, keyString);

                    }

                    MySQLDatabaseBuilder.SetTransferDBTS(migrationStatus.TableName);
                }
            }
            return methodResult;
        }

        public Business.Rules.MethodResult DailyLoadData(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            if (this.TablesAreOkToUse(migrationStatus) == true)
            {
                if (migrationStatus.UnLoadedDataCount > 0)
                {
                    List<string> keys = this.GetDailyLoadDataKeys(migrationStatus);
                    string keyString = this.KeyStringFromList(migrationStatus, keys);
                    string deleteCmd = "Delete from " + this.m_DBName + '.' + migrationStatus.TableName + " where " + migrationStatus.KeyFieldName + " in (" + keyString + ");";
                    this.RunMySqlCommand(deleteCmd);

                    methodResult = this.LoadData(migrationStatus, keyString);
                    MySQLDatabaseBuilder.SetTransferDBTS(migrationStatus.TableName);

                    List<string> checkCommands = new List<string>();
                    Business.Rules.MethodResult checkResult = this.CompareData(migrationStatus, keys, checkCommands);
                    if (checkCommands.Count > 0)
                    {
                        methodResult.Success = false;
                        methodResult.Message += "Update failed on " + checkCommands.Count.ToString();
                    }
                }
            }
            return methodResult;
        }

        private Business.Rules.MethodResult LoadData(MigrationStatus migrationStatus, string keyString)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();

            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            DataTable dataTable = new DataTable();
            dataSet.Tables.Add(dataTable);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = this.GetSelectStatement(migrationStatus, keyString);
            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dataTable.Load(dr, LoadOption.OverwriteChanges);
                }
            }

            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string commandText = this.GetInsertStatement(migrationStatus.TableName, migrationStatus.PersistentProperties, dataTableReader);
                Business.Rules.MethodResult result = this.RunMySqlCommand(commandText);
                if (result.Success == false)
                {
                    methodResult.Message += "Error in Loading Data " + commandText + Environment.NewLine;
                    methodResult.Success = false;
                    this.SaveError(migrationStatus.TableName, commandText);
                }
            }

            this.SetTransfered(migrationStatus.TableName, migrationStatus.KeyFieldName, keyString);
            return methodResult;
        }

        private int GetTableRepeatCount(string TableName, string rowsToUse)
        {
            int rows = Convert.ToInt32(rowsToUse);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select count(*) from " + TableName + " where Transferred is null or Transferred = 0";

            int result = 0;
            int count = 0;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        count = (int)dr[0];
                    }
                }
            }

            if (count > 0)
            {
                result = count / rows;
                if (count % rows > 0) result++;
            }
            return result;
        }

        private void SetTransfered(string tableName, string keyName, string keys)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update " + tableName + " set Transferred = 1 where  " + keyName + " in (" + keys + ")";
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        private string GetSelectStatement(MigrationStatus migrationStatus, string keys)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Select ");

            foreach (PropertyInfo property in migrationStatus.PersistentProperties)
            {
                result.Append("[");
                result.Append(property.Name);
                result.Append("], ");
            }

            result = result.Remove(result.Length - 2, 2);
            result.Append(" from ");
            result.Append(migrationStatus.TableName);
            //result.Append(" Where (Transferred is null or Transferred = 0) and ");
            result.Append(" Where ");
            result.Append(migrationStatus.KeyFieldName);
            result.Append(" in (");
            result.Append(keys);
            result.Append(") Order By ");
            result.Append(migrationStatus.KeyFieldName);
            return result.ToString();
        }

        private string GetInsertStatement(string tableName, List<PropertyInfo> properties, System.Data.Common.DbDataReader dr)
        {
            string result = "Insert " + this.m_DBName + '.' + tableName + "(";

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyInfo property = properties[i];
                string name = properties[i].Name;
                result = result + name + ", ";
            }

            result = result.Remove(result.Length - 2, 2);

            result = result + ") values (";

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyInfo property = properties[i];
                if (dr[i] == DBNull.Value)
                {
                    result = result + "NULL, ";
                }
                else
                {
                    if (property.PropertyType == typeof(string))
                    {
                        string text = dr[i].ToString().Replace("'", "''");
                        text = text.Replace("\\", "\\\\");
                        result = result + "'" + text + "', ";
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        DateTime dt = (DateTime)dr[i];
                        result = result + "'" + dt.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + "', ";
                    }
                    else
                    {
                        result = result + dr[i] + ", ";
                    }
                }
            }

            result = result.Remove(result.Length - 2, 2);

            result = result + ")";
            return result;
        }

        public Business.Rules.MethodResult BuildTable(MigrationStatus migrationSatus)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            methodResult.Success = false;

            if (migrationSatus.PersistentProperties.Count > 0)
            {
                string createTableCommand = this.GetCreateTableCommand(migrationSatus.TableName, migrationSatus.PersistentProperties);
                string createPrimaryKeyCommand = this.GetCreatePrimaryKeyCommand(migrationSatus.TableName, migrationSatus.KeyFieldName);
                string createTimeStampColumn = this.GetAddColumnCommand(migrationSatus.TableName, "Timestamp", "Timestamp");

                Business.Rules.MethodResult result = this.RunMySqlCommand(createTableCommand);
                if (result.Success == true)
                {
                    result = this.RunMySqlCommand(createPrimaryKeyCommand);
                }
                else
                {
                    methodResult.Success = false;
                    methodResult.Message += result.Message;
                }

                if (result.Success == true)
                {
                    result = this.RunMySqlCommand(createTimeStampColumn);
                }
                else
                {
                    methodResult.Success = false;
                    methodResult.Message += result.Message;
                }

                if (result.Success == false)
                {
                    methodResult.Success = false;
                    methodResult.Message += result.Message;
                }
            }

            return methodResult;
        }

        private bool TablesAreOkToUse(MigrationStatus migrationStatus)
        {
            bool result = false;
            this.GetStatus(migrationStatus);
            if (migrationStatus.HasTable && migrationStatus.HasAllColumns && migrationStatus.HasDBTS &&
                migrationStatus.HasTimestampColumn && migrationStatus.HasTransferredColumn)
            {
                result = true;
            }
            return result;
        }

        public Business.Rules.MethodResult SynchronizeData(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            if (this.TablesAreOkToUse(migrationStatus) == true)
            {
                if (migrationStatus.OutOfSyncCount > 0)
                {
                    List<string> updateCommands = new List<string>();
                    List<string> keys = this.GetSyncDataKeyList(migrationStatus);
                    string deleteKeys = this.KeyStringFromList(migrationStatus, keys);
                    string deleteCmd = "Delete from " + migrationStatus.TableName + " where " + migrationStatus.KeyFieldName + " in (" + deleteKeys + ");";

                    Business.Rules.MethodResult result = this.RunMySqlCommand(deleteCmd);
                    if (result.Success == true)
                    {
                        result = this.LoadData(migrationStatus, deleteKeys);
                        if (result.Success == true)
                        {
                            result = this.CompareData(migrationStatus, keys, updateCommands);
                            MySQLDatabaseBuilder.SetTransferDBTS(migrationStatus.TableName);
                            if (updateCommands.Count > 0)
                            {
                                overallResult.Success = false;
                                overallResult.Message += "Update failed on " + updateCommands.Count.ToString();
                            }

                            if (result.Success == false)
                            {
                                overallResult.Success = false;
                                overallResult.Message += result.Message;
                            }
                        }
                        else
                        {
                            overallResult.Success = false;
                            overallResult.Message += result.Message;
                        }
                    }
                    else
                    {
                        overallResult.Success = false;
                        overallResult.Message += result.Message;
                    }
                }
            }
            return overallResult;
        }

        private Business.Rules.MethodResult Synchronize(List<string> updateCommands)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int updatedRowCount = 0;
            foreach (string cmdText in updateCommands)
            {
                Business.Rules.MethodResult cmdResult = this.RunMySqlCommand(cmdText);
                if (cmdResult.Success == false)
                {
                    result.Message += "Errored in " + cmdText + Environment.NewLine;
                }
                else
                {
                    updatedRowCount++;
                }
            }
            result.Message += "Updated " + updatedRowCount.ToString() + Environment.NewLine;

            return result;
        }

        public void GetStatus(MigrationStatus migrationStatus)
        {
            migrationStatus.HasTimestampColumn = MySQLDatabaseBuilder.HasSQLTimestamp(migrationStatus.TableName);
            migrationStatus.HasTransferredColumn = this.TableHasTransferColumn(migrationStatus.TableName);
            bool hasDBTS = MySQLDatabaseBuilder.HasTransferDBTSAttribute(migrationStatus.TableName);
            bool hasTSA = MySQLDatabaseBuilder.HasTransferTransferStraightAcrossAttribute(migrationStatus.TableName);
            migrationStatus.HasDBTS = hasDBTS && hasTSA;

            migrationStatus.HasTable = this.HasMySqlTable(migrationStatus.TableName);
            if (migrationStatus.HasTransferredColumn && migrationStatus.HasTable)
            {
                migrationStatus.UnLoadedDataCount = this.GetUnloadedDataCount(migrationStatus.TableName);
                migrationStatus.OutOfSyncCount = this.GetOutOfSyncCount(migrationStatus.TableName);
                string cmdString = "Select count(*) from " + migrationStatus.TableName + " where Transferred = 1;";
                migrationStatus.SqlServerTransferredCount = this.GetSqlServerRowCount(cmdString);
                migrationStatus.MySqlRowCount = this.GetMySqlRowCount(migrationStatus.TableName);
            }
            else
            {
                migrationStatus.UnLoadedDataCount = this.GetDataCount(migrationStatus.TableName);
                migrationStatus.OutOfSyncCount = migrationStatus.UnLoadedDataCount;
            }

            Business.Rules.MethodResult missingColumnResult = CompareTable(migrationStatus);
            migrationStatus.HasAllColumns = missingColumnResult.Success;
        }

        private bool TableHasTransferColumn(string tableName)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from sys.columns where Name = N'Transferred' and Object_ID = Object_ID(N'" + tableName + "')";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        private bool HasMySqlTable(string tableName)
        {
            bool result = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_schema = '" + this.m_DBName + "' AND table_name = '" + tableName + "'";

            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                string mySqlTableName = string.Empty;
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        mySqlTableName = dr.GetValue(0).ToString();
                        if (string.IsNullOrEmpty(mySqlTableName) == false) result = true;
                    }
                    dr.Close();
                }
            }

            return result;
        }

        private int GetUnloadedDataCount(string tableName)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName + " where [Transferred] is null or [Transferred] = 0 ";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = (int)value;
            }

            return result;
        }

        private int GetDataCount(string tableName)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = (int)value;
            }

            return result;
        }

        private int GetOutOfSyncCount(string tableName)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName + " where Transferred = 1 and [TimeStamp] > " +
                "(SELECT convert(int, ep.value) " +
                "FROM sys.extended_properties AS ep " +
                "INNER JOIN sys.tables AS t ON ep.major_id = t.object_id " +
                "INNER JOIN sys.columns AS c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id " +
                "WHERE class = 1 and t.Name = '" + tableName + "' and c.Name = 'TimeStamp' and ep.Name = 'TransferDBTS')";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = (int)value;
            }

            return result;
        }

        private string GetDataColumnDefinition(string tableName, PropertyInfo propertyInfo)
        {
            string result = propertyInfo.Name + " ";

            Attribute attribute = propertyInfo.GetCustomAttribute(typeof(YellowstonePathology.Business.Persistence.PersistentDataColumnProperty));
            if (attribute != null)
            {
                YellowstonePathology.Business.Persistence.PersistentDataColumnProperty persistentDataColumnProperty = (Business.Persistence.PersistentDataColumnProperty)attribute;
                result += persistentDataColumnProperty.DataType;

                if (string.IsNullOrEmpty(persistentDataColumnProperty.ColumnLength) == false)
                {
                    if (persistentDataColumnProperty.DataType != "text")
                    {
                        result += "(" + persistentDataColumnProperty.ColumnLength + ")";
                    }
                }

                result += " ";

                if (persistentDataColumnProperty.IsNullable == false)
                {
                    result += "NOT NULL ";
                }

                if (string.IsNullOrEmpty(persistentDataColumnProperty.DefaultValue) == false)
                {
                    if (!(persistentDataColumnProperty.IsNullable == false && persistentDataColumnProperty.DefaultValue == "null"))
                    {
                        result += "DEFAULT " + persistentDataColumnProperty.DefaultValue;
                    }
                }
            }

            result += ", ";

            return result;
        }

        private string AddDefaultToColumnDefinition(PropertyInfo propertyInfo)
        {
            string result = string.Empty;

            Attribute attribute = propertyInfo.GetCustomAttribute(typeof(YellowstonePathology.Business.Persistence.PersistentProperty));
            if (attribute != null)
            {
                YellowstonePathology.Business.Persistence.PersistentProperty persistentProperty = (Business.Persistence.PersistentProperty)attribute;
                //result = persistentProperty.DefaultValue;
            }

            if (string.IsNullOrEmpty(result) == false)
            {
                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    result = string.Empty;
                }
                else
                {
                    result = " DEFAULT " + result;
                }
            }

            return result;
        }

        private int GetMaxCurrentDataLength(string propertyName, string tableName)
        {
            int dataLength = 0;
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "select max(len(" + propertyName + ")) from " + tableName;
            cmd1.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd1.Connection = cn;
                using (SqlDataReader dr = cmd1.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr[0] != DBNull.Value)
                        {
                            string data = dr[0].ToString();
                            dataLength = Convert.ToInt32(data);
                            if (dataLength < 50) dataLength = 50;
                            else if (dataLength < 100) dataLength = 100;
                            else if (dataLength < 500) dataLength = 500;
                            else if (dataLength < 1000) dataLength = 1000;
                            else if (dataLength < 5000) dataLength = 5000;
                            else if (dataLength <= 8000) dataLength = 8000;
                            else dataLength = -1;
                        }
                        else
                        {
                            dataLength = 50;
                        }
                    }
                }
            }
            return dataLength;
        }

        public void GetDBDataTypes(string tableName, List<string> currentTypes)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select distinct data_type from INFORMATION_SCHEMA.COLUMNS where table_name = '" + tableName + "'";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string dataType = dr[0].ToString();
                        bool found = false;
                        foreach (string etype in currentTypes)
                        {
                            if (etype == dataType)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            currentTypes.Add(dataType);
                        }
                    }
                }
            }
        }

        private string GetDefaultString(string defaultValue)
        {
            string result = defaultValue.Substring(1);
            result = result.Substring(0, result.Length - 1);
            if (result[0] == '(')
            {
                result = result.Substring(1);
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        public bool BuildPersistentDataColumnProperty(MigrationStatus migrationStatus, string[] lines)
        {
            bool result = false;
            for (int idx = 0; idx < lines.Length; idx++)
            {
                if (lines[idx].Contains("[PersistentProperty") ||
                    lines[idx].Contains("[PersistentDocumentIdProperty") ||
                    lines[idx].Contains("[PersistentPrimaryKeyProperty"))
                {
                    foreach (PropertyInfo property in migrationStatus.PersistentProperties)
                    {
                        string matchstring = " " + property.Name;
                        int ndx = lines[idx + 1].LastIndexOf(" ");
                        string lineString = lines[idx + 1].Substring(ndx).TrimEnd();
                        if (lineString == matchstring)
                        {
                            int start = lines[idx].IndexOf("[Persistent");
                            string startingString = lines[idx].Substring(0, start);
                            StringBuilder dataDefinition = new StringBuilder();
                            dataDefinition.AppendLine(lines[idx]);
                            dataDefinition.Append(startingString);
                            this.GetDataColumnProperties(migrationStatus.TableName, property, dataDefinition);
                            lines[idx] = dataDefinition.ToString();
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private void GetDataColumnProperties(string tableName, PropertyInfo property, StringBuilder dataDefinition)
        {
            string defaultValue = string.Empty;
            string isNullable = string.Empty;
            string dataType = string.Empty;
            string columnLength = string.Empty;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select column_default, is_nullable, data_type, character_maximum_length from INFORMATION_SCHEMA.COLUMNS where table_name = '" +
                tableName + "' and Column_name = '" + property.Name + "'";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr[0] != DBNull.Value)
                        {
                            defaultValue = dr[0].ToString();
                        }
                        isNullable = dr[1].ToString();
                        dataType = dr[2].ToString();
                        if (dr[3] != DBNull.Value)
                        {
                            columnLength = dr[3].ToString();
                        }

                    }
                }
            }

            if (isNullable == "NO")
            {
                isNullable = "false";
            }
            else
            {
                isNullable = "true";
            }

            if (string.IsNullOrEmpty(columnLength) == true)
            {
                if (dataType == "int")
                {
                    columnLength = "11";
                }
                else if (dataType == "tinyint")
                {
                    columnLength = "1";
                }
                else if (dataType == "datetime")
                {
                    columnLength = "3";
                }
                else
                {
                    string s = dataType;
                }
            }

            if (dataType == "varchar" && columnLength == "-1")
            {
                int length = this.GetMaxCurrentDataLength(property.Name, tableName);
                columnLength = length.ToString();
                if (columnLength == "-1")
                {
                    dataType = "text";
                }
            }

            if (string.IsNullOrEmpty(defaultValue) == false)
            {
                defaultValue = this.GetDefaultString(defaultValue);
                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    defaultValue = "null";
                }
            }
            else
            {
                defaultValue = "null";
            }

            dataDefinition.Append("[PersistentDataColumnProperty(");
            dataDefinition.Append(isNullable);
            dataDefinition.Append(", \"");
            dataDefinition.Append(columnLength);
            dataDefinition.Append("\", \"");
            dataDefinition.Append(defaultValue);
            dataDefinition.Append("\", \"");
            dataDefinition.Append(dataType);
            dataDefinition.Append("\")]");
        }

        public Business.Rules.MethodResult CompareTable(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Message = "Missing columns: ";
            List<string> columnNames = this.RunTableQuery(migrationStatus.TableName);
            foreach (PropertyInfo property in migrationStatus.PersistentProperties)
            {
                bool found = false;
                foreach (string columnName in columnNames)
                {
                    if (columnName == property.Name)
                    {
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    result.Success = false;
                    result.Message += property.Name + ", ";
                }
            }
            return result;
        }

        private List<string> RunTableQuery(string tableName)
        {
            List<string> result = new List<string>();

            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select c.NAME from INFORMATION_SCHEMA.INNODB_SYS_COLUMNS c join INFORMATION_SCHEMA.INNODB_SYS_TABLES t " +
                    "on c.TABLE_ID = t.TABLE_ID where t.Name = concat('" + this.m_DBName + "/', @TableName);";
                cmd.Parameters.Add("@TableName", MySqlDbType.VarChar).Value = tableName;

                using (MySqlDataReader msdr = cmd.ExecuteReader())
                {
                    while (msdr.Read())
                    {
                        result.Add(msdr[0].ToString());
                    }
                }
            }
            return result;
        }

        public Business.Rules.MethodResult AddMissingColumns(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Message = "Errored on: ";
            Business.Rules.MethodResult missingColumnResult = this.CompareTable(migrationStatus);
            if (missingColumnResult.Success == false)
            {
                string columns = missingColumnResult.Message.Replace("Missing columns: ", string.Empty);
                string[] missingColumns = columns.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string columnName in missingColumns)
                {
                    foreach (PropertyInfo property in migrationStatus.PersistentProperties)
                    {
                        if (property.Name == columnName)
                        {
                            string columnDefinition = this.GetDataColumnDefinition(migrationStatus.TableName, property);
                            string columnDataType = columnDefinition.Replace(columnName + " ", string.Empty);
                            columnDataType = columnDataType.Substring(0, columnDataType.Length - 2);
                            string command = this.GetAddColumnCommand(migrationStatus.TableName, columnName, columnDataType);
                            Business.Rules.MethodResult columnResult = this.RunMySqlCommand(command);
                            if (columnResult.Success == false)
                            {
                                result.Success = false;
                                result.Message += columnName + ", ";
                            }
                            break;
                        }
                    }
                }
            }

            migrationStatus.HasAllColumns = result.Success;
            return result;
        }

        private void SaveError(string tableName, string errorCommand)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Insert tblMySqlError(TableName, ErrorCommand) values(@TableName, @ErrorCommand)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;
            cmd.Parameters.Add("@ErrorCommand", SqlDbType.VarChar).Value = errorCommand;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public Business.Rules.MethodResult DailySync(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            if (this.TablesAreOkToUse(migrationStatus) == true)
            {
                overallResult = this.SynchronizeData(migrationStatus);

                Business.Rules.MethodResult loadresult = this.DailyLoadData(migrationStatus);
                overallResult.Message += loadresult.Message;
                if (loadresult.Success == false)
                {
                    overallResult.Success = false;
                }
            }
            else
            {
                overallResult.Success = false;
                overallResult.Message += "Table or column issue with " + migrationStatus.TableName;
            }
            return overallResult;
        }

        private List<string> GetMatchList(string tableName, string keyField)
        {
            List<string> result = new List<string>();

            //int cnt = this.GetDataCount(tableName);
            //if (cnt < 200000) result.Add("'%'");
            //else
            //{
            if (keyField == "MasterAccessionNo")
            {
                /*result.Add("'19%'"); result.Add("'50%'"); result.Add("'2000%'"); result.Add("'2001%'"); result.Add("'2002%'");
                result.Add("'2003%'"); result.Add("'2004%'"); result.Add("'2005%'"); result.Add("'2006%'"); result.Add("'2007%'");
                result.Add("'2008%'"); result.Add("'2009%'"); result.Add("'2010%'"); result.Add("'2011%'"); result.Add("'2012%'");
                result.Add("'2013%'"); result.Add("'13-%'"); ; result.Add("'14-%'"); result.Add("'15-%'"); result.Add("'16-%'");*/
                result.Add("'17-%'");
            }
            else if (keyField == "ReportNo")
            {
                /*result.Add("'_99-%'"); result.Add("'_00-%'"); result.Add("'_01-%'"); result.Add("'_02-%'"); result.Add("'_03-%'");
                result.Add("'_04-%'"); result.Add("'_05-%'"); result.Add("'_06-%'"); result.Add("'_07-%'"); result.Add("'_08-%'");
                result.Add("'_09-%'"); result.Add("'_10-%'"); result.Add("'_11-%'"); result.Add("'_12-%'"); result.Add("'_13-%'");
                result.Add("'_14-%'"); result.Add("'14-%'"); result.Add("'_15-%'"); result.Add("'15-%'"); result.Add("'_16-%'");
                result.Add("'16-%'"); result.Add("'_17-%'");*/
                result.Add("'17-%'");
            }
            else
            {
                /*result.Add("'0%'"); result.Add("'1%'"); result.Add("'2%'"); result.Add("'3%'"); result.Add("'4%'");
                result.Add("'5%'"); result.Add("'6%'"); result.Add("'7%'"); result.Add("'8%'"); result.Add("'9%'");
                result.Add("'a%'"); result.Add("'b%'"); result.Add("'c%'"); result.Add("'d%'"); result.Add("'e%'");
                result.Add("'f%'");*/
                switch (tableName)
                {
                    case "tblAliquotOrder":
                        result.Add("select AliquotOrderId from tblAliquotOrder a join tblSpecimenOrder s on a.SpecimenORderId = s.SpecimenORderID where s.MasterAccessionNo like '17-%'");
                        break;
                    case "tblAmendment":
                        result.Add("select AmendmentId from tblAmendment where finalDate > '12/31/2016'");
                        break;
                    case "tblFlowMarkers":
                        result.Add("select FlowMarkerId from tblFlowMarkers where ReportNo like '17-%'");
                        break;
                    case "tblIntraoperativeConsultationResult":
                        result.Add("select IntraoperativeConsultationResultId from tblIntraoperativeConsultationResult");
                        break;
                    case "tblMaterialTrackingBatch":
                        result.Add("select MaterialTrackingBatchId from tblMaterialTrackingBatch where BatchDate > '12/31/2016'");
                        break;
                    case "tblMaterialTrackingLog":
                        result.Add("select MaterialTrackingLogId from tblMaterialTrackingLog l join tblMaterialTrackingBatch b on l.MaterialTrackingBatchId = b.MaterialTrackingBatchId where b.BatchDate > '12/31/2016'");
                        break;
                    case "tblPanelOrder":
                        result.Add("select PanelOrderId from tblPanelOrder where ReportNo like '17-%'");
                        break;
                    case "tblPanelOrderCytology":
                        result.Add("select t.PanelOrderId from tblPanelOrderCytology t join tblPanelOrder p on t.PanelOrderId = p.PanelOrderId where p.ReportNo like '17-%'");
                        break;
                    case "tblPanelSetOrderCPTCode":
                        result.Add("select PanelSetOrderCptCodeId from tblPanelSetOrderCptCode where ReportNo like '17-%'");
                        break;
                    case "tblPanelSetOrderCPTCodeBill":
                        result.Add("select PanelSetOrderCptCodeBillId from tblPanelSetOrderCptCodeBill where ReportNo like '17-%'");
                        break;
                    case "tblPhysician":
                        result.Add("select PhysicianId from tblPhysician");
                        break;
                    case "tblPhysicianClient":
                        result.Add("select PhysicianClientId from tblPhysicianClient");
                        break;
                    case "tblPhysicianClientDistribution":
                        result.Add("select PhysicianClientDistributionID from tblPhysicianClientDistribution");
                        break;
                    case "tblSlideOrder":
                        result.Add("select SlideOrderId from tblSlideOrder where orderDate > '12/31/2016'");
                        break;
                    case "tblSpecimenOrder":
                        result.Add("select SpecimenOrderId from tblSpecimenOrder where MasterAccessionNo like '17-%'");
                        break;
                    case "tblStainResult":
                        result.Add("select s.StainResultId from tblStainResult s join tblTestOrder t on s.TestOrderId = t.TestOrderId join tblPanelOrder p on t.PanelOrderId = p.PanelOrderId where p.ReportNo like '17-%'");
                        break;
                    case "tblStainTest":
                        result.Add("select stainTestId from tblStainTest");
                        break;
                    case "tblSurgicalAudit":
                        result.Add("select SurgicalAuditId from tblSurgicalAudit where ReportNo like '17-%'");
                        break;
                    case "tblSurgicalSpecimen":
                        result.Add("select SurgicalSpecimenId from tblSurgicalSpecimen where ReportNo like '17-%'");
                        break;
                    case "tblSurgicalSpecimenAudit":
                        result.Add("select SurgicalSpecimenAuditId from tblSurgicalSpecimenAudit where ReportNo like '17-%'");
                        break;
                    case "tblTaskOrder":
                        result.Add("select TaskOrderId from tblTaskOrder where ReportNo like '17-%'");
                        break;
                    case "tblTaskOrderDetail":
                        result.Add("select d.TaskOrderDetailId from tblTaskOrderDetail d join tblTaskOrder t on d.TaskOrderId = t.TaskOrderId where t.ReportNo like '17-%'");
                        break;
                    case "tblTaskOrderDetailFedexShipment":
                        result.Add("select f.TaskOrderDetailId from tblTaskOrderDetailFedexShipment f join tblTaskOrderDetail d on f.TaskOrderDetailId = d.TaskOrderDetailId join tblTaskOrder t on d.TaskOrderId = t.TaskOrderId where t.ReportNo like '17-%'");
                        break;
                    case "tblTestOrder":
                        result.Add("select t.TestOrderId from tblTestOrder t join tblPanelOrder p on t.PanelOrderId = p.PanelOrderId where p.ReportNo like '17-%'");
                        break;
                    case "tblTestOrderReportDistribution":
                        result.Add("select TestOrderReportDistributionId from tblTestOrderReportDistribution where DateAdded > '12/31/2016'");
                        break;
                    case "tblTestOrderReportDistributionLog":
                        result.Add("select TestOrderReportDistributionLogId from tblTestOrderReportDistributionLog where timeDistributed > '1/1/2017'");
                        break;
                }
            }
            //}
            return result;
        }

        public Business.Rules.MethodResult CompareTables(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            List<string> matchLike = this.GetMatchList(migrationStatus.TableName, migrationStatus.KeyFieldName);

            foreach (string match in matchLike)
            {
                //List<string> keys = new List<string>();

                List<string> keys = this.GetCompareDataKeyList(migrationStatus.TableName, migrationStatus.KeyFieldName, match);
                if (keys.Count > 0)
                {
                    List<string> updateCommands = new List<string>();
                    overallResult = this.CompareData(migrationStatus, keys, updateCommands);
                    if (updateCommands.Count > 0)
                    {
                        Business.Rules.MethodResult result = this.Synchronize(updateCommands);
                        overallResult.Message += result.Message;
                        if (result.Success == false)
                        {
                            overallResult.Success = false;
                        }

                        List<string> checkCommands = new List<string>();
                        Business.Rules.MethodResult checkResult = this.CompareData(migrationStatus, keys, checkCommands);
                        if (checkCommands.Count > 0)
                        {
                            overallResult.Success = false;
                            overallResult.Message += "Update failed on " + checkCommands.Count.ToString();
                            foreach (string cmd in checkCommands)
                            File.AppendAllText("C:/TEMP/NotMatched.txt", cmd + Environment.NewLine + "-- ----------------" + Environment.NewLine + Environment.NewLine);
                        }
                    }
                }
            }
            return overallResult;
        }

        private Business.Rules.MethodResult CompareData(MigrationStatus migrationStatus, List<string> keys, List<string> updateCommands)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            bool needsTic = migrationStatus.PersistentProperties[0].PropertyType == typeof(string) ? true : false;
            string compareSelect = this.GetCompareSelect(migrationStatus);

            foreach (object key in keys)
            {
                string keyString = key.ToString();
                if (needsTic == true)
                {
                    keyString = "'" + keyString + "'";
                }

                DataTable sqlServerDataTable = this.GetSqlServerData(compareSelect, keyString);
                DataTable mySqlDataTable = this.GetMySqlData(compareSelect, keyString);

                if (mySqlDataTable.Rows.Count == 0)
                {
                    // overallResult.Success = false;
                    // overallResult.Message += "Missing " + migrationStatus.TableName + " - " + migrationStatus.KeyFieldName + " = " + keyString;
                    // this.SaveError(migrationStatus.TableName, "Update " + migrationStatus.TableName + " set Transferred = 0 where " + migrationStatus.KeyFieldName + " = " + keyString);
                    continue;
                }

                if (sqlServerDataTable.Rows[0].ItemArray.SequenceEqual(mySqlDataTable.Rows[0].ItemArray) == false)
                {
                    DataRowComparer dataRowComparer = new DataRowComparer(migrationStatus);
                    DataTableReader sqlServerDataTableReader = new DataTableReader(sqlServerDataTable);
                    sqlServerDataTableReader.Read();
                    DataTableReader mySqlDataTableReader = new DataTableReader(mySqlDataTable);
                    mySqlDataTableReader.Read();
                    Business.Rules.MethodResult result = dataRowComparer.Compare(sqlServerDataTableReader, mySqlDataTableReader);
                    if (result.Success == false)
                    {
                        //File.AppendAllText("C:/TEMP/SurgicalTestOrders.txt", key.ToString() + Environment.NewLine);
                        updateCommands.Add(result.Message);
                    }
                }
            }
            return overallResult;
        }

        private int GetSqlServerRowCount(string commandText)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand(commandText);
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (int)dr[0];
                    }
                }
            }
            return result;
        }

        private int GetMySqlRowCount(string tableName)
        {
            int result = 0;
            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("Select count(*) from " + tableName + ";");
                cmd.Connection = cn;

                using (MySqlDataReader msdr = cmd.ExecuteReader())
                {
                    while (msdr.Read())
                    {
                        result = ((int)(long)msdr[0]);
                    }
                }
            }
            return result;
        }

        private List<string> GetCompareDataKeyList(string tableName, string keyField, string compareString)
        {
            List<string> result = new List<string>();
            //SqlCommand cmd = new SqlCommand("Select " + keyField + " from " + tableName + " where " + keyField + " like " + compareString);
            SqlCommand cmd = new SqlCommand(compareString);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }

        private List<string> GetCompareDataKeyList(MigrationStatus migrationStatus)
        {
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand("Select " + migrationStatus.KeyFieldName + " from " + migrationStatus.TableName + " where " +
                migrationStatus.KeyFieldName + " like '16-%' "); // +
                                                                 /*"and " +
                                                                 "Transferred = 1 and [TimeStamp] < (SELECT convert(int, ep.value) FROM sys.extended_properties AS ep " +
                                                                 "INNER JOIN sys.tables AS t ON ep.major_id = t.object_id " +
                                                                 "INNER JOIN sys.columns AS c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id " +
                                                                 "WHERE class = 1 and t.Name = '" + migrationStatus.TableName + "' and c.Name = 'TimeStamp' and ep.Name = 'TransferDBTS') order by 1");*/
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }

        private List<string> GetSyncDataKeyList(MigrationStatus migrationStatus)
        {
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand("Select " + migrationStatus.KeyFieldName + " from " + migrationStatus.TableName + " where Transferred = 1 " +
                "and [TimeStamp] > (SELECT convert(int, ep.value) FROM sys.extended_properties AS ep " +
                "INNER JOIN sys.tables AS t ON ep.major_id = t.object_id " +
                "INNER JOIN sys.columns AS c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id " +
                "WHERE class = 1 and t.Name = '" + migrationStatus.TableName + "' and c.Name = 'TimeStamp' and ep.Name = 'TransferDBTS') order by 1");
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }

        private string GetCompareSelect(MigrationStatus migrationStatus)
        {
            string result = "Select ";
            foreach (PropertyInfo property in migrationStatus.PersistentProperties)
            {
                result = result + "[" + property.Name + "], ";
            }

            result = result.Remove(result.Length - 2, 2);

            result = result + " from " + migrationStatus.TableName + " Where [" + migrationStatus.KeyFieldName + "] = ";
            return result;

        }

        private List<string> GetBulkLoadDataKeys(MigrationStatus migrationStatus, string countToSelect)
        {
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand("Select top (" + countToSelect + ") " + migrationStatus.KeyFieldName + " from " + migrationStatus.TableName + " where " +
                //"ReportNo  like '_9%' and " +
                //"orderdate < '10/1/2016' and " +
                "(Transferred = 0 or Transferred is null) order by 1");
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }

        private List<string> GetDailyLoadDataKeys(MigrationStatus migrationStatus)
        {
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand("Select " + migrationStatus.KeyFieldName + " from " + migrationStatus.TableName + " where " +
                "(Transferred = 0 or Transferred is null) order by 1");
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }

        private DataTable GetSqlServerData(string selectStatement, string keyValue)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            DataTable result = new DataTable();
            dataSet.Tables.Add(result);
            SqlCommand cmd = new SqlCommand(selectStatement + keyValue);
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    result.Load(dr, LoadOption.OverwriteChanges);
                }
            }
            return result;
        }

        private DataTable GetMySqlData(string selectStatement, string keyValue)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            DataTable result = new DataTable();
            dataSet.Tables.Add(result);
            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(selectStatement + keyValue + ";");
                cmd.CommandText = cmd.CommandText.Replace('[', '`');
                cmd.CommandText = cmd.CommandText.Replace(']', '`');
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                using (MySqlDataReader msdr = cmd.ExecuteReader())
                {
                    result.Load(msdr, LoadOption.OverwriteChanges);
                }
            }
            return result;
        }

        public Business.Rules.MethodResult GetStatus(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

            nonpersistentTableDef.HasTable = this.HasMySqlTable(nonpersistentTableDef.TableName);
            if (nonpersistentTableDef.HasTable)
            {
                string cmdString = "Select count(*) from " + nonpersistentTableDef.TableName + ";";
                nonpersistentTableDef.SqlServerRowCount = this.GetSqlServerRowCount(cmdString);
                nonpersistentTableDef.MySqlRowCount = this.GetMySqlRowCount(nonpersistentTableDef.TableName);
                Business.Rules.MethodResult missingColumnResult = CompareTable(nonpersistentTableDef);
                nonpersistentTableDef.HasAllColumns = missingColumnResult.Success;
            }

            return overallResult;
        }

        public Business.Rules.MethodResult BuildTable(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            string createPrimaryKeyCommand = this.GetCreatePrimaryKeyCommand(nonpersistentTableDef.TableName, nonpersistentTableDef.KeyField);

            Business.Rules.MethodResult result = this.RunMySqlCommand(nonpersistentTableDef.GetCreateTableCommand());
            if (result.Success == true && nonpersistentTableDef.HasPrimaryKey == true)
            {
                result = this.RunMySqlCommand(createPrimaryKeyCommand);
            }
            return result;
        }

        public Business.Rules.MethodResult CompareTable(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            List<string> columnNames = this.RunTableQuery(nonpersistentTableDef.TableName);
            foreach (MySQLMigration.NonpersistentColumnDef columnDef in nonpersistentTableDef.ColumnDefinitions)
            {
                bool found = false;
                foreach (string columnName in columnNames)
                {
                    if (columnName == columnDef.ColumnName)
                    {
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    overallResult.Success = false;
                    overallResult.Message += columnDef.ColumnName + ", ";
                }
            }
            return overallResult;
        }

        public Business.Rules.MethodResult AddMissingColumns(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            Business.Rules.MethodResult missingColumnResult = this.CompareTable(nonpersistentTableDef);
            if (missingColumnResult.Success == false)
            {
                string columns = missingColumnResult.Message.Replace("Missing columns: ", string.Empty);
                string[] missingColumns = columns.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string columnName in missingColumns)
                {
                    foreach (MySQLMigration.NonpersistentColumnDef columnDef in nonpersistentTableDef.ColumnDefinitions)
                    {
                        if (columnDef.ColumnName == columnName)
                        {
                            string command = "ALTER TABLE " + nonpersistentTableDef.TableName + " ADD COLUMN " + columnDef.ColumnDefinition + ";";
                            Business.Rules.MethodResult columnResult = this.RunMySqlCommand(command);
                            if (columnResult.Success == false)
                            {
                                overallResult.Success = false;
                                overallResult.Message += columnName + ", ";
                            }
                            break;
                        }
                    }
                }
            }

            nonpersistentTableDef.HasAllColumns = overallResult.Success;
            return overallResult;
        }

        public Business.Rules.MethodResult LoadNonpersistentData(string tableName, string fields)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            string cmdString = "Truncate table " + tableName + ";";
            this.RunMySqlCommand(cmdString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT OPENQUERY(MYSQL, 'select " + fields + " from " + this.m_DBName + "." + tableName + "') SELECT " + fields + " from " + tableName + ";";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                try
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    methodResult.Message = cmd.CommandText;
                    methodResult.Success = false;
                }
            }

            return methodResult;
        }

        private string GetTruncateTableStatement(NonpersistentTableDef tableDef)
        {
            string result = "Truncate table " + tableDef.TableName + ";";
            return result;
        }

        public Business.Rules.MethodResult RemoveDeletedRowsFromMySql(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int mismatchCount = migrationStatus.MySqlRowCount - migrationStatus.SqlServerTransferredCount;
            if (mismatchCount > 0)
            {
                bool needsTic = migrationStatus.KeyFieldProperty.PropertyType == typeof(string);
                List<string> keys = this.GetMySqlKeyList(migrationStatus.TableName, migrationStatus.KeyFieldName, needsTic);
                foreach (string key in keys)
                {
                    if (this.SqlServerHasRow(migrationStatus.TableName, migrationStatus.KeyFieldName, key) == false)
                    {
                        string cmd = "Delete from " + migrationStatus.TableName + " where " + migrationStatus.KeyFieldName + " = " + key + ";";
                        this.RunMySqlCommand(cmd);
                        mismatchCount -= 1;
                        if (mismatchCount == 0) break;
                    }
                }
            }
            return result;
        }

        private List<string> GetMySqlKeyList(string tableName, string keyField, bool needsTic)
        {
            List<string> keys = new List<string>();
            StringBuilder selectStatement = new StringBuilder();
            selectStatement.Append("select ");
            if (needsTic) selectStatement.Append("quote(");
            selectStatement.Append(keyField);
            if (needsTic) selectStatement.Append(") " + keyField);
            selectStatement.Append(" from ");
            selectStatement.Append(tableName);
            selectStatement.Append(" order by 1 desc;");
            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = selectStatement.ToString();

                using (MySqlDataReader msdr = cmd.ExecuteReader())
                {
                    while (msdr.Read())
                    {
                        keys.Add(msdr[0].ToString());
                    }
                }
            }
            return keys;
        }

        private bool SqlServerHasRow(string tableName, string keyField, string keyValue)
        {
            bool result = true;
            SqlCommand cmd = new SqlCommand("select count(*) from " + tableName + " where " + keyField + " = " + keyValue);
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = dr[0].ToString() == "0" ? false : true;
                    }
                }
            }
            return result;

        }

        public Business.Rules.MethodResult AddMissingIndexes(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            migrationStatus.TableIndexCollection = new TableIndexCollection();
            this.GetSqlServerIndexes(migrationStatus.TableName, migrationStatus.TableIndexCollection);
            foreach (MySQLMigration.TableIndex tableIndex in migrationStatus.TableIndexCollection)
            {
                Business.Rules.MethodResult result = this.CreateMySqlIndex(migrationStatus.TableName, tableIndex);
                if (result.Success == false)
                {
                    overallResult.Message += result.Message;
                    overallResult.Success = false;
                }
            }
            return overallResult;
        }

        public Business.Rules.MethodResult AddMissingIndexes(NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            nonpersistentTableDef.TableIndexCollection = new TableIndexCollection();
            this.GetSqlServerIndexes(nonpersistentTableDef.TableName, nonpersistentTableDef.TableIndexCollection);
            foreach (MySQLMigration.TableIndex tableIndex in nonpersistentTableDef.TableIndexCollection)
            {
                Business.Rules.MethodResult result = this.CreateMySqlIndex(nonpersistentTableDef.TableName, tableIndex);
                if (result.Success == false)
                {
                    overallResult.Message += result.Message;
                    overallResult.Success = false;
                }
            }
            return overallResult;
        }

        private void GetSqlServerIndexes(string tableName, TableIndexCollection tableIndexCollection)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select distinct idx.[name] IndexName from sys.[tables] t inner join sys.[indexes] idx on t.[object_id] = idx.[object_id] " +
                "where idx.[is_primary_key] = 0 and idx.Name is not null and t.[name] = @TableName order by idx.[name];" +
                "select idx.[name] IndexName, allc.[name] ColumnName, idxc.index_column_id PositionInIndex, idx.[is_unique] IsUnique " +
                "from sys.[tables] as tab inner join sys.[indexes] idx on tab.[object_id] = idx.[object_id] " +
                "inner join sys.[index_columns] idxc on idx.[object_id] = idxc.[object_id] and idx.[index_id] = idxc.[index_id] " +
                "inner join sys.[all_columns] allc on tab.[object_id] = allc.[object_id] and idxc.[column_id] = allc.[column_id] " +
                "where idx.[is_primary_key] = 0 and tab.[name] = @TableName order by idx.name, idxc.[index_column_id];";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string indexName = dr["IndexName"].ToString();
                        TableIndex tableIndex = new TableIndex(indexName);
                        tableIndexCollection.Add(tableIndex);
                    }

                    dr.NextResult();

                    while (dr.Read())
                    {
                        string indexName = dr["IndexName"].ToString();
                        foreach (TableIndex tableIndex in tableIndexCollection)
                        {
                            if (indexName == tableIndex.SqlServerIndexName)
                            {
                                IndexedColumn indexedColumn = new IndexedColumn(dr["ColumnName"].ToString(), (int)dr["PositionInIndex"], (bool)dr["IsUnique"]);
                                tableIndex.IndexedColumnCollection.Add(indexedColumn);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private Business.Rules.MethodResult CreateMySqlIndex(string tableName, MySQLMigration.TableIndex tableIndex)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            string indexName = tableIndex.GetIndexName(tableName);
            if (this.MySqlIndexExists(indexName) == false)
            {
                StringBuilder command = new StringBuilder();
                command.Append("create Index ");
                command.Append(indexName);
                command.Append(" on ");
                command.Append(tableName);
                command.Append(tableIndex.GetMySqlFormatedColumnNames());
                command.Append(";");
                result = this.RunMySqlCommand(command.ToString());

                if (result.Success == false)
                {
                    result.Message = "Error creating index " + indexName + Environment.NewLine;
                }
            }

            return result;
        }

        private bool MySqlIndexExists(string indexName)
        {
            bool result = false;
            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select count(*) from information_schema.statistics where " +
                    "table_schema = '" + this.m_DBName + "' and index_name = '" + indexName + "';");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                object value = cmd.ExecuteScalar();
                if ((long)value == 1) result = true;
            }
            return result;
        }

        public Business.Rules.MethodResult AddMissingForeignKeys(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            migrationStatus.TableForeignKeyCollection = new TableForeignKeyCollection();
            this.GetSqlServerForeignKeys(migrationStatus.TableName, migrationStatus.TableForeignKeyCollection);
            foreach (MySQLMigration.TableForeignKey tableForeignKey in migrationStatus.TableForeignKeyCollection)
            {
                if (this.MySqlForeignKeyExists(tableForeignKey.MySqlForeignKeyName) == false)
                {
                    string indexName = "idx_" + tableForeignKey.TableName + "_" + tableForeignKey.ColumnName;
                    if (this.MySqlIndexExists(indexName) == false)
                    {
                        this.CreateIndex(tableForeignKey.TableName, tableForeignKey.ColumnName);
                    }

                    string command = tableForeignKey.GetCreateStatement(indexName);
                    Business.Rules.MethodResult result = this.RunMySqlCommand(command);
                    if (result.Success == false)
                    {
                        overallResult.Message += "Unable to create foreign key " + tableForeignKey.MySqlForeignKeyName + Environment.NewLine;
                        overallResult.Success = false;
                    }
                }
            }
            return overallResult;
        }

        public Business.Rules.MethodResult AddMissingForeignKeys(NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            nonpersistentTableDef.TableForeignKeyCollection = new TableForeignKeyCollection();
            this.GetSqlServerForeignKeys(nonpersistentTableDef.TableName, nonpersistentTableDef.TableForeignKeyCollection);
            foreach (MySQLMigration.TableForeignKey tableForeignKey in nonpersistentTableDef.TableForeignKeyCollection)
            {
                if (this.MySqlForeignKeyExists(tableForeignKey.MySqlForeignKeyName) == false)
                {
                    string indexName = "idx_" + tableForeignKey.TableName + "_" + tableForeignKey.ColumnName;
                    if (this.MySqlIndexExists(indexName) == false)
                    {
                        this.CreateIndex(tableForeignKey.TableName, tableForeignKey.ColumnName);
                    }

                    string command = tableForeignKey.GetCreateStatement(indexName);
                    Business.Rules.MethodResult result = this.RunMySqlCommand(command);
                    if (result.Success == false)
                    {
                        overallResult.Message += "Unable to create foreign key " + tableForeignKey.MySqlForeignKeyName + Environment.NewLine;
                        overallResult.Success = false;
                    }
                }
            }
            return overallResult;
        }

        private void GetSqlServerForeignKeys(string tableName, TableForeignKeyCollection tableForeignKeyCollection)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select object_name(sfc.constraint_object_id) AS ForeignKeyName, OBJECT_Name(parent_object_id) AS TableName, " +
                "ac1.name as ColumnName, OBJECT_name(referenced_object_id) as ReferenceTableName, ac2.name as ReferenceColumnName " +
                "from sys.foreign_key_columns sfc join sys.all_columns ac1 on (ac1.object_id = sfc.parent_object_id and " +
                "ac1.column_id = sfc.parent_column_id) " +
                "join sys.all_columns ac2 on(ac2.object_id = sfc.referenced_object_id and ac2.column_id = sfc.referenced_column_id) " +
                "where sfc.parent_object_id = OBJECT_ID('" + tableName + "');";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MySQLMigration.TableForeignKey tableForeignKey = new TableForeignKey(dr["ForeignKeyName"].ToString(), dr["TableName"].ToString(),
                            dr["ColumnName"].ToString(), dr["ReferenceTableName"].ToString(), dr["ReferenceColumnName"].ToString());
                        tableForeignKeyCollection.Add(tableForeignKey);
                    }
                }
            }
        }

        private bool MySqlForeignKeyExists(string foreignKeyName)
        {
            bool result = false;
            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select count(*) from information_schema.key_column_usage where " +
                    "table_schema = '" + this.m_DBName + "' and constraint_name = '" + foreignKeyName + "';");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                object value = cmd.ExecuteScalar();
                if ((long)value == 1) result = true;
            }
            return result;
        }

        public Business.Rules.MethodResult DropMySqlForeignKeys()
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            List<string> dropCommands = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select concat('ALTER TABLE ', `Table_Name`, ' DROP FOREIGN KEY ', `Constraint_Name`, ';') statement from " +
                "information_schema.key_column_usage where table_schema = '" + this.m_DBName + "' and constraint_name like 'fk%' order by constraint_name;";

            using (MySqlConnection cn = new MySqlConnection(MySqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string dropCommand = dr[0].ToString();
                        this.RunMySqlCommand(dropCommand);
                    }
                }
            }
            return overallResult;
        }


        public static bool HasTransferDBTSAttribute(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT objtype, objname, name, value " +
                "FROM fn_listextendedproperty('TransferDBTS' " +
                ",'schema', 'dbo' " +
                ",'table', '" + tableName + "'" +
                ",'column', 'TimeStamp');";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static bool HasTransferTransferStraightAcrossAttribute(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT objtype, objname, name, value " +
                "FROM fn_listextendedproperty('TransferStraightAcross' " +
                ",'schema', 'dbo' " +
                ",'table', '" + tableName + "'" +
                ",'column', 'TimeStamp');";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static bool HasSQLTimestamp(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from sys.columns where Name = N'Timestamp' and Object_ID = Object_ID(N'" + tableName + "')";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public void AddTransferDBTSAttribute(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC sys.sp_addextendedproperty @name = N'TransferDBTS',  " +
                "@value = null,  " +
                "@level0type = N'SCHEMA', @level0name = dbo, " +
                "@level1type = N'TABLE',  @level1name = " + tableName + ", " +
                "@level2type = N'COLUMN', @level2name = [Timestamp];";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public void AddTransferStraightAcrossAttribute(string tableName, bool result)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC sys.sp_addextendedproperty @name = N'TransferStraightAcross',  " +
                "@value = '" + result.ToString() + "',  " +
                "@level0type = N'SCHEMA', @level0name = dbo, " +
                "@level1type = N'TABLE',  @level1name = " + tableName + ", " +
                "@level2type = N'COLUMN', @level2name = [Timestamp];";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSQLTimestampColumn(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                "WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = 'Timestamp') " +
                "BEGIN " +
                "ALTER TABLE [dbo].[" + tableName + "] ADD " +
                "[Timestamp] Timestamp NULL " +
                "END";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void SetTransferDBTS(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_updateextendedproperty " +
                "'TransferDBTS', " +
                "@@DBTS, " +
                "'SCHEMA', 'dbo', " +
                "'TABLE', '" + tableName + "', " +
                "'COLUMN', 'TimeStamp'";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public bool TableHasIdentityColumn(string tableName)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*) FROM SYS.IDENTITY_COLUMNS WHERE OBJECT_NAME(OBJECT_ID) = @TableName";
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if ((int)dr[0] == 1)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public Business.Rules.MethodResult CreateMySqlAutoIncrement(string tableName, string keyField)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            if (this.TableHasIdentityColumn(tableName) == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                string command = "ALTER TABLE " + tableName + " MODIFY `" + keyField + "` int(11) NOT NULL AUTO_INCREMENT; ";
                this.RunMySqlCommand(command);

                int value = this.GetAutoIncrementValue(tableName, keyField);
                command = "Alter Table `" + tableName + "` AUTO_INCREMENT = " + value.ToString();
                this.RunMySqlCommand(command);
            }
            return result;
        }

        private int GetAutoIncrementValue(string tableName, string keyField)
        {
            int result = 1;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select MAX(" + keyField + ") from " + tableName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result += (int)dr[0];
                    }
                }
            }
            return result;
        }

        public Business.Rules.MethodResult RemoveFromMySqlNoLongerInSqlServer(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int mismatchCount = migrationStatus.MySqlRowCount - migrationStatus.SqlServerTransferredCount;
            if (mismatchCount > 0)
            {
                bool needsTic = migrationStatus.KeyFieldProperty.PropertyType == typeof(string);
                List<string> mySqlKeys = this.GetMySqlKeyList(migrationStatus.TableName, migrationStatus.KeyFieldName, needsTic);
                List<string> sqlServerKeys = this.GetSqlServerKeyList(migrationStatus.TableName, migrationStatus.KeyFieldName, needsTic);

                List<string> deleteKeys = mySqlKeys.Except(sqlServerKeys).ToList();
                if (deleteKeys.Count > 0)
                {
                    string keyString = KeyStringFromList(migrationStatus, deleteKeys);

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE OPENQUERY(MYSQL, 'SELECT " + migrationStatus.KeyFieldName + " FROM " + m_DBName + "." + migrationStatus.TableName + " WHERE " + migrationStatus.KeyFieldName + " in (" + keyString + ")')";

                    using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return result;
        }

        private List<string> GetSqlServerKeyList(string tableName, string keyField, bool needsTic)
        {
            List<string> keys = new List<string>();
            StringBuilder selectStatement = new StringBuilder();
            selectStatement.Append("select ");
            selectStatement.Append(keyField);
            selectStatement.Append(" from ");
            selectStatement.Append(tableName);
            selectStatement.Append(" order by 1 desc;");
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = selectStatement.ToString();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (needsTic)
                        {
                            keys.Add("'" + dr[0].ToString() + "'");
                        }
                        else
                        {
                            keys.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return keys;
        }

        public Business.Rules.MethodResult InsertMySqlTransferredButMissing(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            bool needsTic = migrationStatus.KeyFieldProperty.PropertyType == typeof(string);
            List<string> mySqlKeys = this.GetMySqlKeyList(migrationStatus.TableName, migrationStatus.KeyFieldName, needsTic);
            List<string> sqlServerKeys = this.GetSqlServerKeyList(migrationStatus.TableName, migrationStatus.KeyFieldName, needsTic);

            List<string> deleteKeys = sqlServerKeys.Except(mySqlKeys).ToList();
            if (deleteKeys.Count > 0)
            {
                string keyString = KeyStringFromList(migrationStatus, deleteKeys);
                keyString = keyString.Replace("''", "'");
                result = this.LoadData(migrationStatus, keyString);
            }
            return result;
        }

        public Business.Rules.MethodResult CompareSSTableToMySqlTable(string tableName)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int count = 0;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "zCompareSSToMyTable";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBName", SqlDbType.VarChar).Value = this.m_DBName;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            count++;
                        }
                    }
                }
                catch (Exception e)
                {
                    result.Message = tableName + " - " + e.Message + Environment.NewLine;
                    result.Success = false;
                }
            }

            if (count > 0)
            {
                result.Message = tableName + " - " + count.ToString() + Environment.NewLine;
                result.Success = false;
            }
            return result;
        }
        public Business.Rules.MethodResult SyncSSToMyTable(string tableName, string keyField)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            List<string> ids = new List<string>();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "zSyncSSToMySqlTable";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBName", SqlDbType.VarChar).Value = this.m_DBName;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;
                cmd.Parameters.Add("@KeyField", SqlDbType.VarChar).Value = keyField;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    result.Success = false;
                }
            }

            return result;
        }

        public void CompareAccessionOrders(DateTime startDate, DateTime endDate)
        {
            List<string> masterAccessionNos = new List<string>();
            SqlCommand cmd = new SqlCommand("select MasteraccessionNo from tblAccessionOrder where AccessionDate between @StartDate and @EndDate order by 1 asc");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string s = dr[0].ToString();
                        masterAccessionNos.Add(s);
                    }
                }
            }

            foreach (string masterAccessionNo in masterAccessionNos)
            {
                Business.Test.AccessionOrder accessionOrderSS = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                Business.Test.AccessionOrder accessionOrderMy = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);

                Business.Persistence.DocumentTestBuilders document = new Business.Persistence.DocumentTestBuilders(accessionOrderSS, accessionOrderMy);
                if (document.Compare() == false)
                {
                    //resultString = ao1.MasterAccessionNo + ": ";
                    //resultString += "results are not the same.";
                }

            }
        }
    }
}
