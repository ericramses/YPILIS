using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Client
{
    public class XmlEncryptor
    {
        public void Encrypt(XmlDocument xmlDocument)
        {
            TripleDESCryptoServiceProvider encryptionKey = new TripleDESCryptoServiceProvider();
            encryptionKey.Key = SettingsEncryptionKey.GetKey();
            XmlElement orderElem = xmlDocument.DocumentElement as XmlElement;
            EncryptedXml encXml = new EncryptedXml(xmlDocument);
            byte[] encryptedOrder = encXml.EncryptData(orderElem, encryptionKey, false);            
            EncryptedData encryptedData = new EncryptedData();
            encryptedData.Type = EncryptedXml.XmlEncElementUrl;
            encryptedData.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncTripleDESUrl);

            encryptedData.CipherData = new CipherData();
            encryptedData.CipherData.CipherValue = encryptedOrder;
            EncryptedXml.ReplaceElement(orderElem, encryptedData, false);
        }

        public void Decrypt(XmlDocument xmlDocument)
        {
            XmlElement encOrderElem = xmlDocument.GetElementsByTagName("EncryptedData")[0] as XmlElement;
            EncryptedData encData = new EncryptedData();
            encData.LoadXml(encOrderElem);

            TripleDESCryptoServiceProvider encryptionKey = new TripleDESCryptoServiceProvider();
            encryptionKey.Key = SettingsEncryptionKey.GetKey();
            EncryptedXml encryptedXml = new EncryptedXml();
            byte[] decryptedOrder = encryptedXml.DecryptData(encData, encryptionKey);

            encryptedXml.ReplaceData(encOrderElem, decryptedOrder);
        }
    }    
}
