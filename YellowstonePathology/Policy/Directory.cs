using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Policy
{
    public class Directory
    {        
        private string m_DirectoryName;        
        private List<Directory> m_Subdirectories;

        private bool m_IsNew;
        private bool m_IsModified;

        public Directory(string dirName)
        {
            this.m_DirectoryName = dirName;
            this.m_Subdirectories = new List<Directory>();
        }

        public static async Task<Directory> Build(string rootDirName)
        {
            Directory root = new Directory(rootDirName);
            JObject result = await IPFS.FilesLs(rootDirName);
            return root;
        }

        public List<Directory> Subdirectories
        {
            get { return this.m_Subdirectories; }
        }        

        public string DirectoryName
        {
            get { return this.m_DirectoryName; }
            set { this.m_DirectoryName = value; }
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
