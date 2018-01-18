using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.WebServices.Data;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class Dashboard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_DashboardDate;
        private MonitorStateEnum m_State;
        private BlockCountCollection m_BlockCountCollection;

        public Dashboard()
        {
            this.m_State = MonitorStateEnum.Normal;
        }

        public DateTime DashboardDate
        {
            get { return this.m_DashboardDate; }
            set
            {
                if (this.m_DashboardDate != value)
                {
                    this.m_DashboardDate = value;
                    this.NotifyPropertyChanged("DashboardDate");
                }
            }
        }

        public BlockCountCollection BlockCountCollection
        {
            get { return this.m_BlockCountCollection; }
            set
            {
                this.m_BlockCountCollection = value;
                if (this.m_BlockCountCollection != value)
                {
                    this.m_BlockCountCollection = value;
                    this.NotifyPropertyChanged("BlockCountCollection");
                }
            }
        }

        public void LoadData()
        {
            Nullable<int> bozemanCount = this.GetBozemanBlockCount();
            string count = "Unknown";
            if (bozemanCount.HasValue == true) count = bozemanCount.Value.ToString();
                
            Store.RedisDB db = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.BozemanBlockCount);
            db.DataBase.StringSet(DateTime.Today.ToString("yyyyMMdd"), count);
            string[] results = db.GetAllStrings();
            foreach(string result in results)
            {
                BlockCount block = new Model.BlockCount(result);
                this.m_BlockCountCollection.Add(block);
            }


            this.NotifyPropertyChanged("");

        }

        public void SetBozemanBlockCount(int blockCount)
        {            
            //this.m_BozemanBlockCount = blockCount.ToString();
            //this.m_BozemanBlocks = blockCount;
            this.NotifyPropertyChanged(string.Empty);            
        }

        public Nullable<int> GetBozemanBlockCount()
        {
            Nullable<int> blockCount = null;
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service.Credentials = new WebCredentials("ypiilab\\blockcount", "blockorama");

            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;

            service.AutodiscoverUrl("blockcount@ypii.com", RedirectionUrlValidationCallback);
            SearchFilter searchFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);

            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(searchFilter);
            ItemView view = new ItemView(50);
            view.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);
            view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            view.Traversal = ItemTraversal.Shallow;
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, searchFilter, view);

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\d{1,3}(?=\\D*$)");
            foreach (Item mailItem in findResults.Items)
            {
                if (mailItem is EmailMessage)
                {
                    System.Text.RegularExpressions.Match match = regex.Match(mailItem.Subject);
                    if (match.Captures.Count != 0)
                    {
                        blockCount = Convert.ToInt32(match.Value);
                        mailItem.Delete(DeleteMode.MoveToDeletedItems);
                    }
                }
            }
            return blockCount;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        private static bool CertificateValidationCallBack(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }
    }
}
