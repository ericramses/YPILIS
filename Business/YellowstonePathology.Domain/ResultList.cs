using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Domain
{
	public class ResultList : ObservableCollection<ResultListItem>
	{

		public ResultList()
		{
		}

		public ResultListItem GetResultListItemByResultId(int resultId)
        {
            ResultListItem result = null;            
            foreach (ResultListItem resultListItem in this)
            {
                if (resultListItem.ResultId == resultId)
                {
                    result = resultListItem;
                }
            }            
            return result;
        }

		public void SetResultById(int resultId, string result)
		{
			foreach (ResultListItem item in this)
			{
				if (item.ResultId == resultId)
				{
					item.Result = result;
					break;
				}
			}
		}
	}

	public class ResultListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_ResultId;
		private string m_Result;

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentPrimaryKeyProperty(true)]
		public int ResultId
        {
			get { return this.m_ResultId; }
			set
			{
				if (this.m_ResultId != value)
				{
					this.m_ResultId = value;
					NotifyPropertyChanged("ResultId");
				}
			}
        }

		[PersistentProperty()]
        public string Result
        {
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					NotifyPropertyChanged("Result");
				}
			}
        }
	}
}
