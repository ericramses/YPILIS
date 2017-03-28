using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace YellowstonePathology.MySQLMigration
{
    public class SqlServerFromMySqlRefresher
    {
         public static string SqlServerConnectionString = "Data Source = TestSQL; Initial Catalog = YPIData_FromMySql; Integrated Security = True;";

        public SqlServerFromMySqlRefresher()
        {

        }

        public void GetRowCounts(NonpersistentTableDef nonpersistentTableDef)
        {
            nonpersistentTableDef.SqlServerRowCount = this.GetSqlServerRowCount(nonpersistentTableDef.TableName);
            nonpersistentTableDef.MySqlRowCount = this.GetMySqlRowCount(nonpersistentTableDef.TableName);
        }
        public void GetRowCounts(MigrationStatus migrationStatus)
        {
            migrationStatus.SqlServerTransferredCount = this.GetSqlServerRowCount(migrationStatus.TableName);
            migrationStatus.MySqlRowCount = this.GetMySqlRowCount(migrationStatus.TableName);
        }

        public Business.Rules.MethodResult CompareTables(MigrationStatus migrationStatus)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();
            List<string> matchLike = this.GetMatchList(migrationStatus.TableName, migrationStatus.KeyFieldName);

            foreach (string match in matchLike)
            {
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
                            {
                                File.AppendAllText("C:/TEMP/NotMatched.txt", cmd + Environment.NewLine + "-- ----------------" + Environment.NewLine + Environment.NewLine);
                            }
                        }
                    }
                }
            }
            return overallResult;
        }

        public Business.Rules.MethodResult GetKeysForInsert(string tableName)
        {
            Business.Rules.MethodResult overallResult = new Business.Rules.MethodResult();

            return overallResult;
        }
    

        private List<string> GetMatchList(string tableName, string keyField)
        {
            List<string> result = new List<string>();

            int cnt = this.GetMySqlRowCount(tableName);
            if (cnt < 200000) result.Add("'%'");
            else
            {
                if (keyField == "MasterAccessionNo")
                {
                    result.Add("'19%'"); result.Add("'50%'"); result.Add("'2000%'"); result.Add("'2001%'"); result.Add("'2002%'");
                    result.Add("'2003%'"); result.Add("'2004%'"); result.Add("'2005%'"); result.Add("'2006%'"); result.Add("'2007%'");
                    result.Add("'2008%'"); result.Add("'2009%'"); result.Add("'2010%'"); result.Add("'2011%'"); result.Add("'2012%'");
                    result.Add("'2013%'"); result.Add("'13-%'"); ; result.Add("'14-%'"); result.Add("'15-%'"); result.Add("'16-%'");
                    result.Add("'17-%'");
                }
                else if (keyField == "ReportNo")
                {
                    result.Add("'_99-%'"); result.Add("'_00-%'"); result.Add("'_01-%'"); result.Add("'_02-%'"); result.Add("'_03-%'");
                    result.Add("'_04-%'"); result.Add("'_05-%'"); result.Add("'_06-%'"); result.Add("'_07-%'"); result.Add("'_08-%'");
                    result.Add("'_09-%'"); result.Add("'_10-%'"); result.Add("'_11-%'"); result.Add("'_12-%'"); result.Add("'_13-%'");
                    result.Add("'_14-%'"); result.Add("'14-%'"); result.Add("'_15-%'"); result.Add("'15-%'"); result.Add("'_16-%'");
                    result.Add("'16-%'"); result.Add("'_17-%'"); result.Add("'17-%'");
                }
                else
                {
                    result.Add("'0%'"); result.Add("'1%'"); result.Add("'2%'"); result.Add("'3%'"); result.Add("'4%'");
                    result.Add("'5%'"); result.Add("'6%'"); result.Add("'7%'"); result.Add("'8%'"); result.Add("'9%'");
                    result.Add("'a%'"); result.Add("'b%'"); result.Add("'c%'"); result.Add("'d%'"); result.Add("'e%'");
                    result.Add("'f%'");
                }
            }
            return result;
        }

        private List<string> GetCompareDataKeyList(string tableName, string keyField, string compareString)
        {
            List<string> result = new List<string>();
            MySqlCommand cmd = new MySqlCommand("Select `" + keyField + "` from lis.`" + tableName + "` where `" + keyField + "` like " + compareString);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.MySqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
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

                if (sqlServerDataTable.Rows.Count == 0)
                {
                    overallResult.Success = false;
                    overallResult.Message += "Missing " + migrationStatus.TableName + " - " + migrationStatus.KeyFieldName + " = " + keyString;
                    this.SaveError(migrationStatus.TableName, "Update " + migrationStatus.TableName + " set Transferred = 0 where " + migrationStatus.KeyFieldName + " = " + keyString);
                    continue;
                }

                if (sqlServerDataTable.Rows[0].ItemArray.SequenceEqual(mySqlDataTable.Rows[0].ItemArray) == false)
                {
                    DataRowRefreshComparer dataRowRefreshComparer = new DataRowRefreshComparer(migrationStatus);
                    DataTableReader sqlServerDataTableReader = new DataTableReader(sqlServerDataTable);
                    sqlServerDataTableReader.Read();
                    DataTableReader mySqlDataTableReader = new DataTableReader(mySqlDataTable);
                    mySqlDataTableReader.Read();
                    Business.Rules.MethodResult result = dataRowRefreshComparer.Compare(sqlServerDataTableReader, mySqlDataTableReader);
                    if (result.Success == false)
                    {
                        updateCommands.Add(result.Message);
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
                Business.Rules.MethodResult cmdResult = this.RunSqlServerCommand(cmdText);
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

        private DataTable GetSqlServerData(string selectStatement, string keyValue)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;
            DataTable result = new DataTable();
            dataSet.Tables.Add(result);
            SqlCommand cmd = new SqlCommand(selectStatement + keyValue);
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(SqlServerConnectionString))
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
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.MySqlConnectionString))
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

        private Business.Rules.MethodResult RunSqlServerCommand(string command)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            using (SqlConnection cn = new SqlConnection(SqlServerConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = command;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    methodResult.Message += "SQLServer error executing " + command;
                    methodResult.Success = false;
                }
            }
            return methodResult;
        }

        private void SaveError(string tableName, string errorCommand)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Insert tblMySqlError(TableName, ErrorCommand) values(@TableName, @ErrorCommand)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;
            cmd.Parameters.Add("@ErrorCommand", SqlDbType.VarChar).Value = errorCommand;

            using (SqlConnection cn = new SqlConnection(SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        private int GetSqlServerRowCount(string tableName)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("Select count(*) from " + tableName + ";");
            using (SqlConnection cn = new SqlConnection(SqlServerConnectionString))
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
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.MySqlConnectionString))
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
    }
}
