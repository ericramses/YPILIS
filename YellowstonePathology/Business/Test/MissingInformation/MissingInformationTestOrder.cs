using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MissingInformation
{
	[PersistentClass("tblMissingInformationTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class MissingInformationTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_MissingInformation;
        private string m_CallComments;

        private bool m_FirstCall;
        private string m_FirstCallMadeBy;
        private Nullable<DateTime> m_TimeOfFirstCall;
        private string m_FirstCallComment;

        private bool m_SecondCall;
        private string m_SecondCallMadeBy;
        private Nullable<DateTime> m_TimeOfSecondCall;
        private string m_SecondCallComment;

        private bool m_ThirdCall;
        private string m_ThirdCallMadeBy;
        private Nullable<DateTime> m_TimeOfThirdCall;
        private string m_ThirdCallComment;

        private bool m_Fax;
        private string m_FaxSentBy;
        private Nullable<DateTime> m_TimeFaxSent;
        private string m_LetterBody;

        private bool m_ClientSystemLookup;
        private string m_ClientSystemLookupBy;
        private Nullable<DateTime> m_TimeOfClientSystemLookup;
       
        public MissingInformationTestOrder()
		{

		}

        public MissingInformationTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
            this.m_FirstCall = false;
            this.m_SecondCall = false;
            this.m_ThirdCall = false;
            this.m_Fax = false;
            this.m_ClientSystemLookup = false;
        }       

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string CallComments
        {
            get { return this.m_CallComments; }
            set
            {
                if (this.m_CallComments != value)
                {
                    this.m_CallComments = value;
                    this.NotifyPropertyChanged("CallComments");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string FirstCallComment
        {
            get { return this.m_FirstCallComment; }
            set
            {
                if (this.m_FirstCallComment != value)
                {
                    this.m_FirstCallComment = value;
                    this.NotifyPropertyChanged("FirstCallComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool SecondCall
        {
            get { return this.m_SecondCall; }
            set
            {
                if (this.m_SecondCall != value)
                {
                    this.m_SecondCall = value;
                    this.NotifyPropertyChanged("SecondCall");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string SecondCallMadeBy
        {
            get { return this.m_SecondCallMadeBy; }
            set
            {
                if (this.m_SecondCallMadeBy != value)
                {
                    this.m_SecondCallMadeBy = value;
                    this.NotifyPropertyChanged("SecondCallMadeBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeOfSecondCall
        {
            get { return this.m_TimeOfSecondCall; }
            set
            {
                if (this.m_TimeOfSecondCall != value)
                {
                    this.m_TimeOfSecondCall = value;
                    this.NotifyPropertyChanged("TimeOfSecondCall");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string SecondCallComment
        {
            get { return this.m_SecondCallComment; }
            set
            {
                if (this.m_SecondCallComment != value)
                {
                    this.m_SecondCallComment = value;
                    this.NotifyPropertyChanged("SecondCallComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ThirdCall
        {
            get { return this.m_ThirdCall; }
            set
            {
                if (this.m_ThirdCall != value)
                {
                    this.m_ThirdCall = value;
                    this.NotifyPropertyChanged("ThirdCall");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ThirdCallMadeBy
        {
            get { return this.m_ThirdCallMadeBy; }
            set
            {
                if (this.m_ThirdCallMadeBy != value)
                {
                    this.m_ThirdCallMadeBy = value;
                    this.NotifyPropertyChanged("ThirdCallMadeBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeOfThirdCall
        {
            get { return this.m_TimeOfThirdCall; }
            set
            {
                if (this.m_TimeOfThirdCall != value)
                {
                    this.m_TimeOfThirdCall = value;
                    this.NotifyPropertyChanged("TimeOfThirdCall");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ThirdCallComment
        {
            get { return this.m_ThirdCallComment; }
            set
            {
                if (this.m_ThirdCallComment != value)
                {
                    this.m_ThirdCallComment = value;
                    this.NotifyPropertyChanged("ThirdCallComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Fax
        {
            get { return this.m_Fax; }
            set
            {
                if (this.m_Fax != value)
                {
                    this.m_Fax = value;
                    this.NotifyPropertyChanged("Fax");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string FaxSentBy
        {
            get { return this.m_FaxSentBy; }
            set
            {
                if (this.m_FaxSentBy != value)
                {
                    this.m_FaxSentBy = value;
                    this.NotifyPropertyChanged("FaxSentBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeFaxSent
        {
            get { return this.m_TimeFaxSent; }
            set
            {
                if (this.m_TimeFaxSent != value)
                {
                    this.m_TimeFaxSent = value;
                    this.NotifyPropertyChanged("TimeFaxSent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ClientSystemLookup
        {
            get { return this.m_ClientSystemLookup; }
            set
            {
                if (this.m_ClientSystemLookup != value)
                {
                    this.m_ClientSystemLookup = value;
                    this.NotifyPropertyChanged("ClientSystemLookup");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ClientSystemLookupBy
        {
            get { return this.m_ClientSystemLookupBy; }
            set
            {
                if (this.m_ClientSystemLookupBy != value)
                {
                    this.m_ClientSystemLookupBy = value;
                    this.NotifyPropertyChanged("ClientSystemLookupBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string LetterBody
        {
            get { return this.m_LetterBody; }
            set
            {
                if (this.m_LetterBody != value)
                {
                    this.m_LetterBody = value;
                    this.NotifyPropertyChanged("LetterBody");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeOfClientSystemLookup
        {
            get { return this.m_TimeOfClientSystemLookup; }
            set
            {
                if (this.m_TimeOfClientSystemLookup != value)
                {
                    this.m_TimeOfClientSystemLookup = value;
                    this.NotifyPropertyChanged("TimeOfClientSystemLookup");
                }
            }
        }

        public void SetFirstCall()
        {
            this.m_FirstCall = true;
            this.m_FirstCallMadeBy = Business.User.SystemIdentity.Instance.User.DisplayName;
            this.m_TimeOfFirstCall = DateTime.Now;
            this.m_FirstCallComment = "Made By " + this.m_FirstCallMadeBy + " @ " + this.m_TimeOfFirstCall.Value.ToString("MM/dd/yyyy HH:mm");
            this.NotifyPropertyChanged(string.Empty);
        }

        public void SetSecondCall()
        {
            this.m_SecondCall = true;
            this.m_SecondCallMadeBy = Business.User.SystemIdentity.Instance.User.DisplayName;
            this.m_TimeOfSecondCall = DateTime.Now;
            this.m_SecondCallComment = "Made By " + this.m_SecondCallMadeBy + " @ " + this.m_TimeOfSecondCall.Value.ToString("MM/dd/yyyy HH:mm");
            this.NotifyPropertyChanged(string.Empty);
        }

        public void SetThirdCall()
        {
            this.m_ThirdCall = true;
            this.m_ThirdCallMadeBy = Business.User.SystemIdentity.Instance.User.DisplayName;
            this.m_TimeOfThirdCall = DateTime.Now;
            this.m_ThirdCallComment = "Made By " + this.m_ThirdCallMadeBy + " @ " + this.m_TimeOfThirdCall.Value.ToString("MM/dd/yyyy HH:mm");
            this.NotifyPropertyChanged(string.Empty);
        }
    }
}
