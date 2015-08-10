using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class LockGateway
	{
		public static void ReleaseUserLocks(YellowstonePathology.Business.User.SystemUser systemUser)
		{
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = cn;
				cmd.CommandText = "Delete tblLock where LockedBy = @UserName";
				cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = systemUser.UserName;
				cmd.CommandType = CommandType.Text;
				cmd.ExecuteNonQuery();
			}
		}

		public static void UpdateLock(YellowstonePathology.Business.Domain.KeyLock keyLock, YellowstonePathology.Business.User.SystemUser systemUser)
        {            
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Update tblLock set LockedBy = @UserName where KeyString = @KeyString");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = systemUser.UserName;
                cmd.Parameters.Add("@KeyString", SqlDbType.VarChar).Value = keyLock.Key;
                cmd.ExecuteNonQuery();                
            }
        }

		public static void ReleaseLock(YellowstonePathology.Business.Domain.KeyLock keyLock, YellowstonePathology.Business.User.SystemUser systemUser)
        {           
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Delete tblLock where LockedBy = @UserName and KeyString = @KeyString");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = systemUser.UserName;
                cmd.Parameters.Add("@KeyString", SqlDbType.VarChar).Value = keyLock.Key;
                cmd.ExecuteNonQuery();
            }            
        }

		public static void GetLock(YellowstonePathology.Business.Domain.KeyLock keyLock, YellowstonePathology.Business.User.SystemUser systemUser, YellowstonePathology.Business.Domain.Lock theLock)
		{			
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = cn;
				cmd.CommandText = "pSetLock";
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add("@KeyString", SqlDbType.VarChar).Value = keyLock.Key;
				cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = systemUser.UserName;

				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						theLock.LockDate = dr.GetValue(1) as Nullable<DateTime>;
						theLock.LockedBy = dr.GetValue(2) as string;                        
					}
				}
			}		
		}

		public static YellowstonePathology.Business.Domain.LockItemCollection GetLocks()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM tblLock order by LockDate for xml path('LockItem'), root('LockItemCollection')";
			cmd.CommandType = CommandType.Text;
			Business.Domain.LockItemCollection result = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteCollectionCommand<Business.Domain.LockItemCollection>(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
			return result;
		}
	}
}
