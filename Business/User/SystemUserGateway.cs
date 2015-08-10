using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.User
{
    public class SystemUserGateway
    {
		public static YellowstonePathology.Business.User.SystemUserCollection GetSystemUserCollection()
        {
			SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select su.UserId, su.Active, su.UserName, su.FirstName, su.LastName, su.Initials, su.Signature, su.DisplayName, su.EmailAddress, su.NationalProviderId, (select sr.* from tblSystemUserRole sr where sr.UserId = su.UserId " +
				"for xml Path('SystemUserRole'), type) [SystemUserRoleCollection] from tblSystemUser su order by su.UserName for xml Path('SystemUser'), root('SystemUserCollection')";
			cmd.CommandType = System.Data.CommandType.Text;
			YellowstonePathology.Business.User.SystemUserCollection systemUserCollection = YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteCollectionCommand<YellowstonePathology.Business.User.SystemUserCollection>(cmd, YellowstonePathology.Business.Domain.Persistence.DataLocationEnum.ProductionData);
			return systemUserCollection;
		}

		public static YellowstonePathology.Business.User.UserPreference GetUserPreference()
        {
			string hostName = Environment.MachineName;
			SqlCommand cmd = new SqlCommand("select * from tblUserPreference where HostName = @HostName");
            cmd.CommandType = System.Data.CommandType.Text;
			cmd.Parameters.Add("@HostName", System.Data.SqlDbType.VarChar).Value = hostName;
			YellowstonePathology.Business.User.UserPreference userPreference = null;            

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        userPreference = new UserPreference();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(userPreference, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            return userPreference;
        }
	}
}
