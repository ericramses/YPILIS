using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public static class JSONIndenter
    {        
        public static void Indent(StringBuilder stringToIndent)
		{
        	stringToIndent = stringToIndent.Append("\t");
		}

		public static void Exdent(StringBuilder stringToExdent)
		{
		    //stringToExdent = stringToExdent.Remove(0, "\t");
		}
    }
}
