using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;

namespace YellowstonePathology.Business
{
    public class SSHFileTransfer
    {        
        public void GetFileList()
        {
            var localPath = @"D:\Testing\test.xps";
            var keyFile = new PrivateKeyFile(@"C:\GIT\Sid\YPILIS\YellowstonePathology\ssh.key");
            var keyFiles = new[] { keyFile };
            var username = "Yellowstone";

            var methods = new List<AuthenticationMethod>();
            methods.Add(new PasswordAuthenticationMethod(username, "Y0g1B34r"));
            methods.Add(new PrivateKeyAuthenticationMethod(username, keyFiles));

            var con = new ConnectionInfo("ftp05.med3000.com", 22, username, methods.ToArray());
            using (var client = new SftpClient(con))
            {
                client.Connect();
                                                
                using (var fs = new FileStream(@"d:\testing\whatisup.tif", FileMode.Open))
                {                    
                    client.UploadFile(fs, "whatisup.tif");                    
                    fs.Close();
                }                

                client.Disconnect();
            }
        }
    }
}
