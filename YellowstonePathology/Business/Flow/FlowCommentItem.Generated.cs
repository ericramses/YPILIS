using System;
using System.Data;

namespace YellowstonePathology.Business.Flow
{
	[YellowstonePathology.Business.CustomAttributes.SqlTableAttribute("tblFlowCommentV2", "CommentId", SqlDbType.VarChar, 50)]
	public partial class FlowCommentItem : BaseItem
	{
		string m_Category = string.Empty;
		string m_Comment = string.Empty;
		string m_CommentId;
		string m_Description = string.Empty;
		string m_Impression = string.Empty;

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Category", 50, SqlDbType.VarChar)]
		public string Category
		{
			get { return this.m_Category; }
			set
			{
				if (value != this.m_Category)
				{
					this.m_Category = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Category", this.m_Category);
					this.NotifyPropertyChanged("Category");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Comment", 5000, SqlDbType.VarChar)]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (value != this.m_Comment)
				{
					this.m_Comment = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Comment", this.m_Comment);
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("CommentId", 50, SqlDbType.VarChar)]
		public string CommentId
		{
			get { return this.m_CommentId; }
			set
			{
				if (value != this.m_CommentId)
				{
					this.m_CommentId = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "CommentId", this.m_CommentId);
					this.NotifyPropertyChanged("CommentId");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Description", 500, SqlDbType.VarChar)]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (value != this.m_Description)
				{
					this.m_Description = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Description", this.m_Description);
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Impression", 5000, SqlDbType.VarChar)]
		public string Impression
		{
			get { return this.m_Impression; }
			set
			{
				if (value != this.m_Impression)
				{
					this.m_Impression = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Impression", this.m_Impression);
					this.NotifyPropertyChanged("Impression");
				}
			}
		}
	}
}
