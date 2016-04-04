using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence
{
    public class CrudOperations
    {
		public static T ExecuteCollectionCommand<T>(SqlCommand sqlCommand, DataLocationEnum dataLocation)
        {         
			string connectionString = GetSqlConnectionString(dataLocation);
            StringBuilder xmlString = new StringBuilder();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;                
                using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
                {
                    while (xmlReader.Read())
                    {
                        xmlString.Append(xmlReader.ReadOuterXml());
                    }
                }                
            }
            return SerializationHelper.DeserializeCollection<T>(xmlString.ToString());
        }

        public static T ExecuteCommand<T>(SqlCommand sqlCommand, DataLocationEnum dataLocation)
        {
            string connectionString = GetSqlConnectionString(dataLocation);
            StringBuilder xmlString = new StringBuilder();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;                
                using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
                {
                    while (xmlReader.Read())
                    {
                        xmlString.Append(xmlReader.ReadOuterXml());
                    }
                }                
            }
            return SerializationHelper.DeserializeCollection<T>(xmlString.ToString());
        }

        public static XElement ExecuteCommand(SqlCommand sqlCommand, DataLocationEnum dataLocation)
        {
            string connectionString = GetSqlConnectionString(dataLocation);
            StringBuilder xmlString = new StringBuilder();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;
                using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
                {
                    while (xmlReader.Read())
                    {
                        xmlString.Append(xmlReader.ReadOuterXml());
                    }
                }
            }

            XElement result = null;
            if (xmlString.Length != 0)
            {
                result = XElement.Parse(xmlString.ToString());
            }
            return result;
        }        

		public static List<T> ExecuteListCommand<T>(SqlCommand sqlCommand, DataLocationEnum dataLocation)
        {
            List<T> itemList = new List<T>();
			string connectionString = GetSqlConnectionString(dataLocation);
			using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;                
                
                using (SqlDataReader dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string xmlString = dr.GetSqlXml(1).Value.ToString();
                        T item = YellowstonePathology.Business.Domain.Persistence.SerializationHelper.DeserializeItem<T>(xmlString);
                        itemList.Add(item);
                    }
                }
                return itemList;
            }
        }

        public static T ExecuteXmlCommand<T>(SqlCommand sqlCommand, DataLocationEnum dataLocation)
        {            
            string connectionString = GetSqlConnectionString(dataLocation);
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;

                System.Xml.XmlReader xmlReader = sqlCommand.ExecuteXmlReader();                                        
                T item = YellowstonePathology.Business.Domain.Persistence.SerializationHelper.DeserializeItem<T>(xmlReader);
                xmlReader.Close();         
                
                return item;
            }
        }

		public static T ExecuteSingleRowsetCommand<T>(SqlCommand sqlCommand, DataLocationEnum dataLocation)
		{
			string xmlString = string.Empty;
			string connectionString = GetSqlConnectionString(dataLocation);
			using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;                
                
                using (SqlDataReader dr = sqlCommand.ExecuteReader())
                {
					while (dr.Read())
                    {
                        xmlString = dr.GetSqlXml(1).Value.ToString();
                    }
                }
               return YellowstonePathology.Business.Domain.Persistence.SerializationHelper.DeserializeItem<T>(xmlString);               
            }
		}

		public static void ExecuteXmlReaderCommand(SqlCommand sqlCommand, DataLocationEnum dataLocation, IBuilder builder)
		{			
			string connectionString = GetSqlConnectionString(dataLocation);
			using (SqlConnection cn = new SqlConnection(connectionString))
			{
				cn.Open();
				sqlCommand.Connection = cn;
				using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        XElement element = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
                        builder.Build(element);
                    }
                    else
                    {
                        builder.Build(null);
                    }
                }
			}
		}

        public static XElement ExecuteXmlReaderCommand(SqlCommand sqlCommand, DataLocationEnum dataLocation)
        {
            XElement result = null;
            string connectionString = GetSqlConnectionString(dataLocation);
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;
                using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        result = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);                        
                    }                    
                }
            }
            return result;
        }

		public static string ExecuteXmlStringCommand(SqlCommand sqlCommand, DataLocationEnum dataLocation)
		{
			StringBuilder xmlString = new StringBuilder();
			string connectionString = GetSqlConnectionString(dataLocation);
			using (SqlConnection cn = new SqlConnection(connectionString))
			{
				cn.Open();
				sqlCommand.Connection = cn;
				using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
				{
					while (xmlReader.Read())
					{
						xmlString.Append(xmlReader.ReadOuterXml());
					}
				}
			}
			return xmlString.ToString();
		}

		protected static string GetSqlConnectionString(DataLocationEnum dataLocation)
		{
			string connectionString = string.Empty;
			switch (dataLocation)
			{
				case DataLocationEnum.ProductionData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.SqlConnectionString;
					break;
				case DataLocationEnum.LocalData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.LocalSqlConnectionString;
					break;
				case DataLocationEnum.TestData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.TestSqlConnectionString;
					break;
				case DataLocationEnum.CubeData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.CubeSqlConnectionString;
					break;
			}
			return connectionString;
		}

		protected static string GetSqlXmlConnectionString(DataLocationEnum dataLocation)
		{
			string connectionString = string.Empty;
			switch (dataLocation)
			{
				case DataLocationEnum.ProductionData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.SqlXmlConnectionString;
					break;
				case DataLocationEnum.LocalData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.LocalSqlXmlConnectionString;
					break;
				case DataLocationEnum.TestData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.TestSqlXmlConnectionString;
					break;
				case DataLocationEnum.CubeData:
					connectionString = SqlXmlPersistence.Properties.Settings.Default.CubeSqlXmlConnectionString;
					break;
			}
			return connectionString;
		}
	}
}
