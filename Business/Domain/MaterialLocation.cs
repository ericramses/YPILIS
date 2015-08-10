using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Domain
{
	public partial class MaterialLocation : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_MaterialLocationId;
		private string m_Name;
		private string m_Address;
		private string m_City;
		private string m_State;
		private string m_Zip;

		public MaterialLocation()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public int MaterialLocationId
		{
			get { return this.m_MaterialLocationId; }
			set
			{
				if(this.m_MaterialLocationId != value)
				{
					this.m_MaterialLocationId = value;
					this.NotifyPropertyChanged("MaterialLocationId");
				}
			}
		}

		public string Name
		{
			get { return this.m_Name; }
			set
			{
				if(this.m_Name != value)
				{
					this.m_Name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}

		public string Address
		{
			get { return this.m_Address; }
			set
			{
				if(this.m_Address != value)
				{
					this.m_Address = value;
					this.NotifyPropertyChanged("Address");
				}
			}
		}

		public string City
		{
			get { return this.m_City; }
			set
			{
				if(this.m_City != value)
				{
					this.m_City = value;
					this.NotifyPropertyChanged("City");
				}
			}
		}

		public string State
		{
			get { return this.m_State; }
			set
			{
				if(this.m_State != value)
				{
					this.m_State = value;
					this.NotifyPropertyChanged("State");
				}
			}
		}

		public string Zip
		{
			get { return this.m_Zip; }
			set
			{
				if(this.m_Zip != value)
				{
					this.m_Zip = value;
					this.NotifyPropertyChanged("Zip");
				}
			}
		}

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_MaterialLocationId = propertyWriter.WriteInt("MaterialLocationId");
			this.m_Name = propertyWriter.WriteString("Name");
			this.m_Address = propertyWriter.WriteString("Address");
			this.m_City = propertyWriter.WriteString("City");
			this.m_State = propertyWriter.WriteString("State");
			this.m_Zip = propertyWriter.WriteString("Zip");
		}

		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadInt("MaterialLocationId", MaterialLocationId);
			propertyReader.ReadString("Name", Name);
			propertyReader.ReadString("Address", Address);
			propertyReader.ReadString("City", City);
			propertyReader.ReadString("State", State);
			propertyReader.ReadString("Zip", Zip);
		}
	}
}
