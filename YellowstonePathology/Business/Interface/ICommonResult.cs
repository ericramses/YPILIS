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
        string Interpretation { get; set; }
        string Method { get; set; }        
        string ReportDisclaimer { get; set; }
    }
}
