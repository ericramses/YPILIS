using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Globalization;

namespace YellowstonePathology.MySQLMigration
{
    public class DataRowRefreshComparer
    {
        DataTableReader m_SqlServerDataTableReader;
        DataTableReader m_MySqlDataTableReader;
        MigrationStatus m_MigrationStatus;
        Business.Rules.MethodResult m_MethodResult;
        string m_StatementStart;
        string m_StatementEnd;
        string m_Fields;
        string m_KeyFieldValue;
        static string m_FieldPlaceHolder = "ERRORCOLUMNNAME = ERRORCOLUMNVALUE, ";

        public DataRowRefreshComparer(MigrationStatus migrationStatus)
        {
            this.m_MigrationStatus = migrationStatus;
        }

        public Business.Rules.MethodResult Compare(DataTableReader sqlServerDataTableReader, DataTableReader mySqlDataTableReader)
        {
            this.m_SqlServerDataTableReader = sqlServerDataTableReader;
            this.m_MySqlDataTableReader = mySqlDataTableReader;
            this.m_MethodResult = new Business.Rules.MethodResult();
            this.m_StatementStart = "Update " + this.m_MigrationStatus.TableName + " set ";
            this.m_StatementEnd = " where " + this.m_MigrationStatus.KeyFieldName + " = KEYCOLUMNVALUE;";
            this.m_Fields = string.Empty;
            this.GetKeyValue();

            foreach (PropertyInfo property in this.m_MigrationStatus.PersistentProperties)
            {
                Type dataType = property.PropertyType;
                if (dataType == typeof(string))
                {
                    this.CompareString(property);
                }
                else if (dataType == typeof(int))
                {
                    this.CompareInt(property);
                }
                else if (dataType == typeof(double))
                {
                    this.CompareDouble(property);
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    this.CompareNullableInt(property);
                }
                else if (dataType == typeof(Nullable<float>))
                {
                    this.CompareNullableDouble(property);
                }
                else if (dataType == typeof(Nullable<double>))
                {
                    this.CompareNullableDouble(property);
                }
                else if (dataType == typeof(DateTime))
                {
                    this.CompareDateTime(property);
                }
                else if (dataType == typeof(bool))
                {
                    this.CompareBoolean(property);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    this.CompareNullableBoolean(property);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    this.CompareNullableDateTime(property);
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }
            if (this.m_MethodResult.Success == false)
            {
                this.SetErrorMessage();
            }
            return this.m_MethodResult;
        }

        private void CompareString(PropertyInfo property)
        {
            string sqlValue = null;
            string myValue = null;
            bool compares = true;

            if (this.m_SqlServerDataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = this.m_SqlServerDataTableReader[property.Name].ToString();
            }
            if (this.m_MySqlDataTableReader[property.Name] != DBNull.Value)
            {
                myValue = this.m_MySqlDataTableReader[property.Name].ToString();
            }

            if ((sqlValue == null && myValue != null) || (sqlValue != null && myValue == null))
            {
                compares = false;
            }
            else if (sqlValue == null && myValue == null)
            {
                compares = true;
            }
            else if (sqlValue != myValue)
            {
                sqlValue = sqlValue.Normalize();
                myValue = myValue.Normalize();
                /*if (sqlValue != myValue)
                {
                    myValue = Encoding.UTF8.GetString(Encoding.GetEncoding(1252).GetBytes(myValue));
                    sqlValue = Encoding.UTF8.GetString(Encoding.GetEncoding("utf-8").GetBytes(sqlValue));

                    if (sqlValue != myValue)
                    {*/
                        compares = false;
                    //}
                //}
            }

            if (compares == false)
            {
                string value = string.Empty;
                if (myValue == null)
                {
                    value = "null";
                }
                else
                {
                    value = "'" + myValue.Replace("'", "''") + "'";
                    value = value.Replace("\\", "\\\\");
                }
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareInt(PropertyInfo property)
        {
            int sqlValue = Convert.ToInt32(this.m_SqlServerDataTableReader[property.Name].ToString());
            int myValue = Convert.ToInt32(this.m_MySqlDataTableReader[property.Name].ToString());

            if (property.Name == this.m_MigrationStatus.KeyFieldName)
            {
                this.m_KeyFieldValue = myValue.ToString();
            }

            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, myValue.ToString());
            }
        }

        private void CompareDouble(PropertyInfo property)
        {
            double sqlValue = Convert.ToDouble(this.m_SqlServerDataTableReader[property.Name].ToString());
            double myValue = Convert.ToDouble(this.m_MySqlDataTableReader[property.Name].ToString());
            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, myValue.ToString());
            }
        }

