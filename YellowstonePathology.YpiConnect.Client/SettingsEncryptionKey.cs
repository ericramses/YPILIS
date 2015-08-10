using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client
{
    public class SettingsEncryptionKey
    {
        public static byte[] GetKey()
        {
            Byte [] encryptionKey = new Byte[24];
            encryptionKey[0] = 123;
            encryptionKey[1] = 97;
            encryptionKey[2] = 45;
            encryptionKey[3] = 123;
            encryptionKey[4] = 91;
            encryptionKey[5] = 27;
            encryptionKey[6] = 66;
            encryptionKey[7] = 46;
            encryptionKey[8] = 22;
            encryptionKey[9] = 33;
            encryptionKey[10] = 99;
            encryptionKey[11] = 93;
            encryptionKey[12] = 43;
            encryptionKey[13] = 16;
            encryptionKey[14] = 24;
            encryptionKey[15] = 37;
            encryptionKey[16] = 21;
            encryptionKey[17] = 90;
            encryptionKey[18] = 33;
            encryptionKey[19] = 38;
            encryptionKey[20] = 87;
            encryptionKey[21] = 5;
            encryptionKey[2] = 147;
            encryptionKey[23] = 33;
            return encryptionKey;
        }
    }
}
