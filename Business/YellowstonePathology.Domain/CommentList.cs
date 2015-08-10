using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Domain
{
	[XmlType("CommentList")]
	public class CommentList : ObservableCollection<CommentListItem>
	{

        public CommentList()
        {
		}

        public CommentListItem GetCommentListItemByCommentId(int commentId)
        {
            CommentListItem commentListItem = null;
            foreach (CommentListItem item in this)
            {
                if (item.CommentId == commentId)
                {
                    commentListItem = item;
                    break;
                }
            }
            return commentListItem;
		}
	}    
}