        private void CompareNullableInt(PropertyInfo property)
        {
            Nullable<int> sqlValue = null;
            Nullable<int> myValue = null;
            bool compares = true;

            if (this.m_SqlServerDataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToInt32(this.m_SqlServerDataTableReader[property.Name].ToString());
            }
            if (this.m_MySqlDataTableReader[property.Name] != DBNull.Value)
            {
                myValue = Convert.ToInt32(this.m_MySqlDataTableReader[property.Name].ToString());
            }
            if ((sqlValue.HasValue && myValue.HasValue == false) || (sqlValue.HasValue == false && myValue.HasValue))
            {
                compares = false;
            }
            else if (sqlValue.HasValue == false && myValue.HasValue == false)
            {
                compares = true;
            }
            else if (myValue.Value != myValue.Value)
            {
                compares = false;
            }
            if (compares == false)
            {
                string value = myValue == null ? "null" : myValue.ToString();
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareNullableDouble(PropertyInfo property)
        {
            Nullable<double> sqlValue = null;
            Nullable<double> myValue = null;
            bool compares = true;

            if (this.m_SqlServerDataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToDouble(this.m_SqlServerDataTableReader[property.Name].ToString());
            }
            if (this.m_MySqlDataTableReader[property.Name] != DBNull.Value)
            {
                myValue = Convert.ToDouble(this.m_MySqlDataTableReader[property.Name].ToString());
            }
            if ((sqlValue.HasValue && myValue.HasValue == false) || (sqlValue.HasValue == false && myValue.HasValue))
            {
                compares = false;
            }
            else if (sqlValue.HasValue == false && myValue.HasValue == false)
            {
                compares = true;
            }
            else if (sqlValue.Value != myValue.Value)
            {
                compares = false;
            }
            if (compares == false)
            {
                string value = myValue == null ? "null" : myValue.ToString();
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareDateTime(PropertyInfo property)
        {
            DateTime sqlValue = (DateTime)this.m_SqlServerDataTableReader[property.Name];
            DateTime myValue = (DateTime)this.m_MySqlDataTableReader[property.Name];
            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, myValue.ToString());
            }
        }

        private void CompareBoolean(PropertyInfo property)
        {
            bool sqlValue = (Boolean)this.m_SqlServerDataTableReader[property.Name];
            bool myValue = (Boolean)this.m_MySqlDataTableReader[property.Name];
            if (sqlValue != myValue)
            {
                string value = myValue == true ? "1" : "0";
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareNullableBoolean(PropertyInfo property)
        {
            Nullable<bool> sqlValue = null;
            Nullable<bool> myValue = null;
            bool compares = true;

            if (this.m_SqlServerDataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = (Boolean)this.m_SqlServerDataTableReader[property.Name];
            }
            if (this.m_MySqlDataTableReader[property.Name] != DBNull.Value)
            {
                myValue = (Boolean)this.m_MySqlDataTableReader[property.Name];
            }
            if ((sqlValue.HasValue && myValue.HasValue == false) || (sqlValue.HasValue == false && myValue.HasValue))
            {
                compares = false;
            }
            else if (sqlValue.HasValue == false && myValue.HasValue == false)
            {
                compares = true;
            }
            else if (sqlValue.Value != myValue.Value)
            {
                compares = false;
            }
            if (compares == false)
            {
                string value = string.Empty;
                if(myValue.HasValue == false)
                {
                    value = "null";
                }
                else
                {
                    value = myValue == true ? "1" : "0";
                }
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareNullableDateTime(PropertyInfo property)
        {
            Nullable<DateTime> sqlValue = null;
            Nullable<DateTime> myValue = null;
            bool compares = true;

            if (this.m_SqlServerDataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = (DateTime)this.m_SqlServerDataTableReader[property.Name];
            }
            if (this.m_SqlServerDataTableReader[property.Name] != DBNull.Value)
            {
                myValue = (DateTime)this.m_SqlServerDataTableReader[property.Name];
            }
            if ((sqlValue.HasValue && myValue.HasValue == false) || (sqlValue.HasValue == false && myValue.HasValue))
            {
                compares = false;
            }
            else if (sqlValue.HasValue == false && myValue.HasValue == false)
            {
                compares = true;
            }
            else if (sqlValue.Value != myValue.Value)
            {
                compares = false;
            }
            if (compares == false)
            {
                string value = myValue == null ? "null" : myValue.ToString();
                this.SetFieldValues(property.Name, value);
            }
        }

        private void SetFieldValues(string errorColumnName, string errorColumnValue)
        {
            string errorField = m_FieldPlaceHolder.Replace("ERRORCOLUMNNAME", errorColumnName);
            errorField = errorField.Replace("ERRORCOLUMNVALUE", errorColumnValue);
            this.m_Fields += errorField;

            this.m_MethodResult.Success = false;
        }

        private void SetErrorMessage()
        {
            this.m_Fields = this.m_Fields.Substring(0, this.m_Fields.Length - 2);
            this.m_StatementEnd = this.m_StatementEnd.Replace("KEYCOLUMNVALUE", this.m_KeyFieldValue);
            this.m_MethodResult.Message = this.m_StatementStart + this.m_Fields + this.m_StatementEnd;
        }

        private void GetKeyValue()
        {
            this.m_KeyFieldValue = this.m_MySqlDataTableReader[this.m_MigrationStatus.KeyFieldName].ToString();
            if (this.m_MigrationStatus.KeyFieldProperty.PropertyType == typeof(string))
            {
                this.m_KeyFieldValue = "'" + this.m_KeyFieldValue + "'";
            }
        }
    }
}
