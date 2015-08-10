using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ClientWebServices
{
    public static class UriExtension
    {
        public static string SetDisplayName(this Uri uri)
        {
            string result = string.Empty;
            int startIndex = uri.AbsolutePath.LastIndexOf('/') + 1;
            int length = uri.AbsolutePath.Length - uri.AbsolutePath.LastIndexOf('/') - 1;
            result = uri.AbsolutePath.Substring(startIndex, length);
            return result.Replace("%20", " ");            
        }
    }
}
