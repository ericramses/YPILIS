using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MissingInformation
{
	[PersistentClass("tblMissingInformationTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class MissingInformtionTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_MissingInformation;
        private bool m_FirstCall;
        private string m_FirstCallMadeBy;
        private Nullable<DateTime> m_TimeOfFirstCall;

		public MissingInformtionTestOrder()
		{

		}

		public MissingInformtionTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{			
			
		}

        [PersistentProperty()]
        public string MissingInformation
        {
            get { return this.m_MissingInformation; }
            set
            {
                if (this.m_MissingInformation != value)
                {
                    this.m_MissingInformation = value;
                    this.NotifyPropertyChanged("MissingInformation");
                }
            }
        }

        [PersistentProperty()]
        public bool FirstCall
        {
            get { return this.m_FirstCall; }
            set
            {
                if (this.m_FirstCall != value)
                {
                    this.m_FirstCall = value;
                    this.NotifyPropertyChanged("FirstCall");
                }
            }
        }

        [PersistentProperty()]
        public string FirstCallMadeBy
        {
            get { return this.m_FirstCallMadeBy; }
            set
            {
                if (this.m_FirstCallMadeBy != value)
                {
                    this.m_FirstCallMadeBy = value;
                    this.NotifyPropertyChanged("FirstCallMadeBy");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> TimeOfFirstCall
        {
            get { return this.m_TimeOfFirstCall; }
            set
            {
                if (this.m_TimeOfFirstCall != value)
                {
                    this.m_TimeOfFirstCall = value;
                    this.NotifyPropertyChanged("TimeOfFirstCall");
                }
            }
        }

        public string FirstCallDisplayString
        {
            get
            {
                string result = null;
                if(this.m_FirstCall == false)
                {
                    result = "Not made.";
                }
                else
                {
                    result = "Made By " + this.m_FirstCallMadeBy + " @ " + this.m_TimeOfFirstCall.Value.ToString("MM/dd/YYYY HH:mm");
                }
                return result;
            }
        }
    }
}
