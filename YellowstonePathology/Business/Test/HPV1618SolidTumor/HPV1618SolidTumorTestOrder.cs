using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	[PersistentClass("tblHPV1618SolidTumorTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class HPV1618SolidTumorTestOrder : PanelSetOrder
	{
        private string m_Indication;
        private string m_HPV6Result;
        private string m_HPV16Result;
		private string m_HPV18Result;
        private string m_HPV31Result;
        private string m_HPV33Result;
        private string m_HPV45Result;
        private string m_HPV58Result;
        private string m_HPVDNAResult;
        private string m_Method;		
        private string m_Interpretation;
        private string m_Comment;

        public HPV1618SolidTumorTestOrder()
		{

		}

		public HPV1618SolidTumorTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
        }
        
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

        [PersistentProperty()]
        public string HPV6Result
        {
            get { return this.m_HPV6Result; }
            set
            {
                if (this.m_HPV6Result != value)
                {
                    this.m_HPV6Result = value;
                    this.NotifyPropertyChanged("HPV6Result");
                }
            }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string HPV16Result
		{
			get { return this.m_HPV16Result; }
			set
			{
				if (this.m_HPV16Result != value)
				{
					this.m_HPV16Result = value;
					this.NotifyPropertyChanged("HPV16Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string HPV18Result
		{
			get { return this.m_HPV18Result; }
			set
			{
				if (this.m_HPV18Result != value)
				{
					this.m_HPV18Result = value;
					this.NotifyPropertyChanged("HPV18Result");
				}
			}
		}

        [PersistentProperty()]
        public string HPV31Result
        {
            get { return this.m_HPV31Result; }
            set
            {
                if (this.m_HPV31Result != value)
                {
                    this.m_HPV31Result = value;
                    this.NotifyPropertyChanged("HPV31Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPV33Result
        {
            get { return this.m_HPV33Result; }
            set
            {
                if (this.m_HPV33Result != value)
                {
                    this.m_HPV33Result = value;
                    this.NotifyPropertyChanged("HPV33Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPV45Result
        {
            get { return this.m_HPV45Result; }
            set
            {
                if (this.m_HPV45Result != value)
                {
                    this.m_HPV45Result = value;
                    this.NotifyPropertyChanged("HPV45Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPV58Result
        {
            get { return this.m_HPV58Result; }
            set
            {
                if (this.m_HPV58Result != value)
                {
                    this.m_HPV58Result = value;
                    this.NotifyPropertyChanged("HPV58Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPVDNAResult
        {
            get { return this.m_HPVDNAResult; }
            set
            {
                if (this.m_HPVDNAResult != value)
                {
                    this.m_HPVDNAResult = value;
                    this.NotifyPropertyChanged("HPVDNAResult");
                }
            }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public override bool IsOkToAddTasks()
        {
            bool result = false;
            if (this.m_OrderedOn == "Aliquot") result = true;
            return result;
        }

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append("HPV16: ");
			result.Append(this.m_HPV16Result);
			result.Append(" HPV18: ");
			result.Append(this.m_HPV18Result);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("HPV16: ");
			result.AppendLine(this.m_HPV16Result);
			result.Append("HPV18: ");
			result.AppendLine(this.m_HPV18Result);
			return result.ToString();
		}
	}
}
