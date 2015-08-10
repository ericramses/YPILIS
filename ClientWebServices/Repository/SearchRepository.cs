using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace ClientWebServices.Repository
{
    public class SearchRepository 
    {
        DataContext.YpiData m_DataContext;

        public SearchRepository(DataContext.YpiData dataContext)            
        {
            this.m_DataContext = dataContext;
        }
        
        public Domain.Physician GetPhysician(int physicianId)
        {
            var query = from item in this.m_DataContext.GetTable<Domain.Physician>()
                        where item.PhysicianID == physicianId
                        select item;
            return query.Single<Domain.Physician>();
        }

        public Domain.Client GetClient(int clientId)
        {
            Domain.Client client = null;
            var query = from item in this.m_DataContext.GetTable<Domain.Client>()
                        where item.ClientId == clientId
                        select item;
            try
            {
                client = query.Single<Domain.Client>();
            }
            catch {}
            return client;
        }        

        public void AcknowledgeDistributions(string reportDistributionLogIdStringList)
        {            
            this.m_DataContext.AcknowledgeDistributions(reportDistributionLogIdStringList);            
        }

        public List<SearchResult> GetClientCasesByPhysicianId(Search search)
        {                        
            int physicianId = (int)search.SearchParameters[0];
            var query = from ao in this.m_DataContext.GetTable<Domain.AccessionOrder>()
                        from pso in ao.PanelSetOrders
                        where ao.PhysicianId == physicianId
                        orderby ao.MasterAccessionNo descending
                        select new SearchResult
                        {
                            MasterAccessionNo = ao.MasterAccessionNo,
                            ReportNo = pso.ReportNo,
                            AccessionDate = ao.AccessionDate,
                            PatientName = ao.PFirstName + ' ' + ao.PLastName,
                            PCAN = ao.PCAN,
                            PSex = ao.PSex,
                            PBirthdate = ao.PBirthdate,
                            PSSN = ao.PSSN,
                            CollectionDate = ao.CollectionDate,
                            ClientId = ao.ClientId,
                            ClientName = ao.ClientName,
                            PhysicianName = ao.PhysicianName,
                            PhysicianId = ao.PhysicianId,
                            FinalTime = pso.FinalTime,
                            PanelSetName = pso.PanelSetName
                        };
            return query.Take<SearchResult>(100).ToList<SearchResult>();
        }        

        public List<SearchResult> GetClientCasesByPSSN(Search search)
        {
            string pssn = (string)search.SearchParameters[0];
            return this.m_DataContext.GetClientCasesByPSSN(search.ClientUser.ClientIdStringList, pssn).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetPhysicianCasesByPSSN(Search search)
        {
            string pssn = (string)search.SearchParameters[0];
            return this.m_DataContext.GetPhysicianCasesByPSSN(search.ClientUser.PhysicianIdStringList, pssn).ToList<ClientWebServices.SearchResult>();
        }               
        
        public List<SearchResult> GetClientCasesByPBirthDate(Search search)
        {
            DateTime pbirthdate = (DateTime)search.SearchParameters[0];
            return this.m_DataContext.GetClientCasesByPBirthdate(search.ClientUser.ClientIdStringList, pbirthdate).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetPhysicianCasesByPBirthDate(Search search)
        {
            DateTime pbirthdate = (DateTime)search.SearchParameters[0];
            return this.m_DataContext.GetPhysicianCasesByPBirthdate(search.ClientUser.PhysicianIdStringList, pbirthdate).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetClientCasesByPatientLastName(Search search)
        {
            string pLastName = (string)search.SearchParameters[0];
            return this.m_DataContext.GetClientCasesByPatientLastName(search.ClientUser.ClientIdStringList, pLastName).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetPhysicianCasesByPatientLastName(Search search)
        {
            string pLastName = (string)search.SearchParameters[0];
            return this.m_DataContext.GetPhysicianCasesByPatientLastName(search.ClientUser.PhysicianIdStringList, pLastName).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetClientCasesByPatientLastNameAndFirstName(Search search)
        {
            string pLastName = (string)search.SearchParameters[0];
            string pFirstName = (string)search.SearchParameters[1];
            return this.m_DataContext.GetClientCasesByPatientLastNameAndFirstName(search.ClientUser.ClientIdStringList, pLastName, pFirstName).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetPhysicianCasesByPatientLastNameAndFirstName(Search search)
        {
            string pLastName = (string)search.SearchParameters[0];
            string pFirstName = (string)search.SearchParameters[1];
            return this.m_DataContext.GetPhysicianCasesByPatientLastNameAndFirstName(search.ClientUser.PhysicianIdStringList, pLastName, pFirstName).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetClientRecentCases(Search search)
        {
            DateTime startDate = (DateTime)search.SearchParameters[0];            
            return this.m_DataContext.GetClientRecentCases(search.ClientUser.ClientIdStringList, startDate).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetPhysicianRecentCases(Search search)
        {
            DateTime startDate = (DateTime)search.SearchParameters[0];
            return this.m_DataContext.GetPhysicianRecentCases(search.ClientUser.PhysicianIdStringList, startDate).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetClientCasesNotAcknowledged(Search search)
        {
            return this.m_DataContext.GetClientCasesNotAcknowledged(search.ClientUser.ClientIdStringList).ToList<ClientWebServices.SearchResult>();
        }

        public List<SearchResult> GetPhysicianCasesNotAcknowledged(Search search)
        {
            return this.m_DataContext.GetPhysicianCasesNotDistributed(search.ClientUser.PhysicianIdStringList).ToList<ClientWebServices.SearchResult>();
        }             
    }
}
