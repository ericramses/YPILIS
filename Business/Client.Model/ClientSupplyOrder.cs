using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblClientSupplyOrder", "YPIDATA")]
	public class ClientSupplyOrder : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;				

		private int m_ClientSupplyOrderId;
		private int m_ClientId;
		private string m_ClientName;
		private string m_ClientAddress;
		private string m_ClientCity;
		private string m_ClientState;
		private string m_ClientZip;
		private DateTime? m_OrderDate;
		private DateTime? m_DateOrderSent;
		private bool m_OrderFinal;
		private int m_OrderTakenById;
		private int m_OrderFilledById;
		private string m_ContactName;
		private string m_Comment;
		private string m_ObjectId;

		private ClientSupplyOrderDetailCollection m_ClientSupplyOrderDetailCollection;

		public ClientSupplyOrder()
		{
			this.m_ClientSupplyOrderDetailCollection = new ClientSupplyOrderDetailCollection();
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		[PersistentCollection()]
		public ClientSupplyOrderDetailCollection ClientSupplyOrderDetailCollection
		{
			get { return this.m_ClientSupplyOrderDetailCollection; }
		}

        [PersistentPrimaryKeyProperty(true)]
		private int ClientSupplyOrderId
		{
			get { return this.m_ClientSupplyOrderId; }
			set
			{
				if (this.m_ClientSupplyOrderId != value)
				{
					this.m_ClientSupplyOrderId = value;
					this.NotifyPropertyChanged("ClientSupplyOrderId");
				}
			}
		}

        [PersistentProperty()]
		private int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}

        [PersistentProperty()]
		private string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (this.m_ClientName != value)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}

        [PersistentProperty()]
		private string ClientAddress
		{
			get { return this.m_ClientAddress; }
			set
			{
				if (this.m_ClientAddress != value)
				{
					this.m_ClientAddress = value;
					this.NotifyPropertyChanged("ClientAddress");
				}
			}
		}

        [PersistentProperty()]
		private string ClientCity
		{
			get { return this.m_ClientCity; }
			set
			{
				if (this.m_ClientCity != value)
				{
					this.m_ClientCity = value;
					this.NotifyPropertyChanged("ClientCity");
				}
			}
		}

        [PersistentProperty()]
		private string ClientState
		{
			get { return this.m_ClientState; }
			set
			{
				if (this.m_ClientState != value)
				{
					this.m_ClientState = value;
					this.NotifyPropertyChanged("ClientState");
				}
			}
		}

        [PersistentProperty()]
		private string ClientZip
		{
			get { return this.m_ClientZip; }
			set
			{
				if (this.m_ClientZip != value)
				{
					this.m_ClientZip = value;
					this.NotifyPropertyChanged("ClientZip");
				}
			}
		}

        [PersistentProperty()]
		private DateTime? OrderDate
		{
			get { return this.m_OrderDate; }
			set
			{
				if (this.m_OrderDate != value)
				{
					this.m_OrderDate = value;
					this.NotifyPropertyChanged("OrderDate");
				}
			}
		}

        [PersistentProperty()]
		private DateTime? DateOrderSent
		{
			get { return this.m_DateOrderSent; }
			set
			{
				if (this.m_DateOrderSent != value)
				{
					this.m_DateOrderSent = value;
					this.NotifyPropertyChanged("DateOrderSent");
				}
			}
		}

        [PersistentProperty()]
		private bool OrderFinal
		{
			get { return this.m_OrderFinal; }
			set
			{
				if (this.m_OrderFinal != value)
				{
					this.m_OrderFinal = value;
					this.NotifyPropertyChanged("OrderFinal");
				}
			}
		}

        [PersistentProperty()]
		private int OrderTakenById
		{
			get { return this.m_OrderTakenById; }
			set
			{
				if (this.m_OrderTakenById != value)
				{
					this.m_OrderTakenById = value;
					this.NotifyPropertyChanged("OrderTakenById");
				}
			}
		}

        [PersistentProperty()]
		private int OrderFilledById
		{
			get { return this.m_OrderFilledById; }
			set
			{
				if (this.m_OrderFilledById != value)
				{
					this.m_OrderFilledById = value;
					this.NotifyPropertyChanged("OrderFilledById");
				}
			}
		}

        [PersistentProperty()]
		private string ContactName
		{
			get { return this.m_ContactName; }
			set
			{
				if (this.m_ContactName != value)
				{
					this.m_ContactName = value;
					this.NotifyPropertyChanged("ContactName");
				}
			}
		}

        [PersistentProperty()]
		private string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

        [PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");					
				}
			}
		}
	}
}
