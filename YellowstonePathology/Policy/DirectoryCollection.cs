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
