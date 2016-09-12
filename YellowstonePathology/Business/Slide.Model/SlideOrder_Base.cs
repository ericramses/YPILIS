using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Slide.Model
{
    [PersistentClass("tblSlideOrder", "YPIDATA")]
    public class SlideOrder_Base: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_ObjectId;
        protected string m_SlideOrderId;
        protected string m_AliquotOrderId;
        protected bool m_ClientAccessioned;
        protected DateTime m_OrderDate;
        protected string m_Description;
        protected string m_AliquotType;
        protected string m_Label;
        protected string m_ClientLabel;

        protected int m_OrderedById;
        protected string m_OrderedBy;
        protected string m_OrderedFrom;        
        protected bool m_Validated;
        protected string m_ValidationStation;
        protected int m_ValidatedById;
        protected string m_ValidatedBy;
        protected Nullable<DateTime> m_ValidationDate;

        protected bool m_Printed;
        protected string m_PrintStation;
        protected int m_PrintedById;
        protected string m_PrintedBy;
        protected Nullable<DateTime> m_PrintDate;

        protected string m_Status;
        protected int m_TestId;
        protected string m_TestName;
        protected string m_TestAbbreviation;
        protected string m_TestOrderId;
        protected string m_PatientLastName;
        protected string m_Location;
        protected string m_ReportNo;
        protected string m_LabelType;
        protected bool m_OrderedAsDual;        

        private string m_LocationId;
        private string m_FacilityId;

        public SlideOrder_Base()
        {

        }

        public SlideOrder_Base(string objectId, string slideOrderId, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity, int slideNumber)
        {            
			this.m_ObjectId = objectId;
			this.m_SlideOrderId = slideOrderId;
            this.m_AliquotOrderId = aliquotOrder.AliquotOrderId;
            this.m_OrderDate = DateTime.Now;
            this.m_AliquotType = "Slide";
            this.m_Description = "Histology Slide";
            this.m_Status = SlideStatusEnum.Created.ToString();
            this.m_Label = aliquotOrder.Label + slideNumber.ToString();
            this.m_OrderedBy = systemIdentity.User.UserName;
            this.m_OrderedById = systemIdentity.User.UserId;
            this.m_OrderedFrom = Environment.MachineName;
            this.m_OrderedAsDual = testOrder.OrderedAsDual;        
        }

        public void Validate(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_ValidationStation = System.Environment.MachineName;
            this.m_ValidatedBy = systemIdentity.User.UserName;
            this.m_ValidatedById = systemIdentity.User.UserId;
            this.m_ValidationDate = DateTime.Now;
            this.m_Validated = true;
            this.m_Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Validated.ToString();
            this.NotifyPropertyChanged(string.Empty);
        }

        public void SetAsPrinted(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            if (this.m_Status != YellowstonePathology.Business.Slide.Model.SlideStatusEnum.ClientAccessioned.ToString())
            {
                this.m_PrintStation = System.Environment.MachineName;
                this.m_PrintedBy = systemIdentity.User.UserName;
                this.m_PrintedById = systemIdentity.User.UserId;
                this.m_PrintDate = DateTime.Now;
                this.m_Printed = true;

                if (this.m_Validated == false)
                {
                    this.m_Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed.ToString();
                }
            }

            this.NotifyPropertyChanged(string.Empty);
        }

        [PersistentDocumentIdProperty(50)]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (this.m_ObjectId != value)
                {
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }

        [PersistentPrimaryKeyProperty(false, 50)]
        public string SlideOrderId
        {
            get { return this.m_SlideOrderId; }
            set
            {
                if ((this.m_SlideOrderId != value))
                {
                    this.m_SlideOrderId = value;
                }
            }
        }

        [PersistentProperty("0")]
        public bool ClientAccessioned
        {
            get { return this.m_ClientAccessioned; }
            set
            {
                if (this.m_ClientAccessioned != value)
                {
                    this.m_ClientAccessioned = value;
                    this.NotifyPropertyChanged("ClientAccessioned");
                }
            }
        }        

        [PersistentStringProperty(50, "0")]
        public string AliquotOrderId
        {
            get
            {
                return this.m_AliquotOrderId;
            }
            set
            {
                if (this.m_AliquotOrderId != value)
                {
                    this.m_AliquotOrderId = value;
                    this.NotifyPropertyChanged("AliquotOrderId");
                }
            }
        }

        [PersistentProperty("getdate()")]
        public DateTime OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                if (this.m_OrderDate != value)
                {
                    this.m_OrderDate = value;
                    this.NotifyPropertyChanged("OrderDate");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string ValidationStation
        {
            get
            {
                return this.m_ValidationStation;
            }
            set
            {
                if (this.m_ValidationStation != value)
                {
                    this.m_ValidationStation = value;
                    this.NotifyPropertyChanged("ValidationStation");
                }
            }
        }

        [PersistentProperty("0")]
        public bool Validated
        {
            get { return this.m_Validated; }
            set
            {
                if (this.m_Validated != value)
                {
                    this.m_Validated = value;
                    this.NotifyPropertyChanged("Validated");
                }
            }
        }

        [PersistentProperty("0")]
        public int ValidatedById
        {
            get
            {
                return this.m_ValidatedById;
            }
            set
            {
                if (this.m_ValidatedById != value)
                {
                    this.m_ValidatedById = value;
                    this.NotifyPropertyChanged("ValidatedById");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string ValidatedBy
        {
            get
            {
                return this.m_ValidatedBy;
            }
            set
            {
                if (this.m_ValidatedBy != value)
                {
                    this.m_ValidatedBy = value;
                    this.NotifyPropertyChanged("ValidatedBy");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> ValidationDate
        {
            get { return this.m_ValidationDate; }
            set
            {
                if (this.m_ValidationDate != value)
                {
                    this.m_ValidationDate = value;
                    this.NotifyPropertyChanged("ValidationDate");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string PrintStation
        {
            get
            {
                return this.m_PrintStation;
            }
            set
            {
                if (this.m_PrintStation != value)
                {
                    this.m_PrintStation = value;
                    this.NotifyPropertyChanged("PrintStation");
                }
            }
        }

        [PersistentProperty("0")]
        public bool Printed
        {
            get { return this.m_Printed; }
            set
            {
                if (this.m_Printed != value)
                {
                    this.m_Printed = value;
                    this.NotifyPropertyChanged("Printed");
                }
            }
        }

        [PersistentProperty()]
        public int PrintedById
        {
            get
            {
                return this.m_PrintedById;
            }
            set
            {
                if (this.m_PrintedById != value)
                {
                    this.m_PrintedById = value;
                    this.NotifyPropertyChanged("PrintedById");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string PrintedBy
        {
            get
            {
                return this.m_PrintedBy;
            }
            set
            {
                if (this.m_PrintedBy != value)
                {
                    this.m_PrintedBy = value;
                    this.NotifyPropertyChanged("PrintedBy");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> PrintDate
        {
            get { return this.m_PrintDate; }
            set
            {
                if (this.m_PrintDate != value)
                {
                    this.m_PrintDate = value;
                    this.NotifyPropertyChanged("PrintDate");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                if (this.m_Description != value)
                {
                    this.m_Description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string AliquotType
        {
            get
            {
                return this.m_AliquotType;
            }
            set
            {

                if (this.m_AliquotType != value)
                {
                    this.m_AliquotType = value;
                    this.NotifyPropertyChanged("AliquotType");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string Label
        {
            get
            {
                return this.m_Label;
            }
            set
            {
                if (this.m_Label != value)
                {
                    this.m_Label = value;
                    this.NotifyPropertyChanged("Label");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string ClientLabel
        {
            get
            {
                return this.m_ClientLabel;
            }
            set
            {
                if (this.m_ClientLabel != value)
                {
                    this.m_ClientLabel = value;
                    this.NotifyPropertyChanged("ClientLabel");
                }
            }
        }

        [PersistentProperty("0")]
        public int OrderedById
        {
            get
            {
                return this.m_OrderedById;
            }
            set
            {
                if (this.m_OrderedById != value)
                {
                    this.m_OrderedById = value;
                    this.NotifyPropertyChanged("OrderedById");
                }
            }
        }

        [PersistentStringProperty(100)]
        public string OrderedBy
        {
            get
            {
                return this.m_OrderedBy;
            }
            set
            {
                if (this.m_OrderedBy != value)
                {
                    this.m_OrderedBy = value;
                    this.NotifyPropertyChanged("OrderedBy");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string OrderedFrom
        {
            get
            {
                return this.m_OrderedFrom;
            }
            set
            {
                if (this.m_OrderedFrom != value)
                {
                    this.m_OrderedFrom = value;
                    this.NotifyPropertyChanged("OrderedFrom");
                }
            }
        }

        [PersistentStringProperty(50, "'Created'")]
        public string Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                if (this.m_Status != value)
                {
                    this.m_Status = value;
                    this.NotifyPropertyChanged("Status");
                }
            }
        }

        [PersistentProperty()]
        public int TestId
        {
            get
            {
                return this.m_TestId;
            }
            set
            {
                if (this.m_TestId != value)
                {
                    this.m_TestId = value;
                    this.NotifyPropertyChanged("TestId");
                }
            }
        }

        [PersistentStringProperty(100)]
        public string TestName
        {
            get
            {
                return this.m_TestName;
            }
            set
            {
                if (this.m_TestName != value)
                {
                    this.m_TestName = value;
                    this.NotifyPropertyChanged("TestName");
                }
            }
        }

        [PersistentStringProperty(100)]
        public string TestAbbreviation
        {
            get
            {
                return this.m_TestAbbreviation;
            }
            set
            {
                if (this.m_TestAbbreviation != value)
                {
                    this.m_TestAbbreviation = value;
                    this.NotifyPropertyChanged("TestAbbreviation");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string TestOrderId
        {
            get
            {
                return this.m_TestOrderId;
            }
            set
            {
                if (this.m_TestOrderId != value)
                {
                    this.m_TestOrderId = value;
                    this.NotifyPropertyChanged("TestOrderId");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string PatientLastName
        {
            get
            {
                return this.m_PatientLastName;
            }
            set
            {
                if (this.m_PatientLastName != value)
                {
                    this.m_PatientLastName = value;
                    this.NotifyPropertyChanged("PatientLastName");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string Location
        {
            get
            {
                return this.m_Location;
            }
            set
            {
                if (this.m_Location != value)
                {
                    this.m_Location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string ReportNo
        {
            get
            {
                return this.m_ReportNo;
            }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string LabelType
        {
            get
            {
                return this.m_LabelType;
            }
            set
            {
                if (this.m_LabelType != value)
                {
                    this.m_LabelType = value;
                    this.NotifyPropertyChanged("LabelType");
                }
            }
        }

        [PersistentProperty("0")]
        public bool OrderedAsDual
        {
            get
            {
                return this.m_OrderedAsDual;
            }
            set
            {
                if (this.m_OrderedAsDual != value)
                {
                    this.m_OrderedAsDual = value;
                    this.NotifyPropertyChanged("OrderedAsDual");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string LocationId
        {
            get { return this.m_LocationId; }
            set
            {
                if (this.m_LocationId != value)
                {
                    this.m_LocationId = value;
                    this.NotifyPropertyChanged("LocationId");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set
            {
                if (this.m_FacilityId != value)
                {
                    this.m_FacilityId = value;
                    this.NotifyPropertyChanged("FacilityId");
                }
            }
        }

        public static string GetSlideLabel(int slideNumber, string blockLabel, string aliquotType)
        {
            StringBuilder slideOrderLabel = new StringBuilder();
            switch (aliquotType)
            {
                case "FrozenBlock":
                    slideOrderLabel.Append("FS" + blockLabel + slideNumber.ToString());
                    break;
                case "CellBlock":
                    slideOrderLabel.Append("CB" + blockLabel + slideNumber.ToString());
                    break;
                default:
                    slideOrderLabel.Append(blockLabel + slideNumber.ToString());
                    break;
            }
            return slideOrderLabel.ToString();
        }     

        public void FromXml(XElement xml)
        {
            if (xml.Element("SlideOrderId") != null) m_SlideOrderId = xml.Element("SlideOrderId").Value;
            if (xml.Element("AliquotOrderId") != null) m_AliquotOrderId = xml.Element("AliquotOrderId").Value;
            if (xml.Element("OrderDate") != null) m_OrderDate = DateTime.Parse(xml.Element("OrderDate").Value);
            if (xml.Element("Description") != null) m_Description = xml.Element("Description").Value;
            if (xml.Element("AliquotType") != null) m_AliquotType = xml.Element("AliquotType").Value;
            if (xml.Element("Label") != null) m_Label = xml.Element("Label").Value;
            if (xml.Element("OrderedById") != null) m_OrderedById = Convert.ToInt32(xml.Element("OrderedById").Value);
            if (xml.Element("OrderedBy") != null) m_OrderedBy = xml.Element("OrderedBy").Value;
            if (xml.Element("OrderedFrom") != null) m_OrderedFrom = xml.Element("OrderedFrom").Value;
            if (xml.Element("ValidationDate") != null) m_ValidationDate = DateTime.Parse(xml.Element("ValidationDate").Value);
            if (xml.Element("Status") != null) m_Status = xml.Element("Status").Value;
        }

        public XElement ToXml()
        {
            XElement result = new XElement("SlideOrder");
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "SlideOrderId", SlideOrderId.ToString());
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "AliquotOrderId", AliquotOrderId.ToString());
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "OrderDate", OrderDate.ToString("yyyy-MM-ddTHH:mm:ss.FFF"));
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "Description", Description);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "AliquotType", AliquotType);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "Label", Label);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "OrderedById", OrderedById.ToString());
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "OrderedBy", OrderedBy);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "OrderedFrom", OrderedFrom);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "ValidationDate", ValidationDate);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "Status", Status);
            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
