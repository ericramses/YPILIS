using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass("tblPanelSetOrderComment", "YPIDATA")]
	public class PanelSetOrderComment : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_PanelSetOrderCommentId;
		private string m_ReportNo;
		private int m_DocumentCommentId;
		private string m_CommentName;
		private string m_PlaceHolder;
		private string m_Comment;
	
		private List<YellowstonePathology.Business.Domain.Core.Comment> m_AvailableComments;

        public PanelSetOrderComment()
        {
		}

		public PanelSetOrderComment(string reportNo, string objectId, string panelSetOrderCommentId)
		{
			this.m_ReportNo = reportNo;
			this.m_ObjectId = objectId;
			this.m_PanelSetOrderCommentId = panelSetOrderCommentId;
		}

		public List<YellowstonePathology.Business.Domain.Core.Comment> AvailableComments
		{
			get
			{				
				return m_AvailableComments;
			}
			set
			{
				m_AvailableComments = value;
				NotifyPropertyChanged("AvaliableComments");
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

		[PersistentPrimaryKeyProperty(false)]
		public string PanelSetOrderCommentId
		{
			get { return this.m_PanelSetOrderCommentId; }
			set
			{
				if (this.m_PanelSetOrderCommentId != value)
				{
					this.m_PanelSetOrderCommentId = value;
					this.NotifyPropertyChanged("PanelSetOrderCommentId");
				}
			}
		}

		[PersistentProperty()]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

		[PersistentProperty()]
		public int DocumentCommentId
		{
			get { return this.m_DocumentCommentId; }
			set
			{
				if (this.m_DocumentCommentId != value)
				{
					this.m_DocumentCommentId = value;
					this.NotifyPropertyChanged("DocumentCommentId");
				}
			}
		}

		[PersistentProperty()]
		public string CommentName
		{
			get { return this.m_CommentName; }
			set
			{
				if (this.m_CommentName != value)
				{
					this.m_CommentName = value;
					this.NotifyPropertyChanged("CommentName");
				}
			}
		}

		[PersistentProperty()]
		public string PlaceHolder
		{
			get { return this.m_PlaceHolder; }
			set
			{
				if (this.m_PlaceHolder != value)
				{
					this.m_PlaceHolder = value;
					this.NotifyPropertyChanged("PlaceHolder");
				}
			}
		}

		[PersistentProperty()]
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

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
