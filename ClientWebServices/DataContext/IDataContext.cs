using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientWebServices.DataContext
{
    public interface IDataContext : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Insert<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
        int ExecuteCommand(string command, object[] parameters);
        void SubmitChanges();
        void SubmitChanges<T>(T item);
        void Refresh();
        IQueryable<T> ExecuteQuery<T>(string command, object[] parameters) where T : class;
    }
}
