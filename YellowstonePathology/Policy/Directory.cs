using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Policy
{
    public class Directory
    {
        private int m_DirectoryId;
        private string m_DirectoryName;
        private int m_ParentId;
        private List<Directory> m_Subdirectories;

        private bool m_IsNew;
        private bool m_IsModified;

        public Directory()
        {
            this.m_Subdirectories = new List<Directory>();
        }

        public List<Directory> Subdirectories
        {
            get { return this.m_Subdirectories; }
        }

        public Directory(MySqlDataReader dr)
        {
            this.m_Subdirectories = new List<Directory>();
            this.m_DirectoryId = dr.GetInt32("DirectoryId");
            this.m_DirectoryName = dr.GetString("DirectoryName");
            this.m_ParentId = dr.GetInt32("ParentId");
        }

        public Directory(int directoryId, string directoryName, int parentId)
        {
            this.m_Subdirectories = new List<Directory>();
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
        
        public bool IsNew
        {
            get { return this.m_IsNew; }
            set { this.m_IsNew = value; }
        }

        public bool IsModified
        {
            get { return this.m_IsModified; }
            set { this.m_IsModified = value; }
        }
    }
}
