using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;

namespace YellowstonePathology.Business.Client
{
    public class ClientListItem : ListItem
    {
        int m_ClientId;
        string m_ClientName;
        string m_Address;

        public ClientListItem()
        {

        }


        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("ClientId", SqlDbType.Int)]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (value != this.m_ClientId)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("ClientName", SqlDbType.VarChar)]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (value != this.m_ClientName)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Address", SqlDbType.VarChar)]
        public string Address
        {
            get { return this.m_Address; }
            set
            {
                if (value != this.m_Address)
                {
                    this.m_Address = value;
                    this.NotifyPropertyChanged("Address");
                }
            }
        }

        public override void Fill(SqlDataReader dr)
        {
            this.m_ClientId = BaseData.GetIntValue("ClientId", dr);
            this.m_ClientName = BaseData.GetStringValue("ClientName", dr);
            this.m_Address = BaseData.GetStringValue("Address", dr);
        }
    }
}
