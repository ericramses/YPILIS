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
                    sql = @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "
                    + "[HoldDistribution] = 1, "
                    + "[Accepted] = 1, "
                    + "[AcceptedBy] = 'Optimus Prime', "
                    + "[AcceptedById] = 5134, "
                    + "[AcceptedDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
                    + "[AcceptedTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "', "
                    + "[Final] = 1, "
                    + "[Signature] = 'Optimus Prime', "
                    + "[FinaledById] = 5134, "
                    + "[FinalDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
                    + "[FinalTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "' "
                    + "where Accepted = 0 and ReportNo = '" + reportNo + "';";
                }
                else if (overallInterpretation == "POSITIVE")
                {
                    hpvResult = new HPVPositiveResult();
                    sql = @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "
                    + "[HoldDistribution] = 1, "
                    + "[Accepted] = 1, "
                    + "[AcceptedBy] = 'Optimus Prime', "
                    + "[AcceptedById] = 5134, "
                    + "[AcceptedDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
                    + "[AcceptedTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "' "                    
                    + "where Accepted = 0 and ReportNo = '" + reportNo + "';";
                }                                

                sql += @"Update tblPanelSetOrderHPVTWI set[Result] = '" + hpvResult.Result + "' "
                        + "from tblPanelSetOrderHPVTWI psoh, tblPanelSetOrder pso "
                        + "where psoh.ReportNo = pso.ReportNo "
                        + "and psoh.ReportNo = '" + reportNo + "' and pso.Accepted = 0";

            }

            using (var cnx = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, cnx))
                {
                    await cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
                    
            return "Result Update.";
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
