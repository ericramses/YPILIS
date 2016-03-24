using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs;

namespace YellowstonePathology.Business.nodejs
{
    public class HelloWorld
    {
        public static void DoIt()
        {
            Start().Wait();
        }

        public static async System.Threading.Tasks.Task Start()
        {
            var func = Edge.Func(@"
                return function(data, cb) {
                    cb(null,  data);
                };
            ");

            System.Windows.MessageBox.Show(func("Hello World").Result.ToString());
        }        
    }
}
