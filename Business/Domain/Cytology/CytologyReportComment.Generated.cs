using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Cytology
{
	public partial class CytologyReportComment : DomainBase
	{
		#region Fields
		private int m_CommentId;
		private string m_Comment;
		private string m_AbbreviatedComment;
		#endregion

		#region Properties
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

		public string AbbreviatedComment
		{
			get { return this.m_AbbreviatedComment; }
			set
			{
				if(this.m_AbbreviatedComment != value)
				{
					this.m_AbbreviatedComment = value;
					this.NotifyPropertyChanged("AbbreviatedComment");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_CommentId = propertyWriter.WriteInt("CommentID");
			this.m_Comment = propertyWriter.WriteString("Comment");
			this.m_AbbreviatedComment = propertyWriter.WriteString("AbbreviatedComment");
		}
		#endregion
	}
}
