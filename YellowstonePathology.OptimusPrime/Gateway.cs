using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace YellowstonePathology.OptimusPrime
{
    public class Gateway
    {
        public async Task<object> Invoke(object input)
        {            
            var payload = (IDictionary<string, object>)input;            
            return await QueryUsers();
        }

        public async Task<List<PanelSetOrder>> QueryUsers()
        {
            var connectionString = Environment.GetEnvironmentVariable("EDGE_SQL_CONNECTION_STRING");
            if (connectionString == null)
            {
                throw new ArgumentException("You must set the EDGE_SQL_CONNECTION_STRING environment variable.");
            }            

            var sql = @"Select * from tblPanelSetOrder where OrderDate = '9/4/2015'";
            var users = new List<PanelSetOrder>();

            using (var cnx = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, cnx))
                {
                    await cnx.OpenAsync();                    

                    using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        while (await reader.ReadAsync())
                        {
                            var panelSetOrder = new PanelSetOrder
                            {
                                ObjectId = reader.GetString(0),
                                ReportNo = reader.GetString(1),
                                PanelSetId = reader.GetInt32(2),
                                PanelSetName = reader.GetString(3),
                                MasterAccessionNo = reader.GetString(4)
                            };
                            users.Add(panelSetOrder);
                        }
                    }
                }
            }
            return users;
        }

    }
}
