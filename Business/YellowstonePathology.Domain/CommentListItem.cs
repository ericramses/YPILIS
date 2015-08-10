using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Domain
{
	[XmlType("CommentListItem")]
	public class CommentListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public CommentListItem()
		{
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		#region Fields
		private int m_CommentId;
		private string m_Description;
		private string m_Comment;
		#endregion

		#region Properties
		public int CommentId
		{
			get { return this.m_CommentId; }
			set
			{
				if (this.m_CommentId != value)
				{
					this.m_CommentId = value;
					this.NotifyPropertyChanged("CommentId");
				}
			}
		}

		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		public string Comment
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

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_CommentId = propertyWriter.WriteInt("CommentId");
			this.m_Description = propertyWriter.WriteString("Description");
			this.m_Comment = propertyWriter.WriteString("Comment");
		}
		#endregion
	}
}
