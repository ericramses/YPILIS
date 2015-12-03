using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONIndenter
    {
        private static string IndentString = "\t";
        private string m_IndentationString;

        public JSONIndenter()
        {
            this.m_IndentationString = string.Empty;
        }

        public string IndentationString
        {
            get { return this.m_IndentationString; }
        }

        public void Indent()
        {
            this.m_IndentationString += IndentString;
        }

        public void Exdent()
        {
            this.m_IndentationString = this.m_IndentationString.Remove(0, IndentString.Length);
        }
    }
}
