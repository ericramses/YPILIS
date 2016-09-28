using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace YellowstonePathology.Business.User
{
    public class SystemUserGateway
    {
		public static YellowstonePathology.Business.User.SystemUserCollection GetSystemUserCollection()
        {
            string xmlString = "<SystemUserCollection/>";
            YellowstonePathology.Business.User.SystemUserCollection systemUserCollection = DeserializeUserCollection<YellowstonePathology.Business.User.SystemUserCollection>(xmlString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select UserId, Active, UserName, FirstName, LastName, Initials, Signature, DisplayName, EmailAddress, NationalProviderId from tblSystemUser order by UserName " +
                "select * from tblSystemUserRole";
            cmd.CommandType = System.Data.CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.User.SystemUser systemUser = new SystemUser();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(systemUser, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        systemUserCollection.Add(systemUser);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.User.SystemUserRole systemUserRole = new SystemUserRole();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(systemUserRole, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (SystemUser systemUser in systemUserCollection)
                        {
                            if (systemUser.UserId == systemUserRole.UserID)
                            {
                                systemUser.SystemUserRoleCollection.Add(systemUserRole);
                                break;
                            }
                        }
                    }
                }
            }
            return systemUserCollection;
		}

        private static T DeserializeUserCollection<T>(String xmlString)
        {
            if (!string.IsNullOrEmpty(xmlString))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(StringToUTF8ByteArray(xmlString));
                System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
                T result = (T)xs.Deserialize(memoryStream);
                return result;
            }
            return default(T);
        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
    }
}
