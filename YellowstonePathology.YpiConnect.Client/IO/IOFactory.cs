using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.IO
{
    public class IOFactory
    {
        public static IFileIO GetIOHandler()
        {
            IFileIO handler = null;            
            handler = new IO.IsolatedStorageIO();            
            return handler;
        }
    }
}
