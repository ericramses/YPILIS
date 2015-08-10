using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace ClientWebServices.DataContext
{
    public class LinqToSqlDataContext : IDataContext
    {
        private readonly System.Data.Linq.DataContext m_DataContext;

        public LinqToSqlDataContext()
        {            
            this.m_DataContext = new System.Data.Linq.DataContext(@"Data Source=TESTSQL;Initial Catalog=YPIData;Integrated Security=True");
        }

        public void Dispose()
        {

        }

        public void SubmitChanges()
        {
            this.m_DataContext.SubmitChanges();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            ITable table = this.m_DataContext.GetTable(typeof(T));            
            return table.Cast<T>();
        }        

        public void Insert<T>(T item) where T : class
        {
            ITable table = this.m_DataContext.GetTable(typeof(T));
            table.InsertOnSubmit(item);
        }

        public void Delete<T>(T item) where T : class
        {
            ITable table = this.m_DataContext.GetTable(typeof(T));
            table.DeleteOnSubmit(item);
        }

        public int ExecuteCommand(string command, object [] parameters)
        {            
            return this.m_DataContext.ExecuteCommand(command, parameters);            
        }        

        public void Refresh(object entity)
        {
            this.m_DataContext.Refresh(RefreshMode.KeepChanges, entity);                        
        }

        public void Refresh()
        {
            throw new NotImplementedException("Not Implemented Here.");
        }

		public IQueryable<T> ExecuteQuery<T>(string command, object[] parameters) where T : class
		{
			IQueryable<T> result = this.m_DataContext.ExecuteQuery<T>(command, parameters).AsQueryable<T>();
			return result;
		}

        public void SubmitChanges<T>(T item)
        {
            throw new NotImplementedException("Not Implemented Here.");
        }
	}
}
