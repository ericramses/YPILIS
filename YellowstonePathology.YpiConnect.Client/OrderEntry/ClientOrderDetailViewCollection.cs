using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class ClientOrderDetailViewCollection : ObservableCollection<ClientOrderDetailView>
    {
        YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection m_ClientOrderDetailCollection;

        public ClientOrderDetailViewCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection, bool showInactive)
        {
            this.m_ClientOrderDetailCollection = clientOrderDetailCollection;
            this.Load(showInactive);            
        }

        public void Reload(bool showInactive)
        {
            this.ClearItems();
            this.Load(showInactive);
        }

        private void Load(bool showInactive)
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrderDetailCollection)
            {
                bool addToCollection = true;
                if (showInactive == false)
                {
                    if (clientOrderDetail.Inactive == true)
                    {
                        addToCollection = false;
                    }
                }
                if (addToCollection == true)
                {
                    ClientOrderDetailView clientOrderDetailView = new ClientOrderDetailView(clientOrderDetail);
                    this.Add(clientOrderDetailView);
                }
            }
        }        
    }
}
