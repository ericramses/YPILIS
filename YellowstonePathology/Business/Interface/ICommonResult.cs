using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Interface
{
    public interface ICommonResult
    {
        string Result { get; set; }
        //string ResultCode { get; set; }
        string Interpreation { get; set; }
        string Method { get; set; }
        string ReportReferences { get; set; }
        string ReportDisclaimer { get; set; }
    }
}
