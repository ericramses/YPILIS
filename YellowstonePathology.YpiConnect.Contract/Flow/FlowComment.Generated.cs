using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[DataContract]
	public partial class FlowComment
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in FlowLeukemia");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in FlowLeukemia");
		}
		#endregion

		#region Fields
		private int m_CommentId;
		private string m_Category;
		private string m_Description;
		private string m_Comment;
		private string m_Impression;
		#endregion

		#region Properties
		[DataMember]
		public int CommentId
		{
			get { return this.m_CommentId; }
			set
			{
				if(this.m_CommentId != value)
				{
					this.m_CommentId = value;
					this.NotifyPropertyChanged("CommentId");
				}
			}
		}

		[DataMember]
		public string Category
		{
			get { return this.m_Category; }
			set
			{
				if(this.m_Category != value)
				{
					this.m_Category = value;
					this.NotifyPropertyChanged("Category");
				}
			}
		}

		[DataMember]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if(this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[DataMember]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if(this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[DataMember]
		public string Impression
		{
			get { return this.m_Impression; }
			set
			{
				if(this.m_Impression != value)
				{
					this.m_Impression = value;
					this.NotifyPropertyChanged("Impression");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_CommentId = propertyWriter.WriteInt("CommentId");
			this.m_Category = propertyWriter.WriteString("Category");
			this.m_Description = propertyWriter.WriteString("Description");
			this.m_Comment = propertyWriter.WriteString("Comment");
			this.m_Impression = propertyWriter.WriteString("Impression");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
		}
		#endregion
	}
}
