using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace YellowstonePathology.MySQLMigration
{
    public class DataRowComparer
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

        public DataRowComparer(MigrationStatus migrationStatus)
        {
            this.m_MigrationStatus = migrationStatus;
        }

        public Business.Rules.MethodResult Compare(DataTableReader sqlServerDataTableReader, DataTableReader mySqlDataTableReader)
        {
            this.m_SqlServerDataTableReader = sqlServerDataTableReader;
            this.m_MySqlDataTableReader = mySqlDataTableReader;
            this.m_MethodResult = new Business.Rules.MethodResult();
            this.m_StatementStart = "Update " + this.m_MigrationStatus.TableName + " set ";
            this.m_StatementEnd = " where " + this.m_MigrationStatus.KeyFieldName + " = KEYCOLUMNVALUE;ENDR";
            this.m_Fields = string.Empty;
            
            foreach (PropertyInfo property in this.m_MigrationStatus.PersistentProperties)
            {
                if (this.SqlServerColumnExists(property.Name) && this.MySqlColumnExists(property.Name))
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
                else if((this.SqlServerColumnExists(property.Name) == false && this.MySqlColumnExists(property.Name)) ||
                    (this.SqlServerColumnExists(property.Name) && this.MySqlColumnExists(property.Name) == false))
                {
                    this.m_MethodResult.Success = false;
                    this.m_MethodResult.Message += property.Name + " Differs";
                }
            }
            if(this.m_MethodResult.Success == false)
            {
                this.SetErrorMessage();
            }
            return this.m_MethodResult;
        }

        private bool SqlServerColumnExists(string name)
        {
            try
            {
                int a = this.m_SqlServerDataTableReader.GetOrdinal(name);
                a = this.m_MySqlDataTableReader.GetOrdinal(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool MySqlColumnExists(string name)
        {
            try
            {
                int a = this.m_MySqlDataTableReader.GetOrdinal(name);
                return true;
            }
            catch
            {
                return false;
            }
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

            if (property.Name == this.m_MigrationStatus.KeyFieldName)
            {
                this.m_KeyFieldValue = "'" + sqlValue + "'";
            }

            if ((sqlValue == null && myValue != null) || (sqlValue != null && myValue == null))
            {
                compares = false;
            }
            else if(sqlValue == null && myValue == null)
            {
                compares = true;
            }
            else if(sqlValue != myValue)
            {
                compares = false;
            }

            if (compares == false)
            {
                string value = sqlValue == null ? "null" : "'" + sqlValue + "'";
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareInt(PropertyInfo property)
        {
            int sqlValue = Convert.ToInt32(this.m_SqlServerDataTableReader[property.Name].ToString());
            int myValue = Convert.ToInt32(this.m_MySqlDataTableReader[property.Name].ToString());

            if (property.Name == this.m_MigrationStatus.KeyFieldName)
            {
                this.m_KeyFieldValue = sqlValue.ToString();
            }

            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, sqlValue.ToString());
            }
        }

        private void CompareDouble(PropertyInfo property)
        {
            double sqlValue = Convert.ToDouble(this.m_SqlServerDataTableReader[property.Name].ToString());
            double myValue = Convert.ToDouble(this.m_MySqlDataTableReader[property.Name].ToString());
            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, sqlValue.ToString());
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
            else if (sqlValue.Value != myValue.Value)
            {
                compares = false;
            }
            if (compares == false)
            {
                string value = sqlValue == null ? "null" : sqlValue.ToString();
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
                string value = sqlValue == null ? "null" : sqlValue.ToString();
                this.SetFieldValues(property.Name, value);
            }
        }

        private void CompareDateTime(PropertyInfo property)
        {
            DateTime sqlValue = (DateTime)this.m_SqlServerDataTableReader[property.Name];
            DateTime myValue = (DateTime)this.m_MySqlDataTableReader[property.Name];
            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, sqlValue.ToString());
            }
        }

        private void CompareBoolean(PropertyInfo property)
        {
            bool sqlValue = (Boolean)this.m_SqlServerDataTableReader[property.Name];
            bool myValue = (Boolean)this.m_MySqlDataTableReader[property.Name];
            if (sqlValue != myValue)
            {
                this.SetFieldValues(property.Name, sqlValue.ToString());
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
                string value = sqlValue == null ? "null" : sqlValue.ToString();
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
                string value = sqlValue == null ? "null" : sqlValue.ToString();
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
    }
}
