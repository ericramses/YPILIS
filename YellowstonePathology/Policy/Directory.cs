using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Policy
{
    public class Directory
    {
        private int m_DirectoryId;
        private string m_DirectoryName;
        private int m_ParentId;

        public Directory()
        {

        }

        public Directory(int directoryId, string directoryName, int parentId)
        {
            this.m_DirectoryId = directoryId;
            this.m_DirectoryName = directoryName;
            this.m_ParentId = parentId;
        }

        public int DirectoryId
        {
            get { return this.m_DirectoryId; }
            set { this.m_DirectoryId = value; }
        }        

        public string DirectoryName
        {
            get { return this.m_DirectoryName; }
            set { this.m_DirectoryName = value; }
        }        

        public int ParentId
        {
            get { return this.m_ParentId; }
            set { this.m_ParentId = value; }
        }
    }
}
