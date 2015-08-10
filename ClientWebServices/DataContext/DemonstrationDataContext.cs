using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace ClientWebServices.DataContext
{
    public class DemonstrationDataContext 
    {
        /*
        List<Object> m_DataContext;

        public DemonstrationDataContext()
        {
            this.m_DataContext = new List<object>();

            Domain.AccessionOrder accessionOrder1 = new ClientWebServices.Domain.AccessionOrder();
            accessionOrder1.MasterAccessionNo = 200900001;
            accessionOrder1.AccessionDate = DateTime.Today.AddDays(-3);
            accessionOrder1.ClientId = 0;
            accessionOrder1.ClientName = "The Mouse Clinic";            
            accessionOrder1.PhysicianId = 0;
            accessionOrder1.PhysicianName = "Dr Mickey Mouse";
            accessionOrder1.PFirstName = "Donald";
            accessionOrder1.PLastName = "Duck";
            accessionOrder1.PMiddleInitial = "E.";            

            Domain.PanelSetOrder panelSetOrder1 = new ClientWebServices.Domain.PanelSetOrder();
            panelSetOrder1.MasterAccessionNo = 200900001;
            panelSetOrder1.ReportNo = "Z09-12345";
            panelSetOrder1.OrderDate = DateTime.Today.AddDays(-3);
            panelSetOrder1.OrderTime = DateTime.Now.AddDays(-3);
            panelSetOrder1.PanelSetName = "Surgical Pathology";
            panelSetOrder1.PanelSetId = 13;
            panelSetOrder1.Final = true;
            panelSetOrder1.FinalDate = DateTime.Today.AddDays(-1);
            panelSetOrder1.FinalTime = DateTime.Now.AddDays(-1);

            accessionOrder1.PanelSetOrders.Add(panelSetOrder1);

            Domain.AccessionOrder accessionOrder2 = new ClientWebServices.Domain.AccessionOrder();
            accessionOrder2.MasterAccessionNo = 200900002;            
            accessionOrder2.AccessionDate = DateTime.Today.AddDays(-3);
            accessionOrder2.ClientId = 0;
            accessionOrder2.ClientName = "The Mouse Clinic";
            accessionOrder2.PhysicianId = 0;
            accessionOrder2.PhysicianName = "Dr Mickey Mouse";
            accessionOrder2.PFirstName = "Kermit";
            accessionOrder2.PLastName = "Frog";
            accessionOrder2.PMiddleInitial = "F.";

            Domain.PanelSetOrder panelSetOrder2 = new ClientWebServices.Domain.PanelSetOrder();
            panelSetOrder2.MasterAccessionNo = 200900002;
            panelSetOrder2.ReportNo = "Z09-12346";
            panelSetOrder2.OrderDate = DateTime.Today.AddDays(-3);
            panelSetOrder2.OrderTime = DateTime.Now.AddDays(-3);
            panelSetOrder2.PanelSetName = "Surgical Pathology";
            panelSetOrder2.PanelSetId = 13;
            panelSetOrder2.Final = true;
            panelSetOrder2.FinalDate = DateTime.Today.AddDays(-1);
            panelSetOrder2.FinalTime = DateTime.Now.AddDays(-1);

            accessionOrder2.PanelSetOrders.Add(panelSetOrder2);

            Domain.AccessionOrder accessionOrder3 = new ClientWebServices.Domain.AccessionOrder();
            accessionOrder3.MasterAccessionNo = 200900003;
            accessionOrder3.AccessionDate = DateTime.Today.AddDays(-3);
            accessionOrder3.ClientId = 0;
            accessionOrder3.ClientName = "The Mouse Clinic";
            accessionOrder3.PhysicianId = 0;
            accessionOrder3.PhysicianName = "Dr Mickey Mouse";
            accessionOrder3.PFirstName = "Benjamin";
            accessionOrder3.PLastName = "Franklin";
            accessionOrder3.PMiddleInitial = "P.";

            Domain.PanelSetOrder panelSetOrder3 = new ClientWebServices.Domain.PanelSetOrder();
            panelSetOrder3.MasterAccessionNo = 200900003;
            panelSetOrder3.ReportNo = "Z09-12347";
            panelSetOrder3.OrderDate = DateTime.Today.AddDays(-3);
            panelSetOrder3.OrderTime = DateTime.Now.AddDays(-3);
            panelSetOrder3.PanelSetName = "Surgical Pathology";
            panelSetOrder3.PanelSetId = 13;
            panelSetOrder3.Final = true;
            panelSetOrder3.FinalDate = DateTime.Today.AddDays(-1);
            panelSetOrder3.FinalTime = DateTime.Now.AddDays(-1);

            accessionOrder3.PanelSetOrders.Add(panelSetOrder3);

            Domain.AccessionOrder accessionOrder4 = new ClientWebServices.Domain.AccessionOrder();
            accessionOrder4.MasterAccessionNo = 200900004;
            accessionOrder4.AccessionDate = DateTime.Today.AddDays(-3);
            accessionOrder4.ClientId = 0;
            accessionOrder4.ClientName = "The Mouse Clinic";
            accessionOrder4.PhysicianId = 0;
            accessionOrder4.PhysicianName = "Dr Goofy Gander";
            accessionOrder4.PFirstName = "John";
            accessionOrder4.PLastName = "Adams";
            accessionOrder4.PMiddleInitial = "Q.";

            Domain.PanelSetOrder panelSetOrder4 = new ClientWebServices.Domain.PanelSetOrder();
            panelSetOrder4.MasterAccessionNo = 200900004;
            panelSetOrder4.ReportNo = "Z09-12347";
            panelSetOrder4.OrderDate = DateTime.Today.AddDays(-3);
            panelSetOrder4.OrderTime = DateTime.Now.AddDays(-3);
            panelSetOrder4.PanelSetName = "Surgical Pathology";
            panelSetOrder4.PanelSetId = 13;
            panelSetOrder4.Final = true;
            panelSetOrder4.FinalDate = DateTime.Today.AddDays(-1);
            panelSetOrder4.FinalTime = DateTime.Now.AddDays(-1);

            accessionOrder3.PanelSetOrders.Add(panelSetOrder4);

            this.m_DataContext.Add(accessionOrder1);
            this.m_DataContext.Add(accessionOrder2);
            this.m_DataContext.Add(accessionOrder3);
            this.m_DataContext.Add(accessionOrder4);
        }

        public void Dispose()
        {
            throw new NotImplementedException("Not Implemented Here.");
        }

        public void SubmitChanges()
        {
            throw new NotImplementedException("Not Implemented Here.");
        }       

        public IQueryable<T> Query<T>() where T : class
        {            
            var query = from objects in this.m_DataContext where typeof(T).IsAssignableFrom(objects.GetType()) select objects;
            IQueryable<T> items = query.Select(o => (T)o).AsQueryable();
            return items;
        }

        public IQueryable<T> ExecuteQuery<T>(string command, object[] parameters) where T : class
        {
            throw new NotImplementedException("Not Implemented Here.");
        }

        public void Insert<T>(T item) where T : class
        {
            throw new NotImplementedException("Not Implemented Here.");
        }

        public void Delete<T>(T item) where T : class
        {
            throw new NotImplementedException("Not Implemented Here.");
        }

        public int ExecuteCommand(string command, object [] parameters)
        {
            throw new NotImplementedException("Not Implemented Here.");         
        }        

        public void Refresh(object entity)
        {
            throw new NotImplementedException("Not Implemented Here.");                      
        }

        public void Refresh()
        {
            throw new NotImplementedException("Not Implemented Here.");
        }        

        public void SubmitChanges<T>(T item)
        {
            throw new NotImplementedException("Not Implemented Here.");
        }
        */
	}
}
