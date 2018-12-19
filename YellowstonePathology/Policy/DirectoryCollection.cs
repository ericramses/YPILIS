using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Policy
{
    public class DirectoryCollection : ObservableCollection<Directory>
    {
        public static DirectoryCollection GetRoot()
        {
            List<Directory> flatList = GetFlat("Select * from directory");
            return MakeTree(flatList);
        }

        private static DirectoryCollection MakeTree(List<Directory> flatList)
        {
            DirectoryCollection result = new DirectoryCollection();
            foreach(Directory directory in flatList)
            {
                if(directory.ParentId == 0)
                {
                    result.Add(directory);
                }
                else
                {
                    Directory parent = FindParent(flatList, directory);
                    parent.Subdirectories.Add(directory);
                }
            }
            return result;
        }       
        
        private static Directory FindParent(List<Directory> flatList, Directory child)
        {
            Directory result = null;
            foreach(Directory directory in flatList)
            {
                if(directory.DirectoryId == child.ParentId)
                {
                    result = directory;
                    break;
                }
            }
            return result;
        }    

        private static List<Directory> GetFlat(string sql)
        {
            List<Directory> result = new List<Directory>();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = System.Data.CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection("Server = 10.1.2.26; Uid = sqldude; Pwd = 123Whatsup; Database = policy; Pooling = True;"))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(new Directory(dr));
                    }
                }
            }

            return result;
        }
    }
}
