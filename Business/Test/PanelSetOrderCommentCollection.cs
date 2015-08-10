using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test
{
	public class PanelSetOrderCommentCollection : ObservableCollection<PanelSetOrderComment>
	{
 		public const string PREFIXID = "PSOC";

		public PanelSetOrderCommentCollection()
        {

		}

		public PanelSetOrderComment GetCurrent()
		{
			return this.Count > 0 ? this[0] : null;
		}

		public PanelSetOrderComment GetCurrent(string panelSetOrderCommentId)
		{
			foreach (PanelSetOrderComment item in this)
			{
				if (item.PanelSetOrderCommentId == panelSetOrderCommentId)
				{
					return item;
				}
			}
			return null;
		}

        public PanelSetOrderComment GetByDocumentCommentId(int documentCommentId)
        {
            PanelSetOrderComment result = null;
            foreach (PanelSetOrderComment item in this)
            {
                if (item.DocumentCommentId == documentCommentId)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

		public PanelSetOrderComment GetPanelSetOrderComment(string placeHolder)
		{
			PanelSetOrderComment panelSetOrderComment = null;
			foreach (PanelSetOrderComment item in this)
			{
				if (item.PlaceHolder == placeHolder)
				{
					panelSetOrderComment = item;
					break;
				}
			}
			return panelSetOrderComment;
		}

		public void Remove(object obj)
		{
			PanelSetOrderComment item = obj as PanelSetOrderComment;
			if (item != null)
			{
				base.Remove(item);
			}
		}

		public PanelSetOrderComment Add(string reportNo)
		{
			PanelSetOrderComment panelSetOrderComment = this.GetNextItem(reportNo);
			this.Add(panelSetOrderComment);
			return panelSetOrderComment;
		}

		public PanelSetOrderComment GetNextItem(string reportNo)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			string panelSetOrderCommentId = this.GetNextId(reportNo);
			PanelSetOrderComment panelSetOrderComment = new PanelSetOrderComment(reportNo, objectId, panelSetOrderCommentId);
			return panelSetOrderComment;
		}

		public string GetNextId(string reportNo)
		{
			string result = OrderIdParser.GetNextPanelSetOrderCommentId(this, reportNo);
			return result;
		}
	}
}
