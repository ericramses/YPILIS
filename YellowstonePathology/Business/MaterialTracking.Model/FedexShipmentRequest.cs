using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Xml.XPath;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class FedexShipmentRequest
    {
        private Business.Facility.Model.Facility m_Facility;
        private FedexAccount m_FedexAccount;
        private XDocument m_ProcessShipmentRequest;

        public FedexShipmentRequest(Business.Facility.Model.Facility facility, FedexAccount fedexAccount)
        {
            this.m_Facility = facility;
            this.m_FedexAccount = fedexAccount;

            this.OpenShipmentRequestFile();
            this.SetShipementRequestData();
        }        

        public string RequestShipment()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.m_FedexAccount.URL);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(this.m_ProcessShipmentRequest.ToString());
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.Stream responseStream = response.GetResponseStream();
                string responseStr = new System.IO.StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        private void SetShipementRequestData()
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());            
            namespaces.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.AddNamespace("v19", "http://fedex.com/ws/ship/v19");

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:WebAuthenticationDetail/v19:UserCredential/v19:Key", namespaces).Value = this.m_FedexAccount.Key;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:WebAuthenticationDetail/v19:UserCredential/v19:Password", namespaces).Value = this.m_FedexAccount.Password;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:ClientDetail/v19:AccountNumber", namespaces).Value = this.m_FedexAccount.AccountNo;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:ClientDetail/v19:MeterNumber", namespaces).Value = this.m_FedexAccount.MeterNo;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShipTimestamp", namespaces).Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); //2016-08-25T16:30:12

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:PersonName", namespaces).Value = string.Empty;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:CompanyName", namespaces).Value = this.m_Facility.FacilityName;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:PhoneNumber", namespaces).Value = this.m_Facility.PhoneNumber;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:EMailAddress", namespaces).Value = string.Empty;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:StreetLines[1]", namespaces).Value = this.m_Facility.Address1;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:StreetLines[2]", namespaces).Value = this.m_Facility.Address2;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:City", namespaces).Value = this.m_Facility.City;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:StateOrProvinceCode", namespaces).Value = this.m_Facility.State;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:PostalCode", namespaces).Value = this.m_Facility.ZipCode;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:CountryCode", namespaces).Value = "US";
        }

        private void OpenShipmentRequestFile()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("YellowstonePathology.Business.MaterialTracking.Model.FedexProcessShipmentRequest.v19.xml"))
            {
                this.m_ProcessShipmentRequest = XDocument.Load(stream);
            }
        }
    }
}
