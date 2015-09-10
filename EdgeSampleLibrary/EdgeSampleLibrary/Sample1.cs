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
            return await QueryUsers(payload);
        }

        public async Task<string> QueryUsers(IDictionary<string, object> payload)
        {            
            var connectionString = "Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";

            string reportNo = (string)payload["reportNo"];
            string testName = (string)payload["testName"];
            string overallInterpretation = (string)payload["overallInterpretation"];
            string sql = null;

            if (testName == "HPV")
            {
                HPVResult hpvResult = null;
                if (overallInterpretation == "Negative")
                {
                    hpvResult = new HPVNegativeResult();                
                }
                else if (overallInterpretation == "POSITIVE")
                {
                    hpvResult = new HPVPositiveResult();
                }

                sql = @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "' where ReportNo = '" + reportNo + "';";
                sql += @"Update tblPanelSetOrderHPVTWI set [Result] = '" + hpvResult.Result + "', "
                    + "[References] = '" + HPVResult.References + "', "
                    + "[TestInformation] = '" + HPVResult.TestInformation + "' "
                    + "where [ReportNo] = '" + reportNo + "';";
            }
            
            using (var cnx = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, cnx))
                {
                    await cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

                    /*
                    var users = new List<SampleUser>();

                    using (var cnx = new SqlConnection(connectionString))
                    {
                        using (var cmd = new SqlCommand(sql, cnx))
                        {
                            await cnx.OpenAsync();                    

                            using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                            {
                                while (await reader.ReadAsync())
                                {
                                    var user = new SampleUser
                                    {
                                        Id = reader.GetInt32(0),
                                        FirstName = reader.GetString(1),
                                        LastName = reader.GetString(2),
                                        Email = reader.GetString(3),
                                        CreateDate = reader.GetDateTime(4)
                                    };
                                    users.Add(user);
                                }
                            }
                        }
                    }
                    */

                    return connectionString;
        }
    }

    public class SampleUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
