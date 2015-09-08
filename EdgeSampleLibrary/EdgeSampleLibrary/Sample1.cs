using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EdgeSampleLibrary
{
    public class Sample1
    {
        public async Task<object> Invoke(object input)
        {
            // Edge marshalls data to .NET using an IDictionary<string, object>
            var payload = (IDictionary<string, object>)input;
            var accessionDate = (DateTime)payload["accessionDate"];
            var testing = (int)payload["testing"];
            return await QueryUsers(accessionDate);
        }

        public async Task<List<PanelSetOrder>> QueryUsers(DateTime accessionDate)
        {            
            var connectionString = Environment.GetEnvironmentVariable("EDGE_SQL_CONNECTION_STRING");
            if (connectionString == null)
                throw new ArgumentException("You must set the EDGE_SQL_CONNECTION_STRING environment variable.");
                        
            var sql = @"Select PanelSetId, ReportNo, PanelSetName, FinalDate from tblPanelSetOrder where OrderDate = '" + accessionDate.ToShortDateString() + "'";
            var panelSetOrders = new List<PanelSetOrder>();

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
                                PanelSetId = reader.GetInt32(0),
                                ReportNo = reader.GetString(1),
                                PanelSetName = reader.GetString(2),
                                FinalDate = reader.GetDateTime(3)                       
                            };
                            panelSetOrders.Add(panelSetOrder);
                        }
                    }
                }
            }
            return panelSetOrders;
        }
    }

    public class PanelSetOrder
    {
        public int PanelSetId { get; set; }
        public string PanelSetName { get; set; }
        public string ReportNo { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
