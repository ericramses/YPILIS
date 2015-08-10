using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.Domain
{
	[XmlType("TemplateList")]
	public class TemplateList : ObservableCollection<TemplateListItem>
	{
		public TemplateList()
		{
		}
	}

	[XmlType("TemplateListItem")]
	public class TemplateListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_TemplateId;
		private string m_TemplateName;
		private string m_FileName;
		private bool m_Retired;

		public TemplateListItem()
        {
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public int TemplateId
        {
			get { return this.m_TemplateId; }
			set
			{
				if (this.m_TemplateId != value)
				{
					this.m_TemplateId = value;
					NotifyPropertyChanged("TemplateId");
				}
			}
        }

        public string TemplateName
        {
			get { return this.m_TemplateName; }
			set
			{
				if (this.m_TemplateName != value)
				{
					this.m_TemplateName = value;
					NotifyPropertyChanged("TemplateName");
				}
			}
        }

        public string FileName
        {
			get { return this.m_FileName; }
			set
			{
				if (this.m_FileName != value)
				{
					this.m_FileName = value;
					NotifyPropertyChanged("FileName");
				}
			}
        }

        public bool Retired
        {
			get { return this.m_Retired; }
			set
			{
				if (this.m_Retired != value)
				{
					this.m_Retired = value;
					NotifyPropertyChanged("Retired");
				}
            }
        }
	}
}
