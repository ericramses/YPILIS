using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain
{
	public partial class ImmunoComment
	{
		#region Fields
		private int m_ImmunoCommentId;
		private string m_Comment;
		#endregion

		#region Properties
		public int ImmunoCommentId
		{
			get { return this.m_ImmunoCommentId; }
			set
			{
				if(this.m_ImmunoCommentId != value)
				{
					this.m_ImmunoCommentId = value;
					this.NotifyPropertyChanged("ImmunoCommentId");
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

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_ImmunoCommentId = propertyWriter.WriteInt("immunocommentid");
			this.m_Comment = propertyWriter.WriteString("immunocomment");
		}
		#endregion
	}
}
