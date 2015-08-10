using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientWebServices
{
    public class FileHelper
    {
        public static string GetCasePath(string strReportNo)
        {
            string strPath = @"\\CFileServer\documents";
            string strCaseType = strReportNo.Substring(0, 1);
            switch (strCaseType)
            {
                case "A":
                    strPath += @"\Autopsy";
                    break;
                case "S":
                    strPath += @"\surgical";
                    break;
                case "P":
                    strPath += @"\cytology";
                    break;
                case "B":
                    strPath += @"\bozemandeaconesscytologyscreening";
                    break;
                case "F":
                    switch (strReportNo.Substring(0, 2))
                    {
                        case "FM":
                            strPath += @"\ForensicKHM";
                            break;
                        case "FB":
                            strPath += @"\ForensicTLB";
                            break;
                        default:
                            strPath += @"\flowcytometry";
                            break;
                    }
                    break;
                case "M":
                    if (strReportNo.Substring(0, 2) == "ME")
                    {
                        strPath += @"\AutopsyMueller";
                    }
                    else
                    {
                        strPath += @"\MolecularTesting";
                    }
                    break;
                case "T":
                    strPath += @"\TechnicalOnly";
                    break;
            }
            int dashPosition = strReportNo.IndexOf('-');
            string strYear = strReportNo.Substring(dashPosition - 2, 2);
            switch (strYear)
            {
                case "99":
                    strPath += @"\19" + strYear;
                    break;
                default:
                    strPath += @"\20" + strYear;
                    break;
            }
            string[] splitAtDash = strReportNo.Split('-');
            string number = splitAtDash[1];
            int length = number.Length;

            switch (length)
            {
                case 5:
                    strPath += @"\" + number.Substring(0, 2) + "000-" + number.Substring(0, 2) + "999";
                    break;
                case 4:
                    strPath += @"\0" + number.Substring(0, 1) + "000-0" + number.Substring(0, 1) + "999";
                    break;
                default:
                    strPath += @"\00001-00999";
                    break;
            }
            strPath += @"\" + strReportNo + @"\";
            return strPath;
        }
    }
}
