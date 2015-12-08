using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public static class JSONIndenter
    {
        static int m_Depth  = 1;

        public static void Indent(StringBuilder stringToIndent)
		{
            for (int idx = 0; idx < m_Depth; idx++)
            {
                stringToIndent.Append("\t");
            }
		}

        public static int IndentDepth
        {
            get { return m_Depth; }
            set { m_Depth = value; }
        }
    }
}
