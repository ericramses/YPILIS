using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Search
{
    [DataContract]
    public class SearchResult
    {
        string m_MasterAccessionNo;
        string m_ReportNo;
        string m_PatientName;        
        string m_PSex;
        string m_PCAN;
        string m_PSSN;
        Nullable<DateTime> m_PBirthdate;
        Nullable<DateTime> m_CollectionDate;
        DateTime m_AccessionDate;
        Nullable<DateTime> m_FinalTime;
        int m_ClientId;
        int m_PhysicianId;
        string m_ClientName;
        string m_PhysicianName;
        string m_PanelSetName;
        string m_ReportDistributionLogId;

        public SearchResult()
        {

        }

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter)
        {
            this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
            this.m_ReportNo = propertyWriter.WriteString("ReportNo");
            this.m_PatientName = propertyWriter.WriteString("PatientName");
            this.m_PSex = propertyWriter.WriteString("PSex");
            this.m_PCAN = propertyWriter.WriteString("PCAN");
            this.m_PSSN = propertyWriter.WriteString("PSSN");
            this.m_PBirthdate = propertyWriter.WriteNullableDateTime("PBirthdate");
            this.m_CollectionDate = propertyWriter.WriteNullableDateTime("CollectionDate");
            this.m_AccessionDate = propertyWriter.WriteDateTime("AccessionDate");
            this.m_FinalTime = propertyWriter.WriteNullableDateTime("FinalTime");
            this.m_ClientId = propertyWriter.WriteInt("ClientId");
            this.m_PhysicianId = propertyWriter.WriteInt("PhysicianId");
            this.m_ClientName = propertyWriter.WriteString("ClientName");
            this.m_PhysicianName = propertyWriter.WriteString("PhysicianName");
            this.m_PanelSetName = propertyWriter.WriteString("PanelSetName");
            this.m_ReportDistributionLogId = propertyWriter.WriteString("ReportDistributionLogId");
        }

        [DataMember]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [DataMember]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [DataMember]
        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }

        [DataMember]
        public string PCAN
        {
            get { return this.m_PCAN; }
            set { this.m_PCAN = value; }
        }

        [DataMember]
        public string PSSN
        {
            get { return this.m_PSSN; }
            set { this.m_PSSN = value; }
        }

        [DataMember]
        public string PSex
        {
            get { return this.m_PSex; }
            set { this.m_PSex = value; }
        }

        [DataMember]
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set { this.m_PBirthdate = value; }
        }

        [DataMember]
        public Nullable<DateTime> CollectionDate
        {
            get { return this.m_CollectionDate; }
            set { this.m_CollectionDate = value; }
        }

        [DataMember]
        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate; }
            set { this.m_AccessionDate = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }            
        }

        [DataMember]
        public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set { this.m_PhysicianId = value; }            
        }

        [DataMember]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [DataMember]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }            
        }

        [DataMember]
        public Nullable<DateTime> FinalTime
        {
            get { return this.m_FinalTime; }
            set { this.m_FinalTime = value; }
        }

        [DataMember]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
        }

        [DataMember]
        public string ReportDistributionLogId
        {
            get { return this.m_ReportDistributionLogId; }
            set { this.m_ReportDistributionLogId = value; }
        }        
    }
}
