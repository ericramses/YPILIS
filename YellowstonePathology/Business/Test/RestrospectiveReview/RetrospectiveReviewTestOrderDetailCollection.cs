using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Test.RetrospectiveReview
{
    public class RetrospectiveReviewTestOrderDetailCollection : ObservableCollection<RetrospectiveReviewTestOrderDetail>
    {
        public void Sync(DataTable dataTable)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string id = dataTableReader["RetrospectiveReviewTestOrderDetailId"].ToString();
                RetrospectiveReviewTestOrderDetail rsrd = null;

                if (this.Exists(id) == true)
                {
                    rsrd = this.Get(id);
                }
                else
                {                    
                    rsrd = new RetrospectiveReviewTestOrderDetail();
                    this.Add(rsrd);
                }

                YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(rsrd, dataTableReader);
                sqlDataTableReaderPropertyWriter.WriteProperties();
            }
        }

        public RetrospectiveReviewTestOrderDetail Get(string id)
        {
            foreach (RetrospectiveReviewTestOrderDetail item in this)
            {
                if (item.RetrospectiveReviewTestOrderDetailId == item.RetrospectiveReviewTestOrderDetailId)
                {
                    return item;
                }
            }
            return null;
        }

        public bool Exists(string retrospectiveReviewTestOrderDetailId)
        {
            bool result = false;
            foreach (RetrospectiveReviewTestOrderDetail item in this)
            {
                if (item.RetrospectiveReviewTestOrderDetailId == retrospectiveReviewTestOrderDetailId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string id = dataTable.Rows[idx]["RetrospectiveReviewTestOrderDetailId"].ToString();
                    if (this[i].RetrospectiveReviewTestOrderDetailId == id)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

    }
}
